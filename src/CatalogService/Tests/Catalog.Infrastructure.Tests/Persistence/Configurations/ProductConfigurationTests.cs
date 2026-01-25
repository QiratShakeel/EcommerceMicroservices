using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.Enums;
using Ecommerce.Catalog.Infrastructure.Tests.Persistence.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Infrastructure.Tests.Persistence.Configurations
{
    public class ProductConfigurationTests: IClassFixture<CatalogDbContextFixture>
    {
        private readonly CatalogDbContextFixture _fixture;

        public ProductConfigurationTests(CatalogDbContextFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public void ProductConfiguration_ShouldHaveCorrectSettings()
        {
            // Arrange
            var model = _fixture.DbContext.Model;
            var entityType = model.FindEntityType(typeof(Product));
            Assert.NotNull(entityType);
            // Act & Assert
            // Check if Username is Required
            var prodnameProperty = entityType.FindProperty(nameof(Product.Name));
            prodnameProperty.Should().NotBeNull();

            // Check MaxLength
            prodnameProperty.GetMaxLength().Should().Be(200);
            var descprop = entityType.FindProperty(nameof(Product.Description));
            descprop.GetMaxLength().Should().Be(1000);
            descprop.IsNullable.Should().BeTrue();
            var priceProperty = entityType.FindNavigation(nameof(Product.Price)).TargetEntityType.FindProperty(nameof(Money.Amount));
            priceProperty.GetColumnName().Should().Be("Price");
            priceProperty.GetPrecision().Should().Be(18);
            priceProperty.GetScale().Should().Be(2);
            var prodstatus = entityType.FindProperty(nameof(Product.Status));
            //var converter = prodstatus.GetValueConverter();
            prodstatus.Should().NotBeNull();
            prodstatus.ClrType.Should().Be(typeof(ProductStatus));
        }
    }
}