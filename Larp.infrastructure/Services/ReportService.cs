using Larp.domain.DTO;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class ReportService : IReportService
    {
        // Philippine time is UTC+8. Sales and purchases are stored in UTC.
        private static readonly TimeSpan PhOffset = TimeSpan.FromHours(8);

        private readonly ITenantDBContextFactory _tenantFactory;

        public ReportService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<SalesReportRequest> GetSalesReportAsync(
            int companyId,
            DateTime startDate,
            DateTime endDate)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            DateTime start = startDate.Date;
            DateTime end = endDate.Date.AddDays(1);

            var sales = await db.Sales
                .Where(s =>
                    s.SaleDate >= start &&
                    s.SaleDate < end)
                .ToListAsync();

            var saleIds = sales
                .Select(s => s.SalesId)
                .ToList();

            var saleItems = await db.SaleItems
                .Where(si => saleIds.Contains(si.SaleId))
                .Include(si => si.Product)
                .ToListAsync();

            var reportItems = saleItems
                .GroupBy(si => new
                {
                    si.ProductId,
                    ProductName = si.Product != null
                        ? si.Product.ProductName
                        : "Unknown Product",
                    si.UnitPrice
                })
                .Select(group => new SalesReportItemDto
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.Key.ProductName,
                    UnitPrice = group.Key.UnitPrice,
                    QuantitySold = group.Sum(x => x.Quantity),
                    TotalAmount = group.Sum(x =>
                        x.UnitPrice * x.Quantity)
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

            return new SalesReportRequest
            {
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                TotalTransactions = sales.Count,
                TotalSales = saleItems.Sum(x =>
                    x.UnitPrice * x.Quantity),
                TotalItemsSold = saleItems.Sum(x =>
                    x.Quantity),
                Items = reportItems
            };
        }

        public async Task<IncomeStatementDto> GetIncomeStatementAsync(
            int companyId,
            DateTime startDate,
            DateTime endDate)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            // The user picks whole days. endDayExclusive is midnight after the last day.
            DateTime startDay = startDate.Date;
            DateTime endDayExclusive = endDate.Date.AddDays(1);

            // Sales and purchases carry a UTC timestamp, so shift the day
            // boundaries from Philippine time to UTC.
            DateTime startUtc = startDay - PhOffset;
            DateTime endUtc = endDayExclusive - PhOffset;

            // Revenue: sales that were not cancelled, after discounts.
            decimal totalSales = await db.Sales
                .Where(s =>
                    s.SaleDate >= startUtc &&
                    s.SaleDate < endUtc &&
                    s.Status != "Cancelled")
                .SumAsync(s => (decimal?)s.TotalAmount) ?? 0m;

            // Cost: purchases that were not cancelled.
            decimal totalPurchases = await db.Purchases
                .Where(p =>
                    p.PurchaseDate >= startUtc &&
                    p.PurchaseDate < endUtc &&
                    p.Status != "Cancelled")
                .SumAsync(p => (decimal?)p.TotalAmount) ?? 0m;

            // Expenses hold a plain date, so no time zone shift.
            var expenseRows = await db.Expenses
                .Where(e =>
                    e.ExpenseDate >= startDay &&
                    e.ExpenseDate < endDayExclusive &&
                    e.Status != "Cancelled" &&
                    e.Status != "Voided")
                .Select(e => new
                {
                    Category = e.ExpenseCategory != null
                        ? e.ExpenseCategory.CategoryName
                        : "Uncategorized",
                    e.Amount
                })
                .ToListAsync();

            var expensesByCategory = expenseRows
                .GroupBy(x => x.Category)
                .Select(g => new { Category = g.Key, Amount = g.Sum(x => x.Amount) })
                .OrderByDescending(x => x.Amount)
                .ToList();

            decimal totalExpenses = expensesByCategory.Sum(x => x.Amount);

            // Payroll counts in the period where the pay period ends.
            // Cost to the business = basic salary + allowances.
            // Pending, cancelled and rejected payroll is left out.
            decimal totalPayroll = await db.Payrolls
                .Where(p =>
                    p.PayPeriodEnd >= startDay &&
                    p.PayPeriodEnd < endDayExclusive &&
                    p.Status != "Pending" &&
                    p.Status != "Cancelled" &&
                    p.Status != "Rejected")
                .SumAsync(p => (decimal?)(p.BasicSalary + p.Allowances)) ?? 0m;

            decimal salesLessPurchases = totalSales - totalPurchases;
            decimal totalOperating = totalExpenses + totalPayroll;
            decimal netIncome = salesLessPurchases - totalOperating;

            var lines = new List<IncomeStatementLineDto>
            {
                new() { Label = "REVENUE", Kind = "Header" },
                new() { Label = "Sales (after discounts)", Amount = totalSales },

                new() { Label = "COST", Kind = "Header" },
                new() { Label = "Purchases (stock bought in this period)", Amount = totalPurchases },
                new() { Label = "Sales less purchases", Amount = salesLessPurchases, Kind = "Subtotal" },

                new() { Label = "OPERATING EXPENSES", Kind = "Header" }
            };

            foreach (var category in expensesByCategory)
                lines.Add(new() { Label = category.Category, Amount = category.Amount });

            lines.Add(new() { Label = "Payroll (basic salary + allowances)", Amount = totalPayroll });
            lines.Add(new() { Label = "Total operating expenses", Amount = totalOperating, Kind = "Subtotal" });
            lines.Add(new() { Label = "NET INCOME", Amount = netIncome, Kind = "Total" });

            return new IncomeStatementDto
            {
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                TotalSales = totalSales,
                TotalPurchases = totalPurchases,
                TotalExpenses = totalExpenses,
                TotalPayroll = totalPayroll,
                NetIncome = netIncome,
                Lines = lines
            };
        }
    }
}