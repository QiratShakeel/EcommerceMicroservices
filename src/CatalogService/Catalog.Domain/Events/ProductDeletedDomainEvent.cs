using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductDeletedDomainEvent(
        Guid ProductId,
        List<string> ImageUrls
    ) : IDomainEvent;
}