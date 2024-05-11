// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Exercises;

public sealed class Subject
{
    private Subject()
    {
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;

    public static Subject Create(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is null or empty");

        return new Subject { Name = name };
    }
}