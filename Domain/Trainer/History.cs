namespace Domain.Trainer;

public sealed class History(int userId, int exerciseId, bool isPassed)
{
    public int StudentId { get; private set; } = userId;
    public Student Student { get; private set; } = null!;

    public int ExerciseId { get; private set; } = exerciseId;
    public Exercise Exercise { get; private set; } = null!;

    public bool? IsPassed { get; private set; } = isPassed;

    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; private set; } = null;
}