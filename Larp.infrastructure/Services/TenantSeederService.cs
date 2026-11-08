using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Larp.infrastructure.Services;

public class TenantSeederService
{
    private readonly MasterErpDbContext _masterDb;
    private readonly ITenantDBResolver _tenantResolver;
    private readonly IConfiguration _configuration;

    public TenantSeederService(
        MasterErpDbContext masterDb,
        ITenantDBResolver tenantResolver,
        IConfiguration configuration)
    {
        _masterDb = masterDb;
        _tenantResolver = tenantResolver;
        _configuration = configuration;
    }


    private async Task<TenantErpDbContext> CreateTenantDbAsync(int companyId)
    {
        var company = await _masterDb.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.isActive);

        if (company == null)
        {
            throw new InvalidOperationException(
                $"Active company with ID {companyId} was not found.");
        }

        var databaseInfo =
            await _tenantResolver.GetDatabaseInfoAsync(companyId);

        var credential = _configuration
            .GetSection($"TenantCredentials:{databaseInfo.CredentialKey}")
            .Get<TenantDatabaseCredential>();

        if (credential == null)
        {
            throw new InvalidOperationException(
                $"No database credentials found for CredentialKey '{databaseInfo.CredentialKey}'.");
        }

        var connectionString =
            $"Server={databaseInfo.ServerName};" +
            $"Database={databaseInfo.DatabaseName};" +
            $"User Id={credential.UserId};" +
            $"Password={credential.Password};" +
            "TrustServerCertificate=True;";

        var options =
            new DbContextOptionsBuilder<TenantErpDbContext>()
                .UseSqlServer(
                    connectionString,
                    sqlOptions => sqlOptions.CommandTimeout(120))
                .Options;

        var tenantDb = new TenantErpDbContext(options);

        if (!await tenantDb.Database.CanConnectAsync())
        {
            await tenantDb.DisposeAsync();

            throw new InvalidOperationException(
                $"Cannot connect to tenant database '{databaseInfo.DatabaseName}'.");
        }
        return tenantDb;
    }

    public async Task<object> SeedProductsForTenantAsync(int companyId)
    {
        var company = await _masterDb.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.isActive);

        if (company == null)
        {
            throw new InvalidOperationException(
                $"Active company with ID {companyId} was not found.");
        }

        var databaseInfo =
            await _tenantResolver.GetDatabaseInfoAsync(companyId);

        var credential = _configuration
            .GetSection($"TenantCredentials:{databaseInfo.CredentialKey}")
            .Get<TenantDatabaseCredential>();

        if (credential == null)
        {
            throw new InvalidOperationException(
                $"No database credentials found for CredentialKey '{databaseInfo.CredentialKey}'.");
        }

        var connectionString =
            $"Server={databaseInfo.ServerName};" +
            $"Database={databaseInfo.DatabaseName};" +
            $"User Id={credential.UserId};" +
            $"Password={credential.Password};" +
            "TrustServerCertificate=True;";

        var options =
            new DbContextOptionsBuilder<TenantErpDbContext>()
                .UseSqlServer(
                    connectionString,
                    sqlOptions => sqlOptions.CommandTimeout(120))
                .Options;

        await using var tenantDb =
            new TenantErpDbContext(options);

        var canConnect = await tenantDb.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new InvalidOperationException(
                $"Cannot connect to tenant database '{databaseInfo.DatabaseName}'.");
        }

        await tenantDb.Database.MigrateAsync();

        var inserted = await ProductSeeder.SeedAsync(tenantDb);

        return new
        {
            CompanyId = company.CompanyId,
            CompanyName = company.CompanyName,
            DatabaseName = databaseInfo.DatabaseName,
            ProductsInserted = inserted,
            Success = true
        };
    }

public async Task<object> SeedPurchasesForTenantAsync(int companyId)
    {
        var company = await _masterDb.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.isActive);

        if (company == null)
        {
            throw new InvalidOperationException(
                $"Active company with ID {companyId} was not found.");
        }

        await using var db =
            await CreateTenantDbAsync(companyId);

        var employee = await db.Employees
            .Where(x =>
                x.IsActive &&
                x.ApprovalStatus != "Rejected")
            .OrderBy(x => x.EmployeeId)
            .FirstOrDefaultAsync();

        if (employee == null)
        {
            throw new InvalidOperationException(
                "No active employee is available to create the procurement.");
        }

        var suppliers = await db.Suppliers
            .Where(x =>
                x.IsActive &&
                x.ApprovalStatus == "Approved")
            .OrderBy(x => x.SupplierId)
            .ToListAsync();

        if (!suppliers.Any())
        {
            throw new InvalidOperationException(
                "No active approved suppliers are available.");
        }

        var products = await db.Products
            .Where(x => x.IsActive)
            .OrderBy(x => x.ProductId)
            .ToListAsync();

        if (!products.Any())
        {
            throw new InvalidOperationException(
                "No active products were found. Seed the products first.");
        }

        var random = new Random(20261006);

        const int purchasesToSeed = 60;

        int purchasesInserted = 0;
        int itemsInserted = 0;

        for (int i = 0; i < purchasesToSeed; i++)
        {
            var supplier =
                suppliers[random.Next(suppliers.Count)];

            int itemCount =
                Math.Min(
                    random.Next(2, 7),
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
                Status = "Completed",
                CreatedByEmployeeId = employee.EmployeeId,
                BranchId = employee.BranchId,
                CreatedAt = purchaseDate
            };

            decimal subtotal = 0;

            foreach (var product in selectedProducts)
            {
                decimal quantity =
                    random.Next(10, 51);

                decimal costPercentage =
                    random.Next(65, 86) / 100m;

                decimal unitCost =
                    Math.Max(
                        1m,
                        Math.Round(
                            product.UnitPrice *
                            costPercentage,
                            2));

                decimal itemTotal =
                    Math.Round(
                        quantity * unitCost,
                        2);

                var inventory =
                    await db.Inventory
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == product.ProductId);

                if (inventory == null)
                {
                    inventory = new Inventory
                    {
                        ProductId = product.ProductId,
                        QuantityOnHand = 0,
                        ReorderLevel = 5,
                        LastUpdatedAt = purchaseDate,
                        CreatedByEmployeeId =
                            employee.EmployeeId
                    };

                    db.Inventory.Add(inventory);
                }

                decimal quantityBefore =
                    inventory.QuantityOnHand;

                inventory.QuantityOnHand += quantity;

                inventory.LastUpdatedAt =
                    purchaseDate;

                inventory.UpdatedByEmployeeId =
                    employee.EmployeeId;

                var item = new PurchaseItem
                {
                    ProductId = product.ProductId,
                    Quantity = quantity,
                    UnitCost = unitCost,
                    TotalAmount = itemTotal
                };

                purchase.PurchaseItems.Add(item);

                db.StockMovements.Add(
                    new StockMovement
                    {
                        ProductId = product.ProductId,
                        MovementType = "Purchase",
                        Quantity = quantity,
                        QuantityBefore =
                            quantityBefore,
                        QuantityAfter =
                            inventory.QuantityOnHand,
                        EmployeeId =
                            employee.EmployeeId,
                        MovementDate =
                            purchaseDate,
                        Remarks =
                            $"Purchase {purchaseNumber}",
                        Purchase = purchase
                    });

                subtotal += itemTotal;
                itemsInserted++;
            }

            purchase.Subtotal =
                Math.Round(subtotal, 2);

            purchase.TotalAmount =
                Math.Round(
                    purchase.Subtotal -
                    purchase.Discount,
                    2);

            db.Purchases.Add(purchase);

            purchasesInserted++;
        }

        await db.SaveChangesAsync();

        return new
        {
            CompanyId = company.CompanyId,
            CompanyName = company.CompanyName,
            ProductsFound = products.Count,
            PurchasesInserted = purchasesInserted,
            PurchaseItemsInserted = itemsInserted,
            EmployeeUsed = employee.EmployeeId,
            SuppliersAvailable = suppliers.Count,
            Success = true
        };
    }



    public async Task<object> SeedSalesForTenantAsync(int companyId) 
    { 
        var company = await _masterDb.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.CompanyId == companyId && x.isActive); if (company == null) 
        { 
            throw new InvalidOperationException($"Active company with ID {companyId} was not found."); 
        } 
        await using var db = await CreateTenantDbAsync(companyId); 
        var employee = await db.Employees.Where(x => x.IsActive && x.ApprovalStatus != "Rejected").OrderBy(x => x.EmployeeId).FirstOrDefaultAsync(); 
        if (employee == null) 
        { 
            throw new InvalidOperationException("No active employee is available to create the seeded sales."); 
        } 
        var productsCount = await db.Products.CountAsync(x => x.IsActive); 
        if (productsCount == 0) 
        { 
            throw new InvalidOperationException("No active products were found. Seed the products first."); 
        } 
        var inventoryCount = await db.Inventory.CountAsync(); 
        if (inventoryCount == 0) 
        { 
            throw new InvalidOperationException("No inventory records were found. Seed purchases first."); 
        } 
        var inserted = await SalesSeeder.SeedAsync(db, employee.EmployeeId); 
        var seededSales = await db.Sales.Where(x => x.SaleNumber.StartsWith("SALE-")).CountAsync(); 
        var seededItems = await db.SaleItems.Where(x => x.Sale.SaleNumber.StartsWith("SALE-")).CountAsync(); 
        var stockMovements = await db.StockMovements.CountAsync(x => x.MovementType == "Sale" && x.Remarks != null && x.Remarks.StartsWith("Seeded sale SALE-")); 
        return new 
        { 
            CompanyId = company.CompanyId, 
            CompanyName = company.CompanyName, 
            ProductsAvailable = productsCount, 
            InventoryRecords = inventoryCount, 
            SalesInserted = inserted, 
            TotalSeededSales = seededSales, 
            TotalSeededSaleItems = seededItems, 
            TotalSeededStockMovements = stockMovements, 
            EmployeeUsed = employee.EmployeeId, 
            Success = true 
        }; 
    }
}

    public class TenantDatabaseCredential
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}