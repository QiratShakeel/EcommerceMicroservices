using MediatR;
using Ecommerce.Orders.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Orders.Domain.Events;
using System.Threading;

namespace Ecommerce.Orders.Infrastructure.Messaging.EventHandlers
{
    public class OrderCreatedEventHandler
        : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public OrderCreatedEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(
            OrderCreatedDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var integrationEvent =
                new OrderCreatedIntegrationEvent(
                    notification.OrderId,
                    notification.TotalAmount);

            await _outbox.PublishAsync(integrationEvent);
        }
    }
}