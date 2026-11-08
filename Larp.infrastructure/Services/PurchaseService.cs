using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class PurchaseService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public PurchaseService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<Purchase>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Purchases
                .Include(x => x.Supplier)
                .Include(x => x.Branch)
                .Include(x => x.PurchaseItems)
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .OrderByDescending(x => x.PurchaseDate)
                .ToListAsync();
        }

        public async Task<Purchase?> GetByIdAsync(
            int companyId,
            int purchaseId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Purchases
                .Include(x => x.Supplier)
                .Include(x => x.Branch)
                .Include(x => x.PurchaseItems)
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PurchaseId == purchaseId);
        }

        public async Task<Purchase> CreateAsync(
            int companyId,
            Purchase purchase,
            int employeeId,
            int? requestedBranchId = null,
            bool canChooseBranch = false)
        {
            if (purchase.PurchaseItems == null ||
                !purchase.PurchaseItems.Any())
            {
                throw new InvalidOperationException(
                    "A purchase must contain at least one item.");
            }

            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            await using var transaction =
                await db.Database.BeginTransactionAsync();

            try
            {
                var supplierExists = await db.Suppliers
                    .AnyAsync(x =>
                        x.SupplierId == purchase.SupplierId &&
                        x.IsActive);

                if (!supplierExists)
                    throw new InvalidOperationException(
                        "Supplier does not exist.");

                if (purchase.Discount < 0)
                {
                    throw new InvalidOperationException(
                        "Discount cannot be negative.");
                }

                purchase.PurchaseId = 0;
                purchase.Supplier = null;
                purchase.CreatedByEmployee = null;
                purchase.UpdatedByEmployee = null;
                purchase.CreatedByEmployeeId = employeeId;
                purchase.UpdatedByEmployeeId = null;
                purchase.CreatedAt = DateTime.UtcNow;
                purchase.PurchaseDate = DateTime.UtcNow;
                purchase.Status = "Completed";
                purchase.Branch = null;
                purchase.BranchId = await ResolveBranchAsync(db, requestedBranchId, canChooseBranch, employeeId);

                if (string.IsNullOrWhiteSpace(purchase.PurchaseNumber))
                {
                    purchase.PurchaseNumber =
                        $"PUR-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
                }

                var movements = new List<StockMovement>();
                decimal subtotal = 0;

                foreach (var item in purchase.PurchaseItems)
                {
                    item.PurchaseItemId = 0;
                    item.PurchaseId = 0;
                    item.Purchase = null;
                    item.Product = null;

                    if (item.Quantity <= 0)
                    {
                        throw new InvalidOperationException(
                            "Purchase quantity must be greater than zero.");
                    }

                    if (item.UnitCost < 0)
                    {
                        throw new InvalidOperationException(
                            "Unit cost cannot be negative.");
                    }

                    var productExists = await db.Products
                        .AnyAsync(x =>
                            x.ProductId == item.ProductId &&
                            x.IsActive);

                    if (!productExists)
                    {
                        throw new InvalidOperationException(
                            $"Product {item.ProductId} does not exist.");
                    }

                    // Local first: the same product may appear on two lines,
                    // and a new inventory row is not in the database yet.
                    var inventory =
                        db.Inventory.Local.FirstOrDefault(x =>
                            x.ProductId == item.ProductId)
                        ?? await db.Inventory.FirstOrDefaultAsync(x =>
                            x.ProductId == item.ProductId);

                    if (inventory == null)
                    {
                        inventory = new Inventory
                        {
                            ProductId = item.ProductId,
                            QuantityOnHand = 0,
                            ReorderLevel = 0,
                            LastUpdatedAt = DateTime.UtcNow,
                            CreatedByEmployeeId = employeeId
                        };

                        db.Inventory.Add(inventory);
                    }

                    item.TotalAmount = item.Quantity * item.UnitCost;

                    var before = inventory.QuantityOnHand;

                    inventory.QuantityOnHand += item.Quantity;
                    inventory.LastUpdatedAt = DateTime.UtcNow;
                    inventory.UpdatedByEmployeeId = employeeId;

                    movements.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        MovementType = "Purchase",
                        Quantity = item.Quantity,
                        QuantityBefore = before,
                        QuantityAfter = inventory.QuantityOnHand,
                        EmployeeId = employeeId,
                        MovementDate = DateTime.UtcNow,
                        Remarks = $"Purchase {purchase.PurchaseNumber}",
                        Purchase = purchase
                    });

                    subtotal += item.TotalAmount;
                }

                purchase.Subtotal = subtotal;
                purchase.TotalAmount = purchase.Subtotal - purchase.Discount;

                if (purchase.TotalAmount < 0)
                {
                    throw new InvalidOperationException(
                        "Purchase total cannot be negative.");
                }

                db.Purchases.Add(purchase);
                db.StockMovements.AddRange(movements);

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return purchase;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        private static async Task<int?> ResolveBranchAsync(
            TenantErpDbContext db, int? requested, bool canChoose, int employeeId)
        {
            if (canChoose && requested.HasValue)
            {
                bool ok = await db.Branches.AnyAsync(x => x.BranchId == requested.Value && x.IsActive);
                if (!ok) throw new InvalidOperationException("Branch does not exist or is inactive.");
                return requested;
            }

            return await db.Employees.AsNoTracking()
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => x.BranchId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns false if the purchase does not exist.
        /// Throws InvalidOperationException if it is already cancelled
        /// or if the stock it added has already been used.
        /// </summary>
        public async Task<bool> CancelAsync(
            int companyId,
            int purchaseId,
            int employeeId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            await using var transaction =
                await db.Database.BeginTransactionAsync();

            try
            {
                var purchase = await db.Purchases
                    .Include(x => x.PurchaseItems)
                    .FirstOrDefaultAsync(x =>
                        x.PurchaseId == purchaseId);

                if (purchase == null)
                    return false;

                if (purchase.Status == "Cancelled")
                {
                    throw new InvalidOperationException(
                        "Purchase is already cancelled.");
                }

                foreach (var item in purchase.PurchaseItems)
                {
                    var inventory = await db.Inventory
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == item.ProductId);

                    if (inventory == null)
                        continue;

                    if (inventory.QuantityOnHand < item.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Cannot cancel purchase because there is not enough stock for product {item.ProductId}.");
                    }

                    var before = inventory.QuantityOnHand;

                    inventory.QuantityOnHand -= item.Quantity;
                    inventory.LastUpdatedAt = DateTime.UtcNow;
                    inventory.UpdatedByEmployeeId = employeeId;

                    db.StockMovements.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        MovementType = "PurchaseCancel",
                        Quantity = item.Quantity,
                        QuantityBefore = before,
                        QuantityAfter = inventory.QuantityOnHand,
                        PurchaseId = purchase.PurchaseId,
                        EmployeeId = employeeId,
                        MovementDate = DateTime.UtcNow,
                        Remarks = $"Cancelled {purchase.PurchaseNumber}"
                    });
                }

                purchase.Status = "Cancelled";
                purchase.UpdatedByEmployeeId = employeeId;
                purchase.UpdatedAt = DateTime.UtcNow;

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}