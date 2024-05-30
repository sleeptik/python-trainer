namespace Trainer.Database.Entities.Assignments;

public sealed class AssignmentStatus
{
    public const string New = "New";
    public const string Finished = "Finished";
    public const string Verified = "Verified";
    public const string Failed = "Failed";
    
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
}