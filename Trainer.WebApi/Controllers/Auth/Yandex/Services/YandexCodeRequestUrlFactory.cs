using Microsoft.Extensions.Options;
using Trainer.WebApi.Controllers.Auth.Yandex.Options;

namespace Trainer.WebApi.Controllers.Auth.Yandex.Services;

public class YandexCodeRequestUrlFactory(IOptions<YandexAppOptions> options)
{
    private static string? _redirectUrl;

    public string Create()
    {
        _redirectUrl ??= $"https://oauth.yandex.ru/authorize?response_type=code&client_id={options.Value.ClientId}";
        return _redirectUrl;
    }
}