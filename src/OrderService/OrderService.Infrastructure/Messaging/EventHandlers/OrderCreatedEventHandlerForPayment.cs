//using MediatR;
//using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
//using BuildingBlocks.Shared.Outbox;
//using Ecommerce.Orders.Domain.Events;
//using System.Threading;

//namespace Ecommerce.Orders.Infrastructure.Messaging.EventHandlers
//{
//    public class OrderCreatedEventHandlerForPayment : INotificationHandler<OrderCreatedDomainEvent>
//    {
//        private readonly IOutboxPublisher _outbox;

//        public OrderCreatedEventHandlerForPayment(IOutboxPublisher outbox)
//        {
//            _outbox = outbox;
//        }

//        public async Task Handle(OrderCreatedDomainEvent notification,CancellationToken cancellationToken)
//        {
//            var integrationEvent = new OrderCreatedIntegrationEventForPayment(notification.OrderId,notification.CustomerId,notification.TotalAmount);

//            await _outbox.PublishAsync(integrationEvent);
//        }
//    }
//}