using Ecommerce.Payment.Domain.Aggregates;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;
using Ecommerce.Payment.Application.Interfaces;
using MediatR;
using System.Threading;

namespace Ecommerce.Payment.Application.Commands
{

    public class ProcessPaymentCommandHandler
    : IRequestHandler<ProcessPaymentCommand, Result>
    {
        private readonly IPaymentRepository _repository;

        public ProcessPaymentCommandHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ProcessPaymentCommand request,CancellationToken cancellationToken)
        {
            var payment = new PaymentEntity(request.OrderId, request.Amount);

            // Simulate payment gateway
            var success = Random.Shared.Next(0, 10) > 2;

            if (success)
                payment.MarkAsCompleted();
            else
                payment.MarkAsFailed("Insufficient funds");

            await _repository.AddAsync(payment);

            return Result.Success();
        }
    }
}