using Lesson8.Configurations.Services;
using Polly;

namespace Lesson10.ExceptionHandling;

public class MailingBackgroundService : BackgroundService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<MailingBackgroundService> _logger;

    public MailingBackgroundService(
        IEmailSender emailSender,
        ILogger<MailingBackgroundService> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await TrySendStartupMessageForWhen(5, stoppingToken);
    }

    private async Task TrySendStartupMessageDoWhile(int retryCount, CancellationToken stoppingToken)
    {
        var currentRetry = 0;
        do
        {
            Exception? exception;
            try
            {
                await _emailSender.SendAsync(
                    "asp2022@rodion-m.ru",
                    "rody66@yandex.ru",
                    "Message from a great programmers",
                    "Just have a great day, buddy!",
                    stoppingToken
                );
                return;
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Email sending was cancelled");
                return;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (currentRetry < retryCount)
            {
                _logger.LogWarning(exception, "There was an error while sending email. Trying again");
                currentRetry++;
            }
            else
            {
                _logger.LogError(exception, "There was an error while sending email");
                break;
            }
        } while (true);
    }

    private async Task TrySendStartupMessageFor(int retryCount, CancellationToken stoppingToken)
    {
        for (var currentRetry = 0; currentRetry <= retryCount; currentRetry++)
        {
            Exception? exception;
            try
            {
                await _emailSender.SendAsync(
                    "asp2022@rodion-m.ru",
                    "rody66@yandex.ru",
                    "Message from a great programmers",
                    "Just have a great day, buddy!",
                    stoppingToken
                );
                return;
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Email sending was cancelled");
                return;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (currentRetry < retryCount)
                _logger.LogWarning(exception, "There was an error while sending email. Trying again");
            else
                _logger.LogError(exception, "There was an error while sending email");
        }
    }

    private async Task TrySendStartupMessageForWhen(int retryCount, CancellationToken stoppingToken)
    {
        for (var currentRetry = 0; currentRetry <= retryCount; currentRetry++)
            try
            {
                await _emailSender.SendAsync(
                    "asp2022@rodion-m.ru",
                    "rody66@yandex.ru",
                    "Message from a great programmers",
                    "Just have a great day, buddy!",
                    stoppingToken
                );
                return;
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Email sending was cancelled");
                return;
            }
            catch (Exception e) when (currentRetry < retryCount)
            {
                _logger.LogWarning(e, "There was an error while sending email. Trying again");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There was an error while sending email");
            }
    }


    private async Task TrySendStartupMessagePolly(int retryCount, CancellationToken stoppingToken)
    {
        Task SendAsync(CancellationToken cancellationToken)
        {
            return _emailSender.SendAsync(
                "asp2022@rodion-m.ru",
                "rody66@yandex.ru",
                "Message from a great programmers",
                "Just have a great day, buddy!",
                cancellationToken
            );
        }

        var policy = Policy
            .Handle<Exception>()
            .RetryAsync(retryCount, (exception, retryAttempt) =>
            {
                _logger.LogWarning(
                    exception, "There was an error while sending email. Retrying: {Attempt}", retryAttempt);
            });
        var result = await policy.ExecuteAndCaptureAsync(SendAsync, stoppingToken);
        if (result.Outcome == OutcomeType.Failure)
            _logger.LogError(result.FinalException, "There was an error while sending email");
    }
}