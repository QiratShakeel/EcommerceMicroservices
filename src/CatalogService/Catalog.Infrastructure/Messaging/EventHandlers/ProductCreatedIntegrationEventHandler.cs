using MediatR;
using Ecommerce.Catalog.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Catalog.Domain.Events;
using System.Threading;

namespace Ecommerce.Catalog.Infrastructure.Messaging.EventHandlers
{
    public class ProductCreatedIntegrationEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly IOutboxPublisher _outboxPublisher;
        public ProductCreatedIntegrationEventHandler(IOutboxPublisher outboxPublisher)
        {
            _outboxPublisher = outboxPublisher;
        }

        public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken ct)
        {
            var integrationEvent = new ProductCreatedIntegrationEvent(notification.ProductId, notification.Name, notification.SKU);  // Convert Domain Event to Integration Event
            await _outboxPublisher.PublishAsync(integrationEvent); // Publish the Integration Event to a message broker (e.g., RabbitMQ or Kafka)
        }
    }
}