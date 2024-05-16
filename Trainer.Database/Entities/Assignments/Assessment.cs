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
}