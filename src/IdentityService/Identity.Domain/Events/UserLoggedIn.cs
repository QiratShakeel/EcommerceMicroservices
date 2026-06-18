using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Identity.Domain.Events
{
    public record UserLoggedIn(Guid userId, DateTime occurredAt) : IDomainEvent;
}