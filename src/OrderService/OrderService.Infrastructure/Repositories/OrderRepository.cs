using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _context;

        public OrderRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderEntity order)
        { await _context.Orders.AddAsync(order); }

        public async Task<OrderEntity?> GetByIdAsync(Guid id)
        { return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id); }
    }
}