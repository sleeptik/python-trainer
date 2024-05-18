using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Auth;
using Trainer.WebApi.Common;
using Trainer.WebApi.Controllers.Auth.Yandex;
using Trainer.WebApi.Controllers.Auth.Yandex.DTO;
using Trainer.WebApi.Controllers.Auth.Yandex.Services;

namespace Trainer.WebApi.Controllers.Auth;

[Route("api/auth")]
public sealed class AuthenticationController(
    SignInManager<User> signInManager,
    SimpleYandexUserInfoRetriever userInfoRetriever,
    YandexCodeRequestUrlFactory yandexCodeRequestUrlFactory
) : ApiController
{
    [HttpGet("yandex-redirect")]
    public IActionResult RedirectToAuthPage()
    {
        return Redirect(yandexCodeRequestUrlFactory.Create());
    }
    
    [HttpPost("yandex-login")]
    public async Task<IActionResult> LogIn(int code)
    {
        var info = await userInfoRetriever.GetUserInfoAsync(code);
        var user = await GetOrCreateUserByEmailAsync(info);
        
        await signInManager.SignInAsync(user, true);

        return NoContent();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await signInManager.SignOutAsync();
        return NoContent();
    }

    private async Task<User> GetOrCreateUserByEmailAsync(UserInfo userInfo)
    {
        var user =  await TrainerContext.Users.SingleOrDefaultAsync(user => user.Email == userInfo.DefaultEmail);

        if (user is not null)
            return user;

        user = Database.Entities.Auth.User.Create(userInfo.DefaultEmail, userInfo.RealName);
        await signInManager.UserManager.CreateAsync(user);
        await TrainerContext.Users.AddAsync(user);
        await TrainerContext.SaveChangesAsync();

        return user;
    }
}