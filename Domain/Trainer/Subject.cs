namespace Domain.Trainer;

public sealed class Subject
{
    public int Id { get; }
    public string Name { get; private set; } = null!;
}