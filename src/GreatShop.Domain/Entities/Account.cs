namespace GreatShop.Domain.Entities;

public record Account : IEntity
{
#pragma warning disable CS8618
    protected Account()
#pragma warning restore CS8618
    {
    }
    
    public Account(Guid id, string name, string email, string passwordHash, string[] roles)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        if (roles.Length == 0)
        {
            throw new ArgumentException("Value cannot be an empty collection.", nameof(roles));
        }
    }

    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public DateTimeOffset AllTokensBlockedAt { get; set; }
    public string[] Roles { get; set; }
}