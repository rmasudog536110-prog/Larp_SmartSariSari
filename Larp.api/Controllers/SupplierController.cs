using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class SupplierController : ControllerBase
{
    [HttpGet("/tenant/{companyId:int}/suppliers")]
    public async Task<IActionResult> GetAll(
        int companyId,
        SupplierService supplierService)
    {
        var suppliers = await supplierService.GetAllAsync(companyId);

        return Ok(suppliers);
    }

    [HttpGet("/tenant/{companyId:int}/suppliers/{supplierId:int}")]
    public async Task<IActionResult> GetById(
        int companyId,
        int supplierId,
        SupplierService supplierService)
    {
        var supplier = await supplierService.GetByIdAsync(
            companyId,
            supplierId);

        if (supplier == null)
        {
            return NotFound(new
            {
                message = "Supplier not found."
            });
        }

        return Ok(supplier);
    }

    [HttpPost("/tenant/{companyId:int}/suppliers")]
    public async Task<IActionResult> Create(
        int companyId,
        Supplier supplier,
        SupplierService supplierService)
    {
        try
        {
            // An Admin creates suppliers directly. A Manager's new supplier waits
            // for an Admin and cannot be used in a purchase until approved.
            bool isAdmin = User.IsInRole("Admin") || User.IsInRole("SuperAdmin");
            supplier.ApprovalStatus = isAdmin ? "Approved" : "Pending";

            var created = await supplierService.CreateAsync(
                companyId,
                supplier);

            return Ok(created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("/tenant/{companyId:int}/suppliers/{supplierId:int}")]
    public async Task<IActionResult> Update(
        int companyId,
        int supplierId,
        Supplier supplier,
        SupplierService supplierService)
    {
        try
        {
            var updated = await supplierService.UpdateAsync(
                companyId,
                supplierId,
                supplier.SupplierCode,
                supplier.SupplierName,
                supplier.ContactPerson,
                supplier.ContactNumber,
                supplier.EmailAddress,
                supplier.Address,
                supplier.IsActive);

            if (updated == null)
            {
                return NotFound(new
                {
                    message = "Supplier not found."
                });
            }

            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("/tenant/{companyId:int}/suppliers/{supplierId:int}")]
    public async Task<IActionResult> Delete(
        int companyId,
        int supplierId,
        SupplierService supplierService)
    {
        var deleted = await supplierService.DeleteAsync(
            companyId,
            supplierId);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Supplier not found."
            });
        }

        return Ok(new
        {
            message = "Supplier deleted successfully."
        });
    }
}