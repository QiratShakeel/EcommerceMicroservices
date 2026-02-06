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
            // Execution Strategy ke saath Unit of Work ka Commit use karein
            return await _uow.ExecuteWithTransactionAsync(async () =>
            {
                var response = await next(); // Business logic (Handler)
                return response;
            }, _dispatcher);
        }
    }
}


//sabse phly validator run hta ha 