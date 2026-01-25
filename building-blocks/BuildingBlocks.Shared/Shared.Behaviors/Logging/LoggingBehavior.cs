using MediatR;

namespace BuildingBlocks.Shared.Behaviors.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILoggerService _logger;

        public LoggingBehavior(ILoggerService logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            _logger.LogInformation("Handling {RequestName} {@Request}", typeof(TRequest).Name, request);
            var response = await next();
            _logger.LogInformation("Handled {RequestName} {@Response}", typeof(TRequest).Name, response);
            return response;
        }
    }

}
