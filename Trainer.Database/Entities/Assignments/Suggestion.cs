namespace Trainer.Database.Entities.Assignments;

public sealed class Suggestion
{
    public int SolutionId { get; }
    public Solution Solution { get; }

    public string Mistake { get; }
    public string Advice { get; }
}