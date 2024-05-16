using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Trainer.Database;

// ReSharper disable once UnusedType.Global
public sealed class DesignTimeTrainerContextFactory : IDesignTimeDbContextFactory<TrainerContext>
{
    public TrainerContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<TrainerContext>()
            .UseNpgsql(args[0])
            .Options;
        
        // Server=localhost;Port=5432;Database=trainer;UserId=postgres;Password=postgres;
        return new TrainerContext(options);
    }
}