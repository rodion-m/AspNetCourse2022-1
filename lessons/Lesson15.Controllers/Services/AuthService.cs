namespace Lesson15.Controllers.Services;

public class AuthService
{
    public Task ProcessLogIn(LoginRequest request)
    {
        return Task.CompletedTask;
    }
}

public class LoginRequest
{
}