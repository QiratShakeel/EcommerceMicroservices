using MediatR;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.Domain.Events;
using System.Threading;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;

namespace Ecommerce.Payment.Infrastructure.Messaging.EventHandlers
{
    public class PaymentSucceededDomainEventHandler: INotificationHandler<PaymentSucceededDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public PaymentSucceededDomainEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(PaymentSucceededDomainEvent notification,CancellationToken cancellationToken)
        {
            await _outbox.PublishAsync(new PaymentSucceededIntegrationEvent(notification.orderId), cancellationToken);
        }
    }
}