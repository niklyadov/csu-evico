using System.Text.Json.Serialization;

namespace Evico.Api.Services.Auth;

public class VkProfileInfo
{
    [JsonPropertyName("id")] public long UserId { get; set; }

    [JsonPropertyName("first_name")] public string Firstname { get; set; } = string.Empty;

    [JsonPropertyName("last_name")] public string Lastname { get; set; } = string.Empty;

    [JsonPropertyName("bdate")] public string BirthDate { get; set; } = string.Empty;

    [JsonPropertyName("domain")] public string Domain { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonBoolCustomConverter))]
    [JsonPropertyName("has_photo")]
    public bool HasPhoto { get; set; }

    [JsonPropertyName("photo_50")] public Uri PhotoUri { get; set; }

    [JsonPropertyName("sex")] public int Sex { get; set; }
}