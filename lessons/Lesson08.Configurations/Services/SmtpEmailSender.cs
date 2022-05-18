using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Lesson08.Configurations.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpCredentials _credentials;

    public SmtpEmailSender(IOptions<SmtpCredentials> options)
    {
        _credentials = options.Value;
    }

    public async Task SendAsync(
        string from,
        string recipients,
        string? subject,
        string? body,
        CancellationToken cancellationToken = default)
    {
        using var smtpClient = new SmtpClient(_credentials.Host)
        {
            Port = 25,
            Credentials = new NetworkCredential(_credentials.UserName, _credentials.Password)
        };

        await smtpClient.SendMailAsync(from, recipients, subject, body, cancellationToken);
    }
}