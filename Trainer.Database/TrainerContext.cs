using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.Database.Entities.Auth;
using Trainer.Database.Entities.Exercises;
using Trainer.Database.Entities.Students;

namespace Trainer.Database;

public sealed class TrainerContext(DbContextOptions options) : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Assignment> Assignments => Set<Assignment>();

    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Subject> Subjects => Set<Subject>();

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Rank> Ranks => Set<Rank>();

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainerContext).Assembly);
    }
}