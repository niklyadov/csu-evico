using System.Text.Json.Serialization;

namespace Evico.Api.Services.Auth.Vk;

public class VkProfileInfoResponse
{
    [JsonPropertyName("response")] public List<VkProfileInfo> Response { get; set; } = default!;
}