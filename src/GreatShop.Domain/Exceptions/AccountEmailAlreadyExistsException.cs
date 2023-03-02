namespace GreatShop.Domain.Exceptions;

public class AccountEmailAlreadyExistsException : DomainException
{
    public string Email { get; }
    
    public AccountEmailAlreadyExistsException(string email) 
        : base($"Account with email {email} already exists.")
    {
        Email = email;
    }
}