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
            // 1. Idempotency check
            if (await _repository.ExistsAsync(request.OrderId))
                return Result.Failure("Payment already processed for this order");

            var payment = new PaymentEntity(request.OrderId,request.CustomerId, request.Amount);
            var transaction = payment.AddTransaction(request.Amount, "Stripe", "txn_123");
            try
            {
                // Simulate payment gateway
                var success = Random.Shared.Next(0, 10) > 2;

                if (success)
                {
                    transaction.MarkSuccess();
                    payment.MarkAsCompleted();
                }
                else
                {
                    transaction.MarkFailed();
                    payment.MarkAsFailed("Transaction Failed");
                }

                await _repository.AddAsync(payment);

            }
            catch (Exception ex)
            {
                return Result.Failure($"Payment processing error: {ex.Message}");
            }
            
            return Result.Success();
        }
    }
}