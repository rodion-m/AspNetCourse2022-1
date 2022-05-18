namespace Lesson7_DI;

public class LifeTimeTester : IDisposable
{
    public Guid Id { get; } = Guid.NewGuid();

    public void Dispose()
    {
        ;
    }
}