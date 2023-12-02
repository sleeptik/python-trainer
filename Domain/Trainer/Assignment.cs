namespace Domain.Trainer;

public sealed class Assignment(int studentId, int exerciseId)
{
    public int StudentId { get; private set; } = studentId;
    public Student Student { get; private set; } = null!;

    public int ExerciseId { get; private set; } = exerciseId;
    public Exercise Exercise { get; private set; } = null!;

    public bool? IsPassed { get; private set; }

    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; private set; }

    public void SetResult(bool isPassed)
    {
        IsPassed = IsPassed is null
            ? isPassed
            : throw new InvalidOperationException("Updating completion status is forbidden");
    }

    public void Finish()
    {
        FinishedAt = FinishedAt is null
            ? DateTime.UtcNow
            : throw new InvalidOperationException("Updating finish date is forbidden");
    }
}