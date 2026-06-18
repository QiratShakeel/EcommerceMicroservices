using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Identity.API.Extensions;
using Ecommerce.Identity.API.Extensions;
using Ecommerce.Identity.Application;
using Ecommerce.Identity.Application;
using Ecommerce.Identity.Infrastructure;
using Ecommerce.Identity.Infrastructure.Persistence.Context;
using Ecommerce.Identity.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Polly;
using Polly.Retry;
using System;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//var jwtSettings = builder.Configuration.GetSection("JwtSettings");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(jwtSettings["Key"]))
//    };
//});


//---------------openid
builder.Services.AddOpenIddict()

    // Core
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<IdentityDbContext>();
    })

    // Server
    .AddServer(options =>
    {
        options.SetTokenEndpointUris("/connect/token");

        options.AllowPasswordFlow();

        options.AcceptAnonymousClients();
        options.DisableAccessTokenEncryption();

        options.AddDevelopmentSigningCertificate();
        options.AddDevelopmentEncryptionCertificate();

        options.SetIssuer(new Uri(builder.Configuration["OpenIddict:Issuer"]!));

        //options.DisableTransportSecurityRequirement(); // 🔥 IMPORTANT FOR HTTP
        options.UseAspNetCore().DisableTransportSecurityRequirement().EnableTokenEndpointPassthrough();
        
        //options.DisableAccessTokenEncryption();

        options.SetAccessTokenLifetime(TimeSpan.FromHours(2)); // 2 hours
    })
    .AddValidation(options =>
    {
         options.UseLocalServer(); // validate tokens issued by same server
         options.UseAspNetCore();
    });

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

builder.Services.AddAuthorization();

// --------------------------
// Add Infrastructure Layer
// --------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration); // DbContext + Repositories

// --------------------------
// Register Authorization
// --------------------------
builder.Services.AddAuthorization();

// --------------------------
// Register Shared BuildingBlocks
// --------------------------
builder.Services.AddSharedLogging();
builder.Services.AddSharedExceptions();    // ExceptionExtensions
//builder.Services.AddOutbox();       // OutboxExtensions
builder.Services.AddSharedBehaviors();

// --------------------------
// Register EventBus (RabbitMQ)
// --------------------------
//builder.Services.AddRabbitMQEventBus(builder.Configuration);


// --------------------------
// Add Controllers, Swagger, etc.
// --------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var retryPolicy = new ResiliencePipelineBuilder()
    .AddRetry(new RetryStrategyOptions
    {
        MaxRetryAttempts = 3,
        Delay = TimeSpan.FromSeconds(15),
        BackoffType = DelayBackoffType.Exponential
    })
    .Build();

var app = builder.Build();
var loggerService = app.Services.GetRequiredService<ILoggerService>();
await retryPolicy.Execute(async () =>
{
    await IdentityDbSeeder.SeedAsync(app.Services, loggerService);
});


using (var scope = app.Services.CreateScope())
{
    var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

    if (await manager.FindByClientIdAsync("react_app") == null)
    {
        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "react_app",
            ClientSecret = "secret123",
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.GrantTypes.Password,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Prefixes.Scope + "ecommerce_api"
            }
        });
    }
    var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

    if (await scopeManager.FindByNameAsync("ecommerce_api") == null)
    {
        await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
        {
            Name = "ecommerce_api",
            Resources = { "ecommerce_api" }
        });
    }
}



// Middleware pipeline
app.UseSharedExceptions();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
