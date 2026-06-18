namespace BuildingBlocks.Shared.Infrastructure
{
    public interface IEventTypeResolver
    {
        Type Resolve(string eventName);
    }
    // Implementation
    public class EventTypeResolver : IEventTypeResolver
    {
        private readonly Dictionary<string, Type> _map;

        public EventTypeResolver(Dictionary<string, Type> map)
        {
            _map = map;
        }

        public Type Resolve(string eventName)
        {
            if (!_map.TryGetValue(eventName, out var type))
                throw new Exception($"Event type not registered for {eventName}");
            return type;
        }
    }
}