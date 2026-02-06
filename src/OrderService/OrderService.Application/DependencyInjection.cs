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

            return services;
        }
    }
}
