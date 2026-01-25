using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductOutOfStockDomainEvent(Guid ProductId)
    : IDomainEvent;
}