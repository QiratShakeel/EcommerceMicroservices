using AutoMapper;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Application.Mapping;
using Ecommerce.Catalog.Domain.Aggregates;

namespace Catalog.Application.Tests.Mapping
{
    public class ProductMappingProfileTests
    {
        [Fact]
        public void ProductMapping_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<ProductMappingProfile>());

            config.AssertConfigurationIsValid();
        }
        [Fact]
        public void Product_To_Dto_Should_Map_Price()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile<ProductMappingProfile>())
                .CreateMapper();

            var product = new Product("Test", "SKU1", new Money(100), "Desc");

            var dto = mapper.Map<ProductDto>(product);

            Assert.Equal(product.Price.Amount, dto.Price);
        }


    }
}