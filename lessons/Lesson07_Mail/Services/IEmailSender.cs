namespace Lesson7_Mail.Services;

public interface IEmailSender
{
    Task SendAsync(
        string from,
        string recipients,
        string? subject,
        string? body,
        CancellationToken cancellationToken = default
    );
}