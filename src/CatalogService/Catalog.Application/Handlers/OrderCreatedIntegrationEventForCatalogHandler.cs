using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using Ecommerce.Catalog.Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Ecommerce.Catalog.Application.EventsHandlers
{
    public class OrderCreatedIntegrationEventForCatalogHandler : IIntegrationEventHandler<OrderCreatedIntegrationEventForCatalog>
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;
        public OrderCreatedIntegrationEventForCatalogHandler(IMediator mediator,ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(OrderCreatedIntegrationEventForCatalog @event)
        {
            _logger.LogInformation("Executing handler for {EventType}", typeof(OrderCreatedIntegrationEventForCatalog));
            _logger.LogInformation("Items count: {Count}", @event.OrderItems?.Count);
            if (@event.OrderItems == null || !@event.OrderItems.Any())
            {
                _logger.LogWarning("OrderItems is null or empty in OrderCreatedIntegrationEventForCatalog");
                throw new InvalidOperationException("OrderItems is null or empty in OrderCreatedIntegrationEventForCatalog");
            }
            var cmd = new ReduceInventoryCommand(@event.OrderItems);
            var result = await _mediator.Send(cmd); // MediatR command for each product
            if (!result.IsSuccess)
            {
                // log error
                _logger.LogError(new Exception(result.Error), "Failed to reduce inventory: {Error}");
                // optionally publish an event for compensation or retry
            }
            else
            {
                _logger.LogInformation("Executed handler for {EventType}", typeof(OrderCreatedIntegrationEventForCatalog));
            }
        }
    }
}