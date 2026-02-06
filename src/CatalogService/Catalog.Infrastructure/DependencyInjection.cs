using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Catalog.Application.EventsHandlers;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Ecommerce.Catalog.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            // DbContext
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseSqlServer(
                    config.GetConnectionString("CatalogConnection"),
                    sql =>
                    {
                        sql.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });
            });
            
            services.AddHostedService<RabbitMQConsumer>();
            services.AddScoped<IUnitOfWork, UnitOfWork<CatalogDbContext>>();
            services.AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<CatalogDbContext>());
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
//builder.Services.AddInfrastructure(builder.Configuration);
