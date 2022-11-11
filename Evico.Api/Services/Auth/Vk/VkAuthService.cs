using System.Globalization;
using System.Text.Json;
using Evico.Api.Entities;
using FluentResults;
using Microsoft.Extensions.Options;

namespace Evico.Api.Services.Auth.Vk;

public class VkAuthService
{
    private readonly VkAuthServiceConfiguration _configuration;
    private readonly ProfileService _profileService;

    public VkAuthService(ProfileService profileService,
        IOptions<VkAuthServiceConfiguration> configuration)
    {
        _profileService = profileService;
        _configuration = configuration.Value;
    }

    public async Task<Result<ProfileRecord>> GetExistingProfileAsync(VkProfileInfo vkProfileInfo)
    {
        return await _profileService.GetByVkIdAsync(vkProfileInfo.UserId);
    }
    
    public async Task<Result<ProfileRecord>> RegisterVkUser(VkProfileInfo vkProfileInfo)
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

        return await _profileService.AddAsync(newUser);
    }

    public async Task<Result<VkProfileInfo>> GetVkProfileInfoAsync(string accessToken,
        string url = "https://api.vk.com/method/users.get")
    {
        return await Result.Try(async () =>
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("fields",
                    "first_name,last_name,bdate,domain,id,has_photo,photo_50,sex"),
                new KeyValuePair<string, string>("access_token", accessToken),
                new KeyValuePair<string, string>("v", "5.131")
            });

            using var response = await new HttpClient().PostAsync(url, content).ConfigureAwait(false);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseVkProfileInfo = JsonSerializer.Deserialize<VkProfileInfoResponse>(responseString);

            if (responseVkProfileInfo == null)
                throw new InvalidOperationException("Can't parse vk response");

            return responseVkProfileInfo.Response.Single();
        });
    }

    public async Task<Result<string>> GetAccessTokenFromCode(string code, string redirectUri)
    {
        return await Result.Try(async () =>
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

            if (string.IsNullOrEmpty(responseAccessToken.AccessToken))
                throw new InvalidOperationException($"Error with receive access token: {responseAccessToken.Error}. " +
                                                    $"Description: {responseAccessToken.ErrorDescription} ");

            return responseAccessToken.AccessToken!;
        });
    }
}