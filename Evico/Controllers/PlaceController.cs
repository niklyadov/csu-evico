using Evico.Entity;
using Evico.Models;
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
    public async Task<IActionResult> Add([FromBody] PlaceModel placeModel)
    {
        return await _placeService.AddAsync(placeModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _placeService.GetAllAsync();
    }
}