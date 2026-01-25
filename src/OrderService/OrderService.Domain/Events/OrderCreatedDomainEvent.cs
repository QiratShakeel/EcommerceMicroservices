using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Orders.Domain.Events
{
    public record OrderCreatedDomainEvent(Guid OrderId,Guid CustomerId, decimal TotalAmount)
    : IDomainEvent;
}
