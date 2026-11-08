using Larp.api.Documents;
using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Larp.api.Controllers;

[ApiController]
[Authorize(Roles = "SuperAdmin")]
[Route("admin/plans")]
public class PlansController : ControllerBase
{
    private readonly MasterErpDbContext _master;
    private readonly PlanService _plans;

    public PlansController(MasterErpDbContext master, PlanService plans)
    {
        _master = master;
        _plans = plans;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await LoadDtosAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var plan = (await LoadDtosAsync()).FirstOrDefault(p => p.PlanId == id);

        return plan == null
            ? NotFound(new { message = "Plan not found." })
            : Ok(plan);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SavePlanRequest r)
    {
        var error = Validate(r, out var modules);
        if (error != null) return BadRequest(new { message = error });

        var name = r.Name.Trim();

        if (await _master.Plans.AnyAsync(p => p.Name == name))
            return Conflict(new { message = "A plan with this name already exists." });

        var plan = new Plan
        {
            Name = name,
            Description = Clean(r.Description),
            MonthlyPrice = r.MonthlyPrice,
            YearlyPrice = r.YearlyPrice,
            MaxEmployees = r.MaxEmployees,
            MaxBranches = r.MaxBranches,
            Modules = modules,
            IsActive = r.IsActive,
            SortOrder = r.SortOrder,
            CreatedAt = DateTime.UtcNow
        };

        _master.Plans.Add(plan);
        await _master.SaveChangesAsync();

        return Ok(ToDto(plan, 0));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SavePlanRequest r)
    {
        var error = Validate(r, out var modules);
        if (error != null) return BadRequest(new { message = error });

        var plan = await _master.Plans.FirstOrDefaultAsync(p => p.PlanId == id);
        if (plan == null) return NotFound(new { message = "Plan not found." });

        var name = r.Name.Trim();

        if (await _master.Plans.AnyAsync(p => p.Name == name && p.PlanId != id))
            return Conflict(new { message = "A plan with this name already exists." });

        if (plan.IsActive && !r.IsActive)
        {
            int inUse = await ActiveCompaniesOnAsync(id);
            if (inUse > 0)
                return BadRequest(new { message = $"{inUse} active company(ies) are on this plan. Move them to another plan first." });
        }

        plan.Name = name;
        plan.Description = Clean(r.Description);
        plan.MonthlyPrice = r.MonthlyPrice;
        plan.YearlyPrice = r.YearlyPrice;
        plan.MaxEmployees = r.MaxEmployees;
        plan.MaxBranches = r.MaxBranches;
        plan.Modules = modules;
        plan.IsActive = r.IsActive;
        plan.SortOrder = r.SortOrder;

        // Keep the plan name on each company equal to the plan's name.
        var companies = await _master.Companies.Where(c => c.PlanId == id).ToListAsync();
        foreach (var c in companies)
            c.SubscriptionPlan = plan.Name;

        await _master.SaveChangesAsync();

        // The change applies to those companies on their next request.
        foreach (var c in companies)
            _plans.Invalidate(c.CompanyId);

        return Ok(ToDto(plan, companies.Count(c => c.isActive)));
    }

    // Soft delete: the plan is switched off, never removed.
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var plan = await _master.Plans.FirstOrDefaultAsync(p => p.PlanId == id);
        if (plan == null) return NotFound(new { message = "Plan not found." });

        int inUse = await ActiveCompaniesOnAsync(id);
        if (inUse > 0)
            return BadRequest(new { message = $"{inUse} active company(ies) are on this plan. Move them to another plan first." });

        plan.IsActive = false;
        await _master.SaveChangesAsync();

        return Ok(new { message = "Plan deactivated." });
    }

    // GET /admin/plans/pdf : the "Plans and Pricing" price card, locked against editing.
    [HttpGet("pdf")]
    public async Task<IActionResult> Pdf()
    {
        var plans = await _master.Plans.AsNoTracking()
            .Where(p => p.IsActive)
            .OrderBy(p => p.SortOrder).ThenBy(p => p.Name)
            .ToListAsync();

        if (plans.Count == 0)
            return BadRequest(new { message = "There are no active plans to print." });

        var bytes = PlanPdfBuilder.Build(plans);

        return File(bytes, "application/pdf",
            $"Larp-Plans-and-Pricing-{DateTime.Today:yyyyMMdd}.pdf");
    }

    // ---------------- Helpers ----------------

    private Task<int> ActiveCompaniesOnAsync(int planId) =>
        _master.Companies.CountAsync(c => c.PlanId == planId && c.isActive);

    private async Task<List<PlanDto>> LoadDtosAsync()
    {
        var plans = await _master.Plans.AsNoTracking()
            .OrderBy(p => p.SortOrder).ThenBy(p => p.Name)
            .ToListAsync();

        var counts = await _master.Companies.AsNoTracking()
            .Where(c => c.PlanId != null && c.isActive)
            .GroupBy(c => c.PlanId)
            .Select(g => new { PlanId = g.Key, Count = g.Count() })
            .ToListAsync();

        return plans
            .Select(p => ToDto(p, counts.FirstOrDefault(x => x.PlanId == p.PlanId)?.Count ?? 0))
            .ToList();
    }

    private static PlanDto ToDto(Plan p, int companyCount) => new()
    {
        PlanId = p.PlanId,
        Name = p.Name,
        Description = p.Description,
        MonthlyPrice = p.MonthlyPrice,
        YearlyPrice = p.YearlyPrice,
        MaxEmployees = p.MaxEmployees,
        MaxBranches = p.MaxBranches,
        Modules = p.Modules
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList(),
        IsActive = p.IsActive,
        SortOrder = p.SortOrder,
        CompanyCount = companyCount
    };

    // Returns an error message, or null when valid. 'modules' comes back in canonical order.
    private static string? Validate(SavePlanRequest r, out string modules)
    {
        modules = string.Empty;

        if (string.IsNullOrWhiteSpace(r.Name))
            return "Plan name is required.";

        if (r.Name.Trim().Length > 50)
            return "Plan name is too long (50 characters max).";

        if (r.MonthlyPrice < 0 || r.YearlyPrice < 0)
            return "Prices cannot be negative.";

        if (r.MaxEmployees < 1)
            return "A plan must allow at least 1 employee.";

        if (r.MaxBranches < 0)
            return "Max branches cannot be negative (0 means no branching).";

        var unknown = r.Modules.FirstOrDefault(m => !PlanModules.IsKnown(m));
        if (unknown != null)
            return $"Unknown module '{unknown}'.";

        modules = string.Join(",", PlanModules.All
            .Where(m => r.Modules.Contains(m.Key, StringComparer.OrdinalIgnoreCase))
            .Select(m => m.Key));

        return modules.Length == 0 ? "Select at least one module." : null;
    }

    private static string? Clean(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}