using Evico.Api.Extensions;
using Evico.Api.Services.Auth;
using Evico.Api.Services.Auth.Vk;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Auth;

public class AuthViaVkUseCase
{
    private readonly VkAuthService _vkAuthService;
    private readonly JwtTokensService _tokensService;

    public AuthViaVkUseCase(VkAuthService vkAuthService, JwtTokensService tokensService)
    {
        _vkAuthService = vkAuthService;
        _tokensService = tokensService;
    }
    
    public async Task<ActionResult<BearerRefreshTokenPair>> AuthViaVk(String code, String redirectUrl)
    {
        var vkAccessTokenResult = await _vkAuthService.GetAccessTokenFromCode(code, redirectUrl);
        if (vkAccessTokenResult.IsFailed)
            return new BadRequestObjectResult(vkAccessTokenResult.GetReport());

        var vkProfileInfoResult = await _vkAuthService.GetVkProfileInfoAsync(vkAccessTokenResult.Value);
        if (vkProfileInfoResult.IsFailed)
            return new BadRequestObjectResult(vkProfileInfoResult.GetReport());
        
        var registeredUserResult = await _vkAuthService.RegisterOrGetExistingProfileAsync(vkProfileInfoResult.Value);
        if (registeredUserResult.IsFailed)
            return new BadRequestObjectResult(registeredUserResult.GetReport());
        var vkUser = registeredUserResult.Value;
        
        var barerToken = _tokensService.CreateAccessTokenForUser(vkUser);
        var refreshToken = _tokensService.CreateRefreshTokenForUser(vkUser);
        var tokens = new BearerRefreshTokenPair(barerToken, refreshToken);

        // todo: доделать возврат кода 201, если пользователь был именно зарегистрирован
        //if (userRegistered)
        //    return new ObjectResult(tokens) {StatusCode = StatusCodes.Status201Created};

        return new OkObjectResult(tokens);
    }
}