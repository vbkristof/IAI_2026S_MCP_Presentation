using McpServer.Models;
using Microsoft.EntityFrameworkCore;

namespace McpServer.Data;

public static class DatabaseInitializer
{
    public static async Task MigrateAndSeedAsync(IServiceProvider services)
    {
        await using AsyncServiceScope scope = services.CreateAsyncScope();
        DataContext db = scope.ServiceProvider.GetRequiredService<DataContext>();

        await db.Database.MigrateAsync();

        if (await db.InventoryItems.AnyAsync())
        {
            return;
        }

        db.InventoryItems.AddRange(
            new InventoryItem
            {
                Name = "Tomato Sauce 5kg",
                Sku = "TOM-5KG",
                CurrentStock = 8,
                MinimumStock = 20,
                TargetStock = 50,
                SupplierName = "Gastro Supplier Kft."
            },
            new InventoryItem
            {
                Name = "Pasta Penne 1kg",
                Sku = "PEN-1KG",
                CurrentStock = 120,
                MinimumStock = 50,
                TargetStock = 150,
                SupplierName = "Pasta Supplier Kft."
            },
            new InventoryItem
            {
                Name = "Mozzarella 2kg",
                Sku = "MOZ-2KG",
                CurrentStock = 12,
                MinimumStock = 25,
                TargetStock = 60,
                SupplierName = "Dairy Supplier Kft."
            },
            new InventoryItem
            {
                Name = "Olive Oil 5L",
                Sku = "OIL-5L",
                CurrentStock = 5,
                MinimumStock = 10,
                TargetStock = 30,
                SupplierName = "Mediterranean Foods Kft."
            }
        );

        await db.SaveChangesAsync();
    }
}
