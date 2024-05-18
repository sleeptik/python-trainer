using Trainer.WebApi.Controllers.Auth.Yandex.DTO;
using Trainer.WebApi.Controllers.Auth.Yandex.Services;

namespace Trainer.WebApi.Controllers.Auth.Yandex;

public sealed class SimpleYandexUserInfoRetriever(
    YandexCodeExchangeService exchangeService,
    YandexUserService userService
)
{
    public async Task<UserInfo> GetUserInfoAsync(int confirmationCode)
    {
        var exchangeResult = await exchangeService.ExchangeCodeForOAuthTokenAsync(confirmationCode);
        var info = await userService.GetUserInformationUsingTokenAsync(exchangeResult.AccessToken);
        return info;
    }
}