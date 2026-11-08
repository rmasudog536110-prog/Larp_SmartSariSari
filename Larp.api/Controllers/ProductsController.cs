using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Larp.api.Controllers;

[ApiController]
public class ProductsController : ControllerBase
{
    [HttpPost("tenant/{companyId:int}/products")]
    public async Task<IActionResult> Create(
        int companyId,
        Product product,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var lastProduct = await tenantDb.Products
            .OrderByDescending(x => x.ProductId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (lastProduct != null &&
            !string.IsNullOrWhiteSpace(lastProduct.ProductCode) &&
            lastProduct.ProductCode.StartsWith("PRD-"))
        {
            string numberPart =
                lastProduct.ProductCode.Substring(4);

            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        product.ProductCode = $"PRD-{nextNumber:D3}";

        tenantDb.Products.Add(product);

        await tenantDb.SaveChangesAsync();

        return Created(
            $"/tenant/{companyId}/products/{product.ProductId}",
            product);
    }

    [HttpGet("/tenant/{companyId:int}/products/next-code")]
    public async Task<IActionResult> GetNextProductCode(
        int companyId,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var lastProduct = await tenantDb.Products
            .OrderByDescending(x => x.ProductId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (lastProduct != null &&
            !string.IsNullOrWhiteSpace(lastProduct.ProductCode) &&
            lastProduct.ProductCode.StartsWith("PRD-"))
        {
            string numberPart =
                lastProduct.ProductCode.Substring(4);

            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        return Ok($"PRD-{nextNumber:D3}");
    }

    [HttpGet("/tenant/{companyId:int}")]
    public async Task<IActionResult> GetCount(
        int companyId,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var productCount =
            await tenantDb.Products.CountAsync();

        return Ok(new
        {
            companyId,
            productCount
        });
    }

    [HttpGet("/tenant/{companyId:int}/products")]
    public async Task<IActionResult> GetAll(
        int companyId,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var products = await tenantDb.Products
            .AsNoTracking()
            .OrderBy(x => x.ProductId)
            .ToListAsync();

        return Ok(products);
    }

    [HttpPut("/tenant/{companyId:int}/products/{productId:int}")]
    public async Task<IActionResult> Update(
        int companyId,
        int productId,
        Product updatedProduct,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var product = await tenantDb.Products
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        product.ProductName = updatedProduct.ProductName;
        product.UnitPrice = updatedProduct.UnitPrice;
        product.IsVatable = updatedProduct.IsVatable;
        product.IsActive = updatedProduct.IsActive;

        await tenantDb.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("/tenant/{companyId:int}/products/{productId:int}")]
    public async Task<IActionResult> Delete(
        int companyId,
        int productId,
        ITenantDBContextFactory tenantFactory)
    {
        await using var tenantDb =
            await tenantFactory.CreateAsync(companyId);

        var product = await tenantDb.Products
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        tenantDb.Products.Remove(product);

        await tenantDb.SaveChangesAsync();

        return Ok(new
        {
            message = "Product deleted successfully."
        });
    }

    [HttpPut("/tenant/{companyId:int}/products/{productId:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int companyId,
        int productId,
        Product request,
        ITenantDBContextFactory tenantFactory)
    {
        await using var db =
            await tenantFactory.CreateAsync(companyId);

        var product = await db.Products
            .FirstOrDefaultAsync(x =>
                x.ProductId == productId);

        if (product == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        product.IsActive = request.IsActive;

        await db.SaveChangesAsync();

        return Ok(new
        {
            message = request.IsActive
                ? "Product activated."
                : "Product deactivated.",
            productId = product.ProductId,
            isActive = product.IsActive
        });
    }
}