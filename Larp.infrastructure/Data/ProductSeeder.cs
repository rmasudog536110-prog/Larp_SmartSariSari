using Larp.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Data;

public static class ProductSeeder
{
    private static readonly ProductSeed[] Catalog =
    {
        new("PRD-005", "Pepsi Cola 330ml Can", 39.00m),
        new("PRD-006", "Mountain Dew 330ml Can", 39.00m),
        new("PRD-007", "Sprite 330ml Can", 39.00m),
        new("PRD-008", "Royal Tru-Orange 330ml Can", 39.00m),
        new("PRD-009", "C2 Apple Green Tea 355ml", 32.00m),
        new("PRD-010", "Nestea Iced Tea Lemon 330ml", 35.00m),
        new("PRD-011", "Gatorade Blue Bolt 500ml", 52.00m),
        new("PRD-012", "Pocari Sweat 500ml", 48.00m),
        new("PRD-013", "Red Bull Energy Drink 250ml",  95.00m),
        new("PRD-014", "Monster Energy Drink 355ml", 110.00m),
        new("PRD-015", "San Miguel Pale Pilsen 330ml",  65.00m),
        new("PRD-016", "Red Horse Beer 330ml",  75.00m),
        new("PRD-017", "Jack Daniel's Cola 330ml", 135.00m),
        new("PRD-018", "Lucky Me! Pancit Canton Original 60g", 18.00m),
        new("PRD-019", "Lucky Me! Pancit Canton Chili-Mansi 60g", 18.00m),
        new("PRD-020", "Nissin Cup Noodles Beef 40g",  35.00m),
        new("PRD-021", "Payless Instant Mami Chicken 55g", 15.00m),
        new("PRD-022", "Piattos Cheese 85g", 49.00m),
        new("PRD-023", "Nova Country Cheddar 78g", 49.00m),
        new("PRD-024", "Clover Chips Cheese 85g", 45.00m),
        new("PRD-025", "Chippy Barbecue 110g", 39.00m),
        new("PRD-026", "Oishi Prawn Crackers 90g", 42.00m),
        new("PRD-027", "Boy Bawang Garlic 100g",  45.00m),
        new("PRD-028", "Choc-Nut Chocolate Peanut Bar 24g", 12.00m),
        new("PRD-029", "Jack 'n Jill Cloud 9 Classic 28g", 25.00m),
        new("PRD-030", "KitKat 4 Finger Chocolate 41.5g",  55.00m),
        new("PRD-031", "Snickers Chocolate Bar 50g",  60.00m),
        new("PRD-032", "Oreo Original Cookies 133g",  70.00m),
        new("PRD-033", "Fita Crackers 300g",  75.00m),
        new("PRD-034", "SkyFlakes Crackers 250g",  70.00m),
        new("PRD-035", "Rebisco Crackers 250g",  65.00m),
        new("PRD-036", "Gardenia Classic White Bread 600g", 95.00m),
        new("PRD-037", "Purefoods Classic Ham 250g", 185.00m),
        new("PRD-038", "CDO Vienna Sausage 10oz",  90.00m),
        new("PRD-039", "Argentina Corned Beef 175g",  85.00m),
        new("PRD-040", "Century Tuna Flakes in Oil 180g",  80.00m),
        new("PRD-041", "555 Sardines Tomato Sauce 155g",  35.00m),
        new("PRD-042", "Datu Puti Soy Sauce 385ml", 42.00m),
        new("PRD-043", "Datu Puti Vinegar 385ml", 38.00m),
        new("PRD-044", "UFC Banana Catsup 320g",  45.00m),
        new("PRD-045", "Lady's Choice Mayonnaise 220ml",  95.00m),
        new("PRD-046", "Bear Brand Fortified Powdered Milk 33g", 25.00m),
        new("PRD-047", "Milo Chocolate Drink Powder 24g", 20.00m),
        new("PRD-048", "Nescafe Classic Coffee Stick 25g", 15.00m),
        new("PRD-049", "Great Taste White Coffee Stick", 15.00m),
        new("PRD-050", "Energen Chocolate Cereal Drink 40g", 22.00m),
        new("PRD-051", "Safeguard Classic Bar Soap 60g",  40.00m),
        new("PRD-052", "Silka Papaya Bar Soap 65g",  45.00m),
        new("PRD-053", "Colgate Maximum Cavity Protection 50g",  75.00m),
        new("PRD-054", "Closeup Menthol Fresh Toothpaste 50g", 65.00m),
        new("PRD-055", "Head & Shoulders Shampoo Sachet", 12.00m),
        new("PRD-056", "Cream Silk Conditioner Sachet", 12.00m),
        new("PRD-057", "Kleen-Pak Facial Tissue 2-Ply",  60.00m),
        new("PRD-058", "Tide Powder Detergent Sachet 55g",  15.00m),
        new("PRD-059", "Joy Dishwashing Liquid Sachet",  12.00m),
        new("PRD-060", "Ever Bilena Facial Tissue Pack",  30.00m),

    };

    public static async Task<int> SeedAsync(TenantErpDbContext db)
    {
        var existingCodes = await db.Products
            .Where(product => product.ProductCode != null)
            .Select(product => product.ProductCode)
            .ToHashSetAsync();

        var productsToAdd = Catalog
            .Where(seed => !existingCodes.Contains(seed.Code))
            .Select(seed => new Product
            {
                ProductCode = seed.Code,
                ProductName = seed.Name,
                UnitPrice = seed.Price,
                IsActive = true,
                IsVatable = true,
                CreatedAt = DateTime.UtcNow
            })
            .ToList();

        if (productsToAdd.Count == 0)
            return 0;

        await db.Products.AddRangeAsync(productsToAdd);
        await db.SaveChangesAsync();
        return productsToAdd.Count;
    }

    private sealed record ProductSeed(string Code, string Name, decimal Price);
}
