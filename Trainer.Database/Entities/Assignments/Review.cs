namespace Trainer.Database.Entities.Assignments;

public abstract class Review
{
    public abstract bool IsCorrect { get; protected set; }
}