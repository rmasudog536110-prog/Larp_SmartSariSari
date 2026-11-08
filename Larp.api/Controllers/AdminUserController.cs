using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Larp.api.Controllers;

[ApiController]
[Authorize(Roles = "SuperAdmin")]
public class AdminUsersController : ControllerBase
{
    private static readonly string[] AllowedRoles = { "Admin", "Manager", "Staff" };

    [HttpPost("/admin/companies/{companyId:int}/users")]
    public async Task<IActionResult> CreateCompanyUser(
        int companyId,
        CreateCompanyUserRequest request,
        UserManager<ApplicationUser> users,
        MasterErpDbContext masterDb,
        EmployeeService employeeService,
        PlanService planService)
    {
        if (!AllowedRoles.Contains(request.Role))
            return BadRequest(new { message = "Role must be Admin, Manager or Staff." });

        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.EmployeeCode) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Position))
        {
            return BadRequest(new { message = "Username, password, employee code, names and position are required." });
        }

        var company = await masterDb.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == companyId);

        if (company == null)
            return NotFound(new { message = "Company not found." });

        var limit = await planService.CheckEmployeeLimitAsync(companyId);
if (limit != null) return StatusCode(403, new { message = limit });

        if (await users.FindByNameAsync(request.Username.Trim()) != null)
            return Conflict(new { message = "Username already exists." });

        // Check the tenant side first so we don't leave an orphaned login.
        try
        {
            if (await employeeService.GetByCodeAsync(companyId, request.EmployeeCode) != null)
                return Conflict(new { message = "Employee code already exists." });
        }
        catch (Exception)
        {
            return StatusCode(503, new { message = "The company's database could not be reached." });
        }

        var user = new ApplicationUser
        {
            UserName = request.Username.Trim(),
            Email = request.EmailAddress,
            FullName = $"{request.FirstName} {request.LastName}".Trim(),
            CompanyId = companyId,
            IsActive = true
        };

        var created = await users.CreateAsync(user, request.Password);
        if (!created.Succeeded)
            return BadRequest(new { message = string.Join("; ", created.Errors.Select(e => e.Description)) });

        try
        {
            await users.AddToRoleAsync(user, request.Role);

            var employee = await employeeService.CreateAsync(companyId, new EmployeeEntity
            {
                UserId = user.Id,
                EmployeeCode = request.EmployeeCode,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Position = request.Position,
                ContactNumber = request.ContactNumber,
                EmailAddress = request.EmailAddress,
                Address = request.Address,
                DateHired = request.DateHired
            });

            return Ok(new
            {
                userId = user.Id,
                username = user.UserName,
                role = request.Role,
                companyId,
                employeeId = employee.EmployeeId
            });
        }
        catch (Exception ex)
        {
            // No shared transaction across two databases, so undo the login manually.
            await users.DeleteAsync(user);
            return StatusCode(500, new { message = "Could not create the employee record: " + ex.Message });
        }
    }
}