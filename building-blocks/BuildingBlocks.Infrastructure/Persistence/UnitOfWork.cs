using BuildingBlocks.Shared.Behaviors.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Shared.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable
        where TContext : DbContext
    {
        private readonly TContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null) return; // Already started
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                // Log the full details
                Console.WriteLine("DbUpdateException: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                throw; // rethrow so you can see it in API response
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        public async Task CommitAsync(DomainEventDispatcher dispatcher)
        {
            try
            {
                // 1️⃣ Get tracked entities with domain events
                var entitiesWithEvents = _context.ChangeTracker.Entries<Entity>().Select(e => e.Entity).Where(e => e.DomainEvents.Any()).ToList();
                //var allEvents = entitiesWithEvents.SelectMany(e => e.DomainEvents).ToList();
                // 2️⃣ Save changes first
                await SaveChangesAsync();

                // 3️⃣ Dispatch domain events
                if (dispatcher != null)
                    await dispatcher.DispatchEventsAsync(entitiesWithEvents);

                // 4️⃣ Commit transaction
                if (_transaction != null)
                    await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }


        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
