// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public abstract class Review
{
    public int Id { get; private set; }

    public int SolutionId { get; private set; }
    public Solution Solution { get; private set; } = null!;

    public abstract bool IsCorrect { get; }
}