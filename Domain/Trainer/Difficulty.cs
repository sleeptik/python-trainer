namespace Domain.Trainer;

public sealed class Difficulty
{
    public int Id { get; }
    public string Name { get; private set; } = null!;
}