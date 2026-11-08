using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Larp.infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Larp.api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private static readonly string[] RolePriority =
        { "SuperAdmin", "Admin", "Manager", "Staff" };

    private readonly UserManager<ApplicationUser> _users;
    private readonly MasterErpDbContext _masterDb;
    private readonly ITenantDBContextFactory _tenantFactory;
    private readonly IConfiguration _config;
    private readonly PlanService _plans;

    public AuthController(
        UserManager<ApplicationUser> users,
        MasterErpDbContext masterDb,
        ITenantDBContextFactory tenantFactory,
        IConfiguration config,
        PlanService plans)
    {
        _users = users;
        _masterDb = masterDb;
        _tenantFactory = tenantFactory;
        _config = config;
        _plans = plans;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        const string invalid = "Invalid username or password.";

        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Enter both username and password." });
        }

        var user = await _users.FindByNameAsync(request.Username.Trim());

        if (user == null || !user.IsActive ||
            !await _users.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized(new { message = invalid });
        }

        // The user must hold exactly one of the four known roles.
        var userRoles = await _users.GetRolesAsync(user);
        var role = RolePriority.FirstOrDefault(r => userRoles.Contains(r));

        if (role == null)
        {
            return StatusCode(403, new { message = "This account has no valid role assigned." });
        }

        bool isSuperAdmin = role == "SuperAdmin";

        int? companyId = null;
        int? employeeId = null;
        string? companyName = null;
        string? plan = null;
        CompanyPlan? companyPlan = null;

        if (!isSuperAdmin)
        {

            if (user.CompanyId == null)
            {
                return StatusCode(403, new { message = "This account is not linked to a company." });
            }

            var company = await _masterDb.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == user.CompanyId);

            if (company == null || !company.isActive ||
                company.SubscriptionStatus is "Suspended" or "Cancelled")
            {
                return StatusCode(403, new
                {
                    message = "This company's subscription is inactive. Contact support."
                });
            }

            if (company.SubscriptionExpiry.HasValue &&
                company.SubscriptionExpiry.Value < DateTime.UtcNow)
            {
                return StatusCode(403, new
                {
                    message = "This company's subscription has expired. Contact support."
                });
            }

            try
            {
                await using var tenantDb = await _tenantFactory.CreateAsync(company.CompanyId);

                var employee = await tenantDb.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.UserId == user.Id && e.IsActive);

                if (employee == null)
                {
                    return StatusCode(403, new
                    {
                        message = "No active employee record exists for this user."
                    });
                }

                employeeId = employee.EmployeeId;
            }
            catch (Exception)
            {
                return StatusCode(503, new
                {
                    message = "This company's database could not be reached."
                });
            }

            companyId = company.CompanyId;
            companyName = company.CompanyName;
            plan = company.SubscriptionPlan;
            companyPlan = await _plans.GetForCompanyAsync(company.CompanyId, fresh: true);

        }

        var expiryMinutes = _config.GetValue("Jwt:ExpiryMinutes", 480);
        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Role, role)
        };

        if (companyId != null)
            claims.Add(new Claim("companyId", companyId.Value.ToString()));

        if (employeeId != null)
            claims.Add(new Claim("employeeId", employeeId.Value.ToString()));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiresAt,
            fullName = user.FullName,
            role,
            isSuperAdmin,
            companyId,
            companyName,
            subscriptionPlan = plan,
            planName = companyPlan?.Name ?? plan,
            modules = companyPlan?.Modules.ToArray() ?? Array.Empty<string>(),
            maxEmployees = companyPlan?.MaxEmployees ?? 0,
            maxBranches = companyPlan?.MaxBranches ?? 0,
            employeeId
        });
    }
}