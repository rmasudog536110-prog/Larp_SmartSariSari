using Larp.domain.DTO;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// One query layer for every branch figure. The rules copy
    /// ReportService.GetIncomeStatementAsync so that all branches plus
    /// Unassigned add up to the company income statement.
    /// A branchId of null means "Unassigned" (records with no branch).
    /// </summary>
    public class BranchReportService
    {
        // Sales and purchases are stored in UTC; the user picks Philippine days.
        private static readonly TimeSpan PhOffset = TimeSpan.FromHours(8);

        private readonly ITenantDBContextFactory _tenantFactory;

        public BranchReportService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        private readonly record struct Period(
            DateTime StartDay,
            DateTime EndDayExclusive,
            DateTime StartUtc,
            DateTime EndUtc);

        private static Period MakePeriod(DateTime start, DateTime end)
        {
            var sd = start.Date;
            var ed = end.Date.AddDays(1);
            return new Period(sd, ed, sd - PhOffset, ed - PhOffset);
        }

        private class Totals
        {
            public int Count;
            public decimal Gross, Vat, Items, Purchases, Expenses, Payroll;
        }

        // Key 0 = Unassigned.
        private static async Task<Dictionary<int, Totals>> LoadTotalsAsync(
            TenantErpDbContext db, Period p, bool filter, int? bid)
        {
            var result = new Dictionary<int, Totals>();

            Totals Of(int? id)
            {
                int key = id ?? 0;
                if (!result.TryGetValue(key, out var t))
                    result[key] = t = new Totals();
                return t;
            }

            DateTime su = p.StartUtc, eu = p.EndUtc;
            DateTime sd = p.StartDay, ed = p.EndDayExclusive;

            var sales = await db.Sales.AsNoTracking()
                .Where(s => s.SaleDate >= su && s.SaleDate < eu &&
                            s.Status != "Cancelled" &&
                            (!filter || s.BranchId == bid))
                .GroupBy(s => s.BranchId)
                .Select(g => new
                {
                    BranchId = g.Key,
                    Count = g.Count(),
                    Gross = g.Sum(x => x.TotalAmount),
                    Vat = g.Sum(x => x.VatAmount)
                })
                .ToListAsync();

            foreach (var r in sales)
            {
                var t = Of(r.BranchId);
                t.Count = r.Count;
                t.Gross = r.Gross;
                t.Vat = r.Vat;
            }

            var items = await db.SaleItems.AsNoTracking()
                .Where(i => i.Sale!.SaleDate >= su && i.Sale.SaleDate < eu &&
                            i.Sale.Status != "Cancelled" &&
                            (!filter || i.Sale.BranchId == bid))
                .GroupBy(i => i.Sale!.BranchId)
                .Select(g => new { BranchId = g.Key, Qty = g.Sum(x => x.Quantity) })
                .ToListAsync();

            foreach (var r in items)
                Of(r.BranchId).Items = r.Qty;

            var purchases = await db.Purchases.AsNoTracking()
                .Where(x => x.PurchaseDate >= su && x.PurchaseDate < eu &&
                            x.Status != "Cancelled" &&
                            (!filter || x.BranchId == bid))
                .GroupBy(x => x.BranchId)
                .Select(g => new { BranchId = g.Key, Amount = g.Sum(x => x.TotalAmount) })
                .ToListAsync();

            foreach (var r in purchases)
                Of(r.BranchId).Purchases = r.Amount;

            // Expenses hold a plain date: no time zone shift.
            var expenses = await db.Expenses.AsNoTracking()
                .Where(x => x.ExpenseDate >= sd && x.ExpenseDate < ed &&
                            x.Status != "Cancelled" &&
                            x.Status != "Voided" &&
                            (!filter || x.BranchId == bid))
                .GroupBy(x => x.BranchId)
                .Select(g => new { BranchId = g.Key, Amount = g.Sum(x => x.Amount) })
                .ToListAsync();

            foreach (var r in expenses)
                Of(r.BranchId).Expenses = r.Amount;

            // Payroll counts in the period where the pay period ends.
            // Cost = basic salary + allowances. Pending, cancelled and rejected are left out.
            var payroll = await db.Payrolls.AsNoTracking()
                .Where(x => x.PayPeriodEnd >= sd && x.PayPeriodEnd < ed &&
                            x.Status != "Pending" &&
                            x.Status != "Cancelled" &&
                            x.Status != "Rejected" &&
                            (!filter || x.BranchId == bid))
                .GroupBy(x => x.BranchId)
                .Select(g => new { BranchId = g.Key, Amount = g.Sum(x => x.BasicSalary + x.Allowances) })
                .ToListAsync();

            foreach (var r in payroll)
                Of(r.BranchId).Payroll = r.Amount;

            return result;
        }

        // ---------------- Summary ----------------

        /// <summary>Returns null if a numeric branch does not exist.</summary>
        public async Task<BranchSummaryDto?> GetSummaryAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            string code = "-", name = "Unassigned";
            string? address = null, contact = null;
            bool active = true;

            if (branchId.HasValue)
            {
                var b = await db.Branches.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.BranchId == branchId.Value);
                if (b == null) return null;

                code = b.BranchCode;
                name = b.BranchName;
                address = b.Address;
                contact = b.ContactNumber;
                active = b.IsActive;
            }

            var p = MakePeriod(startDate, endDate);
            var totals = await LoadTotalsAsync(db, p, true, branchId);
            totals.TryGetValue(branchId ?? 0, out var t);
            t ??= new Totals();

            int employees = await db.Employees
                .CountAsync(x => x.IsActive && x.BranchId == branchId);

            decimal costs = t.Purchases + t.Expenses + t.Payroll;

            return new BranchSummaryDto
            {
                BranchId = branchId,
                BranchCode = code,
                BranchName = name,
                Address = address,
                ContactNumber = contact,
                IsActive = active,
                EmployeeCount = employees,
                GrossSales = t.Gross,
                VatAmount = t.Vat,
                NetSales = t.Gross - t.Vat,
                Transactions = t.Count,
                ItemsSold = t.Items,
                AverageSale = t.Count > 0 ? Math.Round(t.Gross / t.Count, 2) : 0m,
                Purchases = t.Purchases,
                Expenses = t.Expenses,
                Payroll = t.Payroll,
                TotalCosts = costs,
                Profit = t.Gross - costs
            };
        }

        // ---------------- Compare ----------------

        public async Task<BranchCompareDto> GetCompareAsync(
            int companyId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var p = MakePeriod(startDate, endDate);
            var branches = await db.Branches.AsNoTracking()
                .OrderBy(b => b.BranchName)
                .ToListAsync();
            var totals = await LoadTotalsAsync(db, p, false, null);

            var rows = new List<BranchCompareRowDto>();

            foreach (var b in branches)
            {
                totals.TryGetValue(b.BranchId, out var t);

                // Hide inactive branches that did nothing in this range.
                if (!b.IsActive && t == null) continue;

                rows.Add(ToRow(b.BranchId, b.BranchCode, b.BranchName, b.IsActive, t ?? new Totals()));
            }

            totals.TryGetValue(0, out var unassigned);
            rows.Add(ToRow(null, "-", "Unassigned", true, unassigned ?? new Totals()));

            var total = new BranchCompareRowDto
            {
                BranchCode = string.Empty,
                BranchName = "TOTAL",
                IsActive = true,
                Transactions = rows.Sum(r => r.Transactions),
                GrossSales = rows.Sum(r => r.GrossSales),
                VatAmount = rows.Sum(r => r.VatAmount),
                NetSales = rows.Sum(r => r.NetSales),
                Purchases = rows.Sum(r => r.Purchases),
                Expenses = rows.Sum(r => r.Expenses),
                Payroll = rows.Sum(r => r.Payroll),
                TotalCosts = rows.Sum(r => r.TotalCosts),
                Profit = rows.Sum(r => r.Profit)
            };

            foreach (var r in rows)
            {
                r.SharePercent = total.GrossSales > 0
                    ? Math.Round(r.GrossSales / total.GrossSales * 100m, 1)
                    : 0m;
            }

            total.SharePercent = total.GrossSales > 0 ? 100m : 0m;

            return new BranchCompareDto { Rows = rows, Total = total };
        }

        private static BranchCompareRowDto ToRow(
            int? id, string code, string name, bool active, Totals t)
        {
            decimal costs = t.Purchases + t.Expenses + t.Payroll;

            return new BranchCompareRowDto
            {
                BranchId = id,
                BranchCode = code,
                BranchName = name,
                IsActive = active,
                Transactions = t.Count,
                GrossSales = t.Gross,
                VatAmount = t.Vat,
                NetSales = t.Gross - t.Vat,
                Purchases = t.Purchases,
                Expenses = t.Expenses,
                Payroll = t.Payroll,
                TotalCosts = costs,
                Profit = t.Gross - costs
            };
        }

        // ---------------- Tables ----------------

        public async Task<List<BranchSaleRowDto>> GetSalesAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime su = p.StartUtc, eu = p.EndUtc;

            var rows = await db.Sales.AsNoTracking()
                .Where(x => x.BranchId == branchId && x.SaleDate >= su && x.SaleDate < eu)
                .OrderByDescending(x => x.SaleDate)
                .Select(x => new
                {
                    x.SalesId,
                    x.SaleNumber,
                    x.SaleDate,
                    Cashier = x.Employee != null
                        ? x.Employee.FirstName + " " + x.Employee.LastName
                        : "-",
                    Qty = x.SaleItems.Sum(i => i.Quantity),
                    x.Subtotal,
                    x.Discount,
                    x.VatAmount,
                    x.TotalAmount,
                    x.Status
                })
                .ToListAsync();

            return rows.Select(x => new BranchSaleRowDto
            {
                SalesId = x.SalesId,
                SaleNumber = x.SaleNumber,
                SaleDate = x.SaleDate + PhOffset,
                Cashier = x.Cashier,
                ItemCount = x.Qty,
                Subtotal = x.Subtotal,
                Discount = x.Discount,
                VatAmount = x.VatAmount,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                Counted = x.Status != "Cancelled"
            }).ToList();
        }

        public async Task<List<BranchPurchaseRowDto>> GetPurchasesAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime su = p.StartUtc, eu = p.EndUtc;

            var rows = await db.Purchases.AsNoTracking()
                .Where(x => x.BranchId == branchId && x.PurchaseDate >= su && x.PurchaseDate < eu)
                .OrderByDescending(x => x.PurchaseDate)
                .Select(x => new
                {
                    x.PurchaseId,
                    x.PurchaseNumber,
                    x.PurchaseDate,
                    Supplier = x.Supplier != null ? x.Supplier.SupplierName : "-",
                    x.Discount,
                    x.TotalAmount,
                    x.Status
                })
                .ToListAsync();

            return rows.Select(x => new BranchPurchaseRowDto
            {
                PurchaseId = x.PurchaseId,
                PurchaseNumber = x.PurchaseNumber,
                PurchaseDate = x.PurchaseDate + PhOffset,
                Supplier = x.Supplier,
                Discount = x.Discount,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                Counted = x.Status != "Cancelled"
            }).ToList();
        }

        public async Task<List<BranchExpenseRowDto>> GetExpensesAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime sd = p.StartDay, ed = p.EndDayExclusive;

            var rows = await db.Expenses.AsNoTracking()
                .Where(x => x.BranchId == branchId && x.ExpenseDate >= sd && x.ExpenseDate < ed)
                .OrderByDescending(x => x.ExpenseDate)
                .ThenByDescending(x => x.ExpenseId)
                .Select(x => new
                {
                    x.ExpenseId,
                    x.ExpenseDate,
                    Category = x.ExpenseCategory != null
                        ? x.ExpenseCategory.CategoryName
                        : "Uncategorized",
                    x.Description,
                    x.Amount,
                    x.PaymentMethod,
                    x.Status
                })
                .ToListAsync();

            return rows.Select(x => new BranchExpenseRowDto
            {
                ExpenseId = x.ExpenseId,
                ExpenseDate = x.ExpenseDate,
                Category = x.Category,
                Description = x.Description,
                Amount = x.Amount,
                PaymentMethod = x.PaymentMethod,
                Status = x.Status,
                Counted = x.Status != "Cancelled" && x.Status != "Voided"
            }).ToList();
        }

        public async Task<List<BranchEmployeeRowDto>> GetEmployeesAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime su = p.StartUtc, eu = p.EndUtc;

            var employees = await db.Employees.AsNoTracking()
                .Where(x => x.BranchId == branchId)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();

            var sales = await db.Sales.AsNoTracking()
                .Where(s => s.BranchId == branchId &&
                            s.EmployeeId != null &&
                            s.SaleDate >= su && s.SaleDate < eu &&
                            s.Status != "Cancelled")
                .GroupBy(s => s.EmployeeId)
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    Count = g.Count(),
                    Total = g.Sum(x => x.TotalAmount)
                })
                .ToListAsync();

            return employees.Select(e =>
            {
                var s = sales.FirstOrDefault(x => x.EmployeeId == e.EmployeeId);

                return new BranchEmployeeRowDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeCode = e.EmployeeCode,
                    Name = e.FirstName + " " + e.LastName,
                    Position = e.Position,
                    Status = e.EmploymentStatus,
                    IsActive = e.IsActive,
                    SalesCount = s?.Count ?? 0,
                    SalesTotal = s?.Total ?? 0m
                };
            }).ToList();
        }

        public async Task<List<BranchPayrollRowDto>> GetPayrollAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime sd = p.StartDay, ed = p.EndDayExclusive;

            var rows = await db.Payrolls.AsNoTracking()
                .Where(x => x.BranchId == branchId && x.PayPeriodEnd >= sd && x.PayPeriodEnd < ed)
                .OrderByDescending(x => x.PayPeriodEnd)
                .ThenBy(x => x.Employee!.LastName)
                .Select(x => new
                {
                    x.PayrollId,
                    Employee = x.Employee != null
                        ? x.Employee.FirstName + " " + x.Employee.LastName
                        : "-",
                    x.PayPeriodStart,
                    x.PayPeriodEnd,
                    x.BasicSalary,
                    x.Allowances,
                    x.Deductions,
                    x.NetPay,
                    x.Status
                })
                .ToListAsync();

            return rows.Select(x => new BranchPayrollRowDto
            {
                PayrollId = x.PayrollId,
                Employee = x.Employee,
                PeriodStart = x.PayPeriodStart,
                PeriodEnd = x.PayPeriodEnd,
                Gross = x.BasicSalary + x.Allowances,
                Deductions = x.Deductions,
                NetPay = x.NetPay,
                Status = x.Status,
                Counted = x.Status != "Pending" && x.Status != "Cancelled" && x.Status != "Rejected"
            }).ToList();
        }

        // ---------------- Charts ----------------

        public async Task<List<BranchTopProductDto>> GetTopProductsAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate, int take = 10)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime su = p.StartUtc, eu = p.EndUtc;

            var baseQuery = db.SaleItems.AsNoTracking()
                .Where(i => i.Sale!.BranchId == branchId &&
                            i.Sale.SaleDate >= su && i.Sale.SaleDate < eu &&
                            i.Sale.Status != "Cancelled");

            decimal all = await baseQuery.SumAsync(i => (decimal?)i.TotalAmount) ?? 0m;

            var rows = await baseQuery
                .GroupBy(i => new { i.ProductId, i.Product!.ProductName })
                .Select(g => new
                {
                    g.Key.ProductId,
                    g.Key.ProductName,
                    Qty = g.Sum(x => x.Quantity),
                    Amount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.Amount)
                .Take(take)
                .ToListAsync();

            return rows.Select(x => new BranchTopProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Quantity = x.Qty,
                Amount = x.Amount,
                SharePercent = all > 0 ? Math.Round(x.Amount / all * 100m, 1) : 0m
            }).ToList();
        }

        /// <summary>Sales per day, or per week (Monday start) when the range is over 60 days.</summary>
        public async Task<List<BranchTrendPointDto>> GetTrendAsync(
            int companyId, int? branchId, DateTime startDate, DateTime endDate)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);
            var p = MakePeriod(startDate, endDate);
            DateTime su = p.StartUtc, eu = p.EndUtc;

            var raw = await db.Sales.AsNoTracking()
                .Where(s => s.BranchId == branchId &&
                            s.SaleDate >= su && s.SaleDate < eu &&
                            s.Status != "Cancelled")
                .Select(s => new { s.SaleDate, s.TotalAmount })
                .ToListAsync();

            bool weekly = (p.EndDayExclusive - p.StartDay).TotalDays > 60;

            static DateTime Bucket(DateTime d, bool weekly) =>
                weekly ? d.AddDays(-(((int)d.DayOfWeek + 6) % 7)) : d;

            var grouped = raw
                .GroupBy(x => Bucket((x.SaleDate + PhOffset).Date, weekly))
                .ToDictionary(g => g.Key, g => new { Count = g.Count(), Amount = g.Sum(x => x.TotalAmount) });

            var points = new List<BranchTrendPointDto>();
            int step = weekly ? 7 : 1;

            for (var d = Bucket(p.StartDay, weekly); d < p.EndDayExclusive; d = d.AddDays(step))
            {
                grouped.TryGetValue(d, out var g);

                points.Add(new BranchTrendPointDto
                {
                    Date = d,
                    Label = d.ToString("MMM dd"),
                    Amount = g?.Amount ?? 0m,
                    Count = g?.Count ?? 0
                });
            }

            return points;
        }
    }
}