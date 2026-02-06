using BuildingBlocks.EventBus.Abstractions;

namespace BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents
{
    public record OrderCreatedIntegrationEventForPayment(Guid OrderId, Guid CustomerId, decimal TotalAmount): IIntegrationEvent;
}