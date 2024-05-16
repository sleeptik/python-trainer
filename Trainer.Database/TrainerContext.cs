using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.Database.Entities.Auth;
using Trainer.Database.Entities.Exercises;
using Trainer.Database.Entities.Prompts;
using Trainer.Database.Entities.Students;

namespace Trainer.Database;

public sealed class TrainerContext(DbContextOptions options) : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Assignment> Assignments => Set<Assignment>();

    public DbSet<Solution> Solutions => Set<Solution>();

    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Assessment> Assessments => Set<Assessment>();
    public DbSet<Suggestion> Suggestions => Set<Suggestion>();

    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Subject> Subjects => Set<Subject>();

    public DbSet<Prompt> Prompts => Set<Prompt>();

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Rank> Ranks => Set<Rank>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainerContext).Assembly);
    }
}