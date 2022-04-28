namespace GreatShop.Configurations;

public class DbConfig
{
    public string? ConnectionString { get; set; }
    public bool DisableQueriesLogging { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
    public bool EnableDetailedErrors { get; set; }
}