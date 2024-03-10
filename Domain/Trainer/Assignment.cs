// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Domain.Trainer;

public sealed class Assignment
{
    private Assignment()
    {
    }

    public int StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public int ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;

    public string? Solution { get; private set; }

    public bool? IsPassed { get; private set; }

    public DateTime AssignedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    public void SetResult(bool isPassed)
    {
        IsPassed = IsPassed is null
            ? isPassed
            : throw new InvalidOperationException("Updating completion status is forbidden");
    }

    public void Finish(string solution)
    {
        FinishedAt = FinishedAt is null
            ? DateTime.UtcNow
            : throw new InvalidOperationException("Updating solution is forbidden");

        Solution = solution;
    }

    public static Assignment Create(int studentId, int exerciseId)
    {
        return new Assignment
        {
            StudentId = studentId,
            ExerciseId = exerciseId,
            AssignedAt = DateTime.UtcNow
        };
    }
}