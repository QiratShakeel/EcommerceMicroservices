using AutoMapper;
using Ecommerce.Orders.Application.Commands;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Domain.Aggregates;
using Moq;

namespace Ecommerce.Orders.Tests.Application
{
    public class CreateOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateOrderAndReturnId()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var customerId = Guid.NewGuid();

            var command = new CreateOrderCommand
            (
                CustomerId : customerId,
                Items : new List<OrderItemDto>
                {
                    new() { ProductId = Guid.NewGuid(), Price = 100m, Quantity = 2 },
                    new() { ProductId = Guid.NewGuid(), Price = 50m, Quantity = 1 }
                }
            );

            // Mock mapping from command to OrderEntity
            mapperMock
                .Setup(m => m.Map<OrderEntity>(command))
                .Returns(new OrderEntity(command.CustomerId));

            var handler = new CreateOrderCommandHandler(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);

            // Verify repository AddAsync was called once with an OrderEntity
            repositoryMock.Verify(r => r.AddAsync(It.Is<OrderEntity>(o =>
                o.CustomerId == command.CustomerId &&
                o.OrderItems.Count == 2 &&
                o.Total == 250m &&
                o.Status == Domain.Enums.OrderStatus.Confirmed
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenNoItems_ShouldThrowExceptionOnConfirm()
        {
            // Arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateOrderCommand
            (
                CustomerId: Guid.NewGuid(),
                Items : new List<OrderItemDto>()
            );

            mapperMock
                .Setup(m => m.Map<OrderEntity>(command))
                .Returns(new OrderEntity(command.CustomerId));

            var handler = new CreateOrderCommandHandler(repositoryMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
