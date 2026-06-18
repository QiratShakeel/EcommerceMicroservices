using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Identity.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ApplicationExtensions).Assembly);
            services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);
            return services;
        }
    }
}
