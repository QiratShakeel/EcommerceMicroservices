using Microsoft.EntityFrameworkCore;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;

namespace Ecommerce.Catalog.Infrastructure.Tests.Persistence.Context
{
    public class CatalogDbContextFixture : IDisposable
    {
        public CatalogDbContext DbContext { get; private set; }

        public CatalogDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: $"CatalogDb_{Guid.NewGuid()}")
                .Options;

            DbContext = new CatalogDbContext(options);

            // Seed initial data if needed
            SeedData().GetAwaiter().GetResult();
        }

        private async Task SeedData()
        {
            DbContext.Products.Add(new Ecommerce.Catalog.Domain.Aggregates.Product
            (
                name: "Test Product",
                sku: "sku-001",
                price: new BuildingBlocks.Shared.Infrastructure.Money(10)
            ));

            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}