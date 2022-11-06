using Evico.Api.Services.Auth;
using Evico.Api.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

public class AuthController : BaseController
{
    private readonly AuthViaVkUseCase _authViaVkUseCase;
    private readonly CreateNewTokensUseCase _createNewTokensUseCase;

    public AuthController(AuthViaVkUseCase authViaVkUseCase, CreateNewTokensUseCase createNewTokensUseCase)
    {
        _authViaVkUseCase = authViaVkUseCase;
        _createNewTokensUseCase = createNewTokensUseCase;
    }

    [HttpPost("vkGateway")]
    [AllowAnonymous]
    public async Task<ActionResult<BearerRefreshTokenPair>> VkGateway([FromBody] string accessToken,
        [FromQuery] string redirectUrl)
    {
        return await _authViaVkUseCase.AuthViaVk(accessToken, redirectUrl);
    }
    
    [HttpPost("createNewToken")]
    [AllowAnonymous]
    public async Task<ActionResult<BearerRefreshTokenPair>> CreateNewToken([FromQuery] String username)
    {
        return await _createNewTokensUseCase.CreateNewToken(username);
    }
}