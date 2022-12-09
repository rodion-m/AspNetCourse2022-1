namespace GreatShop.Domain;

public static class Roles
{
    public const string Customer = "Customer";
    public const string Admin = "Admin";
    
    public static class Defaults
    {
        public static string[] Customers { get; } = { Customer };
    }
}