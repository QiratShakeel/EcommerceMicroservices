using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using Ecommerce.Catalog.Application.Commands;
using MediatR;

namespace Ecommerce.Catalog.Application.EventsHandlers
{
    public class OrderCompletedIntegrationEventConsumer : IIntegrationEventHandler<OrderCompletedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;
        public OrderCompletedIntegrationEventConsumer(IMediator mediator,ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(OrderCompletedIntegrationEvent @event)
        {
            if (@event.OrderItems == null || !@event.OrderItems.Any())
            {
                _logger.LogWarning("OrderItems is null or empty in OrderCreatedIntegrationEventForCatalog");
                return;
            }
            //if (await _inventoryRepo.IsAlreadyProcessed(@event.OrderId))
            //    return;
            var cmd = new ReduceInventoryCommand(@event.OrderItems);
            var result = await _mediator.Send(cmd);             if (!result.IsSuccess)
            {
                _logger.LogError(new Exception(result.Error), "Failed to reduce inventory: {Error}");
            }
            else
            {
                _logger.LogInformation("Reduced Inventory for {EventType}", typeof(OrderCompletedIntegrationEvent));
            }
        }
    }
}