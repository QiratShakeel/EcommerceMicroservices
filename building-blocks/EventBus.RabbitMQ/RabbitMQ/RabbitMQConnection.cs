using RabbitMQ.Client;
using Microsoft.Extensions.Options;
namespace BuildingBlocks.EventBus.RabbitMQ
{
    public sealed class RabbitMQConnection : IAsyncDisposable
    {
        private readonly IConnection _connection;

        public RabbitMQConnection(IOptions<EventBusOptions> options)
        {
            var opt = options.Value;
            var factory = new ConnectionFactory
            {
                HostName = opt.HostName,
                UserName = opt.UserName,
                Password = opt.Password
            };

            // v7 FIX: async-only connection
            _connection = factory.CreateConnectionAsync()
                                 .GetAwaiter()
                                 .GetResult();
        }

        public Task<IChannel> CreateChannelAsync()
            => _connection.CreateChannelAsync();

        public async ValueTask DisposeAsync()
        {
            if (_connection.IsOpen)
                await _connection.CloseAsync();

            _connection.Dispose();
        }
    }
}
