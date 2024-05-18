using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Trainer.WebApi.Controllers.Auth.Yandex.DTO;
using Trainer.WebApi.Controllers.Auth.Yandex.Json;
using Trainer.WebApi.Controllers.Auth.Yandex.Options;

namespace Trainer.WebApi.Controllers.Auth.Yandex.Services;

public sealed class YandexCodeExchangeService(HttpClient httpClient, IOptions<YandexAppOptions> options)
{
    public async Task<CodeExchangeResult> ExchangeCodeForOAuthTokenAsync(int code)
    {
        SetAuthorizationToken(EncodeAuthenticationTokenFromOptions());
        var response = await httpClient.PostAsync(
            $"token?grant_type=authorization_code&code={code}", CreateBodyFromCode(code)
        );

        var exchangeResult = await response.Content.ReadFromJsonAsync<CodeExchangeResult>(
            YandexJsonSerializerOptions.Default
        );

        return exchangeResult!;
    }

    private void SetAuthorizationToken(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
    }

    private static FormUrlEncodedContent CreateBodyFromCode(int code)
    {
        var values = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "code", code.ToString() }
        };
        return new FormUrlEncodedContent(values);
    }

    private string EncodeAuthenticationTokenFromOptions()
    {
        var clientPair = $"{options.Value.ClientId}:{options.Value.ClientSecret}";
        var bytes = Encoding.UTF8.GetBytes(clientPair);
        return Convert.ToBase64String(bytes);
    }
}