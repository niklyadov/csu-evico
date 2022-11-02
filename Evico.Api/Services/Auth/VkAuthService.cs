using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Evico.Api.Entity;
using Evico.Api.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Services.Auth;

public class VkAuthService
{
    private readonly JwtTokensService _tokensService;
    private readonly ProfileQueryBuilder _profileQb;

    public VkAuthService(JwtTokensService tokensService, ProfileQueryBuilder profileQb)
    {
        _tokensService = tokensService;
        _profileQb = profileQb;
    }
    
    public async Task<ActionResult<BearerRefreshTokenPair>> AuthAsync(String code)
    {
        var accessTokenResponse = await GetAccessTokenFromCode(code);

        if (!string.IsNullOrEmpty(accessTokenResponse.Error))
            throw new InvalidDataException($"{accessTokenResponse.Error}, {accessTokenResponse.ErrorDescription??"no-description"}");

        if (string.IsNullOrEmpty(accessTokenResponse.AccessToken))
            throw new InvalidOperationException("Access token is empty");
        
        var vkProfileInfo = await GetVkProfileInfoAsync(accessTokenResponse.AccessToken);
        var vkUser = await _profileQb.WithVkId(vkProfileInfo.UserId).FirstOrDefaultAsync();
        var userRegistered = false;

        if (vkUser == null)
        {
            vkUser = await RegisterVkUser(vkProfileInfo);
            userRegistered = true;
        }
            
        var barerToken = _tokensService.GenerateNewTokenForUser(vkUser);
        var refreshToken = _tokensService.GenerateNewTokenForUser(vkUser);
        var tokens = new BearerRefreshTokenPair(barerToken, refreshToken);

        if (userRegistered)
            return new ObjectResult(tokens) {StatusCode = StatusCodes.Status201Created};
            
        return new OkObjectResult(tokens);
    }
    
    private async Task<VkProfileInfo> GetVkProfileInfoAsync(String accessToken, String url = "https://api.vk.com/method/users.get")
    { 
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<String, String>("fields", "first_name,last_name,bdate,domain,id,has_photo,photo_50,sex"), 
            new KeyValuePair<String, String>("access_token", accessToken),
            new KeyValuePair<String, String>("v", "5.131") 
        });
        
        using HttpResponseMessage response = await new HttpClient().PostAsync(url, content).ConfigureAwait(false);

        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var responseVkProfileInfo = JsonSerializer.Deserialize<VkProfileInfoResponse>(responseString);

        if (responseVkProfileInfo == null)
            throw new InvalidOperationException("Can't parse vk response");
        
        return responseVkProfileInfo.Response.Single();
    }
    
    private async Task<VkAccessTokenResponse> GetAccessTokenFromCode(String code)
    {
        var clientId = "51458458";
        var clientSecret = "GT5EbZ28T4SloETf8j0D";
        var redirUri = "https://web.csu-evico.ru:62666/auth/vk-callback";

        var url =
            $"https://oauth.vk.com/access_token?client_id={clientId}&client_secret={clientSecret}&code={code}&redirect_uri={redirUri}";
        
        using HttpResponseMessage response = await new HttpClient().GetAsync(url).ConfigureAwait(false);

        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var responseAccessToken = JsonSerializer.Deserialize<VkAccessTokenResponse>(responseString);

        if (responseAccessToken == null)
            throw new InvalidOperationException("Can't parse vk response");

        return responseAccessToken;
    }

    private async Task<ProfileRecord> RegisterVkUser(VkProfileInfo vkProfileInfo)
    {
        ProfileRecord newUser = new()
        {
            VkUserId = vkProfileInfo.UserId,
            Name = vkProfileInfo.UserId.ToString(),
            Firstname = vkProfileInfo.Firstname,
            Lastname = vkProfileInfo.Lastname
        };

        if (!String.IsNullOrEmpty(vkProfileInfo.Domain))
            newUser.Name = vkProfileInfo.Domain;

        if (DateTime.TryParse(vkProfileInfo.BirthDate, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var birthDate))
            newUser.BirthDate = birthDate;

        if (vkProfileInfo.HasPhoto)
        {
            // TODO: добавить фото
            //vkProfileInfo.PhotoUri;
        }
                
        return await _profileQb.AddAsync(newUser);
    }
}

public class VkAccessTokenResponse
{
    [JsonPropertyName("access_token")] 
    public string? AccessToken { get; set; }
    [JsonPropertyName("error")] 
    public string? Error { get; set; }
    [JsonPropertyName("error_description")] 
    public string? ErrorDescription { get; set; }
}

public class VkProfileInfoResponse
{
    [JsonPropertyName("response")]
    public List<VkProfileInfo> Response { get; set; } = default!;
}

public class VkProfileInfo
{
    [JsonPropertyName("id")]
    public long UserId { get; set; }
    
    [JsonPropertyName("first_name")]
    public String Firstname { get; set; } = string.Empty;
    
    [JsonPropertyName("last_name")]
    public String Lastname { get; set; } = string.Empty;
    
    [JsonPropertyName("bdate")]
    public String BirthDate { get; set; } = string.Empty;

    [JsonPropertyName("domain")] 
    public String Domain { get; set; } = string.Empty;
    
    [JsonConverter(typeof(JsonBoolCustomConverter))]
    [JsonPropertyName("has_photo")]
    public bool HasPhoto { get; set; }
    
    [JsonPropertyName("photo_50")]
    public Uri PhotoUri { get; set; }
    
    [JsonPropertyName("sex")]
    public int Sex { get; set; }
}