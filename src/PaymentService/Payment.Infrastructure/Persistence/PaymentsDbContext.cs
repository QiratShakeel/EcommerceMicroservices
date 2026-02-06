using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace Ecommerce.Payment.Infrastructure.Persistence.Context
{
    public class PaymentsDbContext : DbContext, IOutboxDbContext
    {
        private IDbContextTransaction _transaction;

        public DbSet<PaymentEntity> Payments => Set<PaymentEntity>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
            : base(options) { }

        public async Task AddMessageAsync(OutboxMessage message)
        {
            await OutboxMessages.AddAsync(message);
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
            modelBuilder.HasDefaultSchema("payment");
            // Apply Fluent API configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentsDbContext).Assembly);
        }
    }
}