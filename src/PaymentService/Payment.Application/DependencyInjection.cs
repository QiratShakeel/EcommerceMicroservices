using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Payment.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);
            //services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
            // Register your event handlers as usual
            //services.AddScoped<IIntegrationEventHandler<OrderCreatedIntegrationEventForPayment>, OrderCreatedIntegrationEventForPaymentHandler>();
            // Register EventTypeResolver with Catalog-specific mappings
            var mappings = new Dictionary<string, Type>
            {
                { "order.created.payment", typeof(OrderCreatedIntegrationEventForPayment) }
            };
            services.AddSingleton<IEventTypeResolver>(new EventTypeResolver(mappings));
            return services;
        }
    }
}
