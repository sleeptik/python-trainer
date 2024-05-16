using Microsoft.AspNetCore.Identity;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Auth;

public sealed class Role : IdentityRole<int>
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
}