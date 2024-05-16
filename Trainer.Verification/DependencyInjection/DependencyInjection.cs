using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;
using Trainer.Verification.ChatBot;

namespace Trainer.Verification.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddVerificationServices(this IServiceCollection services)
    {
        services.AddOpenAIService();
        services.AddTransient<SolutionVerifyingService>();

        return services;
    }
}