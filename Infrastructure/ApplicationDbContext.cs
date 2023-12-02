using Domain.Trainer;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Rank> Ranks => Set<Rank>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}