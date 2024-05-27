// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public sealed class Assessment
{
    public int Id { get; private set; }

    public int ReviewId { get; private set; }
    public ValidatedReview Review { get; private set; } = null!;

    public float Readability { get; private set; }
    public float Complexity { get; private set; }
    public float Creativity { get; private set; }
    public float Efficiency { get; private set; }
    public float Structure { get; private set; }
    public float Reasoning { get; private set; }

    public static Assessment Create(float readability, float complexity, float creativity,
        float efficiency, float structure, float reasoning)
    {
        return new Assessment
        {
            Readability = readability,
            Complexity = complexity,
            Creativity = creativity,
            Efficiency = efficiency,
            Structure = structure,
            Reasoning = reasoning
        };
    }
}