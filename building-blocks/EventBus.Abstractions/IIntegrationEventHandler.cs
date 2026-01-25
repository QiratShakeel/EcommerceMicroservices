namespace BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TEvent> where TEvent : IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}