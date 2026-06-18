using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using Ecommerce.Payment.Application.Commands;
using MediatR;

namespace Ecommerce.Payment.Application.EventsHandlers
{
    public class OrderCreatedIntegrationEventConsumer : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;

        public OrderCreatedIntegrationEventConsumer(IMediator mediator,ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            if (@event.OrderId == Guid.Empty || @event.CustomerId == Guid.Empty || @event.TotalAmount <= 0)
            {
                _logger.LogError(new Exception(), "Invalid OrderCreatedIntegrationEvent data: {@Event}", @event);
                return;
            }
            var cmd = new ProcessPaymentCommand(@event.OrderId, @event.CustomerId, @event.TotalAmount);
            var result = await _mediator.Send(cmd);
            if (!result.IsSuccess)
            {
                _logger.LogError(new Exception(result.Error), "Failed to process payment for OrderId {OrderId}", @event.OrderId);
            }
            else
            {
                _logger.LogInformation("Successfully processed payment for OrderId {OrderId}", @event.OrderId);
            }
        }
    }
}