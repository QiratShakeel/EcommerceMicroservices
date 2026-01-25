using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Orders.Tests.Infrastructure.Persistence
{
    public class OrdersDbContextTests : IClassFixture<OrdersDbContextFixture>
    {
        private readonly OrdersDbContext _context;

        public OrdersDbContextTests(OrdersDbContextFixture fixture)
        {
            _context = fixture.DbContext;
        }

        [Fact]
        public async Task ShouldHaveSeededOrders()
        {
            var count = await _context.Orders.CountAsync();
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task ShouldHaveSeededOutboxMessages()
        {
            var count = await _context.OutboxMessages.CountAsync();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task AddOrder_ShouldPersistSuccessfully()
        {
            var order = new Domain.Aggregates.OrderEntity(System.Guid.NewGuid());
            order.AddItem(System.Guid.NewGuid(), 100m, 1);
            order.Confirm();

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var saved = await _context.Orders.FindAsync(order.Id);
            Assert.NotNull(saved);
            Assert.Equal(order.Total, saved.Total);
        }
    }
}
