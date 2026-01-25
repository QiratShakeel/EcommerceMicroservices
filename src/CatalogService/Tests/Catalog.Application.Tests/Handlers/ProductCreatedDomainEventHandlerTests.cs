using Ecommerce.Catalog.Application.EventsHandlers;
using Ecommerce.Catalog.Domain.Events;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ecommerce.Catalog.Application.Tests.EventsHandlers
{
    public class ProductCreatedDomainEventHandlerTests
    {
        private readonly Mock<ILogger<ProductCreatedDomainEventHandler>> _loggerMock;
        private readonly ProductCreatedDomainEventHandler _handler;

        public ProductCreatedDomainEventHandlerTests()
        {
            _loggerMock = new Mock<ILogger<ProductCreatedDomainEventHandler>>();
            _handler = new ProductCreatedDomainEventHandler(_loggerMock.Object);
        }

        [Fact]
        public async Task Handle_Should_LogInformation_When_EventRaised()
        {
            // Arrange
            var @event = new ProductCreatedDomainEvent(
                ProductId: Guid.NewGuid(),
                Name: "Test Product",
                SKU: "SKU123"
            );

            // Act
            await _handler.Handle(@event, CancellationToken.None);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Test Product")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }
    }
}
