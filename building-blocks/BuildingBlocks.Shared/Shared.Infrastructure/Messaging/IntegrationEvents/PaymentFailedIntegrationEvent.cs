using BuildingBlocks.EventBus.Abstractions;

namespace BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents
{
    public record PaymentFailedIntegrationEvent(Guid OrderId, string Reason)
    : IIntegrationEvent;
}