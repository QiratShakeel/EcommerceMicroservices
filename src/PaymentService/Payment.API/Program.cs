using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Payment.API.Extensions;
using Ecommerce.Payment.Application;
using Ecommerce.Payment.Infrastructure;
using Ecommerce.Payment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

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
//builder.Services.AddGrpc();

// --------------------------
// Add Controllers, Swagger, etc.
// --------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//builder.WebHost.ConfigureKestrel(options =>
//{
//    // "Grpc" endpoint from launchSettings.json
//    options.ListenAnyIP(5200, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http2;
//        listenOptions.UseHttps();
//    });
//    // REST / browser endpoint
//    options.ListenAnyIP(5108, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http1;
//    });
//});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
//    db.Database.Migrate();
//}

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

// --------------------------
// gRPC endpoints
// --------------------------
//app.MapGrpcService<CatalogGrpcService>();
//app.MapGet("/", () => "Catalog gRPC Service");

app.Run();
