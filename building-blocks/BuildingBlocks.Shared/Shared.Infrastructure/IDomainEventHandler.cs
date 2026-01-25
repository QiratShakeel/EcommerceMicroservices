using MediatR;

namespace BuildingBlocks.Shared.Infrastructure
{
    public interface IDomainEventHandler<TEvent>: INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent, CancellationToken token);
    }
}