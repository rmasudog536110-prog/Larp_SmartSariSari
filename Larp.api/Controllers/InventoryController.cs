using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Larp.api.Controllers;

[ApiController]
public class InventoryController : ControllerBase
{
    [HttpGet("/tenant/{companyId:int}/inventory")]
    public async Task<IActionResult> GetAll(
        int companyId,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var inventory = await tenantDb.Products
            .AsNoTracking()
            .Select(product => new
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                QuantityOnHand = tenantDb.Inventory
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => (decimal?)i.QuantityOnHand)
                    .FirstOrDefault() ?? 0,
                ReorderLevel = tenantDb.Inventory
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => (decimal?)i.ReorderLevel)
                    .FirstOrDefault() ?? 0,
                IsActive = product.IsActive
            })
            .OrderBy(x => x.ProductName)
            .ToListAsync();

        return Ok(inventory);
    }

    [HttpPut("/tenant/{companyId:int}/inventory/{productId:int}")]
    public async Task<IActionResult> UpdateStock(
        int companyId,
        int productId,
        InventoryStockRequest request,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var product = await tenantDb.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        var inventory = await tenantDb.Inventory
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        if (inventory == null)
        {
            inventory = new Inventory
            {
                ProductId = productId,
                QuantityOnHand = request.QuantityOnHand,
                ReorderLevel = request.ReorderLevel,
                LastUpdatedAt = DateTime.UtcNow
            };

            tenantDb.Inventory.Add(inventory);
        }
        else
        {
            inventory.QuantityOnHand = request.QuantityOnHand;
            inventory.ReorderLevel = request.ReorderLevel;
            inventory.LastUpdatedAt = DateTime.UtcNow;
        }

        await tenantDb.SaveChangesAsync();

        return Ok(new
        {
            inventory.InventoryId,
            inventory.ProductId,
            inventory.QuantityOnHand,
            inventory.ReorderLevel,
            inventory.LastUpdatedAt
        });
    }

    [HttpGet("/tenant/{companyId:int}/inventory/{productId:int}")]
    public async Task<IActionResult> GetProductInventory(
        int companyId,
        int productId,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var inventory = await tenantDb.Inventory
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        if (inventory == null)
        {
            return Ok(new
            {
                ProductId = productId,
                QuantityOnHand = 0,
                ReorderLevel = 0f
            });
        }

        return Ok(new
        {
            inventory.ProductId,
            inventory.Product?.ProductCode,
            inventory.Product?.ProductName,
            inventory.QuantityOnHand,
            inventory.ReorderLevel
        });
    }

    [HttpPut("/tenant/{companyId:int}/inventory/{productId:int}/sell")]
    public async Task<IActionResult> Sell(
        int companyId,
        int productId,
        InventoryStockRequest request,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var inventory = await tenantDb.Inventory
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        if (inventory == null)
        {
            return NotFound(new
            {
                message = "Inventory record not found."
            });
        }

        if (request.QuantityOnHand <= 0)
        {
            return BadRequest(new
            {
                message = "Quantity must be greater than 0."
            });
        }

        if (inventory.QuantityOnHand < request.QuantityOnHand)
        {
            return BadRequest(new
            {
                message = "Insufficient stock.",
                availableStock = inventory.QuantityOnHand
            });
        }

        inventory.QuantityOnHand -= request.QuantityOnHand;
        inventory.LastUpdatedAt = DateTime.UtcNow;

        await tenantDb.SaveChangesAsync();

        return Ok(new
        {
            inventory.ProductId,
            inventory.QuantityOnHand,
            inventory.ReorderLevel
        });
    }
}