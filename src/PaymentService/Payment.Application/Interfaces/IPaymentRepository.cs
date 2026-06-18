using Ecommerce.Payment.Domain.Aggregates;
namespace Ecommerce.Payment.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(PaymentEntity payment);
        Task<bool> ExistsAsync(Guid orderId);

    }
}