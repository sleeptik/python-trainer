using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;
using Trainer.Verification.ChatBot;
using Trainer.Verification.Python;

namespace Trainer.Verification.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddVerificationServices(this IServiceCollection services)
    {
        services.AddOpenAIService();

        services.AddTransient<AiVerificationService>();
        services.AddTransient<SourceCodeCompilingService>();
        services.AddTransient<VerificationService>();

        return services;
    }
}