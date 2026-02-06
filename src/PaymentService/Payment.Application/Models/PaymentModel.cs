using Ecommerce.Payment.Domain.Enums;

namespace Ecommerce.Payment.Application.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
