using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Payment.Domain.Entities;
using Ecommerce.Payment.Domain.Enums;
using Ecommerce.Payment.Domain.Events;

namespace Ecommerce.Payment.Domain.Aggregates
{
    public class PaymentEntity : Entity, IAggregateRoot
    {
        private readonly List<Transaction> _transactions = new();

        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        private PaymentEntity() { } // EF Core

        public PaymentEntity(Guid orderId, decimal amount)
        {
            OrderId = orderId;
            Amount = amount;
            Status = PaymentStatus.Pending;
            AddDomainEvent(new PaymentCreatedDomainEvent(Id, OrderId));
        }

        public Transaction AddTransaction(decimal amount, string provider, string referenceId)
        {
            var transaction = new Transaction(amount, provider, referenceId);
            _transactions.Add(transaction);
            return transaction;
        }

        public void MarkAsCompleted()
        {
            Status = PaymentStatus.Completed;
            AddDomainEvent(new PaymentSucceededDomainEvent(OrderId));
        }

        public void MarkAsFailed(string error)
        {
            Status = PaymentStatus.Failed;
            AddDomainEvent(new PaymentFailedDomainEvent(OrderId, error));
        }
    }

}