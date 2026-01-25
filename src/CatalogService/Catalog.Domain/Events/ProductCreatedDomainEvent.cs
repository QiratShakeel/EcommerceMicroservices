using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductCreatedDomainEvent (Guid ProductId, string Name, string SKU)
    : IDomainEvent;
}