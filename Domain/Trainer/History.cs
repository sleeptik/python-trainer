using Domain.Users;

namespace Domain.Trainer;

public sealed class History(int userId, int exerciseId, bool isPassed)
{
    public int UserId { get; private set; } = userId;
    public User User { get; private set; } = null!;

    public int ExerciseId { get; private set; } = exerciseId;
    public Exercise Exercise { get; private set; } = null!;

    public bool IsPassed { get; private set; } = isPassed;
    public DateTime Finished { get; private set; } = DateTime.Now;
}