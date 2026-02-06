using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Dto;
using Ecommerce.Orders.Domain.Aggregates;
namespace Ecommerce.Orders.Domain.Events
{
    public record OrderCreatedDomainEvent(Guid OrderId,Guid CustomerId, decimal TotalAmount, IReadOnlyCollection<OrderItem> OrderItems)
    : IDomainEvent;
}
