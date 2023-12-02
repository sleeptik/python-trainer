using WebApi.Features.EducationAdmin.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<RankService>();
        return services;
    }
}