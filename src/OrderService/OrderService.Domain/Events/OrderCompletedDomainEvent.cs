using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Orders.Domain.Aggregates;

namespace Ecommerce.Orders.Domain.Events
{
    public record OrderCompletedDomainEvent(Guid OrderId, IReadOnlyCollection<(Guid ProductId, int Quantity)> Items) : IDomainEvent;
}
