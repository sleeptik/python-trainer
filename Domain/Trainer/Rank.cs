// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Domain.Trainer;

public sealed class Rank
{
    private Rank()
    {
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;

    public float LowerBound { get; private set; }
    public float UpperBound { get; private set; }

    public float FallThreshold { get; private set; }

    public static Rank Create(string name, float lowerBound, float upperBound, float fallThreshold)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is null or empty");

        if (lowerBound > upperBound)
            throw new ArgumentException("Lower bound greater than upper bound");

        return new Rank
        {
            Name = name,
            LowerBound = lowerBound,
            UpperBound = upperBound,
            FallThreshold = fallThreshold
        };
    }
}