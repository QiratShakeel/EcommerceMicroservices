using System;
using Ecommerce.Orders.Domain.Aggregates;
using Xunit;

namespace Ecommerce.Orders.Tests.Domain
{
    public class OrderItemTests
    {
        [Fact]
        public void CreateOrderItem_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var productId = Guid.NewGuid();
            decimal unitPrice = 100m;
            int quantity = 2;

            // Act
            var orderItem = new OrderItem(productId, unitPrice, quantity);

            // Assert
            Assert.Equal(productId, orderItem.ProductId);
            Assert.Equal(unitPrice, orderItem.UnitPrice);
            Assert.Equal(quantity, orderItem.Quantity);
            Assert.Equal(unitPrice * quantity, orderItem.SubTotal);
        }

        [Fact]
        public void AddQuantity_ShouldIncreaseQuantity()
        {
            // Arrange
            var orderItem = new OrderItem(Guid.NewGuid(), 50m, 1);

            // Act
            orderItem.AddQuantity(3);

            // Assert
            Assert.Equal(4, orderItem.Quantity);
            Assert.Equal(50m * 4, orderItem.SubTotal);
        }
    }
}
