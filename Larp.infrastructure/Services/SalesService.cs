using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class SalesService
    {
        private readonly ITenantDBContextFactory _tenantFactory;
        private const decimal VatRate = 0.12m;

        public SalesService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<Sales>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Sales
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(x => x.SaleItems)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .OrderByDescending(x => x.SaleDate)
                .ToListAsync();
        }

        public async Task<Sales?> GetByIdAsync(
            int companyId,
            int salesId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Sales
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(x => x.Branch)
                .Include(x => x.SaleItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(
                    x => x.SalesId == salesId);
        }

        public async Task<Sales> CreateAsync(
            int companyId,
            Sales sale,
            int employeeId)
        {
            if (sale.SaleItems == null || !sale.SaleItems.Any())
                throw new InvalidOperationException(
                    "A sale must contain at least one item.");

            await using var db = await _tenantFactory.CreateAsync(companyId);

            await using var transaction =
                await db.Database.BeginTransactionAsync();

            try
            {
                if (sale.CustomerId.HasValue)
                {
                    var customerExists = await db.Customers
                        .AnyAsync(x =>
                            x.CustomerId == sale.CustomerId.Value &&
                            x.IsActive);

                    if (!customerExists)
                        throw new InvalidOperationException(
                            "Customer does not exist.");
                }

                if (sale.Discount < 0)
                    throw new InvalidOperationException(
                        "Discount cannot be negative.");

                if (string.IsNullOrWhiteSpace(sale.PaymentMethod))
                {
                    sale.PaymentMethod = "Cash";
                }

                sale.PaymentMethod = sale.PaymentMethod.Trim();

                if (!new[] { "Cash", "Card", "GCash" }
                    .Contains(sale.PaymentMethod, StringComparer.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "Invalid payment method.");
                }

                if (sale.CashReceived < 0)
                {
                    throw new InvalidOperationException(
                        "Cash received cannot be negative.");
                }

                sale.SalesId = 0;
                sale.Customer = null;
                sale.Employee = null;
                sale.EmployeeId = employeeId;
                sale.SaleDate = DateTime.UtcNow;
                sale.Status = "Completed";

                // The branch always comes from the cashier's own record,
                // never from what the client sends.
                sale.Branch = null;
                sale.BranchId = await db.Employees
                    .Where(x => x.EmployeeId == employeeId)
                    .Select(x => x.BranchId)
                    .FirstOrDefaultAsync();


                if (string.IsNullOrWhiteSpace(sale.SaleNumber))
                {
                    sale.SaleNumber =
                        $"SALE-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
                }

                var movements = new List<StockMovement>();
                decimal subtotal = 0;

                foreach (var item in sale.SaleItems)
                {
                    item.SaleItemId = 0;
                    item.SaleId = 0;
                    item.Sale = null;
                    item.Product = null;

                    if (item.Quantity <= 0)
                        throw new InvalidOperationException(
                            "Sale quantity must be greater than zero.");

                    if (item.Discount < 0)
                        throw new InvalidOperationException(
                            "Item discount cannot be negative.");

                    var product = await db.Products
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == item.ProductId &&
                            x.IsActive);

                    if (product == null)
                        throw new InvalidOperationException(
                            $"Product {item.ProductId} does not exist.");

                    var inventory = await db.Inventory
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == item.ProductId);

                    if (inventory == null)
                        throw new InvalidOperationException(
                            $"No inventory record exists for {product.ProductName}.");

                    if (inventory.QuantityOnHand < item.Quantity)
                        throw new InvalidOperationException(
                            $"Insufficient stock for {product.ProductName}.");

                    item.UnitPrice = product.UnitPrice;
                    item.TotalAmount =
                        (item.Quantity * item.UnitPrice) - item.Discount;

                    if (item.TotalAmount < 0)
                        throw new InvalidOperationException(
                            "Sale item total cannot be negative.");

                    if (product.IsVatable)
                    {
                        item.TaxRate = VatRate;
                        item.TaxAmount = Math.Round(
                            item.TotalAmount / (1 + VatRate) * VatRate, 2);
                    }
                    else
                    {
                        item.TaxRate = 0;
                        item.TaxAmount = 0;
                    }

                    var before = inventory.QuantityOnHand;

                    inventory.QuantityOnHand -= item.Quantity;
                    inventory.LastUpdatedAt = DateTime.UtcNow;
                    inventory.UpdatedByEmployeeId = employeeId;

                    movements.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        MovementType = "Sale",
                        Quantity = item.Quantity,
                        QuantityBefore = before,
                        QuantityAfter = inventory.QuantityOnHand,
                        EmployeeId = employeeId,
                        MovementDate = DateTime.UtcNow,
                        Remarks = $"Sale {sale.SaleNumber}"
                    });

                    subtotal += item.TotalAmount;
                }

                sale.Subtotal = subtotal;
                sale.TotalAmount = sale.Subtotal - sale.Discount;

                if (sale.TotalAmount < 0)
                {
                    throw new InvalidOperationException(
                        "Sale total cannot be negative.");
                }

                if (sale.PaymentMethod.Equals(
                        "Cash",
                        StringComparison.OrdinalIgnoreCase))
                {
                    if (sale.CashReceived < sale.TotalAmount)
                    {
                        throw new InvalidOperationException(
                            "Cash received is less than the sale total.");
                    }

                    sale.ChangeAmount =
                        sale.CashReceived - sale.TotalAmount;
                }
                else
                {
                    sale.CashReceived = sale.TotalAmount;
                    sale.ChangeAmount = 0;
                }

                if (sale.TotalAmount < 0)
                    throw new InvalidOperationException(
                        "Sale total cannot be negative.");

                sale.VatAmount = sale.SaleItems.Sum(i => i.TaxAmount);
                sale.VatableSales = sale.SaleItems
                    .Where(i => i.TaxRate > 0)
                    .Sum(i => i.TotalAmount - i.TaxAmount);
                sale.VatExempt = sale.SaleItems
                    .Where(i => i.TaxRate == 0)
                    .Sum(i => i.TotalAmount);

                db.Sales.Add(sale);
                db.StockMovements.AddRange(movements);

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return sale;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Returns false if the sale does not exist.
        /// Throws InvalidOperationException if it is already cancelled.
        /// </summary>
        public async Task<bool> CancelAsync(
            int companyId,
            int salesId,
            int employeeId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            await using var transaction =
                await db.Database.BeginTransactionAsync();

            try
            {
                var sale = await db.Sales
                    .Include(x => x.SaleItems)
                    .FirstOrDefaultAsync(x => x.SalesId == salesId);

                if (sale == null)
                    return false;

                if (sale.Status == "Cancelled")
                    throw new InvalidOperationException(
                        "Sale is already cancelled.");

                foreach (var item in sale.SaleItems)
                {
                    var inventory = await db.Inventory
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == item.ProductId);

                    if (inventory == null)
                        continue;

                    var before = inventory.QuantityOnHand;

                    inventory.QuantityOnHand += item.Quantity;
                    inventory.LastUpdatedAt = DateTime.UtcNow;
                    inventory.UpdatedByEmployeeId = employeeId;

                    db.StockMovements.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        MovementType = "SaleCancel",
                        Quantity = item.Quantity,
                        QuantityBefore = before,
                        QuantityAfter = inventory.QuantityOnHand,
                        EmployeeId = employeeId,
                        MovementDate = DateTime.UtcNow,
                        Remarks = $"Cancelled {sale.SaleNumber}"
                    });
                }

                sale.Status = "Cancelled";

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