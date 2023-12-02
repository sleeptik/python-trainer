namespace Domain.Trainer;

public sealed class Exercise
{
    public int Id { get; private set; }
    public string Contents { get; private set; } = null!;

    public int DifficultyId { get; private set; }
    public Rank Rank { get; private set; } = null!;
    
    public IList<Subject> Subjects { get; private set; } = new List<Subject>();
}