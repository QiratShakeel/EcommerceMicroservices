using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Behaviors.Validation;
using BuildingBlocks.Shared.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Shared.Extensions
{
    public static class BehaviorExtensions
    {
        public static IServiceCollection AddSharedBehaviors(this IServiceCollection services)
        {
            services.AddTransient<DomainEventDispatcher>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            return services;
        }
    }
}

//Validation → Logging → Transaction → Handler mediatr pipeline run