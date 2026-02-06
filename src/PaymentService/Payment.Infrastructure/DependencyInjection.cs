using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.Application.Interfaces;
using Ecommerce.Payment.Infrastructure.Persistence.Context;
using Ecommerce.Payment.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Payment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            // DbContext
            services.AddDbContext<PaymentsDbContext>(options =>
            {
                options.UseSqlServer(
                    config.GetConnectionString("PaymentConnection"),
                    sql =>
                    {
                        sql.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });
            });
            
            services.AddHostedService<RabbitMQConsumer>();
            services.AddScoped<IUnitOfWork, UnitOfWork<PaymentsDbContext>>();
            services.AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<PaymentsDbContext>());
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
//builder.Services.AddInfrastructure(builder.Configuration);
