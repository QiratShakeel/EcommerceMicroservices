using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Infrastructure.Persistence.Context;
using Ecommerce.Identity.Infrastructure.Persistence.Repositories;
using Ecommerce.Identity.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // DbContext
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(
                    config.GetConnectionString("IdentityConnection"),
                    sql =>
                    {
                        sql.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork<IdentityDbContext>>();
            services.AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<IdentityDbContext>());
            services.AddScoped<IPasswordHasher, PasswordHasherService>();   
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}