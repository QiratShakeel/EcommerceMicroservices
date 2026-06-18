using BuildingBlocks.EventBus.Abstractions;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
namespace BuildingBlocks.Shared.Outbox
{
    public interface IOutboxPublisher
    {
        Task PublishAsync(IIntegrationEvent eventObj, CancellationToken cancellation);
    }
}

///--------------services to addd
//builder.Services.AddScoped<IOutboxPublisher, OutboxPublisher>();
//builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();

//builder.Services.AddHostedService<OutboxProcessor>();