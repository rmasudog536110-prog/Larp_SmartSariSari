using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class InventoryService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public InventoryService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<Inventory>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Inventory
                .Include(x => x.Product)
                .AsNoTracking()
                .OrderBy(x => x.InventoryId)
                .ToListAsync();
        }

        public async Task<Inventory?> GetByProductIdAsync(
            int companyId,
            int productId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Inventory
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<Inventory> CreateAsync(
            int companyId,
            Inventory inventory)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var productExists = await db.Products
                .AnyAsync(x => x.ProductId == inventory.ProductId);

            if (!productExists)
                throw new InvalidOperationException("Product does not exist.");

            var existingInventory = await db.Inventory
                .FirstOrDefaultAsync(x => x.ProductId == inventory.ProductId);

            if (existingInventory != null)
                throw new InvalidOperationException(
                    "Inventory record already exists for this product.");

            inventory.LastUpdatedAt = DateTime.UtcNow;

            db.Inventory.Add(inventory);
            await db.SaveChangesAsync();

            return inventory;
        }

        public async Task<Inventory?> AddStockAsync(
            int companyId,
            int productId,
            decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.");

            await using var db = await _tenantFactory.CreateAsync(companyId);

            var inventory = await db.Inventory
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (inventory == null)
                return null;

            inventory.QuantityOnHand += quantity;
            inventory.LastUpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return inventory;
        }

        public async Task<Inventory?> RemoveStockAsync(
            int companyId,
            int productId,
            decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.");

            await using var db = await _tenantFactory.CreateAsync(companyId);

            var inventory = await db.Inventory
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (inventory == null)
                return null;

            if (inventory.QuantityOnHand < quantity)
                throw new InvalidOperationException(
                    "Insufficient stock.");

            inventory.QuantityOnHand -= quantity;
            inventory.LastUpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return inventory;
        }

        public async Task<Inventory?> UpdateReorderLevelAsync(
            int companyId,
            int productId,
            decimal reorderLevel)
        {
            if (reorderLevel < 0)
                throw new ArgumentException(
                    "Reorder level cannot be negative.");

            await using var db = await _tenantFactory.CreateAsync(companyId);

            var inventory = await db.Inventory
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (inventory == null)
                return null;

            inventory.ReorderLevel = reorderLevel;
            inventory.LastUpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return inventory;
        }

        public async Task<List<Inventory>> GetLowStockAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Inventory
                .Include(x => x.Product)
                .Where(x => x.QuantityOnHand <= x.ReorderLevel)
                .AsNoTracking()
                .OrderBy(x => x.QuantityOnHand)
                .ToListAsync();
        }
    }
}