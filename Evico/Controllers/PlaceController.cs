using Evico.Entity;
using Evico.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaceController : BaseController
{
    private readonly PlaceService _placeService;

    public PlaceController(PlaceService placeService)
    {
        _placeService = placeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PlaceRecord placeRecord)
    {
        return await _placeService.AddAsync(placeRecord);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _placeService.GetAllAsync();
    }
}