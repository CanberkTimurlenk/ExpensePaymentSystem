using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EmailFunction;

// Introducing RabbitMQ delivers the advantage of asynchronous email sending and also optimizing performance.

// After proper changes, this function could be used as serverless (AWS Lambda/Azure Functions)
// a trigger etc could perform integration with RabbitMQ or Azure Service Bus or AWS SNS

// Amazon SNS, with its email sending integration, is also compatible for simpler operations and can be employed with Lambda.

// Amazon has also SES service for email sending, it could be also used with lambda

// Each email triggers a new instance of the function, provides high scalability and cost efficiency.

// While the serverless structure may brings cold start problem as a side effect,
// However in our scenario, it's not a significant concern.
static class Program
{
    static void Main()
    {
        // Configuration setup
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false).Build();

        // RabbitMQ setup
        var rabbitMqConfig = config.GetSection("RabbitMQ");
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqConfig["Url"])
        };

        // Creat the connection and the channel
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Declare the queue
        var queueName = rabbitMqConfig["EmailQueue"];
        channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (s, e) => // When a message is received, send the email
        {
            try
            {
                var body = Encoding.UTF8.GetString(e.Body.Span);
                var email = JsonSerializer.Deserialize<Email>(body); // Deserialize the email

                Console.WriteLine(email.ToString());

                //if (email is not null)
                //    EmailSender.Send(email, config); // Sends the email
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}"); // If the email sending fails, writes the error to the console
            }
        };

        channel.BasicConsume(queueName, true, consumer); // Start consuming the queue

        Console.ReadKey(); // Prevents the application from closing
    }
}