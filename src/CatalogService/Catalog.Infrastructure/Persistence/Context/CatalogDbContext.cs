using Microsoft.EntityFrameworkCore;
using Ecommerce.Catalog.Domain.Entities;
using Ecommerce.Catalog.Domain.Aggregates;
using BuildingBlocks.Shared.Outbox;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Context 
{
    public class CatalogDbContext : DbContext, IOutboxDbContext
    {
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options)
    { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public async Task AddMessageAsync(OutboxMessage message)
    {
        await OutboxMessages.AddAsync(message);
        await SaveChangesAsync();
    }
    public async Task<List<OutboxMessage>> GetUnprocessedMessagesAsync(int batchSize,CancellationToken ct)
    {
        return await OutboxMessages.Where(x => x.ProcessedOn == null).OrderBy(x => x.OccurredOn).Take(batchSize).ToListAsync(ct);
    }
    public async Task MarkAsProcessedAsync(OutboxMessage message,CancellationToken ct)
    {
        message.ProcessedOn = DateTime.UtcNow;
        await SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            //modelBuilder.HasDefaultSchema("catalog");
            // Apply Fluent API configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
    }
}

