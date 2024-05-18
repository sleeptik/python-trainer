using System.Net.Http.Headers;
using Trainer.WebApi.Controllers.Auth.Yandex.DTO;
using Trainer.WebApi.Controllers.Auth.Yandex.Json;

namespace Trainer.WebApi.Controllers.Auth.Yandex.Services;

public sealed class YandexUserService(HttpClient httpClient)
{
    public async Task<UserInfo> GetUserInformationUsingTokenAsync(string oauthToken)
    {
        SetAuthorizationToken(oauthToken);
        var info = await httpClient.GetFromJsonAsync<UserInfo>("info", YandexJsonSerializerOptions.Default);
        return info!;
    }

    private void SetAuthorizationToken(string oauthToken)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", oauthToken);
    }
}