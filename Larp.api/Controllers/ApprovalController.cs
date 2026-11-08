using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;


[ApiController]
public class ApprovalController : ControllerBase
{
    [HttpGet("/tenant/{companyId:int}/approvals")]
    public async Task<IActionResult> GetPending(
        int companyId,
        ApprovalService approvalService)
    {
        var items = await approvalService.GetPendingAsync(companyId);

        return Ok(items);
    }

    // type: employee, supplier or payroll
    [HttpPost("/tenant/{companyId:int}/approvals/{type}/{itemId:int}/approve")]
    public async Task<IActionResult> Approve(
        int companyId,
        string type,
        int itemId,
        ApprovalService approvalService)
    {
        try
        {
            var done = await approvalService.ApproveAsync(companyId, type, itemId);

            if (!done)
                return NotFound(new { message = "Item not found." });

            return Ok(new { message = "Approved." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("/tenant/{companyId:int}/approvals/{type}/{itemId:int}/reject")]
    public async Task<IActionResult> Reject(
        int companyId,
        string type,
        int itemId,
        ApprovalService approvalService)
    {
        try
        {
            var done = await approvalService.RejectAsync(companyId, type, itemId);

            if (!done)
                return NotFound(new { message = "Item not found." });

            return Ok(new { message = "Rejected." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}