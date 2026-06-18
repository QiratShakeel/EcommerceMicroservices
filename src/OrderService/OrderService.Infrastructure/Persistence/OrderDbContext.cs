using Microsoft.EntityFrameworkCore;
using Ecommerce.Orders.Domain.Aggregates;
using BuildingBlocks.Shared.Outbox;
namespace Ecommerce.Orders.Infrastructure.Persistence.Context
{
    public class OrdersDbContext : DbContext, IOutboxDbContext
    {
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options) { }

        public async Task AddMessageAsync(OutboxMessage message, CancellationToken cancellation)
        {
            await OutboxMessages.AddAsync(message, cancellation);
            await SaveChangesAsync();
        }
        public async Task<List<OutboxMessage>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken ct)
        {
            return await OutboxMessages.Where(x => x.ProcessedOn == null).OrderBy(x => x.OccurredOn).Take(batchSize).ToListAsync(ct);
        }
        public async Task MarkAsProcessedAsync(OutboxMessage message, CancellationToken ct)
        {
            message.ProcessedOn = DateTime.UtcNow;
            await SaveChangesAsync(ct);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("orders");
            // Apply Fluent API configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);
        }
    }
}