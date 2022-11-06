using Evico.Api.Entity;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<ActionResult<BearerRefreshTokenPair>> VkGateway([FromBody] string accessToken,
        [FromQuery] string redirectUrl)
    {
        return await _vkAuthService.AuthAsync(accessToken, redirectUrl);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProfileRecord>> GetCurrentProfile()
    {
        return Ok(User.Identity);
    }
}