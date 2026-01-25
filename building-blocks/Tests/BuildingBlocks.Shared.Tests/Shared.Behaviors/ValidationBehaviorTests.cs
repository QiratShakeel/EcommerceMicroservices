using BuildingBlocks.Shared.Behaviors.Validation;
using FluentValidation;
using MediatR;
using Moq;

namespace BuildingBlocks.Shared.Tests.Behaviors
{
    public class TestRequest : IRequest<string> { public string Name { get; set; } = ""; }
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task ValidationBehavior_Allows_ValidRequest()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<TestRequest>>();

            // Valid request => no errors
            validatorMock
                .Setup(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            var behavior = new ValidationBehavior<TestRequest, string>(
                new[] { validatorMock.Object }
            );

            RequestHandlerDelegate<string> next = () => Task.FromResult("Success");

            // Act
            var result = await behavior.Handle(new TestRequest(), next, CancellationToken.None);

            // Assert
            Assert.Equal("Success", result);
            validatorMock.Verify(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()), Times.Once);
        }
        [Fact]
        public async Task ValidationBehavior_ThrowsException_OnValidationFailure()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<TestRequest>>();

            var failures = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Name", "Name is required")
            };

            validatorMock
                .Setup(v => v.Validate(It.IsAny<ValidationContext<TestRequest>>()))
                .Returns(new FluentValidation.Results.ValidationResult(failures));

            var behavior = new ValidationBehavior<TestRequest, string>(
                new[] { validatorMock.Object }
            );

            RequestHandlerDelegate<string> next = () => Task.FromResult("Success");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(new TestRequest(), next, CancellationToken.None)
            );

            Assert.Single(ex.Errors);
            Assert.Equal("Name is required", ex.Errors.First().ErrorMessage);
        }


    }

}