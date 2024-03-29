﻿using Microsoft.Extensions.Options;

namespace Lesson08.Configurations;

public class MyService
{
    public MyService(IOptions<SmtpConfig> options)
    {
        var smtpCredentials = options.Value;
        Console.WriteLine(smtpCredentials.UserName);
    }
}