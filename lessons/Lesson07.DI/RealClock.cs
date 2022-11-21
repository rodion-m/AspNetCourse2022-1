namespace Lesson7.DI;

public class RealClock : IClock
{
    public DateTime Current() => DateTime.Now;
}