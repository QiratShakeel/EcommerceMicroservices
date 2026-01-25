using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace BuildingBlocks.Shared.Exceptions
{
    public static class ExceptionExtension
    {
        public static IServiceCollection AddSharedExceptions(this IServiceCollection services)
        {
            //services.AddTransient<GlobalExceptionMiddleware>();
            return services;
        }

        public static IApplicationBuilder UseSharedExceptions(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
