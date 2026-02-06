using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Dto;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Orders.Domain.Events;
using MediatR;
using System.Threading;

namespace Ecommerce.Orders.Infrastructure.Messaging.EventHandlers
{
    public class OrderCreatedEventHandlerForCatalog : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IOutboxPublisher _outbox;

        public OrderCreatedEventHandlerForCatalog(IOutboxPublisher outbox)
        {
            _outbox = outbox;
        }

        public async Task Handle(OrderCreatedDomainEvent notification,CancellationToken cancellationToken)
        {
            var itemsDto = notification.OrderItems.Select(i => new CreateOrderItemDto(i.ProductId,i.Quantity)).ToList();
            var integrationEvent = new OrderCreatedIntegrationEventForCatalog { OrderId = notification.OrderId, OrderItems=itemsDto };

            await _outbox.PublishAsync(integrationEvent);
        }
    }
}