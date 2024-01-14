using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EmailFunction;

// With RabbitMQ the email sending could be done async 

// With few changes this function could be used as serverless (AWS Lambda/Azure Functions)
// with proper changes(a trigger etc) it integrates with RabbitMQ or Azure Service Bus
// for email service amazon ses could be used
// for simple operations amazon sns has email sending integration 
// it could be also used with lambda

// For each email runs a new instance of the function
// and sends the email
// this provides high scalability and cost efficiency

// A side effect of the serverless architecture could be the cold start problem,
// but in our case it is not a big deal
static class Program
{
    static void Main()
    {
        // Configuration setup
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false).Build();

        // RabbitMQ setup
        var factory = new ConnectionFactory
        {
            Uri = new Uri(config["RabbitMQ"])
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Declare the queue
        channel.QueueDeclare(queue: "email_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (s, e) => // When a message is received, send the email
        {
            try
            {
                var email = JsonSerializer.Deserialize<Email>(Encoding.UTF8.GetString(e.Body.Span));

                if (email is not null)
                    EmailSender.Send(email, config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        };

        channel.BasicConsume("email_queue", true, consumer);

        Console.ReadKey();
    }
}