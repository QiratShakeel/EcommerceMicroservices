using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Behaviors.Logging;

namespace BuildingBlocks.EventBus.RabbitMQ
{
    public sealed class RabbitMQConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RabbitMQConnection _connection;
        private readonly EventBusOptions _options;
        private readonly IEventTypeResolver _resolver;
        private readonly ILoggerService _logger;
        private IChannel? _channel;

        // 🔑 EventName → EventType map
        //private static readonly Dictionary<string, Type> _eventTypes = new()
        //{
        //    { nameof(OrderCreatedIntegrationEvent), typeof(OrderCreatedIntegrationEvent) }
        //    // future events yahan add karna
        //};

        public RabbitMQConsumer(IServiceScopeFactory scopeFactory,RabbitMQConnection connection,IOptions<EventBusOptions> options, IEventTypeResolver resolver, ILoggerService logger)
        {
            _scopeFactory = scopeFactory;
            _connection = connection;
            _options = options.Value;
            _resolver = resolver;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(exchange: _options.ExchangeName,type: ExchangeType.Topic,durable: true);

            // 👇 Consumer-specific queue
            //var queueName = _options.QueueName;
            foreach (var sub in _options.Subscriptions)
            {
                await _channel.QueueDeclareAsync(queue: sub.QueueName, durable:true, exclusive:false, autoDelete:false);
                await _channel.QueueBindAsync(queue:sub.QueueName,exchange:_options.ExchangeName,routingKey:sub.EventName);
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += OnMessageReceived;

            foreach (var sub in _options.Subscriptions)
            {
                await _channel.BasicConsumeAsync(queue: sub.QueueName, autoAck: false, consumer: consumer);
            }
        }

        private async Task OnMessageReceived(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var eventName = args.RoutingKey;

                //if (!_eventTypes.TryGetValue(eventName, out var eventType))
                //    throw new Exception($"Unknown event: {eventName}");
                var eventType = _resolver.Resolve(eventName);

                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                _logger.LogInformation("RabbitmqConsumer: Received message: {eventName} | {eventType} |{Message}", eventName, eventType ,message);
                var @event = JsonSerializer.Deserialize(message, eventType, EventJsonOptions.Default)!;
                _logger.LogInformation("RabbitmqConsumer: Deserialized event: {@event}", @event);

                using var scope = _scopeFactory.CreateScope();

                var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                _logger.LogInformation($"RabbitmqConsumer: handlerType {handlerType}");
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
                await handler.Handle((dynamic)@event);

                await _channel!.BasicAckAsync(args.DeliveryTag, false);
            }
            catch
            {
                await _channel!.BasicNackAsync(args.DeliveryTag, false, false);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel is not null)
                await _channel.CloseAsync();

            await base.StopAsync(cancellationToken);
        }
    }
}
