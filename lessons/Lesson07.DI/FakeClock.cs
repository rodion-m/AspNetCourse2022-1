namespace Lesson7.DI;

public class FakeClock : IClock
{
    private readonly DateTime _dateTime;

    public FakeClock(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public DateTime Current() => _dateTime;
}