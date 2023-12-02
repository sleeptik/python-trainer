namespace Domain.Trainer;

public sealed class Exercise
{
    private readonly IList<Subject> _subjects = new List<Subject>();

    public int Id { get; private set; } = default;
    public string Contents { get; private set; } = null!;

    public int RankId { get; private set; } = default;
    public Rank Rank { get; private set; } = null!;

    public IList<Subject> Subjects => _subjects.AsReadOnly();
}