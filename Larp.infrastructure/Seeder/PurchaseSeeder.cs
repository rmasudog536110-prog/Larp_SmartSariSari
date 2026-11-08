using System;
using System.Linq;
using System.Threading.Tasks;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Larp.infrastructure.Seeders
{
    public class PurchaseSeeder
    {
        private readonly PurchaseService _purchaseService;
        private readonly TenantDBResolver _tenantResolver;
        private readonly IConfiguration _configuration;

        private const int PurchasesToSeed = 60;

        public PurchaseSeeder(
            PurchaseService purchaseService,
            TenantDBResolver tenantResolver,
            IConfiguration configuration)
        {
            _purchaseService = purchaseService;
            _tenantResolver = tenantResolver;
            _configuration = configuration;
        }

        public async Task<int> SeedAsync(
            int companyId,
            int employeeId)
        {
            await using var db = await CreateDb(companyId);

            var products = await db.Products
                .Where(x => x.IsActive)
                .OrderBy(x => x.ProductId)
                .ToListAsync();

            if (!products.Any())
                throw new InvalidOperationException(
                    "No active products exist.");

            var suppliers = await db.Suppliers
                .Where(x =>
                    x.IsActive &&
                    x.ApprovalStatus == "Approved")
                .OrderBy(x => x.SupplierId)
                .ToListAsync();

            if (!suppliers.Any())
                throw new InvalidOperationException(
                    "No active approved suppliers exist.");

            var employee = await db.Employees
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.IsActive &&
                    x.ApprovalStatus != "Rejected");

            if (employee == null)
                throw new InvalidOperationException(
                    "The employee does not exist or is inactive.");

            var random = new Random(20261006);

            int inserted = 0;

            for (int i = 0; i < PurchasesToSeed; i++)
            {
                var supplier =
                    suppliers[random.Next(suppliers.Count)];

                int itemCount =
                    Math.Min(
                        random.Next(1, 7),
                        products.Count);

                var selectedProducts = products
                    .OrderBy(x => random.Next())
                    .Take(itemCount)
                    .ToList();

                var purchaseDate = DateTime.UtcNow
                    .AddDays(-random.Next(0, 60))
                    .AddHours(-random.Next(0, 12))
                    .AddMinutes(-random.Next(0, 60))
                    .AddSeconds(-random.Next(0, 60))
                    .AddMilliseconds(random.Next(0, 1000));

                var purchaseNumber =
                    $"PUR-{purchaseDate:yyyyMMddHHmmssfff}";

                while (await db.Purchases.AnyAsync(
                    x => x.PurchaseNumber == purchaseNumber))
                {
                    purchaseDate =
                        purchaseDate.AddMilliseconds(1);

                    purchaseNumber =
                        $"PUR-{purchaseDate:yyyyMMddHHmmssfff}";
                }

                var purchase = new Purchase
                {
                    PurchaseNumber = purchaseNumber,
                    SupplierId = supplier.SupplierId,
                    PurchaseDate = purchaseDate,
                    Discount = 0,
                    Status = "Received",
                    CreatedByEmployeeId = employee.EmployeeId,
                    BranchId = employee.BranchId
                };

                foreach (var product in selectedProducts)
                {
                    decimal quantity =
                        random.Next(5, 51);

                    decimal costMultiplier =
                        random.Next(65, 86) / 100m;

                    decimal unitCost =
                        Math.Max(
                            1m,
                            Math.Round(
                                product.UnitPrice *
                                costMultiplier,
                                2));

                    purchase.PurchaseItems.Add(
                        new PurchaseItem
                        {
                            ProductId = product.ProductId,
                            Quantity = quantity,
                            UnitCost = unitCost
                        });
                }

                await _purchaseService.CreateAsync(
                    companyId,
                    purchase,
                    employee.EmployeeId);

                inserted++;
            }

            return inserted;
        }

        private async Task<TenantErpDbContext> CreateDb(
            int companyId)
        {
            var dbInfo =
                await _tenantResolver
                    .GetDatabaseInfoAsync(companyId);

            if (dbInfo == null)
                throw new InvalidOperationException(
                    $"Tenant database for company {companyId} was not found.");

            var credentials =
                _configuration
                    .GetSection("TenantCredentials")
                    .Get<TenantDatabaseCredential>();

            if (credentials == null)
                throw new InvalidOperationException(
                    "Tenant database credentials are not configured.");

            var connectionString =
                $"Server={dbInfo.ServerName};" +
                $"Database={dbInfo.DatabaseName};" +
                $"User Id={credentials.UserId};" +
                $"Password={credentials.Password};" +
                $"TrustServerCertificate=True;";

            var options =
                new DbContextOptionsBuilder<TenantErpDbContext>()
                    .UseSqlServer(
                        connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.CommandTimeout(120);
                        })
                    .Options;

            return new TenantErpDbContext(options);
        }
    }
}