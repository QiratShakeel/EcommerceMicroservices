using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BuildingBlocks.Shared.Behaviors.Logging
{
    public static class LoggingServiceExtensions
    {
        public static IServiceCollection AddSharedLogging(this IServiceCollection services)
        {
            // Configure Serilog globally if not already
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddSingleton(Log.Logger);
            services.AddSingleton<ILoggerService, SerilogLoggerService>();

            return services;
        }
    }
}


//builder.Services.AddSharedLogging();  //use in any microservices 

