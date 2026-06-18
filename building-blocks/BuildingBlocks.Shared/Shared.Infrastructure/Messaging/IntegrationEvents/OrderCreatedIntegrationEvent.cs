using BuildingBlocks.EventBus.Abstractions;

namespace BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents
{
    public record OrderCreatedIntegrationEvent(Guid OrderId, Guid CustomerId, decimal TotalAmount) : IIntegrationEvent;
}