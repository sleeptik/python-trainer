using Trainer.WebApi.Services;

namespace Trainer.WebApi.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<InstantVerificationService>();
        return services;
    }
}