using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Trainer.Database.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddTrainerContext(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<TrainerContext>(builder => builder.UseNpgsql(connectionString));
        return services;
    }
}