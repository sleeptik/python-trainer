using Trainer.Database.Entities.Assignments;
using Trainer.Database.Entities.Auth;
using Trainer.Database.Entities.Exercises;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Trainer.Database.Entities.Students;

public sealed class Student
{
    private IList<Assignment> _assignments = new List<Assignment>();

    private float _score;
    private IList<Subject> _subjects = new List<Subject>();

    private Student()
    {
    }

    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public int CurrentRankId { get; set; }
    public Rank CurrentRank { get; private set; } = null!;

    public float Score
    {
        get => _score;
        set => _score = Math.Max(0, value);
    }

    public IReadOnlyList<Assignment> Assignments => _assignments.AsReadOnly();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();

    public static Student Create(int userId, IEnumerable<Rank> ranks, IEnumerable<Subject> subjects)
    {
        var enumeratedRanks = ranks.ToList();
        var enumeratedSubjects = subjects.ToList();

        if (enumeratedRanks.Count == 0)
            throw new ArgumentException();

        var Rank = enumeratedRanks.OrderBy(rank => rank.LowerBound).Skip(1).First();
        var Score = (Rank.LowerBound + Rank.UpperBound)/2.0f;

        return new Student
        {
            UserId = userId,
            CurrentRankId = Rank.Id,
            _subjects = enumeratedSubjects,
            _score = Score
        };
    }
}