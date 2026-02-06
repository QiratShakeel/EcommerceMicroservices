using BuildingBlocks.EventBus.Abstractions;

namespace Ecommerce.Payment.Infrastructure.Messaging.IntegrationEvents
{
    public record PaymentFailedIntegrationEvent(Guid OrderId, string Reason)
    : IIntegrationEvent;
}