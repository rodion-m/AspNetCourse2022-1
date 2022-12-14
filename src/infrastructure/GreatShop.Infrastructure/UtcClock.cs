using GreatShop.Domain;
using GreatShop.Domain.Services;

namespace GreatShop.Infrastructure;

public class UtcClock : IClock
{
    public DateTimeOffset GetCurrentTime() => DateTimeOffset.UtcNow;
}