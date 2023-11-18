namespace Domain.Users;

public sealed class User
{
    private readonly IList<Role> _roles = new List<Role>();

    public int Id { get; private set; } = default;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    public void AddRole(Role role)
    {
        _roles.Add(role);
    }

    public bool RemoveRole(Role role)
    {
        return _roles.Remove(role);
    }
}