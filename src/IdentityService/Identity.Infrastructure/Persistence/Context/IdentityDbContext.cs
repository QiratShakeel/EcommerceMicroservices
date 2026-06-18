using Microsoft.EntityFrameworkCore;
using Ecommerce.Identity.Domain.Aggregates;
using BuildingBlocks.Shared.Outbox;
using OpenIddict.EntityFrameworkCore.Models; // ✅ Required for OpenIddict
using OpenIddict.EntityFrameworkCore; // ✅ Required for UseOpenIddict()


namespace Ecommerce.Identity.Infrastructure.Persistence.Context 
{
    public class IdentityDbContext : DbContext, IOutboxDbContext
    {
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public async Task AddMessageAsync(OutboxMessage message, CancellationToken cancellation)
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
            modelBuilder.HasDefaultSchema("identity");
            // Apply Fluent API configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
            // ✅ Add OpenIddict tables
            modelBuilder.UseOpenIddict();
        }
    }
}

