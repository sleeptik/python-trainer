using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Unit;

public static class MockControllerContextFactory
{
    public static ControllerContext CreateControllerContextWithUserIdClaim()
    {
        var context = new ControllerContext { HttpContext = new DefaultHttpContext() };

        var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1");
        context.HttpContext.User.AddIdentity(new ClaimsIdentity(new[] { userIdClaim }));

        return context;
    }
}