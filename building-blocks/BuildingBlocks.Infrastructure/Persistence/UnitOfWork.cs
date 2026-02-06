using BuildingBlocks.Shared.Behaviors.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Shared.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context) => _context = context;

        public async Task<TResult> ExecuteWithTransactionAsync<TResult>(Func<Task<TResult>> action, DomainEventDispatcher dispatcher)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                // Har retry par nayi transaction start hogi
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var result = await action(); // Handler logic run hogi

                    // 1. Get Domain Events
                    var entitiesWithEvents = _context.ChangeTracker.Entries<Entity>()
                        .Select(e => e.Entity)
                        .Where(e => e.DomainEvents.Any())
                        .ToList();

                    // 2. Save DB Changes
                    await _context.SaveChangesAsync();

                    // 3. Dispatch Events (In-process)
                    if (dispatcher != null && entitiesWithEvents.Any())
                        await dispatcher.DispatchEventsAsync(entitiesWithEvents);

                    // 4. Final Commit
                    await transaction.CommitAsync();
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        // Baki methods (Rollback/Dispose) wese hi rahen ge
    }
}
