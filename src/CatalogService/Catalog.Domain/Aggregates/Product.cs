using System;
using System.Collections.Generic;
using Ecommerce.Catalog.Domain.Enums;
using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Domain.Events;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.Exceptions;

namespace Ecommerce.Catalog.Domain.Aggregates
{
    public class Product : Entity, IAggregateRoot // AGGREGATE ROOT
    {
        // ======== PRIVATE FIELDS ========
        private readonly List<ProductImage> _images = new();
        //private readonly List<int> _categoryIds = new();
        private readonly List<ProductCategory> _productCategories = new();

        // ======== PROPERTIES ========
        //public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string SKU { get; private set; }
        public Money Price { get; private set; }
        public ProductStatus Status { get; private set; } = ProductStatus.Draft;
        public ProductInventory Inventory { get; private set; }

        // Readonly collections
        public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();
        //public IReadOnlyCollection<int> CategoryIds => _categoryIds.AsReadOnly();
        public IReadOnlyCollection<ProductCategory> Categories => _productCategories.AsReadOnly();

        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; private set; }

        // ======== CONSTRUCTOR ========
        public Product() { } //by EF
        public Product(string name, string sku, Money price, string? description = null)
        {
            SetName(name);
            SetSKU(sku); 
            ChangePrice(price);
            Description = description;
            Inventory = new ProductInventory(0);
            //AddDomainEvent(new ProductCreatedDomainEvent(Id, name, sku));
        }
        // ======== DOMAIN METHODS ========
        public void UpdateProduct(string name, Money price, string? description = null)
        {
            SetName(name);
            ChangePrice(price);
            Description = description;
            //AddDomainEvent(new ProductCreatedDomainEvent(Id, name, sku));
        }

        // ---------- Name & SKU ----------
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ProductNameRequiredException();
            Name = name;
            UpdatedDate = DateTime.UtcNow;
        }

        public void SetSKU(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ProductSkuRequiredException();
            SKU = sku;
            UpdatedDate = DateTime.UtcNow;
        }

        // ---------- Price & Discount ----------
        public void ChangePrice(Money newPrice)
        {
            if (newPrice == null)
                throw new ArgumentNullException(nameof(newPrice));

            //var oldPrice = Price.Amount;          // capture current price before change
            var oldPrice = Price?.Amount ?? 0m; // SAFE
            var newPriceValue = newPrice.Amount;  // new price value

            Price = newPrice;                     // update
            UpdatedDate = DateTime.UtcNow;
            if (oldPrice != newPrice.Amount)
            {
                AddDomainEvent(new ProductPriceChangedDomainEvent(Id, oldPrice, newPriceValue));
            }
        }

        // ---------- Categories ----------
        public void AddCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new InvalidProductCategoryException();

            if (_productCategories.Any(x => x.CategoryId == categoryId))
                throw new DuplicateProductCategoryException();

            _productCategories.Add(new ProductCategory(Id, categoryId));
            UpdatedDate = DateTime.UtcNow;

            AddDomainEvent(new ProductCategoryAssignedDomainEvent(Id, categoryId));
        }

        public void RemoveCategory(int categoryId)
        {
            var existing = _productCategories
                .FirstOrDefault(x => x.CategoryId == categoryId);

            if (existing == null)
                throw new InvalidProductCategoryException();

            _productCategories.Remove(existing);
            UpdatedDate = DateTime.UtcNow;
        }


        public void SetCategories(IEnumerable<int> categoryIds)
        {
            if (categoryIds == null || !categoryIds.Any())
                throw new ArgumentException("Product must have at least one category.");

            if (categoryIds.Any(id => id <= 0))
                throw new ArgumentException("Invalid category ID found.");

            _productCategories.Clear();

            foreach (var categoryId in categoryIds.Distinct())
            {
                _productCategories.Add(new ProductCategory(Id, categoryId));
            }

            UpdatedDate = DateTime.UtcNow;
        }

        // ---------- Images ----------
        public void AddImage(ProductImage image)
        {
            if (_images.Any(i => i.Url == image.Url))
                throw new DuplicateProductImageException();
            _images.Add(image);
            UpdatedDate = DateTime.UtcNow;

            //AddDomainEvent(new ProductImageAddedDomainEvent(Id, image.Url));

        }

        public void RemoveImage(string imageUrl)
        {
            var img = _images.FirstOrDefault(i => i.Url == imageUrl);
            if (img == null)
                throw new InvalidOperationException("Image not found.");
            _images.Remove(img);
            UpdatedDate = DateTime.UtcNow;
        }

        // ---------- Inventory ----------
        public void AddInventory(int quantity)
        {
            //var oldQty = Inventory.StockQuantity;
            UpdatedDate = DateTime.UtcNow;
            Inventory.AddStock(quantity);

            //AddDomainEvent(new ProductInventoryChangedDomainEvent(Id, oldQty, Inventory.StockQuantity));

            //if (oldQty == 0 && Inventory.StockQuantity > 0)
            //    AddDomainEvent(new ProductBackInStockDomainEvent(Id, Inventory.StockQuantity));
        }

        public void ReduceInventory(int quantity)
        {
            var oldQty = Inventory.StockQuantity;
            UpdatedDate = DateTime.UtcNow;
            Inventory.ReduceStock(quantity);

            //AddDomainEvent(new ProductInventoryChangedDomainEvent(Id, oldQty, Inventory.StockQuantity));

            if (Inventory.StockQuantity == 0)
                AddDomainEvent(new ProductOutOfStockDomainEvent(Id));
        }

        public void SetInventory(ProductInventory inventory)
        {
            Inventory = inventory ?? throw new InvalidOperationException("Inventory is required.");
            UpdatedDate = DateTime.UtcNow;
        }
        public void MarkAsCreated()
        {
            AddDomainEvent(new ProductCreatedDomainEvent(Id, Name, SKU));
        }

        // ---------- Publish ----------
        public void Publish()
        {
            if (!_images.Any())
                throw new InvalidOperationException("Product must have at least one image.");
            if (Price.Amount <= 0)
                throw new InvalidOperationException("Price must be positive.");
            if (Inventory == null || Inventory.StockQuantity <= 0)
                throw new InvalidOperationException("Cannot publish product without inventory.");

            Status = ProductStatus.Active;
            UpdatedDate = DateTime.UtcNow;
            AddDomainEvent(new ProductPublishedDomainEvent(Id));
        }

        public void Draft()
        {
            Status = ProductStatus.Draft;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
