using Evico.Api.InputModels;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

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
    public async Task<IActionResult> Add([FromBody] PlaceInputModel placeInputModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _placeService.AddAsync(placeInputModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _placeService.GetAllAsync();
    }
}