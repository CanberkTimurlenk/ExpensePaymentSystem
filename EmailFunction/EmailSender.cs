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



        using var mail = new MailMessage { From = new MailAddress(config["EmailAddress"]), Subject = email.Subject, Body = email.Body, IsBodyHtml = true };

        foreach (var item in email.To)
            mail.To.Add(new MailAddress(item));

        smtp.Send(mail);
    }
}
