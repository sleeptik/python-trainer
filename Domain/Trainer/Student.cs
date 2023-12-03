using Domain.Users;

namespace Domain.Trainer;

public sealed class Student(int userId)
{
    private readonly IList<Assignment> _assignments = new List<Assignment>();
    private readonly IList<Subject> _subjects = new List<Subject>();
    private float _score;

    public int UserId { get; private set; } = userId;
    public User User { get; private set; } = null!;

    public int CurrentRankId { get; set; } = default;
    public Rank CurrentRank { get; private set; } = null!;

    public float Score
    {
        get => _score;
        set => _score = Math.Max(0, value);
    }

    public IReadOnlyList<Assignment> Assignments => _assignments.AsReadOnly();
    public IReadOnlyList<Subject> SubjectsToStudy => _subjects.AsReadOnly();
}