using MediatR;

namespace BuildingBlocks.Shared.Infrastructure
{
    public sealed class DomainEventDispatcher
    {
        private readonly IMediator _mediator;
        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchEventsAsync(IEnumerable<Entity> entities)
        {
            var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}