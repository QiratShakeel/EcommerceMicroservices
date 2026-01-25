using System;
using System.Linq;
using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Domain.Enums;
using Ecommerce.Orders.Domain.Events;
using Xunit;

namespace Ecommerce.Orders.Tests.Domain
{
    public class OrderEntityTests
    {
        [Fact]
        public void CreateOrder_ShouldInitializeCorrectly()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act
            var order = new OrderEntity(customerId);

            // Assert
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(OrderStatus.Draft, order.Status);
            Assert.Empty(order.OrderItems);
            Assert.Equal(0, order.Total);
        }

        [Fact]
        public void AddItem_ShouldAddNewItem()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            var item = order.AddItem(productId, 100m, 2);

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Equal(productId, item.ProductId);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(200m, order.Total);
        }

        [Fact]
        public void AddItem_SameProduct_ShouldIncreaseQuantity()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            order.AddItem(productId, 50m, 2);
            var existingItem = order.AddItem(productId, 50m, 3);

            // Assert
            Assert.Single(order.OrderItems);
            Assert.Equal(5, existingItem.Quantity);
            Assert.Equal(250m, order.Total);
        }

        [Fact]
        public void Confirm_WithItems_ShouldSetStatusAndAddDomainEvent()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            order.AddItem(Guid.NewGuid(), 100m, 2);

            // Act
            order.Confirm();

            // Assert
            Assert.Equal(OrderStatus.Confirmed, order.Status);
            var domainEvent = Assert.Single(order.DomainEvents);
            Assert.IsType<OrderCreatedDomainEvent>(domainEvent);
        }

        [Fact]
        public void Confirm_WithoutItems_ShouldThrow()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.Confirm());
        }

        [Fact]
        public void Cancel_DraftOrder_ShouldSetStatusCancelled()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());

            // Act
            order.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        [Fact]
        public void Cancel_CompletedOrder_ShouldThrow()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            order.AddItem(Guid.NewGuid(), 100m, 1);
            order.Confirm(); // Status = Confirmed
            // Simulate completing the order
            typeof(OrderEntity).GetProperty("Status").SetValue(order, OrderStatus.Completed);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.Cancel());
        }
    }
}
