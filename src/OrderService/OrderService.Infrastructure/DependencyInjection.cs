using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Ecommerce.Orders.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Orders.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            // DbContext
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("OrdersConnection")));

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork<OrdersDbContext>>();

            // Outbox
            services.AddScoped<IOutboxDbContext>(sp =>
                sp.GetRequiredService<OrdersDbContext>());

            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();

            // MediatR (domain events, handlers)
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
