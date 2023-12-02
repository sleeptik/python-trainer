namespace Domain.Trainer;

public sealed class Difficulty
{
    public int Id { get; private set; } = default;
    public string Name { get; private set; } = null!;

    public float LowerBound { get; private set; } = default;
    public float UpperBound { get; private set; } = default;

    public float FallThreshold { get; private set; } = default;
}