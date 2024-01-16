using FinalCase.BackgroundJobs.QueueService;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace FinalCase.BackgroundJobs.QueueOperations;

public class QueueService : IQueueService
{
    private readonly static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build(); // Get the configuration from the appsettings.json file.

    /// <summary>
    /// Sends the byte array to the specified queue.
    /// </summary>
    /// <param name="queueName">The key of the queue name where the value is specified in the "RabbitMQ" section in the appsettings.json.</param>
    /// <param name="body">The message body to be sent</param>
    public void SendMessage(string queueNameKey, byte[] body)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMQ");
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqConfig.GetValue<string>("Url"))
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var name = rabbitMqConfig.GetValue<string>(queueNameKey);

        channel.QueueDeclare(queue: name,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
}
