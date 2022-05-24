namespace Lesson7.DI;

public class LifeTimeTester : IDisposable
{
    public Guid Id { get; } = Guid.NewGuid();

    public void Dispose()
    {
        ;
    }
}