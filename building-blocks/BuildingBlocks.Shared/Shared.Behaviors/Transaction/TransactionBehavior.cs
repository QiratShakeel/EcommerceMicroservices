using BuildingBlocks.Shared.Infrastructure;
using MediatR;

namespace BuildingBlocks.Shared.Behaviors.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly DomainEventDispatcher _dispatcher;
        public TransactionBehavior(IUnitOfWork uow, DomainEventDispatcher dispatcher)
        {
            _uow = uow;
            _dispatcher = dispatcher;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var response = await next();
                await _uow.CommitAsync(_dispatcher);
                return response;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}