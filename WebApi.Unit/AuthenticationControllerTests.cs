using Domain.Users;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Features.Authentication;
using WebApi.Features.Authentication.LogIn;

namespace WebApi.Unit;

public class AuthenticationControllerTests
{
    private readonly ServiceCollection _serviceCollection = new();

    public AuthenticationControllerTests()
    {
        _serviceCollection
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        _serviceCollection
            .AddLogging();

        _serviceCollection
            .AddTransient<AuthenticationController>(provider =>
            {
                var mediator = provider.GetRequiredService<IMediator>();
                var controller = new AuthenticationController(mediator);
                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
                controller.ControllerContext.HttpContext.RequestServices = provider;
                return controller;
            });

        _serviceCollection
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<LoginHandler>());

        _serviceCollection
            .AddDbContext<ApplicationDbContext>(builder => builder.UseInMemoryDatabase("base"));
    }

    [Fact]
    public async Task Login_CorrectData_LoginSucceeds()
    {
        var logInRequest = new LogInRequest("example@mail.com", "examplePassword");

        var provider = _serviceCollection.BuildServiceProvider();

        var user = User.Create("example@mail.com", "examplePassword");

        var context = provider.GetRequiredService<ApplicationDbContext>();
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var controller = provider.GetRequiredService<AuthenticationController>();

        var result = await controller.LogIn(logInRequest);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task LogIn_IncorrectData_LoginFails()
    {
        var logInRequest = new LogInRequest("example123@mail.com", "examplePassword");

        var provider = _serviceCollection.BuildServiceProvider();

        var user = User.Create("example@mail.com", "examplePassword");

        var context = provider.GetRequiredService<ApplicationDbContext>();
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var controller = provider.GetRequiredService<AuthenticationController>();

        var result = await controller.LogIn(logInRequest);

        result.Should().BeOfType<NotFoundResult>();
    }
}