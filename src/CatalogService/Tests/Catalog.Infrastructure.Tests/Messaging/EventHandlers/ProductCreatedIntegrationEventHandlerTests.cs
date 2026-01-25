//using BuildingBlocks.Shared.Outbox;
//using Ecommerce.Catalog.Domain.Events;
//using Ecommerce.Catalog.Infrastructure.Messaging.EventHandlers;
//using Ecommerce.Catalog.Infrastructure.Messaging.IntegrationEvents;
//using Ecommerce.Catalog.Infrastructure.Persistence.Context;
//using FluentAssertions;
//using Microsoft.AspNetCore.Connections;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client;
//using System.Text;

//public class ProductCreatedIntegrationTest
//{
//    private const string QueueName = "test.product.created";
//    private readonly CatalogDbContext _dbContext;
//    private readonly IOutboxPublisher _outboxPublisher;
//    private readonly IConnection _connection;
//    private readonly IModel _channel;

//    public ProductCreatedIntegrationTest()
//    {
//        // --- In-memory EF Core for testing ---
//        var options = new DbContextOptionsBuilder<CatalogDbContext>()
//            .UseInMemoryDatabase(databaseName: "CatalogTestDb")
//            .Options;

//        _dbContext = new CatalogDbContext(options);

//        // --- Real RabbitMQ setup ---
//        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
//        _connection = factory.CreateConnection();
//        _channel = _connection.CreateModel();

//        // Declare test queue
//        _channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: true);

//        // --- OutboxPublisher using DbContext ---
//        _outboxPublisher = new OutboxPublisher(_dbContext);
//    }

//    private ProductCreatedIntegrationEventHandler CreateHandler()
//        => new ProductCreatedIntegrationEventHandler(_outboxPublisher);
//    [Fact]
//    public async Task ProductCreatedHandler_ShouldSaveOutboxAndPublishToRabbitMQ()
//    {
//        // Arrange
//        var handler = CreateHandler();
//        var domainEvent = new ProductCreatedDomainEvent(Guid.NewGuid(), "Laptop", "SKU-001");

//        // Act
//        await handler.Handle(domainEvent, CancellationToken.None);

//        // Assert: Outbox message exists
//        var outboxMessages = await _dbContext.GetUnprocessedMessagesAsync(20, CancellationToken.None);
//        outboxMessages.Should().ContainSingle(m => m.Type == typeof(ProductCreatedIntegrationEvent).FullName);

//        // --- Publish manually to RabbitMQ for testing ---
//        var eventBus = new RabbitMQEventBus(new RabbitMQConnection(Options.Create(new EventBusOptions())), Options.Create(new EventBusOptions()));
//        foreach (var msg in outboxMessages)
//        {
//            var evt = JsonSerializer.Deserialize<ProductCreatedIntegrationEvent>(msg.Content);
//            await eventBus.PublishAsync(evt!);

//            // Mark as processed
//            await _dbContext.MarkAsProcessedAsync(msg, CancellationToken.None);
//        }

//        // Assert: RabbitMQ queue should have 1 message
//        var result = _channel.BasicGet(QueueName, true);
//        result.Should().NotBeNull();
//        var body = Encoding.UTF8.GetString(result.Body.ToArray());
//        body.Should().Contain("Laptop");
//    }

//    public void Dispose()
//    {
//        _channel?.Close();
//        _connection?.Close();
//        _dbContext?.Dispose();
//    }

//}
