using Infrastructure;
using Infrastructure.ChatBot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAI.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>(builder => builder.UseNpgsql(connectionString));

        services.AddOpenAIService();
        services.AddTransient<SolutionVerifyingService>();

        return services;
    }
}