namespace GreatShop.Domain.Exceptions;

public class EmailNotFoundException : DomainException
{
    public EmailNotFoundException(string email) : base($"Email {email} not found")
    {
        Email = email;
    }

    public string Email { get; }
}