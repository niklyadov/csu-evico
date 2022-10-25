using Microsoft.AspNetCore.Mvc;

namespace Evico.Web.Controllers;

public class Auth : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    // GET
    public IActionResult VkGateway([FromQuery] string? code = null, [FromQuery] string? error = null)
    {
        // todo: тут надо делать запросы к нашему api чтобы получать токен
        Response.Cookies.Append("bearerToken", "some_first_token");
        Response.Cookies.Append("refreshToken", "some_second_token");
        
        
        return View();
    }
}