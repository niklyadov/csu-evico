using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

public class AuthController : BaseController
{
    private readonly VkAuthService _vkAuthService;

    public AuthController(VkAuthService vkAuthService)
    {
        _vkAuthService = vkAuthService;
    }
    
    [HttpPost("vkGateway")]
    public async Task<ActionResult<BearerRefreshTokenPair>> VkGateway([FromBody] string accessToken, [FromQuery] String redirectUrl)
    {
        return await _vkAuthService.AuthAsync(accessToken, redirectUrl);
    }
}