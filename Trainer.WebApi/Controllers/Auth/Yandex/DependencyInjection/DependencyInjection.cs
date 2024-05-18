using Trainer.WebApi.Controllers.Auth.Yandex.Options;
using Trainer.WebApi.Controllers.Auth.Yandex.Services;

namespace Trainer.WebApi.Controllers.Auth.Yandex.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddYandexServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<YandexAppOptions>(configuration.GetSection(YandexAppOptions.Position));

        services.AddTransient<YandexCodeRequestUrlFactory>();
        
        services.AddTransient<YandexCodeExchangeService>();
        services.AddTransient<YandexUserService>();
        services.AddTransient<SimpleYandexUserInfoRetriever>();

        services.AddHttpClient<YandexCodeExchangeService>(
            client => client.BaseAddress = new Uri("https://oauth.yandex.ru/")
        );
        services.AddHttpClient<YandexUserService>(
            client => client.BaseAddress = new Uri("https://login.yandex.ru/")
        );

        return services;
    }
}