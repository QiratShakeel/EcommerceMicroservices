using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Infrastructure;
using MediatR;
using Moq;

namespace BuildingBlocks.Shared.Tests.Behaviors
{
    public class TransactionBehaviorTests
    {
        private class TestRequest : ICommand<string> { }
        [Fact]
        public async Task TransactionBehavior_Commits_Transaction()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var _mediatorMock = new Mock<IMediator>();
            var dispatcher = new DomainEventDispatcher(_mediatorMock.Object);

            uowMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.CompletedTask);
            uowMock.Setup(x => x.CommitAsync(It.IsAny<DomainEventDispatcher>())).Returns(Task.CompletedTask);

            var behavior = new TransactionBehavior<TestRequest, string>(uowMock.Object,dispatcher);

            RequestHandlerDelegate<string> next = () => Task.FromResult("OK");

            var result = await behavior.Handle(new TestRequest(), next, CancellationToken.None);

            Assert.Equal("OK", result);

            uowMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
            uowMock.Verify(x => x.CommitAsync(dispatcher), Times.Once);
        }
        [Fact]
        public async Task TransactionBehavior_Rollbacks_On_Exception()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var _mediatorMock = new Mock<IMediator>();
            var dispatcher = new DomainEventDispatcher(_mediatorMock.Object);

            uowMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.CompletedTask);
            uowMock.Setup(x => x.RollbackAsync()).Returns(Task.CompletedTask);

            var behavior = new TransactionBehavior<TestRequest, string>(uowMock.Object,dispatcher);

            RequestHandlerDelegate<string> next = () => throw new Exception("boom");

            await Assert.ThrowsAsync<Exception>(() => behavior.Handle(new TestRequest(), next, CancellationToken.None));

            uowMock.Verify(x => x.RollbackAsync(), Times.Once);
        }


    }
}