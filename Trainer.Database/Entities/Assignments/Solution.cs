// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public sealed class Solution
{
    public int Id { get; private set; }

    public int AssignmentId { get; private set; }
    public Assignment Assignment { get; private set; }

    public string Code { get; private set; } = null!;

    public DateTime SubmittedAt { get; private set; }
    public DateTime? VerifiedAt { get; private set; }

    public Review? Review { get; private set; }

    public static Solution Create(string code)
    {
        return new Solution
        {
            Code = code,
            SubmittedAt = DateTime.UtcNow
        };
    }
}