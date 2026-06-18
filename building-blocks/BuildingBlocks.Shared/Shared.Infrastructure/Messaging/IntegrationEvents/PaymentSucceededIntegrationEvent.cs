using BuildingBlocks.EventBus.Abstractions;

namespace BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents
{
    public record PaymentSucceededIntegrationEvent(Guid OrderId)
    : IIntegrationEvent;
}