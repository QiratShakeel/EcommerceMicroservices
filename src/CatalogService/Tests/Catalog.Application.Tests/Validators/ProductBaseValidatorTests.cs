using Ecommerce.Catalog.Application.Validators;
using FluentValidation;

namespace Catalog.Application.Tests.Validators
{
    public class ProductBaseValidatorTests
    {
        private readonly ProductBaseValidator<FakeProductRequest> _validator;

        public ProductBaseValidatorTests()
        {
            _validator = new ProductBaseValidator<FakeProductRequest>();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new FakeProductRequest { Name = "", Price = 10, Desc = "Desc" };
            var result = _validator.Validate(model);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            var model = new FakeProductRequest { Name = "Product", Price = 0, Desc = "Desc" };
            var result = _validator.Validate(model);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors,x => x.PropertyName == "Price");
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new FakeProductRequest { Name = "Product", Price = 100, Desc = "Valid desc" };
            var result = _validator.Validate(model);
            Assert.True(result.IsValid);
        }

        // Fake request to satisfy the generic
        private class FakeProductRequest : IProductRequest
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Desc { get; set; }
        }
    }

}