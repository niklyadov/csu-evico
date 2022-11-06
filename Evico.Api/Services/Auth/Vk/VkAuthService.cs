using System.Globalization;
using System.Text.Json;
using Evico.Api.Entity;
using Evico.Api.QueryBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Evico.Api.Services.Auth;

public class VkAuthService
{
    private readonly VkAuthServiceConfiguration _configuration;
    private readonly ProfileQueryBuilder _profileQb;
    private readonly JwtTokensService _tokensService;

    public VkAuthService(JwtTokensService tokensService,
        ProfileQueryBuilder profileQb,
        IOptions<VkAuthServiceConfiguration> configuration)
    {
        _tokensService = tokensService;
        _profileQb = profileQb;
        _configuration = configuration.Value;
    }

    public async Task<ActionResult<BearerRefreshTokenPair>> AuthAsync(string code, string redirectUrl)
    {
        var accessTokenResponse = await GetAccessTokenFromCode(code, redirectUrl);

        if (!string.IsNullOrEmpty(accessTokenResponse.Error))
            throw new InvalidDataException(
                $"{accessTokenResponse.Error}, {accessTokenResponse.ErrorDescription ?? "no-description"}");

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

        var barerToken = _tokensService.CreateAccessTokenForUser(vkUser);
        var refreshToken = _tokensService.CreateRefreshTokenForUser(vkUser);
        var tokens = new BearerRefreshTokenPair(barerToken, refreshToken);

        if (userRegistered)
            return new ObjectResult(tokens) {StatusCode = StatusCodes.Status201Created};

        return new OkObjectResult(tokens);
    }

    private async Task<VkProfileInfo> GetVkProfileInfoAsync(string accessToken,
        string url = "https://api.vk.com/method/users.get")
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("fields", "first_name,last_name,bdate,domain,id,has_photo,photo_50,sex"),
            new KeyValuePair<string, string>("access_token", accessToken),
            new KeyValuePair<string, string>("v", "5.131")
        });

        using var response = await new HttpClient().PostAsync(url, content).ConfigureAwait(false);

        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var responseVkProfileInfo = JsonSerializer.Deserialize<VkProfileInfoResponse>(responseString);

        if (responseVkProfileInfo == null)
            throw new InvalidOperationException("Can't parse vk response");

        return responseVkProfileInfo.Response.Single();
    }

    private async Task<VkAccessTokenResponse> GetAccessTokenFromCode(string code, string redirectUri)
    {
        var clientId = _configuration.ApplicationClientId;
        var clientSecret = _configuration.ApplicationSecret;

        var url =
            $"https://oauth.vk.com/access_token?client_id={clientId}&client_secret={clientSecret}&code={code}&redirect_uri={redirectUri}";

        using var response = await new HttpClient().GetAsync(url).ConfigureAwait(false);

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

        if (!string.IsNullOrEmpty(vkProfileInfo.Domain))
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