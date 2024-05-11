namespace Trainer.Database.Entities.Assignments;

public sealed class Solution
{
    public int Id { get; }

    public int AssignmentId { get; }

    public string Code { get; private set; } = null!;

    public DateTime SubmittedAt { get; }
    public DateTime? VerifiedAt { get; }

    public Review? Review { get; }
}