using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.Enums;
using Ecommerce.Catalog.Domain.Events;
using Ecommerce.Catalog.Domain.Exceptions;
using Ecommerce.Catalog.Domain.ValueObjects;
using System;
using Xunit;

namespace Catalog.Domain.Tests.Aggregates
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_WithValidData_ShouldSucceed()
        {
            var price = new Money(100);

            var product = new Product("Laptop", "SKU-001", price, "desc");

            Assert.Equal("Laptop", product.Name);
            Assert.Equal("SKU-001", product.SKU);
            Assert.Equal(price, product.Price);
            Assert.Equal(ProductStatus.Draft, product.Status);
        }

        [Fact]
        public void CreateProduct_WithEmptyName_ShouldThrow()
        {
            var price = new Money(100);

            Assert.Throws<ProductNameRequiredException>(() =>
                new Product("", "SKU-001", price));
        }

        [Fact]
        public void ChangePrice_ShouldRaisePriceChangedEvent()
        {
            var product = new Product("Laptop", "SKU-001", new Money(100));

            product.ChangePrice(new Money(150));

            //Assert.Contains(product.DomainEvents,e => e.GetType().Name == "ProductPriceChangedDomainEvent");
            Assert.Contains(product.DomainEvents,e => e is ProductPriceChangedDomainEvent);
        }

        [Fact]
        public void AddCategory_ShouldAddCategory()
        {
            var product = new Product("Laptop", "SKU-001", new Money(100));

            product.AddCategory(1);

            Assert.Single(product.Categories);
        }

        [Fact]
        public void AddDuplicateCategory_ShouldThrow()
        {
            var product = new Product("Laptop", "SKU-001", new Money(100));
            product.AddCategory(1);

            Assert.Throws<DuplicateProductCategoryException>(() =>
                product.AddCategory(1));
        }

        [Fact]
        public void AddImage_WithDuplicateUrl_ShouldThrow()
        {
            var product = new Product("Laptop", "SKU-001", new Money(100));
            var image = new ProductImage("https://img.com/a.jpg", "alt", ".jpg");

            product.AddImage(image);

            Assert.Throws<DuplicateProductImageException>(() =>
                product.AddImage(image));
        }

        [Fact]
        public void Publish_WithMissingInventory_ShouldThrow()
        {
            var product = new Product("Laptop", "SKU-001", new Money(100));
            product.AddImage(new ProductImage("https://img.com/a.jpg", "alt", ".jpg"));

            Assert.Throws<InvalidOperationException>(() =>
                product.Publish());
        }

        [Fact]
        public void Publish_WithValidData_ShouldActivateProduct()
        {
            var product = new Product("Laptop", "SKU-001", new Money(100));
            product.AddImage(new ProductImage("https://img.com/a.jpg", "alt", ".jpg"));
            product.SetInventory(new ProductInventory(10));

            product.Publish();

            Assert.Equal(ProductStatus.Active, product.Status);
        }

        [Fact]
        public void Publish_Product_ShouldRaise_ProductPublishedEvent()
        {
            // Arrange
            var product = new Product("Laptop", "SKU-001", new Money(100));
            product.AddImage(new ProductImage("https://img.com/a.jpg", "alt", ".jpg"));
            product.SetInventory(new ProductInventory(10));

            // Act
            product.Publish();

            // Assert
            Assert.Contains(product.DomainEvents,e => e is ProductPublishedDomainEvent);
        }

    }
}
