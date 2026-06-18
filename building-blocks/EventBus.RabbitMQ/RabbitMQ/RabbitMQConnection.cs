using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Shared.Behaviors.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public sealed class RabbitMQConnection : IAsyncDisposable
{
    private IConnection? _connection;
    private readonly ConnectionFactory _factory;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly ILoggerService _logger;

    public RabbitMQConnection(IOptions<EventBusOptions> options, ILoggerService logger)
    {
        _logger = logger;
        var opt = options.Value;

        _factory = new ConnectionFactory
        {
            HostName = opt.HostName,
            UserName = opt.UserName,
            Password = opt.Password,
            //DispatchConsumersAsync = true,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            RequestedHeartbeat = TimeSpan.FromSeconds(60)
        };
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        var connection = await GetConnectionAsync();

        return await connection.CreateChannelAsync();
    }

    //private async Task ConnectWithRetry()
    //{
    //    const int maxRetries = 20;

    //    for (int i = 1; i <= maxRetries; i++)
    //    {
    //        try
    //        {
    //            _connection = await _factory.CreateConnectionAsync();
    //            return;
    //        }
    //        catch(Exception ex)
    //        {
    //            Console.WriteLine($"RabbitMQ retry {i}/{maxRetries}: {ex.Message}");
    //            await Task.Delay(TimeSpan.FromSeconds(Math.Min(30, i * 2)));
    //        }
    //    }

    //    throw new Exception("RabbitMQ not reachable after retries");
    //}
    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection is { IsOpen: true })
            return _connection;

        await _lock.WaitAsync();

        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            _connection = await _factory.CreateConnectionAsync();
            _connection.ConnectionShutdownAsync += OnConnectionShutdown;
            _connection.CallbackExceptionAsync += OnCallbackException;
            _connection.RecoverySucceededAsync += OnRecoverySucceeded;

            _logger.LogInformation("RabbitMQ connection established.");

            return _connection;
        }
        finally
        {
            _lock.Release();
        }
    }
    private Task OnConnectionShutdown(
    object sender,
    ShutdownEventArgs args)
    {
        _logger.LogWarning(
            "RabbitMQ connection shutdown. ReplyCode={ReplyCode}, ReplyText={ReplyText}",
            args.ReplyCode,
            args.ReplyText);

        return Task.CompletedTask;
    }

    private Task OnCallbackException(
        object sender,
        CallbackExceptionEventArgs args)
    {
        _logger.LogError(
            args.Exception,
            "RabbitMQ callback exception.");

        return Task.CompletedTask;
    }

    private Task OnRecoverySucceeded(
        object sender,
        AsyncEventArgs args)
    {
        _logger.LogInformation(
            "RabbitMQ automatic recovery succeeded.");

        return Task.CompletedTask;
    }
    public async ValueTask DisposeAsync()
    {
        if (_connection is { IsOpen: true })
            await _connection.CloseAsync();

        _connection?.Dispose();
    }
}
