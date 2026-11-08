using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class ProductService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public ProductService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<Product>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Products
                .AsNoTracking()
                .OrderBy(x => x.ProductId)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int companyId, int productId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<Product?> GetByCodeAsync(int companyId, string productCode)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductCode == productCode);
        }

        public async Task<Product> CreateAsync(int companyId, Product product)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var exists = await db.Products
                .AnyAsync(x => x.ProductCode == product.ProductCode);

            if (exists)
                throw new InvalidOperationException("Product code already exists.");

            product.CreatedAt = DateTime.UtcNow;
            product.IsActive = true;

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(
            int companyId,
            int productId,
            string productCode,
            string productName,
            decimal unitPrice,
            bool isVatable,
            bool isActive)

        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var product = await db.Products
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
                return null;

            var duplicate = await db.Products
                .AnyAsync(x =>
                    x.ProductCode == productCode &&
                    x.ProductId != productId);

            if (duplicate)
                throw new InvalidOperationException("Product code already exists.");

            product.ProductCode = productCode;
            product.ProductName = productName;
            product.UnitPrice = unitPrice;
            product.IsVatable = isVatable;
            product.IsActive = isActive;

            await db.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(int companyId, int productId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);   

            var product = await db.Products
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
                return false;

            product.IsActive = false;

            await db.SaveChangesAsync();

            return true;
        }
    }
}