using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Catalog.API.Extensions;
using Ecommerce.Catalog.API.Grpc;
using Ecommerce.Catalog.Application;
using Ecommerce.Catalog.Infrastructure;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Ecommerce.Catalog.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Polly;
using Polly;
using Polly.Retry;
using System;
//using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);


// --------------------------
// Add Authentication Validation
// --------------------------
    //.AddJwtBearer(options =>
    //{
    //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidIssuers = new[]
    //        {
    //            "http://localhost:5014",   // HTTP
    //            "https://localhost:7210"   // HTTPS
    //        },
    //        ValidateAudience = true,
    //        ValidAudience = "ecommerce_api"
    //    };
    //    options.RequireHttpsMetadata = false; // ✅ allow HTTP for dev
    //});
builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.SetIssuer(new Uri(builder.Configuration["OpenIddict:Issuer"]!));

        options.AddAudiences("ecommerce_api");

        options.UseSystemNetHttp(); // required for remote validation

        options.UseAspNetCore();
    });
builder.Services.AddAuthorization();


// --------------------------
// Add Infrastructure Layer
// --------------------------
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration); // DbContext + Repositories

// --------------------------
// Register Shared BuildingBlocks
// --------------------------
builder.Services.AddSharedLogging();
builder.Services.AddSharedExceptions();    // ExceptionExtensions
builder.Services.AddOutbox();       // OutboxExtensions
builder.Services.AddSharedBehaviors();

// --------------------------
// Register EventBus (RabbitMQ)
// --------------------------
builder.Services.AddRabbitMQEventBus(builder.Configuration);

// --------------------------
// Add gRPC
// --------------------------
builder.Services.AddGrpc();

// --------------------------
// Add Controllers, Swagger, etc.
// --------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//builder.WebHost.ConfigureKestrel(options =>
//{
//    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
//    // "Grpc" endpoint from launchSettings.json
//    options.ListenAnyIP(5200, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http2;
//        //listenOptions.UseHttps();
//    });
//    // REST / browser endpoint
//    options.ListenAnyIP(5108, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http1;
//    });
//});

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

await retryPolicy.ExecuteAsync(async token =>
{
    await CatalogDbSeeder.SeedAsync(app.Services,loggerService);
});

// Middleware pipeline
app.UseSharedExceptions();
app.UseSwagger();
app.UseSwaggerUI();
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// --------------------------
// gRPC endpoints
// --------------------------
app.MapGrpcService<CatalogGrpcService>();
app.MapGet("/", () => "Catalog gRPC Service");

app.Run();
