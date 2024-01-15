using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Hangfire;
using FinalCase.Schema.Email;

namespace FinalCase.BackgroundJobs.BackgroundServices.NotificationService;

public static class EmailSender
{
    private readonly static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build(); // Get the configuration from the appsettings.json file.

    /// <summary>
    /// Sends an email to the email queue.
    /// </summary>
    /// <param name="email">The email to be sent.</param>    
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail, DelaysInSeconds = [20, 60])]
    public static void SendEmail(Email email)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMQ");
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqConfig.GetValue<string>("Url"))
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var queueName = rabbitMqConfig.GetValue<string>("EmailQueue");
        channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string body = JsonSerializer.Serialize(email);
        var bodyByte = Encoding.UTF8.GetBytes(body);

        channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: bodyByte);
    }
}
