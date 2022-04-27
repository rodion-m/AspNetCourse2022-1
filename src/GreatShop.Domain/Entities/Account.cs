namespace GreatShop.Domain.Entities;

public static class Role
{
    public const string Buyer = "Buyer";
    public const string Admin = "Admin";
}

public record Account : IEntity
{
    protected Account() {}
    
    public Account(Guid id, string name, string email, string passwordHash, string[] roles)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Roles = roles ?? throw new ArgumentNullException(nameof(roles));
    }

    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public DateTimeOffset AllTokensBlockedAt { get; set; }
    public string[] Roles { get; set; } = null!;
}