using Evico.Api.Entity;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.UseCases.Place;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PlaceController : BaseController
{
    private readonly AddPlaceUseCase _addPlaceUseCase;
    private readonly GetPlaceByIdUseCase _getPlaceByIdUseCase;
    private readonly GetPlacesUseCase _getPlacesUseCase;
    private readonly UpdatePlaceUseCase _updatePlaceUseCase;
    private readonly DeletePlaceUseCase _deletePlaceUseCase;
    
    public PlaceController(IServiceProvider services)
    {
        _addPlaceUseCase = services.GetRequiredService<AddPlaceUseCase>();
        _getPlaceByIdUseCase = services.GetRequiredService<GetPlaceByIdUseCase>();
        _getPlacesUseCase = services.GetRequiredService<GetPlacesUseCase>();
        _updatePlaceUseCase = services.GetRequiredService<UpdatePlaceUseCase>();
        _deletePlaceUseCase = services.GetRequiredService<DeletePlaceUseCase>();
    }

    [HttpPost]
    public async Task<ActionResult<PlaceRecord>> Add([FromBody] AddPlaceInputModel inputModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _addPlaceUseCase.AddAsync(inputModel, User);
    }

    [HttpGet("{placeId}")]
    [AllowAnonymous]

    public async Task<ActionResult<PlaceRecord>> GetById(long placeId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getPlaceByIdUseCase.GetByIdAsync(placeId, User);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PlaceRecord>>> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getPlacesUseCase.GetAllAsync(User);
    }

    [HttpPut]
    public async Task<ActionResult<PlaceRecord>> UpdateAsync([FromBody] UpdatePlaceInputModel inputModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _updatePlaceUseCase.UpdateAsync(inputModel, User);
    }
    
    [HttpDelete("{placeId}")]

    public async Task<ActionResult<PlaceRecord>> DeleteById(long placeId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _deletePlaceUseCase.DeleteByIdAsync(placeId, User);
    }
}