using FinalCase.Schema.Email;

namespace FinalCase.Services.NotificationService;

public interface INotificationService
{
    void SendEmail(Email email);
}
