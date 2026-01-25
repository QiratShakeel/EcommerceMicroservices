using System;
using System.Collections.Generic;
using AutoMapper;
using Ecommerce.Orders.Application.Commands;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Mapping;
using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Domain.Enums;
using Xunit;

namespace Ecommerce.Orders.Tests.Application.Mapping
{
    public class OrderMappingProfileTests
    {
        private readonly IMapper _mapper;

        public OrderMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderMappingProfile>();
            });

            _mapper = config.CreateMapper();

            // Validate mapping configuration
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void CreateOrderCommand_To_OrderEntity_ShouldMapCustomerId()
        {
            // Arrange
            var command = new CreateOrderCommand
            (
                CustomerId : Guid.NewGuid(),
                Items : new List<OrderItemDto>()
            );

            // Act
            var orderEntity = _mapper.Map<OrderEntity>(command);

            // Assert
            Assert.NotNull(orderEntity);
            Assert.Equal(command.CustomerId, orderEntity.CustomerId);
            Assert.Equal(Domain.Enums.OrderStatus.Draft, orderEntity.Status);
            Assert.Empty(orderEntity.OrderItems);
        }

        [Fact]
        public void OrderEntity_To_OrderDto_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var orderEntity = new OrderEntity(Guid.NewGuid());
            orderEntity.AddItem(Guid.NewGuid(), 100m, 2); // Total = 200
            typeof(OrderEntity).GetProperty("Status").SetValue(orderEntity, OrderStatus.Confirmed);

            // Act
            var dto = _mapper.Map<OrderDto>(orderEntity);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(orderEntity.Id, dto.Id);
            Assert.Equal(orderEntity.CustomerId, dto.CustomerId);
            Assert.Equal(orderEntity.Total, dto.TotalAmount);
            Assert.Equal("Confirmed", dto.Status.ToString());
            Assert.Equal(orderEntity.OrderItems.Count, dto.Items.Count);
        }
    }
}
