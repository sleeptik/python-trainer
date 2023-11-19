using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure;

// ReSharper disable once UnusedType.Global
public sealed class ApplicationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql("Server=localhost;Port=5432;Database=trainer;UserId=postgres;Password=postgres;")
            .Options;

        return new ApplicationDbContext(options);
    }
}