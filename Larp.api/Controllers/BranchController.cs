using Larp.domain.DTO;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class BranchController : ControllerBase
{
    [HttpGet("/tenant/{companyId:int}/branches")]
    public async Task<IActionResult> GetAll(
        int companyId,
        BranchService branchService)
    {
        var branches = await branchService.GetAllAsync(companyId);

        return Ok(branches);
    }

    [HttpGet("/tenant/{companyId:int}/branches/{branchId:int}")]
    public async Task<IActionResult> GetById(
        int companyId,
        int branchId,
        BranchService branchService)
    {
        var branch = await branchService.GetByIdAsync(companyId, branchId);

        if (branch == null)
            return NotFound(new { message = "Branch not found." });

        return Ok(branch);
    }

    [HttpPost("/tenant/{companyId:int}/branches")]
    public async Task<IActionResult> Create(
        int companyId,
        SaveBranchRequest request,
        BranchService branchService,
        PlanService planService)
    {
        try
        {

            var limit = await planService.CheckBranchLimitAsync(companyId);
            if (limit != null) 
                return StatusCode(403, new { message = limit });

            var created = await branchService.CreateAsync(companyId, request);

            return Ok(created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("/tenant/{companyId:int}/branches/{branchId:int}")]
    public async Task<IActionResult> Update(
        int companyId,
        int branchId,
        SaveBranchRequest request,
        BranchService branchService,
        PlanService planService)
    {
        try
        {

            if (request.IsActive)
            {
                var existing = await branchService.GetByIdAsync(companyId, branchId);
                if (existing != null && !existing.IsActive)
                {
                    var limit = await planService.CheckBranchLimitAsync(companyId);
                    if (limit != null) return StatusCode(403, new { message = limit });
                }
            }

            var updated = await branchService.UpdateAsync(companyId, branchId, request);

            if (updated == null)
                return NotFound(new { message = "Branch not found." });

            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("/tenant/{companyId:int}/branches/{branchId:int}")]
    public async Task<IActionResult> Deactivate(
        int companyId,
        int branchId,
        BranchService branchService)
    {
        var done = await branchService.DeactivateAsync(companyId, branchId);

        if (!done)
            return NotFound(new { message = "Branch not found." });

        return Ok(new { message = "Branch deactivated." });
    }

    // PUT /tenant/{companyId}/branches/{branchId}/employees/{employeeId}
    [HttpPut("/tenant/{companyId:int}/branches/{branchId:int}/employees/{employeeId:int}")]
    public async Task<IActionResult> AssignEmployee(
        int companyId,
        int branchId,
        int employeeId,
        BranchService branchService)
    {
        try
        {
            var done = await branchService.AssignEmployeeAsync(companyId, branchId, employeeId);

            if (!done)
                return NotFound(new { message = "Branch or employee not found." });

            return Ok(new { message = "Employee assigned to branch." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}