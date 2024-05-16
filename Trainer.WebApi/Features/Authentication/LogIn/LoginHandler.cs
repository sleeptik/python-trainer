using System.Security.Claims;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Trainer.WebApi.Features.Authentication.LogIn;

public class LoginHandler(ApplicationDbContext context) : IRequestHandler<LogInRequest, ClaimsPrincipal?>
{
    public async Task<ClaimsPrincipal?> Handle(LogInRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users.AsNoTracking()
            .Where(user => user.Email == request.Name && user.Password == request.Password)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return null;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principle = new ClaimsPrincipal(identity);

        return principle;
    }
}