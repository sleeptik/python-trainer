namespace Domain.Trainer;

public sealed class Exercise
{
    public int Id { get; }
    public string Contents { get; private set; } = null!;
}