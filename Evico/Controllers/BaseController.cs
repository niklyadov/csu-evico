using Microsoft.AspNetCore.Mvc;

namespace Evico.Controllers;

//[ApiVersion("1.0")]
//[Route("api/v{version:apiVersion}/[controller]")]
//[BearerTokenAuth]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    // protected IActionResult GetResult<T>(Result<T> result) where T : class
    // {
    //     if (result.IsSuccess) return Ok(result.Value);
    //
    //     return Problem(string.Join(',', result.Errors));
    // }
}