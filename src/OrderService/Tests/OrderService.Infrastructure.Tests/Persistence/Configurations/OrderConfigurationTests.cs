using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Orders.Tests.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurationTests : IClassFixture<OrdersDbContextFixture>
    {
        private readonly OrdersDbContext _context;

        public OrderConfigurationTests(OrdersDbContextFixture fixture)
        {
            _context = fixture.DbContext;
        }

        [Fact]
        public async Task OrderEntity_ShouldBeSavedWithOrderItems()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            var productId1 = Guid.NewGuid();
            var productId2 = Guid.NewGuid();

            order.AddItem(productId1, 100m, 2);
            order.AddItem(productId2, 50m, 1);
            order.Confirm();

            // Act
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var savedOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            // Assert
            Assert.NotNull(savedOrder);
            Assert.Equal(2, savedOrder.OrderItems.Count);

            var item1 = savedOrder.OrderItems.FirstOrDefault(i => i.ProductId == productId1);
            var item2 = savedOrder.OrderItems.FirstOrDefault(i => i.ProductId == productId2);

            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal(2, item1.Quantity);
            Assert.Equal(1, item2.Quantity);
            Assert.Equal(100m, item1.UnitPrice);
            Assert.Equal(50m, item2.UnitPrice);
        }

        [Fact]
        public void OrderEntity_Model_ShouldHaveProperKeysAndPrecision()
        {
            // This verifies EF Core model metadata for configuration
            var entityType = _context.Model.FindEntityType(typeof(OrderEntity));
            Assert.NotNull(entityType);

            // Check key
            var pk = entityType.FindPrimaryKey();
            Assert.Single(pk.Properties);
            Assert.Equal("Id", pk.Properties[0].Name);

            // Check owned collection
            var orderItemsNavigation = entityType.FindNavigation(nameof(OrderEntity.OrderItems));
            Assert.NotNull(orderItemsNavigation);

            var orderItemType = orderItemsNavigation.TargetEntityType;

            // Check owned entity properties
            Assert.Contains(orderItemType.GetProperties(), p => p.Name == "ProductId");
            Assert.Contains(orderItemType.GetProperties(), p => p.Name == "UnitPrice");
            Assert.Contains(orderItemType.GetProperties(), p => p.Name == "Quantity");

            // Check precision of UnitPrice
            var unitPriceProp = orderItemType.GetProperty("UnitPrice");
            Assert.Equal(18, unitPriceProp.GetPrecision());
            Assert.Equal(2, unitPriceProp.GetScale());
        }
    }
}
