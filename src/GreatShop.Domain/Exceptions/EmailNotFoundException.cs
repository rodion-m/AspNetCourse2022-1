namespace GreatShop.Domain.Exceptions;

public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string email)
    {
        Email = email;
    }

    public string Email { get; }
}