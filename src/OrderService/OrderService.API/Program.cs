using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Logging;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Grpc.Catalog;
using BuildingBlocks.Shared.Outbox;
using Ecommerce.Orders.API.Extensions;
using Ecommerce.Orders.Application;
using Ecommerce.Orders.Infrastructure;
using Ecommerce.Orders.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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
// Register gRPC client for CatalogService
// --------------------------
builder.Services.AddGrpcClient<CatalogGrpc.CatalogGrpcClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcSettings:CatalogUrl"]);
});
// --------------------------
// Add Controllers, Swagger, etc.
// --------------------------
builder.Services.AddControllers();      
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    db.Database.Migrate();
}

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
app.MapControllers();   //mediatr pipeline controller ke bd chlegi 


app.Run();
