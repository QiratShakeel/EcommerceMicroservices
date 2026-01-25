using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductBackInStockDomainEvent(Guid ProductId, int Quantity)
    : IDomainEvent;
}