using Microsoft.Extensions.Configuration;
using OpenAI.Extensions;
using Trainer.Verification.ChatBot;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        services.AddOpenAIService();
        services.AddTransient<SolutionVerifyingService>();

        return services;
    }
}