using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendingMailByMq;
using System.Text;
using System.Text.Json;
using TicketBuying.Domain;

//EmailService.GenerateQrCode();

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "PurchasedTicketExchange", type: ExchangeType.Fanout);

var queueName = "PurchasedTicketQueue";
channel.QueueDeclare(queue: queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

channel.QueueBind(queue: queueName,
    exchange: "PurchasedTicketExchange",
    routingKey: string.Empty);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var ticket = JsonSerializer.Deserialize<Ticket>(message);

    EmailService.SendEmailAsync(ticket);
    Console.WriteLine("ticket sent to " + ticket.Mail);
};

channel.BasicConsume(queue: queueName,
    autoAck: true,
    consumer: consumer);

Console.ReadLine();