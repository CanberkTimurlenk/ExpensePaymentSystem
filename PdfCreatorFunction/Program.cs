using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PdfCreator;

// A Serverless function example
// This function's role is to transform expense content into a PDF format.
// Reports are sent to a queue by the "FinalCase API", and this function is triggered and executes the PDF creation.

// The PDF creation function then send the created PDF to another queue (email queue) to be sent to the.

// Note: The PDF creation process is not yet complete. Currently, the content is directly sent to the email queue,
// serving as a demonstration of the conceptual design.

// Further improvements are possible, such as employing different topics with RabbitMQ or Amazon SNS.
// Different functions can be designed and triggered based on distinct topics.

public static class PdfCreator
{
    private static IModel channel;
    private static IConnection connection;
    private static IConfiguration config;

    public static void Main()
    {
        LoadConfig();
        InitializeRabbitMQ();
        ConsumeMessages();
    }

    private static void LoadConfig()
    {
        config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false)
            .Build();
    }

    private static void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory() { Uri = new Uri(config.GetSection("RabbitMQ:Url").Value) };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: config.GetSection("RabbitMQ:PdfQueue").Value,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    private static void ConsumeMessages()
    {
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            ProcessMessage(message); // Pseudo code for PDF creation

            SendMessageToEmailQueue(body);
        };
        channel.BasicConsume(queue: config.GetSection("RabbitMQ:PdfQueue").Value,
                             autoAck: true,
                             consumer: consumer);
    }

    private static void ProcessMessage(string message)
    {
        //Convert the message to PDF....
        Console.WriteLine("The content was converted to the pdf !");
    }

    private static void SendMessageToEmailQueue(byte[] body)
    {
        channel.BasicPublish(exchange: "",
                             routingKey: config.GetSection("RabbitMQ:EmailQueue").Value,
                             basicProperties: null,
                             body: body);
    }
}
