using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
[Route("tenant/{companyId:int}/purchases")]
public class PurchaseController : ControllerBase
{
    private readonly PurchaseService _purchaseService;

    public PurchaseController(PurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int companyId)
    {
        var purchases = await _purchaseService.GetAllAsync(companyId);

        return Ok(purchases.Select(ToDto).ToList());
    }

    [HttpGet("{purchaseId:int}")]
    public async Task<IActionResult> GetById(int companyId, int purchaseId)
    {
        var purchase = await _purchaseService.GetByIdAsync(companyId, purchaseId);

        if (purchase == null)
        {
            return NotFound(new { message = "Purchase not found." });
        }

        return Ok(ToDto(purchase));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int companyId,
        [FromBody] CreatePurchaseRequest request)
    {
        if (!TryGetEmployeeId(out var employeeId))
        {
            return StatusCode(403, new
            {
                message = "An employee account is required for this action."
            });
        }

        try
        {
            var purchase = new Purchase
            {
                SupplierId = request.SupplierId,
                PurchaseNumber = request.PurchaseNumber?.Trim() ?? string.Empty,
                Discount = request.Discount,
                PurchaseItems = (request.Items ?? new())
                    .Select(i => new PurchaseItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitCost = i.UnitCost
                    })
                    .ToList()
            };

            var created = await _purchaseService.CreateAsync(
                companyId, purchase, employeeId, request.BranchId, IsAdmin);

            var full = await _purchaseService.GetByIdAsync(
                companyId,
                created.PurchaseId);

            return Ok(ToDto(full!));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{purchaseId:int}/cancel")]
    public async Task<IActionResult> Cancel(int companyId, int purchaseId)
    {
        if (!TryGetEmployeeId(out var employeeId))
        {
            return StatusCode(403, new
            {
                message = "An employee account is required for this action."
            });
        }

        try
        {
            var cancelled = await _purchaseService.CancelAsync(
                companyId,
                purchaseId,
                employeeId);

            if (!cancelled)
            {
                return NotFound(new { message = "Purchase not found." });
            }

            return Ok(new { message = "Purchase cancelled." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private bool IsAdmin => User.IsInRole("Admin") || User.IsInRole("SuperAdmin");
    private bool TryGetEmployeeId(out int employeeId) =>
        int.TryParse(User.FindFirst("employeeId")?.Value, out employeeId);

    private static PurchaseDto ToDto(Purchase p) => new()
    {
        PurchaseId = p.PurchaseId,
        PurchaseNumber = p.PurchaseNumber,
        SupplierId = p.SupplierId,
        SupplierName = p.Supplier?.SupplierName ?? string.Empty,
        PurchaseDate = p.PurchaseDate,
        Subtotal = p.Subtotal,
        Discount = p.Discount,
        TotalAmount = p.TotalAmount,
        Status = p.Status,
        BranchId = p.BranchId,
        BranchName = p.Branch?.BranchName,
        CreatedByEmployeeId = p.CreatedByEmployeeId,
        CreatedAt = p.CreatedAt,
        Items = p.PurchaseItems
            .Select(i => new PurchaseItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product?.ProductName ?? string.Empty,
                Quantity = i.Quantity,
                UnitCost = i.UnitCost,
                TotalAmount = i.TotalAmount
            })
            .ToList()
    };
}