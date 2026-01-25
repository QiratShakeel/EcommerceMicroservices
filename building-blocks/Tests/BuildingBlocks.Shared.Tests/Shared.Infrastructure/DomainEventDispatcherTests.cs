using BuildingBlocks.Shared.Infrastructure;
using MediatR;
using Moq;

namespace BuildingBlocks.Shared.Tests.Infrastructure
{
    public class DomainEventDispatcherTests
    {

        private class TestDomainEvent : IDomainEvent {}
        private class TestDomainEventHandler : INotificationHandler<TestDomainEvent>
        {
            public Task Handle(TestDomainEvent notification, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
        private class TestEntity1 : Entity
        {
            public void Raise(IDomainEvent @event) => AddDomainEvent(@event);
        }

        [Fact]
            public async Task DispatchEventsAsync_Should_Invoke_Event_Handler()
            {
                var _mediatrMock = new Mock<IMediator>();
                var dispatcher = new DomainEventDispatcher(_mediatrMock.Object);
                var domainEvent1 = new TestDomainEvent();
                var domainEvent2 = new TestDomainEvent();
                var entity1 = new TestEntity1();
                entity1.Raise(domainEvent1);
                entity1.Raise(domainEvent2);

                var entities = new List<Entity> { entity1};
                await dispatcher.DispatchEventsAsync(entities);
                // Assert
                _mediatrMock.Verify(m => m.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
                Assert.Empty(entity1.DomainEvents); 

            }
    }
}