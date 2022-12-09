using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Lesson08.Configurations.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpConfig _config;

    public SmtpEmailSender(IOptions<SmtpConfig> options)
    {
        _config = options.Value;
    }

    public async Task SendAsync(
        string from,
        string recipients,
        string? subject,
        string? body,
        CancellationToken cancellationToken = default)
    {
        using var smtpClient = new SmtpClient(_config.Host)
        {
            Port = 25,
            Credentials = new NetworkCredential(_config.UserName, _config.Password)
        };

        await smtpClient.SendMailAsync(from, recipients, subject, body, cancellationToken);
    }
}