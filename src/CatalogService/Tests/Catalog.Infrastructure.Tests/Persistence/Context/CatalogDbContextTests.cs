using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Ecommerce.Catalog.Infrastructure.Tests.Persistence.Context
{
    public class CatalogDbContextTests : IClassFixture<CatalogDbContextFixture>
    {
        private readonly CatalogDbContextFixture _fixture;

        public CatalogDbContextTests(CatalogDbContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Can_Add_Product()
        {
            // Arrange
            var product = new Product
            (
                name: "New Product",
                sku: "sku-002",
                price: new BuildingBlocks.Shared.Infrastructure.Money(10)
            );

            // Act
            _fixture.DbContext.Products.Add(product);
            await _fixture.DbContext.SaveChangesAsync();

            var savedProduct = await _fixture.DbContext.Products.FindAsync(product.Id);

            // Assert
            savedProduct.Should().NotBeNull();
            savedProduct!.Name.Should().Be("New Product");
        }

        [Fact]
        public async Task Can_Add_And_Retrieve_OutboxMessage()
        {
            // Arrange
            var message = new BuildingBlocks.Shared.Outbox.OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                Type = "Test",
                Content = "{}"
            };

            // Act
            await _fixture.DbContext.AddMessageAsync(message);
            var unprocessed = await _fixture.DbContext.GetUnprocessedMessagesAsync(10, default);

            // Assert
            unprocessed.Should().ContainSingle(x => x.Id == message.Id);
        }

        [Fact]
        public async Task Can_Mark_OutboxMessage_As_Processed()
        {
            // Arrange
            var message = new BuildingBlocks.Shared.Outbox.OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                Type = "Test2",
                Content = "{}"
            };

            await _fixture.DbContext.AddMessageAsync(message);

            // Act
            await _fixture.DbContext.MarkAsProcessedAsync(message, default);

            var processed = await _fixture.DbContext.OutboxMessages.FindAsync(message.Id);

            // Assert
            processed!.ProcessedOn.Should().NotBeNull();
        }
    }
}