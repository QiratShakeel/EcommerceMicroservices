using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Ecommerce.Orders.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Orders.Tests.Infrastructure.Persistence.Repositories
{
    public class OrderRepositoryTests : IClassFixture<OrdersDbContextFixture>
    {
        private readonly OrdersDbContext _context;
        private readonly OrderRepository _repository;

        public OrderRepositoryTests(OrdersDbContextFixture fixture)
        {
            _context = fixture.DbContext;
            _repository = new OrderRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddOrder()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            order.AddItem(Guid.NewGuid(), 100m, 2);
            order.AddItem(Guid.NewGuid(), 50m, 1);
            order.Confirm();

            // Act
            await _repository.AddAsync(order);
            await _context.SaveChangesAsync();

            // Assert
            var saved = await _context.Orders.Include(o => o.OrderItems)
                                             .FirstOrDefaultAsync(o => o.Id == order.Id);
            Assert.NotNull(saved);
            Assert.Equal(2, saved.OrderItems.Count);
            Assert.Equal(order.Total, saved.Total);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrderWithItems()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid());
            order.AddItem(Guid.NewGuid(), 75m, 3);
            order.Confirm();

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Act
            var fetched = await _repository.GetByIdAsync(order.Id);

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal(order.Id, fetched.Id);
            Assert.Equal(order.OrderItems.Count, fetched.OrderItems.Count);
            Assert.Equal(order.Total, fetched.Total);
        }

        [Fact]
        public async Task GetByIdAsync_WhenOrderDoesNotExist_ShouldReturnNull()
        {
            // Act
            var result = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
