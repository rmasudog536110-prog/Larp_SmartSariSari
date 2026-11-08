using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class PayrollController : ControllerBase
{
    private bool IsAdmin =>
        User.IsInRole("Admin") || User.IsInRole("SuperAdmin");

    [HttpGet("/tenant/{companyId:int}/payroll")]
    public async Task<IActionResult> GetAll(
        int companyId,
        PayrollService payrollService)
    {
        var payrolls =
            await payrollService.GetAllAsync(companyId);

        return Ok(payrolls);
    }

    [HttpGet("/tenant/{companyId:int}/payroll/{payrollId:int}")]
    public async Task<IActionResult> GetById(
        int companyId,
        int payrollId,
        PayrollService payrollService)
    {
        var payroll =
            await payrollService.GetByIdAsync(
                companyId,
                payrollId);

        if (payroll == null)
        {
            return NotFound(new
            {
                message = "Payroll record not found."
            });
        }

        return Ok(payroll);
    }

    // An Admin approves it in the Approvals form.
    [HttpPost("/tenant/{companyId:int}/payroll")]
    public async Task<IActionResult> Create(
        int companyId,
        PayrollEntity payroll,
        PayrollService payrollService)
    {
        try
        {
            var createdPayroll =
                await payrollService.CreateAsync(
                    companyId,
                    payroll,
                    IsAdmin);

            return Ok(createdPayroll);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("/tenant/{companyId:int}/payroll/{payrollId:int}")]
    public async Task<IActionResult> Update(
        int companyId,
        int payrollId,
        PayrollEntity payroll,
        PayrollService payrollService)
    {
        try
        {
            if (!IsAdmin)
            {
                var existing =
                    await payrollService.GetByIdAsync(
                        companyId,
                        payrollId);

                if (existing == null)
                {
                    return NotFound(new
                    {
                        message = "Payroll record not found."
                    });
                }

                // A Manager can edit payroll only while it is Pending, and cannot
                // release it: the status stays as it is.
                if (existing.Status != "Pending")
                {
                    return StatusCode(403, new
                    {
                        message = "Only an Admin can change payroll that is already approved or paid."
                    });
                }

                payroll.Status = existing.Status;
            }

            var updatedPayroll =
                await payrollService.UpdateAsync(
                    companyId,
                    payrollId,
                    payroll);

            if (updatedPayroll == null)
            {
                return NotFound(new
                {
                    message = "Payroll record not found."
                });
            }

            return Ok(updatedPayroll);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // GET /tenant/{companyId}/payroll/preview?employeeId=5&start=2026-10-01&end=2026-10-15&allowances=0
    [HttpGet("/tenant/{companyId:int}/payroll/preview")]
    public async Task<IActionResult> Preview(
        int companyId,
        int employeeId,
        DateTime start,
        DateTime end,
        decimal allowances,
        PayrollService payrollService)
    {
        try
        {
            return Ok(await payrollService.PreviewAsync(companyId, employeeId, start, end, allowances));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("/tenant/{companyId:int}/payroll/{payrollId:int}")]
    public async Task<IActionResult> Delete(
        int companyId,
        int payrollId,
        PayrollService payrollService)
    {
        if (!IsAdmin)
        {
            var existing =
                await payrollService.GetByIdAsync(
                    companyId,
                    payrollId);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Payroll record not found."
                });
            }

            if (existing.Status != "Pending")
            {
                return StatusCode(403, new
                {
                    message = "Only an Admin can delete payroll that is already approved or paid."
                });
            }
        }

        var deleted =
            await payrollService.DeleteAsync(
                companyId,
                payrollId);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Payroll record not found."
            });
        }

        return Ok(new
        {
            message = "Payroll record deleted successfully."
        });
    }
}