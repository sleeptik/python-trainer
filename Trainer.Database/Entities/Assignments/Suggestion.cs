// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public sealed class Suggestion
{
    public int SolutionId { get; private set; }
    public Solution Solution { get; private set; }

    public string Mistake { get; private set; }
    public string Advice { get; private set; }
}