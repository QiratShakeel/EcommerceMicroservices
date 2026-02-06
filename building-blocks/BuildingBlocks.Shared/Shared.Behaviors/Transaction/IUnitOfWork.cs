using BuildingBlocks.Shared.Infrastructure;

namespace BuildingBlocks.Shared.Behaviors.Transaction
{
    public interface IUnitOfWork
    {
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //Task BeginTransactionAsync();
        //Task CommitAsync(DomainEventDispatcher dispatcher);
        //Task RollbackAsync();
        Task<TResult> ExecuteWithTransactionAsync<TResult>(Func<Task<TResult>> action, DomainEventDispatcher dispatcher);
    }

}