namespace Lesson14.Models;

public static class Role
{
    public const string Buyer = "Buyer";
    public const string Admin = "Admin";
}

public record Account : IEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset AllTokensBlockedAt { get; set; }
    public string[] Roles { get; set; }

    public static Account Fake => new()
    {
        Id = Guid.NewGuid(),
        Email = "fake@fake.com",
        Name = "Fakenshtain Smith"
    };

    public Guid Id { get; init; }
}