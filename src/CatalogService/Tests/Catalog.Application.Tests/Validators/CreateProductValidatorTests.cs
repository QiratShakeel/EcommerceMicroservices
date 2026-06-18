using Ecommerce.Catalog.Application.Commands;
using Ecommerce.Catalog.Application.Validators;
using FluentAssertions;
using Xunit;

namespace Ecommerce.Catalog.Application.Tests.Validators
{
    public class CreateProductValidatorTests
    {
        private readonly CreateProductValidator _validator;

        public CreateProductValidatorTests()
        {
            _validator = new CreateProductValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var command = new CreateProductCommand
            (
                Name : "",
                SKU : "ABC123",
                Price : 100m,
                stock:5,
                Desc: "Description"
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "Name");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Invalid()
        {
            var command = new CreateProductCommand
            (
                Name: "Product",
                SKU: "ABC123",
                Price: 0,
                stock: 4,
                Desc: "Description"
            );

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "Price");
        }

        [Fact]
        public void Should_Have_Error_When_SKU_Is_Invalid()
        {
            var command = new CreateProductCommand
            (
                Name : "Product",
                SKU : "abc",    //lenght or wrong pattern error
                Price : 10,
                stock: 2,
                Desc : "Description"
            );

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "SKU");
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Command()
        {
            var command = new CreateProductCommand
            (
                Name : "Product",
                SKU : "SKU12345",
                Price: 100,
                stock: 5,
                Desc: "Valid description"
            );

            var result = _validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}
