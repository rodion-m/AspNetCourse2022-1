using System.Net;
using System.Net.Mail;

namespace Lesson7_Mail.Services;

public class SmtpEmailSender : IEmailSender
{
    public async Task SendAsync(
        string from,
        string recipients,
        string? subject,
        string? body,
        CancellationToken cancellationToken = default)
    {
        using var smtpClient = new SmtpClient("smtp.beget.com")
        {
            Port = 25,
            Credentials = new NetworkCredential("asp2022@rodion-m.ru", "aHGnOlz7")
        };

        await smtpClient.SendMailAsync(from, recipients, subject, body, cancellationToken);
    }
}