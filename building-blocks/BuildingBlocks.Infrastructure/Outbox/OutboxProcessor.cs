using BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace BuildingBlocks.Shared.Outbox
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxProcessor> _logger;

        public OutboxProcessor(
            IServiceScopeFactory scopeFactory,
            ILogger<OutboxProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessOutboxAsync(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task ProcessOutboxAsync(
            CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var dbContext =
                scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();

            var eventBus =
                scope.ServiceProvider.GetRequiredService<IEventBus>();

            //var messages = await dbContext.OutboxMessages
            //    .Where(x => x.ProcessedOn == null)
            //    .OrderBy(x => x.OccurredOn)
            //    .Take(20)
            //    .ToListAsync(cancellationToken);
            var messages = await dbContext
           .GetUnprocessedMessagesAsync(20, cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    var type = Type.GetType(message.Type)!;
                    var @event = JsonSerializer.Deserialize(
                        message.Content,
                        type) as IIntegrationEvent;

                    eventBus.PublishAsync(@event!);

                    //message.ProcessedOn = DateTime.UtcNow;
                    await dbContext.MarkAsProcessedAsync(message, cancellationToken);
                }
                catch (Exception ex)
                {
                    message.Error = ex.Message;
                    _logger.LogError(ex, "Outbox processing failed");
                }
            }

            //await ((DbContext)dbContext)
            //    .SaveChangesAsync(cancellationToken);
        }
    }
}