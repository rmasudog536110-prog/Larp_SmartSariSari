using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class BenefitsController : ControllerBase
{
    // ---------------- Employee benefits ----------------

    [HttpGet("/tenant/{companyId:int}/benefits/{employeeId:int}")]
    public async Task<IActionResult> Get(
        int companyId,
        int employeeId,
        BenefitsService service)
    {
        var benefits = await service.GetAsync(companyId, employeeId);

        if (benefits == null)
        {
            return NotFound(new { message = "No benefits record for this employee yet." });
        }

        return Ok(benefits);
    }

    [HttpPut("/tenant/{companyId:int}/benefits/{employeeId:int}")]
    public async Task<IActionResult> Save(
        int companyId,
        int employeeId,
        SaveBenefitsRequest request,
        BenefitsService service)
    {
        try
        {
            return Ok(await service.SaveAsync(companyId, employeeId, request));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // ---------------- Contribution rates ----------------

    [HttpGet("/tenant/{companyId:int}/contribution-rates")]
    public async Task<IActionResult> GetRates(
        int companyId,
        BenefitsService service)
    {
        return Ok(await service.GetRatesAsync(companyId));
    }

    [HttpPost("/tenant/{companyId:int}/contribution-rates")]
    public async Task<IActionResult> CreateRate(
        int companyId,
        ContributionRate rate,
        BenefitsService service)
    {
        try
        {
            return Ok(await service.CreateRateAsync(companyId, rate));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("/tenant/{companyId:int}/contribution-rates/{id:int}")]
    public async Task<IActionResult> UpdateRate(
        int companyId,
        int id,
        ContributionRate rate,
        BenefitsService service)
    {
        try
        {
            var updated = await service.UpdateRateAsync(companyId, id, rate);

            return updated == null
                ? NotFound(new { message = "Rate not found." })
                : Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("/tenant/{companyId:int}/contribution-rates/{id:int}")]
    public async Task<IActionResult> DeleteRate(
        int companyId,
        int id,
        BenefitsService service)
    {
        return await service.DeleteRateAsync(companyId, id)
            ? Ok(new { message = "Rate deleted." })
            : NotFound(new { message = "Rate not found." });
    }
}