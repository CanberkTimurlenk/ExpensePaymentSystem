using FinalCase.Schema.Email;

namespace FinalCase.Services.NotificationService;

/// <summary>
/// Represents a service responsible for sending notifications, emails etc...
/// </summary>
public interface INotificationService
{
    // For now, there is only one implementation to send emails to the queue.
    // Note that, with the creation of different implementations of this service,
    // 3rd-party services or subscriptions can be used instead of the queue, thanks to polymorphism.

    void SendEmail(Email email);

    // In the future, The service can be extended to send SMS, push notifications etc...
}