namespace Domain.Users;

public sealed class Role
{
    public int Id { get; }
    public string Name { get; private set; } = null!;
}