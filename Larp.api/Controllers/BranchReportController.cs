using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

/// <summary>
/// Branch BI endpoints. Admin (and SuperAdmin) only: Manager and Staff get 403.
/// {branchId} is a number, or the word "unassigned" for records with no branch.
/// </summary>
[ApiController]
[Authorize(Roles = "Admin,SuperAdmin")]
public class BranchReportController : ControllerBase
{
    private readonly BranchReportService _reports;

    public BranchReportController(BranchReportService reports)
    {
        _reports = reports;
    }

    private static bool TryParseBranch(string branchId, out int? id)
    {
        id = null;

        if (string.Equals(branchId, "unassigned", StringComparison.OrdinalIgnoreCase))
            return true;

        if (int.TryParse(branchId, out var n) && n > 0)
        {
            id = n;
            return true;
        }

        return false;
    }

    private IActionResult? Validate(
        string branchId, DateTime startDate, DateTime endDate, out int? id)
    {
        if (!TryParseBranch(branchId, out id))
            return BadRequest(new { message = "Branch id must be a number or 'unassigned'." });

        if (endDate.Date < startDate.Date)
            return BadRequest(new { message = "End date cannot be earlier than start date." });

        return null;
    }

    [HttpGet("/tenant/{companyId:int}/branches/compare")]
    public async Task<IActionResult> Compare(
        int companyId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        if (endDate.Date < startDate.Date)
            return BadRequest(new { message = "End date cannot be earlier than start date." });

        return Ok(await _reports.GetCompareAsync(companyId, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/summary")]
    public async Task<IActionResult> Summary(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;

        var dto = await _reports.GetSummaryAsync(companyId, id, startDate, endDate);

        return dto == null
            ? NotFound(new { message = "Branch not found." })
            : Ok(dto);
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/sales")]
    public async Task<IActionResult> Sales(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetSalesAsync(companyId, id, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/purchases")]
    public async Task<IActionResult> Purchases(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetPurchasesAsync(companyId, id, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/expenses")]
    public async Task<IActionResult> Expenses(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetExpensesAsync(companyId, id, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/employees")]
    public async Task<IActionResult> Employees(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetEmployeesAsync(companyId, id, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/payroll")]
    public async Task<IActionResult> Payroll(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetPayrollAsync(companyId, id, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/top-products")]
    public async Task<IActionResult> TopProducts(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetTopProductsAsync(companyId, id, startDate, endDate));
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId}/trend")]
    public async Task<IActionResult> Trend(
        int companyId, string branchId,
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (Validate(branchId, startDate, endDate, out var id) is { } bad) return bad;
        return Ok(await _reports.GetTrendAsync(companyId, id, startDate, endDate));
    }
}