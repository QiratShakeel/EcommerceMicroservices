using System;
using System.Threading.Tasks;
using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders.Tests.Infrastructure.Persistence
{
    public class OrdersDbContextFixture : IDisposable
    {
        public OrdersDbContext DbContext { get; private set; }

        public OrdersDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase(databaseName: $"OrdersDb_{Guid.NewGuid()}")
                .Options;

            DbContext = new OrdersDbContext(options);

            // Seed initial data
            SeedData().GetAwaiter().GetResult();
        }

        private async Task SeedData()
        {
            var order1 = new OrderEntity(Guid.NewGuid());
            order1.AddItem(Guid.NewGuid(), 50m, 2);  // Total = 100
            order1.AddItem(Guid.NewGuid(), 25m, 1);  // Total = 125
            order1.Confirm();

            var order2 = new OrderEntity(Guid.NewGuid());
            order2.AddItem(Guid.NewGuid(), 100m, 1); // Total = 100
            order2.Confirm();

            await DbContext.Orders.AddRangeAsync(order1, order2);

            // Optionally seed OutboxMessages
            await DbContext.OutboxMessages.AddAsync(new BuildingBlocks.Shared.Outbox.OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                Type = "OrderCreated",
                Content = "{}"
            });

            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
