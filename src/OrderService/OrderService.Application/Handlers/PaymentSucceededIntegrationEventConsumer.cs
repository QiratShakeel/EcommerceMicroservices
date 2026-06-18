using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Results;
using Ecommerce.Orders.Application.Commands;
using MediatR;

namespace Ecommerce.Orders.Application.EventsHandlers
{
    public class PaymentSucceededIntegrationEventConsumer : IIntegrationEventHandler<PaymentSucceededIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;
        public PaymentSucceededIntegrationEventConsumer(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(PaymentSucceededIntegrationEvent @event)
        {
            if(@event.OrderId == Guid.Empty)
            { 
                _logger.LogError(new Exception(), "Invalid PaymentSucceededIntegrationEvent data: {@Event}", @event);
                return;            
            }
            var cmd = new CompleteOrderCommand(@event.OrderId);
            var result = await _mediator.Send(cmd);
            if (!result.IsSuccess)
            {
                _logger.LogError(new Exception(result.Error), "Failed to complete order for OrderId {OrderId}", @event.OrderId);
            }
            else
            {
                _logger.LogInformation("Successfully completed order for OrderId {OrderId}", @event.OrderId);
            }
        }
    }
}
