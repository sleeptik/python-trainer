using Domain.Trainer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Subject> Subjects { get; private set; } = null!;
    public DbSet<Exercise> Exercises { get; private set; } = null!;
    public DbSet<Difficulty> Difficulties { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}