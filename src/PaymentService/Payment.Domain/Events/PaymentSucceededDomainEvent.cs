using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Payment.Domain.Events
{
    public record PaymentSucceededDomainEvent(Guid orderId): IDomainEvent;
}
