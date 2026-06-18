using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure.Dto;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BuildingBlocks.EventBus.RabbitMQ
{
    public sealed class RabbitMQEventBus : IEventBus
    {
        private readonly RabbitMQConnection _connection;
        private readonly EventBusOptions _options;
        private readonly ILoggerService _logger;

        public RabbitMQEventBus(RabbitMQConnection connection,IOptions<EventBusOptions> options, ILoggerService logger)
        {
            _connection = connection;
            _options = options.Value;
            _logger = logger;
        }

        public async Task PublishAsync(IIntegrationEvent @event)
        {
            await using var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: _options.ExchangeName,type: ExchangeType.Topic,durable: true);

            var eventName = @event.GetType().Name;

            if (!_options.RoutingKeys.TryGetValue(eventName, out var routingKey))
                throw new Exception($"Routing key not configured for event {eventName}");
            
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event, @event.GetType(), EventJsonOptions.Default));

            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(exchange: _options.ExchangeName,routingKey: routingKey,  mandatory: false,basicProperties: properties,body: body);
        }

    }
}
//var evt = new OrderCreatedIntegrationEventForCatalog
//{
//    OrderId = Guid.NewGuid(),
//    OrderItems = new List<CreateOrderItemDto>
//    {
//        new CreateOrderItemDto(Guid.NewGuid(), 5)
//    }
//};