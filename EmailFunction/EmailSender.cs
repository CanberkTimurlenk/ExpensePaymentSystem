using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace EmailFunction;
public static class EmailSender
{
    public static void Send(Email email, IConfiguration config)
    {
        using var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(config["EmailAddress"], config["Password"])
        };

        using var mail = new MailMessage(config["EmailAddress"], email.To, email.Subject, email.Body) { IsBodyHtml = true };

        mail.To.Add(new MailAddress(email.To));

        smtp.Send(mail);
    }
}
