using Evico.Api.Extensions;
using Evico.Api.Services.Auth;
using Evico.Api.Services.Auth.Vk;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Auth;

public class AuthViaVkUseCase
{
    private readonly JwtTokensService _tokensService;
    private readonly VkAuthService _vkAuthService;

    public AuthViaVkUseCase(VkAuthService vkAuthService, JwtTokensService tokensService)
    {
        _vkAuthService = vkAuthService;
        _tokensService = tokensService;
    }

    public async Task<ActionResult<BearerRefreshTokenPair>> AuthViaVk(string code, string redirectUrl)
    {
        var vkAccessTokenResult = await _vkAuthService.GetAccessTokenFromCode(code, redirectUrl);
        if (vkAccessTokenResult.IsFailed)
            return new BadRequestObjectResult(vkAccessTokenResult.GetReport());

        var vkProfileInfoResult = await _vkAuthService.GetVkProfileInfoAsync(vkAccessTokenResult.Value);
        if (vkProfileInfoResult.IsFailed)
            return new BadRequestObjectResult(vkProfileInfoResult.GetReport());
        var vkProfileInfo = vkProfileInfoResult.Value;
        
        var userRegistered = false;
        var vkUser = ( await _vkAuthService.GetExistingProfileAsync(vkProfileInfo)).ValueOrDefault;

        if (vkUser is null)
        {
            var userRegisterResult = await _vkAuthService.RegisterVkUser(vkProfileInfo);
            if (userRegisterResult.IsFailed)
                return new BadRequestObjectResult(userRegisterResult.GetReport());
            vkUser = userRegisterResult.Value;
            userRegistered = true;
        }

        var barerToken = _tokensService.CreateAccessTokenForUser(vkUser);
        var refreshToken = _tokensService.CreateRefreshTokenForUser(vkUser);
        var tokens = new BearerRefreshTokenPair(barerToken, refreshToken);

        if (userRegistered)
            return new ObjectResult(tokens) {StatusCode = StatusCodes.Status201Created};

        return new OkObjectResult(tokens);
    }
}