using BuildingBlocks.EventBus.Abstractions;
using System.Text.Json;

namespace BuildingBlocks.Shared.Outbox
{
    public class OutboxPublisher : IOutboxPublisher
    {
        private readonly IOutboxDbContext _context;

        public OutboxPublisher(IOutboxDbContext context)
        {
            _context = context;
        }
        
        public async Task PublishAsync(IIntegrationEvent @event, CancellationToken cancellation)
        {
            var message = new OutboxMessage
            {
                Type = @event.GetType().AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(@event,@event.GetType(), EventJsonOptions.Default)
            };

            await _context.AddMessageAsync(message, cancellation);
        }
    }
}