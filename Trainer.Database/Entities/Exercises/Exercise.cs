using Trainer.Database.Entities.Students;

namespace Trainer.Database.Entities.Exercises;

public sealed class Exercise
{
    private readonly IList<Subject> _subjects = new List<Subject>();

    public int Id { get; private set; } = default;
    public string Details { get; private set; } = null!;

    public int RankId { get; private set; } = default;
    public Rank Rank { get; private set; } = null!;

    public IList<Subject> Subjects => _subjects.AsReadOnly();
}