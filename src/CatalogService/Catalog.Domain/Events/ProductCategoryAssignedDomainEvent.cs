using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Domain.Events
{
    public record ProductCategoryAssignedDomainEvent(Guid ProductId, int CategoryId)
    : IDomainEvent;

}