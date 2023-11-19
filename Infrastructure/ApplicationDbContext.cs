using Domain.Trainer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public DbSet<Exercise> Exercises { get; private set; }
    public DbSet<Difficulty> Difficulties { get; private set; }
    public DbSet<Subject> Subjects { get; private set; }

}