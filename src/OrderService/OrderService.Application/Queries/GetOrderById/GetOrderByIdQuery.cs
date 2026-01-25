using Ecommerce.Orders.Application.Dto;
using MediatR;
namespace Ecommerce.Orders.Application.Queries
{
    public record GetOrderByIdQuery(Guid OrderId)
        : IRequest<OrderDto>;
}