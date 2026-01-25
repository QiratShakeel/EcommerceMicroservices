//using Microsoft.EntityFrameworkCore;
//using BuildingBlocks.Shared.Outbox;

//public class OutboxDbContext : DbContext, IOutboxDbContext
//{
//    public DbSet<OutboxMessage> OutboxMessages { get; set; }

//    public OutboxDbContext(DbContextOptions<OutboxDbContext> options)
//        : base(options) { }

//    public async Task AddMessageAsync(OutboxMessage message)
//    {
//        await OutboxMessages.AddAsync(message);
//        await SaveChangesAsync();
//    }

//    //public async Task<List<OutboxMessage>> GetUnpublishedMessagesAsync()
//    //{
//    //    return await OutboxMessages
//    //        .Where(m => !m.Published)
//    //        .ToListAsync();
//    //}
//    public async Task<List<OutboxMessage>> GetUnprocessedMessagesAsync(
//        int batchSize,
//        CancellationToken ct)
//    {
//        return await OutboxMessages
//            .Where(x => x.ProcessedOn == null)
//            .OrderBy(x => x.OccurredOn)
//            .Take(batchSize)
//            .ToListAsync(ct);
//    }
//    public async Task MarkAsProcessedAsync(
//        OutboxMessage message,
//        CancellationToken ct)
//    {
//        message.ProcessedOn = DateTime.UtcNow;
//        await SaveChangesAsync(ct);
//    }
//}
