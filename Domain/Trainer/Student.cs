using Domain.Users;

namespace Domain.Trainer;

public sealed class Student(int userId)
{
    public int UserId { get; private set; } = userId;
    public User User { get; private set; } = null!;

    public int CurrentDifficultyId { get; private set; } = default;
    public Difficulty CurrentDifficulty { get; private set; } = null!;

    public int Score { get; private set; } = default;
}