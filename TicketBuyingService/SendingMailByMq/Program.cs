using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendingMailByMq.Models;
using SendingMailByMq.Services;
using System.Configuration;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory
{
    HostName = ConfigurationManager.AppSettings["ConnectionHostName"]!,
    UserName = ConfigurationManager.AppSettings["ConnectionUserName"]!,
    Password = ConfigurationManager.AppSettings["ConnectionPassword"]!
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
    var tickets = JsonSerializer.Deserialize<Ticket[]>(message);

    EmailService.SendEmailAsync(tickets);
    Console.WriteLine("ticket sent to " + tickets[0].Mail);
};

channel.BasicConsume(queue: queueName,
    autoAck: true,
    consumer: consumer);

Console.ReadLine();