using BuildingBlocks.EventBus.Abstractions;
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

        public RabbitMQEventBus(
            RabbitMQConnection connection,
            IOptions<EventBusOptions> options)
        {
            _connection = connection;
            _options = options.Value;
        }

        public async Task PublishAsync(IIntegrationEvent @event)
        {
            await using var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: _options.ExchangeName,
                type: ExchangeType.Topic,
                durable: true);

            var eventName = @event.GetType().Name;

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(@event));

            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(
                exchange: _options.ExchangeName,
                routingKey: eventName,
                mandatory: false,
                basicProperties: properties,
                body: body);
        }
    }
}
