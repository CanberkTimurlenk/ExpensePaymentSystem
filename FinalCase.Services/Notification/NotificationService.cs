using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using FinalCase.Schema.Email;
using FinalCase.BackgroundJobs.QueueService;

namespace FinalCase.Services.NotificationService;

public class NotificationService(IQueueService queueService) : INotificationService
{
    private readonly IQueueService queueService = queueService;

    /// <summary>
    /// Sends an email to the email queue.
    /// </summary>
    /// <param name="email">The email to be sent.</param>
    public void SendEmail(Email email)
    {
        queueService.SendMessage("EmailQueue", Encoding.UTF8.GetBytes(email.ToString()));
    }
}
