using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BuildingBlocks.EventBus.RabbitMQ
{
    public sealed class RabbitMQConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RabbitMQConnection _connection;
        private readonly EventBusOptions _options;
        private readonly IEventTypeResolver _resolver;
        private readonly ILoggerService _logger;

        private readonly List<IChannel> _channels = new();
        private readonly Dictionary<IChannel, string> _consumerTags = new();
        public RabbitMQConsumer(IServiceScopeFactory scopeFactory,RabbitMQConnection connection,IOptions<EventBusOptions> options,IEventTypeResolver resolver,ILoggerService logger)
        {
            _scopeFactory = scopeFactory;
            _connection = connection;
            _options = options.Value;
            _resolver = resolver;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await StartConsumers(stoppingToken);

                    _logger.LogInformation("RabbitMQ Consumers started successfully.");

                    await Task.Delay(Timeout.Infinite,stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,"RabbitMQ consumer crashed. Restarting in 5 seconds.");

                    await DisposeChannels();

                    await Task.Delay(
                        TimeSpan.FromSeconds(5),
                        stoppingToken);
                }
            }
        }

        private async Task StartConsumers(CancellationToken token)
        {
            foreach (var sub in _options.Subscriptions)
            {
                var channel = await _connection.CreateChannelAsync();

                _channels.Add(channel);

                await channel.ExchangeDeclareAsync(exchange: _options.ExchangeName,type: ExchangeType.Topic,durable: true);

                await channel.QueueDeclareAsync(queue: sub.QueueName,durable: true,exclusive: false,autoDelete: false);

                await channel.QueueBindAsync(queue: sub.QueueName,exchange: _options.ExchangeName,routingKey: sub.EventName);

                // Fair dispatch
                await channel.BasicQosAsync(prefetchSize: 0,prefetchCount: 1,global: false);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (_, args) =>
                {
                    await HandleMessage(args, channel);
                };

                var consumerTag = await channel.BasicConsumeAsync(queue: sub.QueueName,autoAck: false,consumer: consumer);
                _consumerTags[channel] = consumerTag;
                _logger.LogInformation("Consumer registered. Queue={Queue}, ConsumerTag={ConsumerTag}",sub.QueueName,consumerTag);
            }
        }

        private async Task HandleMessage(BasicDeliverEventArgs args,IChannel channel)
        {
            try
            {
                var routingKey = args.RoutingKey;

                var eventType = _resolver.Resolve(routingKey);

                if (eventType == null)
                {
                    throw new Exception(
                        $"No event type registered for routing key {routingKey}");
                }

                var message =Encoding.UTF8.GetString(args.Body.ToArray());

                var @event = JsonSerializer.Deserialize(message,eventType,EventJsonOptions.Default);

                if (@event == null)
                {
                    throw new Exception($"Failed to deserialize event {routingKey}");
                }

                using var scope = _scopeFactory.CreateScope();

                var handlerType =typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                dynamic handler =scope.ServiceProvider.GetRequiredService(handlerType);

                await handler.Handle((dynamic)@event);

                await channel.BasicAckAsync(deliveryTag: args.DeliveryTag,multiple: false);

                _logger.LogInformation("Message processed successfully. Event={Event}",routingKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Failed processing message {RoutingKey}",args.RoutingKey);

                // Retry storm avoid
                await channel.BasicRejectAsync(deliveryTag: args.DeliveryTag,requeue: false);
            }
        }

        private async Task DisposeChannels()
        {
            foreach (var item in _consumerTags)
            {
                try
                {
                    var channel = item.Key;
                    var consumerTag = item.Value;

                    if (channel.IsOpen)
                    {
                        await channel.BasicCancelAsync(consumerTag);

                        _logger.LogInformation(
                            "Consumer cancelled. ConsumerTag={ConsumerTag}",
                            consumerTag);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to cancel consumer.", ex);
                }
            }

            foreach (var channel in _channels)
            {
                try
                {
                    if (channel.IsOpen)
                    {
                        await channel.CloseAsync();
                    }
                    channel.Dispose();
                }
                catch
                {
                }
            }

            _consumerTags.Clear();
            _channels.Clear();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await DisposeChannels();

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            foreach (var channel in _channels)
            {
                channel.Dispose();
            }

            _channels.Clear();

            base.Dispose();
        }
    }
}