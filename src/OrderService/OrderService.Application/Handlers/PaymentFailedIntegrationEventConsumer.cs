using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Results;
using Ecommerce.Orders.Application.Commands;
using MediatR;

namespace Ecommerce.Orders.Application.EventsHandlers
{
    public class PaymentFailedIntegrationEventConsumer : IIntegrationEventHandler<PaymentFailedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;
        public PaymentFailedIntegrationEventConsumer(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(PaymentFailedIntegrationEvent @event)
        {
            if(@event.OrderId == Guid.Empty)
            { 
                _logger.LogError(new Exception(), "Invalid PaymentSucceededIntegrationEvent data: {@Event}", @event);
                return;            
            }
            var cmd = new CancelOrderCommand(@event.OrderId, @event.Reason);
            var result = await _mediator.Send(cmd);
            if (!result.IsSuccess)
            {
                _logger.LogError(new Exception(result.Error), "Failed to cancel order for OrderId {OrderId}", @event.OrderId);
            }
            else
            {
                _logger.LogInformation("Successfully cancelled order for OrderId {OrderId}", @event.OrderId);
            }
        }
    }
}
