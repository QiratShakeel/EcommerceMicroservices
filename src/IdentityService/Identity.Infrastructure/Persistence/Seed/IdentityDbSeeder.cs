using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Domain.Aggregates;
using Ecommerce.Identity.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Identity.Infrastructure.Persistence.Seed;

public static class IdentityDbSeeder
{
    public static async Task SeedAsync(
        IServiceProvider services,
        ILoggerService logger)
    {
        logger.LogInformation("Identity Seeder Started");

        using var scope = services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        logger.LogInformation("Identity Migration Started");

        await db.Database.MigrateAsync();

        logger.LogInformation("Identity Migration Completed");

        // =========================
        // Roles
        // =========================

        if (!await db.Roles.AnyAsync())
        {
            var adminRole = new Role("Admin");
            var customerRole = new Role("Customer");
            //var sellerRole = new Role("Seller");

            await db.Roles.AddRangeAsync(
                adminRole,
                customerRole
                //sellerRole
            );

            await db.SaveChangesAsync();

            logger.LogInformation("Roles Seeded");
        }

        // =========================
        // Admin User
        // =========================

        if (!await db.Users.AnyAsync())
        {
            var adminRole = await db.Roles
                .FirstAsync(r => r.Name == "Admin");
            
            var hashedPassword = passwordHasher.Hash("Admin@123");

            // Already hashed password
            var adminUser = new User(
                "System Admin",
                "admin@ecommerce.com",
                hashedPassword
            );

            adminUser.AssignRole(adminRole);

            await db.Users.AddAsync(adminUser);

            await db.SaveChangesAsync();

            logger.LogInformation("Admin User Seeded");
        }

        logger.LogInformation("Identity Seeder Completed");
    }
}