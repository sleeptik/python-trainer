namespace Trainer.WebApi.Controllers.Auth.Yandex.DTO;

public sealed record CodeExchangeResult(
    string TokenType,
    string AccessToken,
    int ExpiresIn,
    string RefreshToken,
    string Scope
);