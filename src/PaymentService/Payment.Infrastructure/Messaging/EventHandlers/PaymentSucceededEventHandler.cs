using MediatR;
using Ecommerce.Payment.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.Domain.Events;
using System.Threading;

namespace Ecommerce.Payment.Infrastructure.Messaging.EventHandlers
{
    public class PaymentSucceededEventHandler
    : INotificationHandler<PaymentSucceededDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public PaymentSucceededEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(
            PaymentSucceededDomainEvent notification,
            CancellationToken cancellationToken)
        {
            await _outbox.PublishAsync(new PaymentSucceededIntegrationEvent(notification.orderId));
        }
    }
}