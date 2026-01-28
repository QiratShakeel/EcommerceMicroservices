using BuildingBlocks.EventBus.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public sealed class RabbitMQConnection : IAsyncDisposable
{
    private IConnection? _connection;
    private readonly ConnectionFactory _factory;

    public RabbitMQConnection(IOptions<EventBusOptions> options)
    {
        var opt = options.Value;

        _factory = new ConnectionFactory
        {
            HostName = opt.HostName,
            UserName = opt.UserName,
            Password = opt.Password,
            //DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            await ConnectWithRetry();
        }

        return await _connection!.CreateChannelAsync();
    }

    private async Task ConnectWithRetry()
    {
        const int maxRetries = 10;

        for (int i = 1; i <= maxRetries; i++)
        {
            try
            {
                _connection = await _factory.CreateConnectionAsync();
                return;
            }
            catch
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        throw new Exception("RabbitMQ not reachable after retries");
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null && _connection.IsOpen)
            await _connection.CloseAsync();

        _connection?.Dispose();
    }
}
