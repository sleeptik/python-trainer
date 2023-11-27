using Domain.Users;

namespace Domain.Trainer;

public sealed class History
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public int ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;
    
    public bool IsPassed { get; private set; }
    public DateTime Finished { get; private set; }
}