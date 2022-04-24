using GreatShop.Domain;

namespace GreatShop.Infrastructure;

public class UtcClock : IClock
{
    public DateTimeOffset GetCurrentTime() => DateTimeOffset.UtcNow;
}