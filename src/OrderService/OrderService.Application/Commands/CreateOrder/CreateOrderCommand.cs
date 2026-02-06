using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Dto;
using BuildingBlocks.Shared.Results;
using Ecommerce.Orders.Application.Interfaces;
using MediatR;
namespace Ecommerce.Orders.Application.Commands
{
    public record CreateOrderCommand(
        Guid CustomerId,
        List<CreateOrderItemDto> Items)
        : IRequest<Result<Guid>>, IOrderRequest;
}
