using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductPublishedDomainEvent(Guid ProductId)
    : IDomainEvent;
}