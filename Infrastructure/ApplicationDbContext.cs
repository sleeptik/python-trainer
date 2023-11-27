using Domain.Trainer;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Difficulty> Difficulties => Set<Difficulty>();
    public DbSet<User> Users => Set<User>();
    public DbSet<History> Histories => Set<History>();
    public DbSet<UserRank> UserRanks => Set<UserRank>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}