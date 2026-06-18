using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Ecommerce.Catalog.Infrastructure.Persistence.Repositories;
using Ecommerce.Catalog.Infrastructure.Tests.Persistence.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
namespace Ecommerce.Catalog.Infrastructure.Tests.Persistence.Repositories
{
    public class ProductRepositoryTests: IClassFixture<CatalogDbContextFixture>
    {
        private readonly IProductRepository _repo;
        private readonly CatalogDbContext _db;
        public ProductRepositoryTests(CatalogDbContextFixture db)
        {
            _db = db.DbContext;
            _repo = new ProductRepository(_db);
        }
        [Fact]
        public async Task AddAsync_And_GetByIdAsync_ShouldWork()
        {

            var product = new Product(
                "Laptop",
                "SKU-001",
                new Money(1000),
                20,
                "Gaming laptop"
            );

            // Act
            await _repo.AddAsync(product, CancellationToken.None);
            await _db.SaveChangesAsync();

            var result = await _repo.GetByIdAsync(product.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.SKU.Should().Be("SKU-001");
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn_AllProducts()
        {
            _db.Products.AddRange(
                new Product("A", "SKU-A", new Money(10), 50),
                new Product("B", "SKU-B", new Money(20),50)
            );
            await _db.SaveChangesAsync();

            // Act
            var products = await _repo.GetAllAsync(CancellationToken.None);

            // Assert
            products.Should().HaveCount(5);
        }

        [Fact]
        public async Task IsSkuUniqueAsync_ShouldReturnFalse_WhenSkuExists()
        {

            _db.Products.Add(
                new Product("Laptop", "SKU-EXIST", new Money(100), 10)
            );
            await _db.SaveChangesAsync();

            // Act
            var isUnique = await _repo.IsSkuUniqueAsync("SKU-EXIST", CancellationToken.None);

            // Assert
            isUnique.Should().BeFalse();
        }

        [Fact]
        public async Task IsSkuUniqueAsync_ShouldReturnTrue_WhenSkuDoesNotExist()
        {
            // Act
            var isUnique = await _repo.IsSkuUniqueAsync("SKU-NEW", CancellationToken.None);

            // Assert
            isUnique.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {

            var product = new Product("Old", "SKU-OLD", new Money(50), 10);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            // Act
            product.UpdateProduct("New", new Money(100), 20);
            await _repo.UpdateAsync(product);
            await _db.SaveChangesAsync();

            var updated = await _repo.GetByIdAsync(product.Id, CancellationToken.None);

            // Assert
            updated!.Name.Should().Be("New");
            updated.Price.Amount.Should().Be(100);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            var product = new Product("Laptop", "SKU-DEL", new Money(200), 10);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            // Act
            await _repo.DeleteAsync(product);
            await _db.SaveChangesAsync();

            var result = await _repo.GetByIdAsync(product.Id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}