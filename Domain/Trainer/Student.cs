using Domain.Users;

namespace Domain.Trainer;

public sealed class Student(int userId)
{
    private readonly IList<Assignment> _assignments = new List<Assignment>();

    public int UserId { get; private set; } = userId;
    public User User { get; private set; } = null!;

    public int CurrentRankId { get; private set; } = default;
    public Rank CurrentRank { get; private set; } = null!;

    public int Score { get; private set; } = default;

    public IList<Assignment> Assignments => _assignments.AsReadOnly();
}