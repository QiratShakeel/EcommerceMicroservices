using BuildingBlocks.EventBus.Abstractions;

namespace Ecommerce.Orders.Infrastructure.Messaging.IntegrationEvents
{
    public record OrderCreatedIntegrationEvent(
    Guid OrderId,
    decimal TotalAmount)
    : IIntegrationEvent;
}