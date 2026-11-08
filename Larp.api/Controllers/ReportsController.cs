using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class ReportsController : ControllerBase
{
    [HttpGet("/tenant/{companyId:int}/reports/sales")]
    public async Task<IActionResult> GetSalesReport(
        int companyId,
        DateTime startDate,
        DateTime endDate,
        IReportService reportService)
    {
        if (endDate < startDate)
        {
            return BadRequest(new
            {
                message = "End date cannot be earlier than start date."
            });
        }

        var report = await reportService.GetSalesReportAsync(
            companyId,
            startDate,
            endDate);

        return Ok(report);
    }

    // GET /tenant/{companyId}/reports/income-statement?startDate=2026-09-01&endDate=2026-09-30
    [HttpGet("/tenant/{companyId:int}/reports/income-statement")]
    public async Task<IActionResult> GetIncomeStatement(
        int companyId,
        DateTime startDate,
        DateTime endDate,
        IReportService reportService)
    {
        if (endDate.Date < startDate.Date)
        {
            return BadRequest(new
            {
                message = "End date cannot be earlier than start date."
            });
        }

        var statement = await reportService.GetIncomeStatementAsync(
            companyId,
            startDate,
            endDate);

        return Ok(statement);
    }
}