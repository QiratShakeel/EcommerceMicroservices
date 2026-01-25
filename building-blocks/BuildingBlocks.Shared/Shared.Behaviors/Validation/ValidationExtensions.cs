//using FluentValidation;
//using Microsoft.Extensions.DependencyInjection;
//using MediatR;
//namespace BuildingBlocks.Shared.Behaviors.Validation
//{
//    public static class ValidationExtension
//    {
//        public static IServiceCollection AddSharedValidation(this IServiceCollection services)
//        {
//            services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
//            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//            return services;
//        }
//    }
//}

