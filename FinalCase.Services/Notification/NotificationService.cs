using System.Text;
using FinalCase.Schema.Email;
using FinalCase.BackgroundJobs.QueueService;
using FinalCase.Services.Queue.Constants;

namespace FinalCase.Services.NotificationService;

/// <summary>
/// Implementation of the INotificationService that sends notifications to the notification queue.
/// </summary>
public class QueueNotificationService(IQueueService queueService) : INotificationService
{
    private readonly IQueueService queueService = queueService;

    /// <summary>
    /// Sends an email to the email queue.
    /// </summary>
    /// <param name="email">The email to be sent.</param>
    public void SendEmail(Email email)
    {
        queueService.SendMessage(Queues.Email, Encoding.UTF8.GetBytes(email.ToString()));
        // EmailQueue is the key of the queue in the appsettings.json file.
    }
}
