﻿using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Trainer.WebApi.Common;
using Trainer.WebApi.Features.Authentication.LogIn;

namespace Trainer.WebApi.Features.Authentication;

public class AuthenticationController(IMediator mediator) : ApiController
{
    [HttpPost("api/auth/login")]
    public async Task<IActionResult> LogIn(LogInRequest request)
    {
        var result = await mediator.Send(request);

        if (result is null)
            return NotFound();

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, result
        );

        return NoContent();
    }
}