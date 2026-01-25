using System;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Ecommerce.Orders.Tests.Application.Validators
{
    // A dummy class implementing IOrderRequest for testing
    public class TestOrderRequest : IOrderRequest
    {
        public Guid CustomerId { get; set; }
    }

    public class OrderBaseValidatorTests
    {
        private readonly OrderBaseValidator<TestOrderRequest> _validator;

        public OrderBaseValidatorTests()
        {
            _validator = new OrderBaseValidator<TestOrderRequest>();
        }

        [Fact]
        public void Validator_Should_HaveError_When_CustomerIdIsEmpty()
        {
            // Arrange
            var request = new TestOrderRequest { CustomerId = Guid.Empty };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.CustomerId)
                  .WithErrorMessage("CustomerId is required.");
        }

        [Fact]
        public void Validator_Should_NotHaveError_When_CustomerIdIsSet()
        {
            // Arrange
            var request = new TestOrderRequest { CustomerId = Guid.NewGuid() };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(r => r.CustomerId);
        }
    }
}
