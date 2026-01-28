using BuildingBlocks.Shared.Infrastructure;
using MediatR;


namespace BuildingBlocks.Shared.Behaviors.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
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
                var response = await next();    // handler logic
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


//sabse phly validator run hta ha 