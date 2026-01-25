using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.EventBus.Abstractions;

namespace BuildingBlocks.Shared.Outbox
{
    public static class OutboxExtensions
    {
        public static IServiceCollection AddOutbox(this IServiceCollection services)
        {
            services.AddScoped<IOutboxPublisher, OutboxPublisher>();
            //builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
            services.AddHostedService<OutboxProcessor>();

            return services;
        }
    }
}
