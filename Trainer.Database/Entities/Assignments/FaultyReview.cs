namespace Trainer.Database.Entities.Assignments;

public sealed class FaultyReview : Review
{
    public override bool IsCorrect => false;
    
    public IList<Suggestion> Suggestions { get; private set; } = new List<Suggestion>();


    public static Review Create(IList<Suggestion> suggestions)
    {
        return new FaultyReview
        {
            Suggestions = suggestions
        };
    }
}