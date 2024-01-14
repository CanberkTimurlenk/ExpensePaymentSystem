using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using FinalCase.Schema.Email;
using Hangfire;

namespace FinalCase.BackgroundJobs.Hangfire.FireAndForgets.SendEmail;

public static class EmailJobs
{
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail, DelaysInSeconds = [20, 60])]
    public static void SendEmail(Email email, IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(configuration.GetSection("RabbitMQ").Value)
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "email_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string body = JsonSerializer.Serialize(email);
        var bodyByte = Encoding.UTF8.GetBytes(body);

        channel.BasicPublish(exchange: "",
            routingKey: "email_queue",
            basicProperties: null,
            body: bodyByte);
    }

}
