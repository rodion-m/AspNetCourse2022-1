namespace GreatShop.Domain.Services;

public interface IClock
{
    DateTimeOffset GetCurrentTime();
}