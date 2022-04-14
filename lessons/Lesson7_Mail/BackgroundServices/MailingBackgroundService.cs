using Lesson7_Mail.Services;

namespace Lesson7_Mail.BackgroundServices;

public class MailingBackgroundService : BackgroundService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<MailingBackgroundService> _logger;

    public MailingBackgroundService(
        IEmailSender emailSender,
        ILogger<MailingBackgroundService> logger,
        IHostApplicationLifetime lifetime)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //await Task.Run(() => throw null);
        // await _emailSender.SendAsync(
        //     "asp2022@rodion-m.ru",
        //     "rody66@yandex.ru",
        //     "Message from a great programmers",
        //     "Just have a great day, buddy!",
        //     stoppingToken
        // );
    }
}