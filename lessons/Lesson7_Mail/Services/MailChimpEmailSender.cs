using MailChimp.Net;

namespace Lesson7_Mail.Services;

public class MailChimpEmailSender : IEmailSender
{
    public Task SendAsync(string from, string recipients, string? subject, string? body,
        CancellationToken cancellationToken = default)
    {
        var manager = new MailChimpManager("0610a160a0c435416406e9f2c2d49540-us14");
        throw new NotImplementedException();
    }
}