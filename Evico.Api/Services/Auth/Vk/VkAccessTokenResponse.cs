using System.Text.Json.Serialization;

namespace Evico.Api.Services.Auth;

public class VkAccessTokenResponse
{
    [JsonPropertyName("access_token")] public string? AccessToken { get; set; }

    [JsonPropertyName("error")] public string? Error { get; set; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }
}