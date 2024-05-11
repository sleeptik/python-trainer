namespace Trainer.Database.Entities.Users;

public sealed class Role
{
    public int Id { get; }
    public string Name { get; private set; } = null!;
}