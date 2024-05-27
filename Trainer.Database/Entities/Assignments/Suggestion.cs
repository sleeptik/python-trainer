// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public sealed class Suggestion
{
    public int Id { get; private set; }

    public int ReviewId { get; private set; }
    public FaultyReview Review { get; private set; } = null!;

    public string Mistake { get; private set; } = null!;
    public string Advice { get; private set; } = null!;

    public static Suggestion Create(string mistake, string? advice)
    {
        return new Suggestion
        {
            Mistake = mistake,
            Advice = advice
        };
    }
}