using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Orders.Domain.Events;
using MediatR;
using System.Threading;

namespace Ecommerce.Orders.Infrastructure.Messaging.EventHandlers
{
    public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public OrderCreatedDomainEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new OrderCreatedIntegrationEvent ( notification.OrderId,  notification.CustomerId, notification.TotalAmount );

            await _outbox.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}