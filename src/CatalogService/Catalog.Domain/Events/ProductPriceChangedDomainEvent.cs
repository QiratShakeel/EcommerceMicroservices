using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductPriceChangedDomainEvent(Guid ProductId, decimal OldPrice, decimal NewPrice)
    : IDomainEvent;
}