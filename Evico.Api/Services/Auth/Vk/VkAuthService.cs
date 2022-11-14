using System.Globalization;
using System.Text.Json;
using Evico.Api.Entities;
using FluentResults;
using Microsoft.Extensions.Options;

namespace Evico.Api.Services.Auth.Vk;

public class VkAuthService
{
    private readonly VkAuthServiceConfiguration _configuration;
    private readonly FileService _fileService;
    private readonly ProfilePhotoService _photoService;
    private readonly ProfileService _profileService;

    public VkAuthService(ProfileService profileService, FileService fileService, ProfilePhotoService photoService,
        IOptions<VkAuthServiceConfiguration> configuration)
    {
        _profileService = profileService;
        _fileService = fileService;
        _photoService = photoService;
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

        var addUserResult = await _profileService.AddAsync(newUser);
        if (addUserResult.IsFailed)
            return Result.Fail(new Error("Add user failed")
                .CausedBy(addUserResult.Errors));

        var addedUser = addUserResult.Value;

        // todo: обработать результат загрузки фото профиля (или не обрабатывать вообще)
        await UploadProfilePhoto(vkProfileInfo, addedUser);

        return addedUser;
    }

    private async Task<Result> UploadProfilePhoto(VkProfileInfo vkProfileInfo, ProfileRecord profile)
    {
        if (!vkProfileInfo.HasPhoto)
            return Result.Ok();

        var minioBucket = MinioBucketNames.UserAvatars;
        var minioInternalId = Guid.NewGuid();

        var fileUploadResult = await _fileService.UploadFileFromUri(vkProfileInfo.PhotoUri,
            minioBucket, minioInternalId.ToString());
        if (fileUploadResult.IsFailed)
            return Result.Fail(new Error("Photo uploading error")
                .CausedBy(fileUploadResult.Errors));

        var userPhotoResult = await _photoService.AddAsync(new ProfilePhotoRecord
        {
            MinioBucket = minioBucket,
            MinioInternalId = minioInternalId,
            Author = profile
        });
        if (userPhotoResult.IsFailed)
            return Result.Fail(new Error("Photo adding error")
                .CausedBy(userPhotoResult.Errors));

        profile.Photo = userPhotoResult.Value;

        var userUpdateResult = await _profileService.UpdateAsync(profile);
        if (userUpdateResult.IsFailed)
            return Result.Fail(new Error("User updating failed"));

        return Result.Ok();
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