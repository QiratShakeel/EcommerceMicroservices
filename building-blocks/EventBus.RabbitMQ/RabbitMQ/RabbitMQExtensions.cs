using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.EventBus.Abstractions;

namespace BuildingBlocks.EventBus.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure options from appsettings.json
            services.Configure<EventBusOptions>(configuration.GetSection("EventBus"));

            // Register RabbitMQ connection and bus
            services.AddSingleton<RabbitMQConnection>();
            services.AddSingleton<IEventBus, RabbitMQEventBus>();
            services.AddSingleton<SubscriptionManager>();

            // Hosted service for consumers
            services.AddHostedService<RabbitMQConsumer>();

            return services;
        }
    }
}
