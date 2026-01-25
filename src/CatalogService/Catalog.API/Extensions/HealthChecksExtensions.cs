//namespace Ecommerce.Catalog.API.Extensions
//{
//    public static class HealthChecksExtensions
//    {
//        public static IServiceCollection AddAppHealthChecks(
//            this IServiceCollection services,
//            IConfiguration configuration)
//        {
//            services.AddHealthChecks()
//                .AddNpgSql(
//                    configuration.GetConnectionString("DefaultConnection"),
//                    name: "postgres");

//            return services;
//        }
//    }
//}

////builder.Services.AddAppHealthChecks(builder.Configuration);

////app.MapHealthChecks("/health");