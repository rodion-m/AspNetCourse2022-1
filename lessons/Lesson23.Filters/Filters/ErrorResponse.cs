namespace Lesson23.Filters.Filters;

// json example: { "Message" = "Аккаунт с таким Email не найден" }
public record ErrorResponse(int StatusCode, string Message)
{
    public override string ToString()
    {
        return $"{{ Message = {Message} }}";
    }
}