using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
namespace BuildingBlocks.Shared.Outbox
{
    public interface IOutboxDbContext
    {
        //DbSet<OutboxMessage> OutboxMessages { get; }
        //IEnumerable<OutboxMessage> OutboxMessages { get; } 
        Task AddMessageAsync(OutboxMessage message, CancellationToken cancellation); // async-friendly method
        //Task<List<OutboxMessage>> GetUnpublishedMessagesAsync();
        Task<List<OutboxMessage>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken);

        Task MarkAsProcessedAsync(OutboxMessage message,CancellationToken cancellationToken);
    }
}