using BuildingBlocks.Shared.Results;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Interfaces;
using MediatR;
namespace Ecommerce.Orders.Application.Commands
{
    public record CreateOrderCommand(
        Guid CustomerId,
        List<OrderItemDto> Items)
        : IRequest<Result<Guid>>, IOrderRequest;
}
