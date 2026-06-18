using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.API.Extensions;
using Ecommerce.Payment.Application;
using Ecommerce.Payment.Infrastructure;
using Ecommerce.Payment.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using Polly;
using Polly.Retry;
using System;

var builder = WebApplication.CreateBuilder(args);

// --------------------------
// Add Authentication Validation
// --------------------------
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

await retryPolicy.Execute(async () =>
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
    await db.Database.MigrateAsync();
});

// Middleware pipeline
app.UseSharedExceptions();
app.UseSwagger();
app.UseSwaggerUI();
//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
