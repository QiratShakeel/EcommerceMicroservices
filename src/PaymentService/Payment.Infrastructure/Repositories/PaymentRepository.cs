using Ecommerce.Payment.Domain.Aggregates;
using Ecommerce.Payment.Application.Interfaces;
using Ecommerce.Payment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentsDbContext _context;

        public PaymentRepository(PaymentsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PaymentEntity payment)
            => await _context.Payments.AddAsync(payment);
        public async Task<bool> ExistsAsync(Guid orderId)
        {
            return await _context.Payments.AnyAsync(p => p.OrderId == orderId);
        }
    }
}