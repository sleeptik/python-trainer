// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Trainer.WebApi.Controllers.Auth.Yandex.Options;

public class YandexAppOptions
{
    public const string Position = "YandexAppOptions";

    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}