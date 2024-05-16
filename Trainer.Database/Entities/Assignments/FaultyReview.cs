namespace Trainer.Database.Entities.Assignments;

public sealed class FaultyReview : Review
{
    public override bool IsCorrect { get; protected set; } = false;
    public IList<Suggestion> Suggestions { get; private set; } = new List<Suggestion>();
}