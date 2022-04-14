namespace GreatShop.Domain;

public interface IClock
{
    DateTimeOffset GetCurrentTime();
}