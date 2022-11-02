using System.Text;
using System.Text.Json;

namespace Evico.Web.Services;

public class VkAuthService
{
    public async Task<BearerRefreshTokenPair> AuthViaVk(string vkAccessToken)
    {
        return await AuthViaVkRequest(vkAccessToken);
    }
    
    private async Task<BearerRefreshTokenPair> AuthViaVkRequest(string accessToken, string url = "http://api.csu-evico.ru:61666/auth/vkGateway")
    {
        using HttpContent content = new StringContent(JsonSerializer.Serialize(accessToken), Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await new HttpClient().PostAsync(url, content).ConfigureAwait(false);

        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var responseTokens = JsonSerializer.Deserialize<BearerRefreshTokenPair>(responseString);

        if (responseTokens == null)
            throw new InvalidOperationException("Tokens is null");
        
        return responseTokens;
    }
}