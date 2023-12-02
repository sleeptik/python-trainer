namespace Domain.Trainer;

public sealed class Assignment(int userId, int exerciseId)
{
    public int StudentId { get; private set; } = userId;
    public Student Student { get; private set; } = null!;

    public int ExerciseId { get; private set; } = exerciseId;
    public Exercise Exercise { get; private set; } = null!;

    public bool? IsPassed { get; private set; }

    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; private set; }

    public void SetCompletion(bool isPassed)
    {
        IsPassed = IsPassed is null
            ? isPassed
            : throw new InvalidOperationException("Updating completion status is forbidden");

        FinishedAt = FinishedAt is null
            ? DateTime.UtcNow
            : throw new InvalidOperationException("Updating finish date is forbidden");
    }
}