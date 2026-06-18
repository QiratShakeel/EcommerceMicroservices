using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents;
using Ecommerce.Orders.Application.EventsHandlers;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Orders.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);
            services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
            // Register your event handlers as usual
            services.AddScoped<IIntegrationEventHandler<PaymentSucceededIntegrationEvent>, PaymentSucceededIntegrationEventConsumer>();
            services.AddScoped<IIntegrationEventHandler<PaymentFailedIntegrationEvent>, PaymentFailedIntegrationEventConsumer>();
            // Register EventTypeResolver with Catalog-specific mappings
            var mappings = new Dictionary<string, Type>
            {
                { "payment.succeeded", typeof(PaymentSucceededIntegrationEvent) },
                { "payment.failed", typeof(PaymentFailedIntegrationEvent) }
            };
            services.AddSingleton<IEventTypeResolver>(new EventTypeResolver(mappings));
            return services;
        }
    }
}
