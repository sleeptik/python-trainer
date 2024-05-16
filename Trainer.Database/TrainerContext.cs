using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.Database.Entities.Exercises;
using Trainer.Database.Entities.Students;
using Trainer.Database.Entities.Users;

namespace Trainer.Database;

public sealed class TrainerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Rank> Ranks => Set<Rank>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainerContext).Assembly);
    }
}