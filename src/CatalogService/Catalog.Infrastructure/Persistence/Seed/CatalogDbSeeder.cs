using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.Entities;
using Ecommerce.Catalog.Domain.Enums;
using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Seed;

public static class CatalogDbSeeder
{
    public static async Task SeedAsync(IServiceProvider services, ILoggerService logger)
    {
        logger.LogInformation("Seeder started");
        using var scope = services.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<CatalogDbContext>();
        logger.LogInformation("Migration started");
        await db.Database.MigrateAsync();
        logger.LogInformation("Migration Completed");

        if (await db.Categories.AnyAsync())
            return;

        // =========================
        // Categories
        // =========================

        var electronics = new Category(
            "Electronics",
            "Electronic products"
        );

        var mobilePhones = new Category(
            "Mobile Phones",
            "Smartphones and accessories",
            electronics.Id
        );

        var laptops = new Category(
            "Laptops",
            "Personal and professional laptops",
            electronics.Id
        );

        var televisions = new Category(
            "Televisions",
            "LED, OLED, and Smart TVs",
            electronics.Id
        );

        var headphones = new Category(
            "Headphones",
            "Wireless and wired headphones",
            electronics.Id
        );

        var gamingConsoles = new Category(
            "Gaming Consoles",
            "PlayStation, Xbox, and others",
            electronics.Id
        );

        var cameras = new Category(
            "Cameras",
            "DSLR, mirrorless, and compact cameras",
            electronics.Id
        );

        var accessories = new Category(
            "Accessories",
            "Chargers, cables, and other add-ons",
            electronics.Id
        );

        await db.Categories.AddRangeAsync(
            electronics,
            mobilePhones,
            laptops,
            televisions,
            headphones,
            gamingConsoles,
            cameras,
            accessories
        );
        await db.SaveChangesAsync();

        if (await db.Products.AnyAsync())
            return;

        // =========================
        // Products
        // =========================

        var iphone = new Product(
            "iPhone 14 Pro",
            "SKU-IPH14PRO",
            new Money(999.99m),
            50,
            "Apple flagship smartphone"
        );

        iphone.AddCategory(mobilePhones.Id);

        iphone.AddImage(new ProductImage(
            "uploads/products/E3F1DEB5-DDC0-40BF-9CF4-DFDB7AEE8D06.png",
            "iPhone 14 Pro Image",
            ".png"
        ));

        iphone.Draft();

        // =========================

        var samsung = new Product(
            "Samsung Galaxy S23",
            "SKU-SAMS23",
            new Money(899.99m),
            40,
            "High-end Android smartphone"
        );

        samsung.AddCategory(mobilePhones.Id);

        samsung.AddImage(new ProductImage(
            "uploads/products/D8D07A22-ED9E-47CE-9FA4-903EE4C5E899.png",
            "Samsung Galaxy S23 Image",
            ".png"
        ));

        samsung.Draft();

        // =========================

        var dell = new Product(
            "Dell XPS 13",
            "SKU-DELLXPS13",
            new Money(1199m),
            25,
            "Compact and powerful ultrabook"
        );

        dell.AddCategory(laptops.Id);

        dell.AddImage(new ProductImage(
            "uploads/products/9B571CEE-B8DA-471C-819F-1FE7401787BA.png",
            "Dell XPS 13 Image",
            ".png"
        ));

        dell.Draft();

        // =========================

        var ps5 = new Product(
            "PlayStation 5",
            "SKU-PS5",
            new Money(499.99m),
            20,
            "Next-gen gaming console"
        );

        ps5.AddCategory(gamingConsoles.Id);

        ps5.AddImage(new ProductImage(
            "uploads/products/F059E19A-8979-4D04-BFE8-74B48123606E.jpg",
            "PlayStation 5 Image",
            ".jpg"
        ));

        ps5.Draft();

        // =========================

        await db.Products.AddRangeAsync(
            iphone,
            samsung,
            dell,
            ps5
        );
        Console.WriteLine("Saving Products");
        await db.SaveChangesAsync();
        Console.WriteLine("Products Saved");
    }
}