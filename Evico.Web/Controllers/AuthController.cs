using Evico.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Web.Controllers;

public class AuthController : Controller
{
    private readonly VkAuthService _vkAuthService;

    public AuthController(VkAuthService vkAuthService)
    {
        _vkAuthService = vkAuthService;
    }
    
    // GET
    public async Task<IActionResult> VkGateway([FromQuery] string? code = null, [FromQuery] string? error = null)
    {
        if (string.IsNullOrEmpty(error))
            return BadRequest($"error: {error}");
        
        if (string.IsNullOrEmpty(code))
            return BadRequest($"no code provided: {code}");
        
        var tokenPair = await _vkAuthService.AuthViaVk(code);
        
        Response.Cookies.Append("bearerToken", tokenPair.BearerToken);
        Response.Cookies.Append("refreshToken", tokenPair.RefreshToken);
        
        
        return View();
    }
}