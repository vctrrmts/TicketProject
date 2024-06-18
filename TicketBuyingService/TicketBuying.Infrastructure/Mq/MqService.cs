using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using TicketBuying.Application.Abstractions.Service;

namespace TicketBuying.Infrastructure.Mq;

public class MqService : IMqService
{
    private readonly ILogger<MqService> _logger;
    public MqService(ILogger<MqService> logger)
    {
        _logger = logger;
    }

    public void SendMessageToExchange(string exchange, string message)
    {
        var factory = new ConnectionFactory 
        {   
            HostName = "my-rabbit",
            UserName = "guest",
            Password = "guest"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "PurchasedTicketExchange", type: ExchangeType.Fanout);
        channel.QueueDeclare(queue: "PurchasedTicketQueue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "PurchasedTicketQueue",
            basicProperties: null,
            body: body);

        _logger.LogInformation($"Sent {message}");
    }
}
