using BuildingBlocks.EventBus.Abstractions;

namespace Ecommerce.Payment.Infrastructure.Messaging.IntegrationEvents
{
    public record PaymentSucceededIntegrationEvent(Guid OrderId)
    : IIntegrationEvent;
}