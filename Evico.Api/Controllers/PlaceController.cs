using Evico.Api.Entities;
using Evico.Api.InputModels;
using Evico.Api.InputModels.Photo;
using Evico.Api.InputModels.Place;
using Evico.Api.UseCases.Place;
using Evico.Api.UseCases.Place.Photo;
using Evico.Api.UseCases.Place.Review;
using Evico.Api.UseCases.Place.Review.Photo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PlaceController : BaseController
{
    private readonly AddPlaceReviewUseCase _addPlaceReviewUseCase;
    private readonly AddPlaceUseCase _addPlaceUseCase;
    private readonly DeletePlaceReviewUseCase _deletePlaceReviewUseCase;
    private readonly DeletePlaceUseCase _deletePlaceUseCase;
    private readonly GetPlaceByIdUseCase _getPlaceByIdUseCase;
    private readonly GetPlaceReviewByIdUseCase _getPlaceReviewByIdUseCase;
    private readonly GetPlaceReviewsUseCase _getPlaceReviewsUseCase;
    private readonly GetPlacesUseCase _getPlacesUseCase;
    private readonly UpdatePlaceReviewUseCase _updatePlaceReviewUseCase;
    private readonly UpdatePlaceUseCase _updatePlaceUseCase;
    private readonly AddPlacePhotoUseCase _addPlacePhotoUseCase;
    private readonly DeletePlacePhotoUseCase _deletePlacePhotoUseCase;
    private readonly AddPlaceReviewPhotoUseCase _addPlaceReviewPhotoUseCase;
    private readonly DeletePlaceReviewPhotoUseCase _deletePlaceReviewPhotoUseCase;
    public PlaceController(IServiceProvider services)
    {
        _addPlaceUseCase = services.GetRequiredService<AddPlaceUseCase>();
        _getPlaceByIdUseCase = services.GetRequiredService<GetPlaceByIdUseCase>();
        _getPlacesUseCase = services.GetRequiredService<GetPlacesUseCase>();
        _updatePlaceUseCase = services.GetRequiredService<UpdatePlaceUseCase>();
        _deletePlaceUseCase = services.GetRequiredService<DeletePlaceUseCase>();
        _addPlaceReviewUseCase = services.GetRequiredService<AddPlaceReviewUseCase>();
        _getPlaceReviewByIdUseCase = services.GetRequiredService<GetPlaceReviewByIdUseCase>();
        _getPlaceReviewsUseCase = services.GetRequiredService<GetPlaceReviewsUseCase>();
        _updatePlaceReviewUseCase = services.GetRequiredService<UpdatePlaceReviewUseCase>();
        _deletePlaceReviewUseCase = services.GetRequiredService<DeletePlaceReviewUseCase>();
        _addPlacePhotoUseCase = services.GetRequiredService<AddPlacePhotoUseCase>();
        _deletePlacePhotoUseCase = services.GetRequiredService<DeletePlacePhotoUseCase>();
        _addPlaceReviewPhotoUseCase = services.GetRequiredService<AddPlaceReviewPhotoUseCase>();
        _deletePlaceReviewPhotoUseCase = services.GetRequiredService<DeletePlaceReviewPhotoUseCase>();
    }

    [HttpPost]
    public async Task<ActionResult<PlaceRecord>> Add([FromBody] AddPlaceInputModel inputModel)
    {
        return await _addPlaceUseCase.AddAsync(inputModel, User);
    }

    [HttpGet("{placeId}")]
    [AllowAnonymous]
    public async Task<ActionResult<PlaceRecord>> GetById(long placeId)
    {
        return await _getPlaceByIdUseCase.GetByIdAsync(placeId, User);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PlaceRecord>>> GetAll()
    {
        return await _getPlacesUseCase.GetAllAsync(User);
    }

    [HttpPut]
    public async Task<ActionResult<PlaceRecord>> UpdateAsync([FromBody] UpdatePlaceInputModel inputModel)
    {
        return await _updatePlaceUseCase.UpdateAsync(inputModel, User);
    }

    [HttpDelete("{placeId}")]
    public async Task<ActionResult<PlaceRecord>> DeleteById([FromRoute] long placeId)
    {
        return await _deletePlaceUseCase.DeleteByIdAsync(placeId, User);
    }
    
    [HttpPost("{placeId}/photo")]
    public async Task<ActionResult<PhotoRecord>> AddPhoto([FromForm] PhotoUploadInputModel inputModel, [FromRoute] long placeId)
    {
        return await _addPlacePhotoUseCase.AddAsync(inputModel, placeId, User);
    }

    [HttpDelete("{placeId}/photo/{photoId}")]
    public async Task<ActionResult<PhotoRecord>> DeletePhoto([FromRoute] long placeId, [FromRoute] long photoId)
    {
        return await _deletePlacePhotoUseCase.DeleteAsync(placeId, photoId, User);
    }

    [HttpPost("{placeId}/review")]
    public async Task<ActionResult<PlaceReviewRecord>> AddReview([FromRoute] long placeId,
        [FromBody] AddPlaceReviewInputModel inputModel)
    {
        return await _addPlaceReviewUseCase.AddAsync(placeId, inputModel, User);
    }

    [HttpGet("{placeId}/review/{reviewId}")]
    [AllowAnonymous]
    public async Task<ActionResult<PlaceReviewRecord>> GetReviewById([FromRoute] long placeId,
        [FromRoute] long reviewId)
    {
        return await _getPlaceReviewByIdUseCase.GetByIdAsync(placeId, reviewId, User);
    }

    [HttpGet("{placeId}/review")]
    [AllowAnonymous]
    public async Task<ActionResult<List<PlaceReviewRecord>>> GetReviews([FromRoute] long placeId)
    {
        return await _getPlaceReviewsUseCase.GetAllAsync(placeId, User);
    }

    [HttpPut("{placeId}/review")]
    public async Task<ActionResult<List<PlaceReviewRecord>>> UpdateReview([FromRoute] long placeId,
        [FromBody] UpdatePlaceInputModel inputModel)
    {
        return await _updatePlaceReviewUseCase.UpdateAsync(placeId, inputModel, User);
    }

    [HttpDelete("{placeId}/review/{reviewId}")]
    public async Task<ActionResult<List<PlaceReviewRecord>>> DeleteReviewById([FromRoute] long placeId,
        [FromRoute] long reviewId)
    {
        return await _deletePlaceReviewUseCase.DeleteAsync(placeId, reviewId, User);
    }
    
    [HttpPost("{placeId}/review/{reviewId}/photo")]
    public async Task<ActionResult<PhotoRecord>> AddReviewPhoto([FromForm] PhotoUploadInputModel inputModel, 
        [FromRoute] long placeId, [FromRoute] long reviewId)
    {
        return await _addPlaceReviewPhotoUseCase.AddAsync(inputModel, placeId, reviewId, User);
    }

    [HttpDelete("{placeId}/review/{reviewId}/photo/{photoId}")]
    public async Task<ActionResult<PhotoRecord>> DeleteReviewPhoto([FromRoute] long placeId, 
        [FromRoute] long reviewId, [FromRoute] long photoId)
    {
        return await _deletePlaceReviewPhotoUseCase.DeleteAsync(placeId, photoId, User);
    }
}