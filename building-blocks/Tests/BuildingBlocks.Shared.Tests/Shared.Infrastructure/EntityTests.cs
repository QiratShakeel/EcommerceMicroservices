using BuildingBlocks.Shared.Infrastructure;
using System.ComponentModel;

namespace BuildingBlocks.Shared.Tests.Infrastructure
{
    //abstract class test by inherited into concreate class 
    public class TestsEntity: Entity
    { 
        public void AddEvent(IDomainEvent @event) { AddDomainEvent(@event); }
    }
    public class EntityTests
    {
        private class TestDomainEvent: IDomainEvent {}
        private TestDomainEvent domainEvent { get; init; }
        private TestsEntity TestsEntity { get; init; }
        public EntityTests()
        {
            domainEvent = new TestDomainEvent();
            TestsEntity = new TestsEntity();
        }
        [Fact]
        public void AddDomainEvent_Should_AddEvent()
        {
            TestsEntity.AddEvent(domainEvent);
            Assert.Single(TestsEntity.DomainEvents);
            Assert.Contains(domainEvent, TestsEntity.DomainEvents);
        }
        [Fact]
        public void ClearDomainEvents_Should_Clear_All_Events()
        {
            TestsEntity.AddEvent(domainEvent);
            TestsEntity.AddEvent(domainEvent);
            TestsEntity.ClearDomainEvents();
            Assert.Empty(TestsEntity.DomainEvents);
        }
    }
}