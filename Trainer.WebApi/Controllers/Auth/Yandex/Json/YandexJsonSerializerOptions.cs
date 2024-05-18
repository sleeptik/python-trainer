using System.Text.Json;

namespace Trainer.WebApi.Controllers.Auth.Yandex.Json;

public static class YandexJsonSerializerOptions
{
    public static readonly JsonSerializerOptions Default = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
}