namespace BuildingBlocks.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync(IIntegrationEvent @event);

        //void Subscribe<TEvent, THandler>()
        //    where TEvent : IIntegrationEvent
        //    where THandler : IIntegrationEventHandler<TEvent>;

        //void Unsubscribe<TEvent, THandler>()
        //    where TEvent : IIntegrationEvent
        //    where THandler : IIntegrationEventHandler<TEvent>;
    }
}