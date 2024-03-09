using System.Security.Claims;
using MediatR;

namespace WebApi.Features.Authentication.LogIn;

public record LogInRequest(string Name, string Password) : IRequest<ClaimsPrincipal?>;