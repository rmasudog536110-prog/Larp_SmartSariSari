using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Larp.api.Controllers;

[ApiController]
[Authorize(Roles = "SuperAdmin")]
[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly MasterErpDbContext _master;
    private readonly ITenantDBContextFactory _tenantFactory;
    private readonly IConfiguration _config;
    private readonly BackupService _backup;
    private readonly PlanService _planService;

    public AdminController(
        MasterErpDbContext master,
        ITenantDBContextFactory tenantFactory,
        IConfiguration config,
        BackupService backup,
        PlanService planService
        )
    {
        _master = master;
        _tenantFactory = tenantFactory;
        _config = config;
        _backup = backup;
        _planService = planService;


    }

    private Task<Plan?> FindPlanAsync(string? name) =>
    _master.Plans.AsNoTracking()
        .FirstOrDefaultAsync(p => p.Name == (name ?? "").Trim() && p.IsActive);

    // ---------- Tenants / subscriptions ----------

    [HttpGet("tenants")]
    public async Task<IActionResult> GetTenants()
    {
        var tenants = await _master.Companies
            .AsNoTracking()
            .OrderBy(c => c.CompanyName)
            .Select(c => new
            {
                tenantId = c.CompanyId,
                code = c.CompanyCode,
                name = c.CompanyName,
                status = c.SubscriptionStatus,
                plan = c.SubscriptionPlan,
                expiry = c.SubscriptionExpiry,
                isActive = c.isActive,
                databaseName = _master.CompanyDatabases
                    .Where(d => d.CompanyId == c.CompanyId && d.isActive)
                    .Select(d => d.DatabaseName)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return Ok(tenants);
    }

    private static readonly SemaphoreSlim CompanyCodeLock = new(1, 1);

    private async Task<string> NextCompanyCodeAsync()
    {
        var codes = await _master.Companies
            .AsNoTracking()
            .Where(c => c.CompanyCode.StartsWith("COM-"))
            .Select(c => c.CompanyCode)
            .ToListAsync();

        int highest = 0;

        foreach (var code in codes)
        {
            if (int.TryParse(code.AsSpan(4), out int number) && number > highest)
                highest = number;
        }

        return $"COM-{highest + 1:000}";
    }

    [HttpGet("tenants/{id:int}")]
    public async Task<IActionResult> GetTenant(int id)
    {
        var company = await _master.Companies
            .AsNoTracking()
            .Where(c => c.CompanyId == id)
            .Select(c => new
            {
                tenantId = c.CompanyId,
                code = c.CompanyCode,
                name = c.CompanyName,
                plan = c.SubscriptionPlan,
                expiry = c.SubscriptionExpiry,
                address = c.Address,
                tin = c.Tin,
                contactNumber = c.ContactNumber
            })
            .FirstOrDefaultAsync();

        if (company == null)
            return NotFound(new { message = "Company not found." });

        return Ok(company);
    }

    [HttpPut("tenants/{id:int}")]
    public async Task<IActionResult> UpdateTenant(
    int id,
    [FromBody] UpdateTenantRequest request)
    {
        var company = await _master.Companies
            .FirstOrDefaultAsync(c => c.CompanyId == id);

        if (company == null)
            return NotFound(new { message = "Company not found." });

        if (string.IsNullOrWhiteSpace(request.Code) ||
            string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new
            {
                message = "Company code and name are required."
            });
        }

        var duplicateCode = await _master.Companies.AnyAsync(c =>
            c.CompanyId != id &&
            c.CompanyCode == request.Code.Trim());

        if (duplicateCode)
        {
            return Conflict(new
            {
                message = "Company code already exists."
            });
        }

        var plan = await _master.Plans
            .FirstOrDefaultAsync(p =>
                p.Name == request.Plan &&
                p.IsActive);

        if (plan == null)
        {
            return BadRequest(new
            {
                message = "The selected plan is invalid or inactive."
            });
        }

        company.CompanyCode = request.Code.Trim();
        company.CompanyName = request.Name.Trim();
        company.SubscriptionPlan = plan.Name;
        company.PlanId = plan.PlanId;
        company.SubscriptionExpiry = request.Expiry;

        company.Address = request.Address;
        company.Tin = request.Tin;
        company.ContactNumber = request.ContactNumber;

        await _master.SaveChangesAsync();

        return Ok(new
        {
            message = "Company updated successfully."
        });
    }

    [HttpPut("tenants/{id:int}/suspend")]
    public async Task<IActionResult> Suspend(int id)
    {
        var company = await _master.Companies.FindAsync(id);
        if (company == null) return NotFound(new { message = "Company not found." });

        company.SubscriptionStatus = "Suspended";
        company.isActive = false;
        await _master.SaveChangesAsync();

        return Ok(new { message = "Company suspended." });
    }

    [HttpPut("tenants/{id:int}/reactivate")]
    public async Task<IActionResult> Reactivate(int id, ReactivateRequest r, PlanService _plans)
    {
        var company = await _master.Companies.FindAsync(id);
        if (company == null) return NotFound(new { message = "Company not found." });

        var plan = await FindPlanAsync(r.Plan);
        if (plan == null)
            return BadRequest(new { message = "Unknown plan. Use Basic, Premium or Enterprise." });

        company.PlanId = plan.PlanId;
        company.SubscriptionStatus = "Active";
        company.SubscriptionPlan = plan.Name;
        company.SubscriptionExpiry = r.Expiry;
        company.isActive = true;
        await _master.SaveChangesAsync();
        _plans.Invalidate(id);

        return Ok(new { message = "Company reactivated." });
    }

    // ---------- Cross-tenant BI ----------

    [HttpGet("overview")]
    public async Task<IActionResult> Overview()
    {
        // "Today" in Philippine time (UTC+8), converted back to UTC for the query.
        var todayStartUtc = DateTime.UtcNow.AddHours(8).Date.AddHours(-8);

        var companies = await _master.Companies.AsNoTracking()
            .OrderBy(c => c.CompanyName).ToListAsync();

        var rows = new List<object>();

        // Sequential on purpose: the resolver shares one master DbContext.
        foreach (var c in companies)
        {
            try
            {
                await using var db = await _tenantFactory.CreateAsync(c.CompanyId);

                var salesToday = await db.Sales
                    .Where(s => s.SaleDate >= todayStartUtc && s.Status == "Completed")
                    .SumAsync(s => (decimal?)s.TotalAmount) ?? 0;

                var lowStock = await db.Inventory
                    .CountAsync(i => i.QuantityOnHand <= i.ReorderLevel);

                rows.Add(new
                {
                    tenantId = c.CompanyId,
                    name = c.CompanyName,
                    code = c.CompanyCode,
                    status = c.SubscriptionStatus,
                    reachable = true,
                    salesToday,
                    lowStockCount = lowStock,
                    error = (string?)null
                });
            }
            catch (Exception ex)
            {
                rows.Add(new
                {
                    tenantId = c.CompanyId,
                    name = c.CompanyName,
                    code = c.CompanyCode,
                    status = c.SubscriptionStatus,
                    reachable = false,
                    salesToday = 0m,
                    lowStockCount = 0,
                    error = ex.Message
                });
            }
        }

        return Ok(rows);
    }

    // ---------- Backup & Restore ----------

    // POST /admin/tenants/{id}/backup
    [HttpPost("tenants/{id:int}/backup")]
    public async Task<IActionResult> CreateBackup(int id)
    {
        var company = await _master.Companies.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == id);
        if (company == null) return NotFound(new { message = "Company not found." });

        try
        {
            var info = await _backup.CreateBackupAsync(id, "manual");
            return Ok(info);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (SqlException ex)
        {
            return StatusCode(503, new { message = "Tenant database error: " + ex.Message });
        }
    }

    // GET /admin/tenants/{id}/backups
    [HttpGet("tenants/{id:int}/backups")]
    public async Task<IActionResult> ListBackups(int id)
    {
        var exists = await _master.Companies.AnyAsync(c => c.CompanyId == id);
        if (!exists) return NotFound(new { message = "Company not found." });

        return Ok(await _backup.ListAsync(id));
    }

    // POST /admin/tenants/{id}/restore
    // Body: { "fileName": "20260930-101500-manual.zip", "confirmCompanyName": "Exact Company Name" }
    [HttpPost("tenants/{id:int}/restore")]
    public async Task<IActionResult> Restore(int id, [FromBody] RestoreRequest req)
    {
        if (req == null || string.IsNullOrWhiteSpace(req.FileName))
            return BadRequest(new { message = "fileName is required." });

        var company = await _master.Companies.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == id);
        if (company == null) return NotFound(new { message = "Company not found." });

        // Typed confirmation: restore wipes all tenant data.
        if (!string.Equals(req.ConfirmCompanyName?.Trim(), company.CompanyName?.Trim(),
                StringComparison.OrdinalIgnoreCase))
            return BadRequest(new { message = "Confirmation does not match the company name." });

        try
        {
            var result = await _backup.RestoreAsync(id, req.FileName);
            return Ok(new
            {
                message = "Restore completed.",
                tables = result.Tables,
                rows = result.Rows,
                safetyBackup = result.SafetyBackup
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (SqlException ex)
        {
            return StatusCode(503, new { message = "Tenant database error: " + ex.Message });
        }
    }

  
    // POST /admin/tenants/{id}/migrate
    // Applies any pending EF migrations to that tenant's database.
    [HttpPost("tenants/{id:int}/migrate")]
    public async Task<IActionResult> Migrate(int id)
    {
        var company = await _master.Companies.AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == id);
        if (company == null) return NotFound(new { message = "Company not found." });

        try
        {
            await using var db = await _tenantFactory.CreateAsync(id);

            var pending = (await db.Database.GetPendingMigrationsAsync()).ToList();

            if (pending.Count == 0)
            {
                return Ok(new
                {
                    message = "The database is already up to date.",
                    applied = Array.Empty<string>()
                });
            }

            await db.Database.MigrateAsync();

            return Ok(new
            {
                message = $"Applied {pending.Count} update(s).",
                applied = pending
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = "Update failed: " + (ex.InnerException?.Message ?? ex.Message)
            });
        }
    }
}