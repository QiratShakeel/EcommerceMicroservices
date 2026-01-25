using Ecommerce.Catalog.Domain.Enums;

namespace Catalog.Domain.Tests.Enums
{
    public class ProductStatusTests
    {
        [Fact]
        public void ProductStatus_ShouldHaveExpectedValues()
        {
            Assert.Equal(4, Enum.GetNames(typeof(ProductStatus)).Length);
            Assert.Contains("Active", Enum.GetNames(typeof(ProductStatus)));
            Assert.Contains("Inactive", Enum.GetNames(typeof(ProductStatus)));
            Assert.Contains("Discontinued", Enum.GetNames(typeof(ProductStatus)));
            Assert.Contains("Draft", Enum.GetNames(typeof(ProductStatus)));
        }
    }
}

