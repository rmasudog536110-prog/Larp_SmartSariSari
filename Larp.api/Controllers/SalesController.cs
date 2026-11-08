using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class SalesController : ControllerBase
{
    [HttpPost("/tenant/{companyId:int}/sales")]
    public async Task<IActionResult> Create(
        int companyId,
        Sales sale,
        SalesService salesService)
    {
        if (!int.TryParse(
                User.FindFirst("employeeId")?.Value,
                out var employeeId))
        {
            return StatusCode(403, new
            {
                message =
                    "An employee account is required to record a sale."
            });
        }

        try
        {
            var createdSale =
                await salesService.CreateAsync(
                    companyId,
                    sale,
                    employeeId);

            return Ok(new
            {
                salesId = createdSale.SalesId,
                saleNumber = createdSale.SaleNumber,
                saleDate = createdSale.SaleDate,
                subtotal = createdSale.Subtotal,
                discount = createdSale.Discount,
                totalAmount = createdSale.TotalAmount,
                vatableSales = createdSale.VatableSales,
                vatExempt = createdSale.VatExempt,
                vatAmount = createdSale.VatAmount,
                cashReceived = createdSale.CashReceived,
                changeAmount = createdSale.ChangeAmount,
                paymentMethod = createdSale.PaymentMethod,
                status = createdSale.Status
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("/tenant/{companyId:int}/sales")]
    public async Task<IActionResult> GetAll(
        int companyId,
        SalesService salesService)
    {
        var sales =
            await salesService.GetAllAsync(companyId);

        return Ok(
            sales.Select(x => new
            {
                salesId = x.SalesId,
                saleNumber = x.SaleNumber,
                saleDate = x.SaleDate,
                cashier = x.Employee == null
                    ? "Unknown"
                    : $"{x.Employee.FirstName} {x.Employee.LastName}",
                subtotal = x.Subtotal,
                discount = x.Discount,
                totalAmount = x.TotalAmount,
                cashReceived = x.CashReceived,
                changeAmount = x.ChangeAmount,
                paymentMethod = x.PaymentMethod,
                vatableSales = x.VatableSales,
                vatExempt = x.VatExempt,
                vatAmount = x.VatAmount,
                status = x.Status
            }));
    }

    [HttpGet("/tenant/{companyId:int}/sales/{salesId:int}")]
    public async Task<IActionResult> GetById(
        int companyId,
        int salesId,
        SalesService salesService)
    {
        var sale =
            await salesService.GetByIdAsync(
                companyId,
                salesId);

        if (sale == null)
        {
            return NotFound(new
            {
                message = "Sale not found."
            });
        }
        return Ok(new
        {
            salesId = sale.SalesId,
            saleNumber = sale.SaleNumber,
            saleDate = sale.SaleDate,

            cashier = sale.Employee == null
                ? "Unknown"
                : $"{sale.Employee.FirstName} {sale.Employee.LastName}",

            customer = sale.Customer == null
                ? "Walk-in Customer"
                : sale.Customer.CustomerName,

            branch = sale.Branch == null
                ? "Main Branch"
                : sale.Branch.BranchName,

            subtotal = sale.Subtotal,
            discount = sale.Discount,
            totalAmount = sale.TotalAmount,

            vatableSales = sale.VatableSales,
            vatExempt = sale.VatExempt,
            vatAmount = sale.VatAmount,

            cashReceived = sale.CashReceived,
            changeAmount = sale.ChangeAmount,
            paymentMethod = sale.PaymentMethod,
            status = sale.Status,

            items = sale.SaleItems.Select(item => new
            {
                saleItemId = item.SaleItemId,
                productId = item.ProductId,
                productName = item.Product?.ProductName ?? "Unknown Product",
                quantity = item.Quantity,
                unitPrice = item.UnitPrice,
                discount = item.Discount,
                totalAmount = item.TotalAmount,
                taxRate = item.TaxRate,
                taxAmount = item.TaxAmount
            }).ToList()
        });
    }
}