using MediatR;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.Domain.Events;
using System.Threading;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;

namespace Ecommerce.Payment.Infrastructure.Messaging.EventHandlers
{
    public class PaymentFailedDomainEventHandler
    : INotificationHandler<PaymentFailedDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public PaymentFailedDomainEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(PaymentFailedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _outbox.PublishAsync(new PaymentFailedIntegrationEvent(notification.orderId,notification.reason), cancellationToken);
        }
    }
}