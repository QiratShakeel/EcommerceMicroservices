using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Payment.Domain.Enums;

namespace Ecommerce.Payment.Domain.Entities
{
    public class Transaction : Entity
    {
        public decimal Amount { get; private set; }
        public string Provider { get; private set; }  // Stripe, PayPal, etc.
        public string ReferenceId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public TransactionStatus Status { get; private set; }

        private Transaction() { } // EF

        public Transaction(decimal amount, string provider, string referenceId)
        {
            Amount = amount;
            Provider = provider;
            ReferenceId = referenceId;
            CreatedAt = DateTime.UtcNow;
            Status = TransactionStatus.Pending;
        }

        public void MarkSuccess() => Status = TransactionStatus.Success;
        public void MarkFailed() => Status = TransactionStatus.Failed;
    }

}