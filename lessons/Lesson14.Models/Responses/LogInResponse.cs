namespace Lesson14.Models.Responses;

public class LogInResponse
{
    public LogInResponse(string? message = null)
    {
        Message = message;
    }

    public string? Message { get; set; }
    public Account Account { get; set; }
    public string Token { get; set; }
}