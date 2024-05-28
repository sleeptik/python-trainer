using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Auth;
using Trainer.Database.Entities.Students;
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
    private int? CurrentUserId => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    [HttpGet("yandex-redirect")]
    public IActionResult RedirectToAuthPage()
    {
        return Ok(yandexCodeRequestUrlFactory.Create());
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
        if (!User.Identity?.IsAuthenticated ?? false)
            return Unauthorized();

        await signInManager.SignOutAsync();
        return NoContent();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        if (CurrentUserId == 0)
            return Unauthorized();

        var data = await TrainerContext.Users.AsNoTracking()
            .Select(user => new { user.Id, user.Email, user.UserName })
            .SingleAsync(arg => arg.Id == CurrentUserId);

        return Ok(new SimpleUserInfoDto(data.Id, data.UserName, data.Email));
    }

    private async Task<User> GetOrCreateUserByEmailAsync(UserInfo userInfo)
    {
        var user = await TrainerContext.Users.SingleOrDefaultAsync(user => user.Email == userInfo.DefaultEmail);

        if (user is not null)
            return user;

        user = Database.Entities.Auth.User.Create(userInfo.DefaultEmail, userInfo.RealName);
        await signInManager.UserManager.CreateAsync(user);
        await TrainerContext.Users.AddAsync(user);

        var ranks = await TrainerContext.Ranks.ToListAsync();
        var subjects = await TrainerContext.Subjects.ToListAsync();
        var student = Student.Create(user.Id, ranks, subjects);
        await TrainerContext.Students.AddAsync(student);
        
        await TrainerContext.SaveChangesAsync();

        return user;
    }
}