using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Payment.Domain.Events
{
    public record PaymentCreatedDomainEvent(Guid PaymentId, Guid OrderId)
    : IDomainEvent;
}