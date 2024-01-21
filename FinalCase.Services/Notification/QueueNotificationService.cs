using System.Text;
using FinalCase.Schema.Email;
using FinalCase.Services.Queue.Constants;
using FinalCase.Services.Queue;

namespace FinalCase.Services.NotificationService;

/// <summary>
/// Implementation of the INotificationService that sends notifications to the notification queue.
/// </summary>
public class QueueNotificationService : INotificationService
{
    /// <summary>
    /// Sends an email to the email queue.
    /// </summary>
    /// <param name="email">The email to be sent.</param>
    public void SendEmail(Email email)
    {
        RabbitMq.SendMessage(Queues.Email, Encoding.UTF8.GetBytes(email.ToString()));
        // EmailQueue is the key of the queue in the appsettings.json file.
    }
}
