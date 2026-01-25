using Ecommerce.Catalog.Domain.ValueObjects;
using System;
using Xunit;

namespace Catalog.Domain.Tests.ValueObjects
{
    public class ProductImageTests
    {
        [Fact]
        public void CreateProductImage_WithValidUrlAndFileType_ShouldSucceed()
        {
            // Arrange
            var url = "https://example.com/image.jpg";
            var altText = "Sample image";
            var fileType = ".jpg";

            // Act
            var image = new ProductImage(url, altText, fileType);

            // Assert
            Assert.Equal(url, image.Url);
            Assert.Equal(altText, image.AltText);
            Assert.Equal(fileType, image.FileType);
        }

        [Fact]
        public void CreateProductImage_WithInvalidUrlOrFileType_ShouldThrow()
        {
            // Invalid URL
            Assert.Throws<ArgumentException>(() =>
                new ProductImage("invalid-url", "alt", ".jpg"));

            // Invalid file type
            Assert.Throws<ArgumentException>(() =>
                new ProductImage("https://example.com/image.jpg", "alt", ".exe"));
        }
    }
}
