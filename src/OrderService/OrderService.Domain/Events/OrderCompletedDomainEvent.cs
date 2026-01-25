using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Orders.Domain.Events
{
    public record OrderCompletedDomainEvent(Guid orderId) : IDomainEvent;
}
