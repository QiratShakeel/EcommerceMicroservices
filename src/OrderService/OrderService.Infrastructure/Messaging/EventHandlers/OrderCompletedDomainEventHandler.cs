using BuildingBlocks.Shared.Infrastructure.Dto;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Orders.Domain.Events;
using MediatR;

namespace Ecommerce.Orders.Infrastructure.Messaging.EventHandlers
{
    public class OrderCompletedDomainEventHandler : INotificationHandler<OrderCompletedDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;
        public OrderCompletedDomainEventHandler(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }
        public async Task Handle(OrderCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var orderItemDto = notification.Items.Select(i=> new CreateOrderItemDto(i.ProductId, i.Quantity)).ToList();
            var integrationEvent = new OrderCompletedIntegrationEvent(orderItemDto);
            await _outbox.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}