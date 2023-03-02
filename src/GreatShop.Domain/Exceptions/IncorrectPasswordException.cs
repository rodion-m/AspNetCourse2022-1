namespace GreatShop.Domain.Exceptions;

public class IncorrectPasswordException : DomainException
{
    public IncorrectPasswordException() : base("Incorrect password")
    {
    }
}