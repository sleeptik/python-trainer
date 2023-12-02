using Domain.Users;

namespace Domain.Trainer;

public sealed class Student(int userId)
{
    private readonly IList<Assignment> _assignments = new List<Assignment>();
    private float _score;

    public int UserId { get; private set; } = userId;
    public User User { get; private set; } = null!;

    public int CurrentRankId { get; private set; } = default;
    public Rank CurrentRank { get; private set; } = null!;

    public float Score
    {
        get => _score;
        set => _score = Math.Max(0, value);
    }

    public IList<Assignment> Assignments => _assignments.AsReadOnly();
}