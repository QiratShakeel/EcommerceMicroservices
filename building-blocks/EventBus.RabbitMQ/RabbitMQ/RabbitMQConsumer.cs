//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;
//using System.Text.Json;

//namespace BuildingBlocks.EventBus.RabbitMQ
//{
//    public sealed class RabbitMQConsumer : BackgroundService
//    {
//        private readonly IServiceScopeFactory _scopeFactory;
//        private readonly RabbitMQConnection _connection;
//        private readonly EventBusOptions _options;

//        private IChannel? _channel;

//        public RabbitMQConsumer(
//            IServiceScopeFactory scopeFactory,
//            RabbitMQConnection connection,
//            IOptions<EventBusOptions> options)
//        {
//            _scopeFactory = scopeFactory;
//            _connection = connection;
//            _options = options.Value;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _channel = await _connection.CreateChannelAsync();

//            await _channel.ExchangeDeclareAsync(
//                exchange: _options.ExchangeName,
//                type: ExchangeType.Topic,
//                durable: true);

//            var queueName = "orders.payment.queue";
//            var retryQueue = "orders.payment.retry";
//            var dlqQueue = "orders.payment.dlq";

//            // DLQ
//            await _channel.QueueDeclareAsync(dlqQueue, true, false, false);
//            await _channel.QueueBindAsync(dlqQueue, _options.ExchangeName, dlqQueue);

//            // Retry Queue
//            await _channel.QueueDeclareAsync(
//                retryQueue,
//                durable: true,
//                exclusive: false,
//                autoDelete: false,
//                arguments: new Dictionary<string, object>
//                {
//                    { "x-dead-letter-exchange", _options.ExchangeName },
//                    { "x-message-ttl", 5000 }
//                });

//            // Main Queue
//            await _channel.QueueDeclareAsync(
//                queueName,
//                durable: true,
//                exclusive: false,
//                autoDelete: false,
//                arguments: new Dictionary<string, object>
//                {
//                    { "x-dead-letter-exchange", _options.ExchangeName },
//                    { "x-dead-letter-routing-key", retryQueue }
//                });

//            await _channel.QueueBindAsync(
//                queueName,
//                _options.ExchangeName,
//                nameof(OrderCreatedIntegrationEvent));

//            var consumer = new AsyncEventingBasicConsumer(_channel);
//            consumer.ReceivedAsync += OnMessageReceived;

//            await _channel.BasicConsumeAsync(
//                queue: queueName,
//                autoAck: false,
//                consumer: consumer);
//        }

//        private async Task OnMessageReceived(
//            object sender,
//            BasicDeliverEventArgs args)
//        {
//            try
//            {
//                var message = Encoding.UTF8.GetString(args.Body.ToArray());
//                var @event = JsonSerializer.Deserialize<OrderCreatedIntegrationEvent>(message);

//                using var scope = _scopeFactory.CreateScope();
//                var handler = scope.ServiceProvider
//                    .GetRequiredService<IIntegrationEventHandler<OrderCreatedIntegrationEvent>>();

//                await handler.Handle(@event!);

//                await _channel!.BasicAckAsync(args.DeliveryTag, false);
//            }
//            catch
//            {
//                await _channel!.BasicNackAsync(args.DeliveryTag, false, false);
//            }
//        }

//        public override async Task StopAsync(CancellationToken cancellationToken)
//        {
//            if (_channel is not null)
//                await _channel.CloseAsync();

//            await base.StopAsync(cancellationToken);
//        }
//    }
//}
