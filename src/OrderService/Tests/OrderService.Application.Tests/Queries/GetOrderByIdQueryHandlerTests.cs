using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Application.Queries;
using Ecommerce.Orders.Domain.Aggregates;
using BuildingBlocks.Shared.Exceptions;
using Moq;
using Xunit;

namespace Ecommerce.Orders.Tests.Application.Queries
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetOrderByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_OrderExists_ShouldReturnOrderDto()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderEntity = new OrderEntity(Guid.NewGuid());
            var query = new GetOrderByIdQuery(orderId);
            var orderDto = new OrderDto
            {
                Id = orderId,
                CustomerId = orderEntity.CustomerId
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(orderEntity);

            _mapperMock.Setup(m => m.Map<OrderDto>(orderEntity)).Returns(orderDto);

            var handler = new GetOrderByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderDto.Id, result.Id);
            Assert.Equal(orderDto.CustomerId, result.CustomerId);

            _repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderDto>(orderEntity), Times.Once);
        }

        [Fact]
        public async Task Handle_OrderDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var query = new GetOrderByIdQuery(orderId);

            _repositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync((OrderEntity)null);

            var handler = new GetOrderByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));

            _repositoryMock.Verify(r => r.GetByIdAsync(orderId), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderDto>(It.IsAny<OrderEntity>()), Times.Never);
        }
    }
}
