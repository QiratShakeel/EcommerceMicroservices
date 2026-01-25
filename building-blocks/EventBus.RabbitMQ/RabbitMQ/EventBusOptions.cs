using BuildingBlocks.EventBus.RabbitMQ;

namespace BuildingBlocks.EventBus.RabbitMQ
{
    public class EventBusOptions
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string ExchangeName { get; set; } = "ecommerce.events";
        public int RetryCount { get; set; } = 5;
    }
}


//builder.Services.Configure<EventBusOptions>(
//    builder.Configuration.GetSection("EventBus"));

//builder.Services.AddSingleton<RabbitMQConnection>();
//builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();
//builder.Services.AddSingleton<SubscriptionManager>();

//builder.Services.AddHostedService<RabbitMQConsumer>();


///appsetting.json
///{
//"EventBus": {
//    "HostName": "localhost",
//    "UserName": "guest",
//    "Password": "guest",
//    "ExchangeName": "ecommerce.events"
//  }
//}
