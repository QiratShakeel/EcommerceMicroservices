using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Identity.Domain.Events
{
    public record UserRegistered(string username, string email) : IDomainEvent;        
}