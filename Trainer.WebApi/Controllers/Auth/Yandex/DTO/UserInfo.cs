namespace Trainer.WebApi.Controllers.Auth.Yandex.DTO;

public sealed record UserInfo(string Id, string Login, string RealName, string DefaultEmail, string? Password = null);