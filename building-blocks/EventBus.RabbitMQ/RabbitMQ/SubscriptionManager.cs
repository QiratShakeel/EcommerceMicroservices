using BuildingBlocks.EventBus.Abstractions;
namespace BuildingBlocks.EventBus.RabbitMQ
{
    public class SubscriptionManager
    {
        private readonly Dictionary<string, Type> _handlers = new();

        public void AddSubscription<TEvent, THandler>()
            where TEvent : IIntegrationEvent
        {
            _handlers[typeof(TEvent).Name] = typeof(THandler);
        }

        public Type GetHandler(string eventName)
            => _handlers[eventName];
    }
}