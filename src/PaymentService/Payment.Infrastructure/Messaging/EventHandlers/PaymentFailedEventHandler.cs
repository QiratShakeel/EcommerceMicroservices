using MediatR;
using Ecommerce.Payment.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.Domain.Events;
using System.Threading;

namespace Ecommerce.Payment.Infrastructure.Messaging.EventHandlers
{
    public class PaymentFailedEventHandler
    : INotificationHandler<PaymentFailedDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public PaymentFailedEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(
            PaymentFailedDomainEvent notification,
            CancellationToken cancellationToken)
        {
            await _outbox.PublishAsync(
                new PaymentFailedIntegrationEvent(
                    notification.orderId,
                    notification.reason));
        }
    }
}