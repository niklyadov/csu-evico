using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Filters;
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
    public async Task<ActionResult<BearerRefreshTokenPair>> VkGateway([FromBody] string accessToken,
        [FromQuery] string redirectUrl)
    {
        return await _vkAuthService.AuthAsync(accessToken, redirectUrl);
    }

    [HttpGet]
    [BearerTokenAuthAction]
    public async Task<ActionResult<ProfileRecord>> GetCurrentProfile()
    {
        var profileResult = await this.GetCurrentUserAsync();

        if (profileResult.IsFailed)
            return BadRequest(profileResult.GetReport());

        return Ok(profileResult.Value);
    }
}