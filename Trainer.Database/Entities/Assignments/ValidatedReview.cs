namespace Trainer.Database.Entities.Assignments;

public sealed class ValidatedReview : Review
{
    public override bool IsCorrect { get; protected set; } = true;
    public Assessment Assessment { get; private set; } = null!;
}