namespace Lesson23.Filters.Filters;

public record ErrorModel(string Message)
{
    public override string ToString()
    {
        return $"{{ Message = {Message} }}";
    }
}