namespace Domain.Trainer;

public sealed class Exercise
{
    public int Id { get; private set; }
    public string Contents { get; private set; } = null!;

    public Difficulty Difficulty { get; private set; } = null!;
}