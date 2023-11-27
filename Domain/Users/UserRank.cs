namespace Domain.Users;

public class UserRank
{
    public int UserId { get; set; }
    public int AssignedDifficultyId { get; set; }
    public float Metric { get; set; }
}