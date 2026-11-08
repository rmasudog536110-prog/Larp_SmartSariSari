using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Larp.infrastructure.Services
{
    public static class SalesSeeder
    {
        private const decimal VatRate = 0.12m;
        private const int SalesToSeed = 60;

        public static async Task<int> SeedAsync(
            DbContext db,
            int employeeId)
        {
            var existingNumbers = await db.Set<Sales>()
                .Where(x => x.SaleNumber.StartsWith("SALE-"))
                .Select(x => x.SaleNumber)
                .ToHashSetAsync();

            var products = await db.Set<Product>()
                .Where(x => x.IsActive)
                .OrderBy(x => x.ProductId)
                .ToListAsync();

            if (!products.Any())
                throw new InvalidOperationException(
                    "No active products were found. Seed the products first.");

            var inventories = await db.Set<Inventory>()
                .ToListAsync();

            var inventoryLookup = inventories
                .ToDictionary(x => x.ProductId);

            var employee = await db.Set<EmployeeEntity>()
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.IsActive &&
                    x.ApprovalStatus != "Rejected");

            if (employee == null)
                throw new InvalidOperationException(
                    "The employee used for sales seeding does not exist or is inactive.");

            var random = new Random(20261006);

            var paymentMethods = new[]
            {
                "Cash",
                "Cash",
                "Cash",
                "GCash",
                "Card"
            };

            var movements = new List<StockMovement>();
            var sales = new List<Sales>();

            int inserted = 0;

            await using var transaction =
                await db.Database.BeginTransactionAsync();

            try
            {
                for (int saleIndex = 1; saleIndex <= SalesToSeed; saleIndex++)
                {
                    var saleDate = DateTime.UtcNow
                        .Date
                        .AddDays(-random.Next(0, 30))
                        .AddHours(random.Next(8, 21))
                        .AddMinutes(random.Next(0, 60))
                        .AddSeconds(random.Next(0, 60))
                        .AddMilliseconds(random.Next(0, 1000));

                    string saleNumber =
                        $"SALE-{saleDate:yyyyMMddHHmmssfff}";

                    if (existingNumbers.Contains(saleNumber))
                        continue;

                    var availableProducts = products
                        .Where(p =>
                            inventoryLookup.ContainsKey(p.ProductId) &&
                            inventoryLookup[p.ProductId].QuantityOnHand > 0)
                        .ToList();

                    if (!availableProducts.Any())
                        break;

                    int itemCount = Math.Min(
                        random.Next(1, 6),
                        availableProducts.Count);

                    var selectedProducts = availableProducts
                        .OrderBy(x => random.Next())
                        .Take(itemCount)
                        .ToList();

                    var paymentMethod =
                        paymentMethods[random.Next(paymentMethods.Length)];

                    var sale = new Sales
                    {
                        SaleNumber = saleNumber,
                        SaleDate = saleDate,
                        EmployeeId = employee.EmployeeId,
                        BranchId = employee.BranchId,
                        Discount = 0,
                        PaymentMethod = paymentMethod,
                        Status = "Completed"
                    };

                    decimal subtotal = 0;

                    foreach (var product in selectedProducts)
                    {
                        var inventory =
                            inventoryLookup[product.ProductId];

                        if (inventory.QuantityOnHand <= 0)
                            continue;

                        decimal maximumQuantity =
                            Math.Min(
                                5m,
                                inventory.QuantityOnHand);

                        decimal quantity =
                            random.Next(1, (int)Math.Floor(maximumQuantity) + 1);

                        if (quantity <= 0)
                            continue;

                        decimal itemDiscount = 0;

                        if (random.Next(1, 11) == 1)
                        {
                            itemDiscount = Math.Round(
                                product.UnitPrice * quantity * 0.05m,
                                2);
                        }

                        decimal itemTotal =
                            Math.Round(
                                (quantity * product.UnitPrice) -
                                itemDiscount,
                                2);

                        if (itemTotal < 0)
                            itemTotal = 0;

                        decimal taxRate = 0;
                        decimal taxAmount = 0;

                        if (product.IsVatable)
                        {
                            taxRate = VatRate;

                            taxAmount = Math.Round(
                                itemTotal /
                                (1 + VatRate) *
                                VatRate,
                                2);
                        }

                        decimal before =
                            inventory.QuantityOnHand;

                        inventory.QuantityOnHand -= quantity;
                        inventory.LastUpdatedAt =
                            DateTime.UtcNow;
                        inventory.UpdatedByEmployeeId =
                            employee.EmployeeId;

                        var item = new SaleItem
                        {
                            ProductId = product.ProductId,
                            Quantity = quantity,
                            UnitPrice = product.UnitPrice,
                            Discount = itemDiscount,
                            TotalAmount = itemTotal,
                            TaxRate = taxRate,
                            TaxAmount = taxAmount
                        };

                        sale.SaleItems.Add(item);

                        movements.Add(new StockMovement
                        {
                            ProductId = product.ProductId,
                            MovementType = "Sale",
                            Quantity = quantity,
                            QuantityBefore = before,
                            QuantityAfter =
                                inventory.QuantityOnHand,
                            EmployeeId =
                                employee.EmployeeId,
                            MovementDate = saleDate,
                            Remarks =
                                $"Sale {saleNumber}"
                        });

                        subtotal += itemTotal;
                    }

                    if (!sale.SaleItems.Any())
                        continue;

                    decimal saleDiscount = 0;

                    if (random.Next(1, 8) == 1)
                    {
                        saleDiscount = Math.Min(
                            Math.Round(subtotal * 0.03m, 2),
                            subtotal);
                    }

                    sale.Discount = saleDiscount;
                    sale.Subtotal = Math.Round(subtotal, 2);
                    sale.TotalAmount =
                        Math.Round(
                            sale.Subtotal - sale.Discount,
                            2);

                    sale.VatAmount =
                        Math.Round(
                            sale.SaleItems.Sum(x => x.TaxAmount),
                            2);

                    sale.VatableSales =
                        Math.Round(
                            sale.SaleItems
                                .Where(x => x.TaxRate > 0)
                                .Sum(x =>
                                    x.TotalAmount - x.TaxAmount),
                            2);

                    sale.VatExempt =
                        Math.Round(
                            sale.SaleItems
                                .Where(x => x.TaxRate == 0)
                                .Sum(x => x.TotalAmount),
                            2);

                    if (paymentMethod.Equals(
                            "Cash",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        decimal extraCash =
                            random.Next(0, 6) * 20m;

                        sale.CashReceived =
                            Math.Ceiling(
                                sale.TotalAmount / 20m) * 20m
                            + extraCash;

                        if (sale.CashReceived <
                            sale.TotalAmount)
                        {
                            sale.CashReceived =
                                sale.TotalAmount;
                        }

                        sale.ChangeAmount =
                            Math.Round(
                                sale.CashReceived -
                                sale.TotalAmount,
                                2);
                    }
                    else
                    {
                        sale.CashReceived =
                            sale.TotalAmount;

                        sale.ChangeAmount = 0;
                    }

                    sales.Add(sale);
                    inserted++;
                }

                db.Set<Sales>().AddRange(sales);
                db.Set<StockMovement>().AddRange(movements);

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return inserted;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

