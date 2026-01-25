using BuildingBlocks.Shared.Behaviors.Logging;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xunit;

namespace BuildingBlocks.Shared.Tests.Behaviors
{
    public class LoggingBehaviorTests
    {
        private class TestRequest : IRequest<string>
        {
            public string Message { get; set; } = string.Empty;
        }

        [Fact]
        public async Task LoggingBehavior_LogsRequestAndResponse()
        {
            // Arrange
            var loggerMock = new Mock<ILoggerService>();
            var behavior = new LoggingBehavior<TestRequest, string>(loggerMock.Object);

            var request = new TestRequest { Message = "Hello" };

            RequestHandlerDelegate<string> next = () => Task.FromResult("Response");

            // Act
            var response = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.Equal("Response", response);
            
            loggerMock.Verify(x => x.LogInformation("Handling {RequestName} {@Request}",It.IsAny<object[]>()),Times.Once);

            loggerMock.Verify(x => x.LogInformation("Handled {RequestName} {@Response}",It.IsAny<object[]>()),Times.Once);
        }
    }
}
