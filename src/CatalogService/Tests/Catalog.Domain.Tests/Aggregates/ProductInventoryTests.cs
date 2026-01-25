using Ecommerce.Catalog.Domain.Aggregates;
using System;
using Xunit;

namespace Catalog.Domain.Tests.Aggregates
{
    public class ProductInventoryTests
    {
        [Fact]
        public void CreateInventory_WithValidStock_ShouldSucceed()
        {
            // Arrange & Act
            var inventory = new ProductInventory(10,"A1");

            // Assert
            Assert.Equal(10, inventory.StockQuantity);
            Assert.Equal(0, inventory.ReservedQuantity);
            Assert.Equal(10, inventory.AvailableStock);
            //Assert.Null(inventory.WarehouseLocation);
            Assert.Equal("A1", inventory.WarehouseLocation);
        }

        [Fact]
        public void CreateInventory_WithNegativeStock_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
                new ProductInventory(-5));
        }

        [Fact]
        public void AddStock_WithPositiveAmount_ShouldIncreaseStock()
        {
            var inventory = new ProductInventory(5);

            inventory.AddStock(5);

            Assert.Equal(10, inventory.StockQuantity);
            Assert.Equal(10, inventory.AvailableStock);
        }

        [Fact]
        public void AddStock_WithInvalidAmount_ShouldThrow()
        {
            var inventory = new ProductInventory(5);

            Assert.Throws<ArgumentException>(() => inventory.AddStock(0));
        }

        [Fact]
        public void ReduceStock_WithValidAmount_ShouldDecreaseStock()
        {
            var inventory = new ProductInventory(10);

            inventory.ReduceStock(3);

            Assert.Equal(7, inventory.StockQuantity);
            Assert.Equal(7, inventory.AvailableStock);
        }

        [Fact]
        public void ReduceStock_MoreThanAvailable_ShouldThrow()
        {
            var inventory = new ProductInventory(5);

            Assert.Throws<InvalidOperationException>(() =>
                inventory.ReduceStock(10));
        }

        [Fact]
        public void ReserveStock_WithAvailableStock_ShouldReserve()
        {
            var inventory = new ProductInventory(10);

            inventory.ReserveStock(4);

            Assert.Equal(4, inventory.ReservedQuantity);
            Assert.Equal(6, inventory.AvailableStock);
            Assert.Equal(10, inventory.StockQuantity);
        }

        [Fact]
        public void ReserveStock_MoreThanAvailable_ShouldThrow()
        {
            var inventory = new ProductInventory(5);

            Assert.Throws<InvalidOperationException>(() =>
                inventory.ReserveStock(6));
        }

        [Fact]
        public void ReleaseReserved_WithValidAmount_ShouldRelease()
        {
            var inventory = new ProductInventory(10);
            inventory.ReserveStock(5);

            inventory.ReleaseReserved(3);

            Assert.Equal(2, inventory.ReservedQuantity);
            Assert.Equal(8, inventory.AvailableStock);
        }

        [Fact]
        public void ReleaseReserved_MoreThanReserved_ShouldThrow()
        {
            var inventory = new ProductInventory(10);
            inventory.ReserveStock(3);

            Assert.Throws<InvalidOperationException>(() =>
                inventory.ReleaseReserved(5));
        }
    }
}
