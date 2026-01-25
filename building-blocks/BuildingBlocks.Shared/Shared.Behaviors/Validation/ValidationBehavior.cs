using System.Threading;
using FluentValidation;
using MediatR;
namespace BuildingBlocks.Shared.Behaviors.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validator.Select(v => v.Validate(context)).SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Any())
                throw new ValidationException(failures);
            return await next();
        }
    }
}