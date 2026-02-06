using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Payment.Domain.Events
{
    public record PaymentFailedDomainEvent(Guid orderId, string reason): IDomainEvent;
}