using Evico.Api.Entities;
using Evico.Api.InputModels.Event;
using Evico.Api.InputModels.Photo;
using Evico.Api.UseCases.Event;
using Evico.Api.UseCases.Event.Photo;
using Evico.Api.UseCases.Event.Review;
using Evico.Api.UseCases.Event.Review.Photo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : BaseController
{
    private readonly AddEventPhotoUseCase _addEventPhotoUseCase;
    private readonly AddEventReviewPhotoUseCase _addEventReviewPhotoUseCase;
    private readonly AddEventReviewUseCase _addEventReviewUseCase;
    private readonly AddEventUseCase _addEventUseCase;
    private readonly DeleteEventByIdUseCase _deleteEventByIdUseCase;
    private readonly DeleteEventPhotoUseCase _deleteEventPhotoUseCase;
    private readonly DeleteEventReviewPhotoUseCase _deleteEventReviewPhotoUseCase;
    private readonly DeleteEventReviewUseCase _deleteEventReviewUseCase;
    private readonly GetEventByIdUseCase _getEventByIdUseCase;
    private readonly GetEventReviewByIdUseCase _getEventReviewByIdUseCase;
    private readonly GetEventReviewsUseCase _getEventReviewsUseCase;
    private readonly GetEventsUseCase _getEventsUseCase;
    private readonly UpdateEventReviewUseCase _updateEventReviewUseCase;
    private readonly UpdateEventUseCase _updateEventUseCase;

    public EventController(IServiceProvider services)
    {
        _addEventUseCase = services.GetRequiredService<AddEventUseCase>();
        _getEventsUseCase = services.GetRequiredService<GetEventsUseCase>();
        _getEventByIdUseCase = services.GetRequiredService<GetEventByIdUseCase>();
        _updateEventUseCase = services.GetRequiredService<UpdateEventUseCase>();
        _deleteEventByIdUseCase = services.GetRequiredService<DeleteEventByIdUseCase>();
        _addEventReviewUseCase = services.GetRequiredService<AddEventReviewUseCase>();
        _getEventReviewByIdUseCase = services.GetRequiredService<GetEventReviewByIdUseCase>();
        _getEventReviewsUseCase = services.GetRequiredService<GetEventReviewsUseCase>();
        _updateEventReviewUseCase = services.GetRequiredService<UpdateEventReviewUseCase>();
        _deleteEventReviewUseCase = services.GetRequiredService<DeleteEventReviewUseCase>();
        _addEventPhotoUseCase = services.GetRequiredService<AddEventPhotoUseCase>();
        _deleteEventPhotoUseCase = services.GetRequiredService<DeleteEventPhotoUseCase>();
        _addEventReviewPhotoUseCase = services.GetRequiredService<AddEventReviewPhotoUseCase>();
        _deleteEventReviewPhotoUseCase = services.GetRequiredService<DeleteEventReviewPhotoUseCase>();
    }

    [HttpPost]
    public async Task<ActionResult<EventRecord>> Add([FromBody] AddEventInputModel addEventModel)
    {
        return await _addEventUseCase.AddAsync(addEventModel, User);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<EventRecord>>> Search([FromQuery] EventSearchFilters filters)
    {
        //return await _getEventsUseCase.GetAllAsync(User);
        return await _getEventsUseCase.SearchAsync(filters, User);
    }

    [AllowAnonymous]
    [HttpGet("{eventId}")]
    public async Task<ActionResult<EventRecord>> GetById([FromRoute] long eventId)
    {
        return await _getEventByIdUseCase.GetByIdAsync(eventId, User);
    }

    [HttpPut]
    public async Task<ActionResult<EventRecord>> Update([FromBody] UpdateEventInputModel updateEventModel)
    {
        return await _updateEventUseCase.UpdateAsync(updateEventModel, User);
    }

    [HttpDelete("{eventId}")]
    public async Task<ActionResult<EventRecord>> DeleteById([FromRoute] long eventId)
    {
        return await _deleteEventByIdUseCase.DeleteByIdAsync(eventId, User);
    }

    [HttpPost("{eventId}/photo")]
    public async Task<ActionResult<PhotoRecord>> AddPhoto([FromForm] PhotoUploadInputModel inputModel,
        [FromRoute] long eventId)
    {
        return await _addEventPhotoUseCase.AddAsync(inputModel, eventId, User);
    }

    [HttpDelete("{eventId}/photo/{photoId}")]
    public async Task<ActionResult<PhotoRecord>> DeletePhoto([FromRoute] long eventId, [FromRoute] long photoId)
    {
        return await _deleteEventPhotoUseCase.DeleteAsync(eventId, photoId, User);
    }

    [HttpPost("{eventId}/review")]
    public async Task<ActionResult<EventReviewRecord>> AddReview([FromRoute] long eventId,
        [FromBody] AddEventReviewInputModel inputModel)
    {
        return await _addEventReviewUseCase.AddAsync(eventId, inputModel, User);
    }

    [AllowAnonymous]
    [HttpGet("{eventId}/review/{reviewId}")]
    public async Task<ActionResult<EventReviewRecord>> GetReviewById([FromRoute] long eventId,
        [FromRoute] long reviewId)
    {
        return await _getEventReviewByIdUseCase.GetByIdAsync(eventId, reviewId, User);
    }

    [AllowAnonymous]
    [HttpGet("{eventId}/review")]
    public async Task<ActionResult<List<EventReviewRecord>>> GetReviews([FromRoute] long eventId)
    {
        return await _getEventReviewsUseCase.GetAllAsync(eventId, User);
    }

    [HttpPut("{eventId}/review")]
    public async Task<ActionResult<EventReviewRecord>> UpdateReview([FromRoute] long eventId,
        [FromBody] UpdateEventReviewInputModel inputModel)
    {
        return await _updateEventReviewUseCase.UpdateAsync(eventId, inputModel, User);
    }

    [HttpDelete("{eventId}/review/{reviewId}")]
    public async Task<ActionResult<EventReviewRecord>> DeleteReview([FromRoute] long eventId, [FromRoute] long reviewId)
    {
        return await _deleteEventReviewUseCase.DeleteByIdAsync(eventId, reviewId, User);
    }

    [HttpPost("{eventId}/review/{reviewId}/photo")]
    public async Task<ActionResult<PhotoRecord>> AddReviewPhoto([FromForm] PhotoUploadInputModel inputModel,
        [FromRoute] long eventId, [FromRoute] long reviewId)
    {
        return await _addEventReviewPhotoUseCase.AddAsync(inputModel, eventId, reviewId, User);
    }

    [HttpDelete("{eventId}/review/{reviewId}/photo/{photoId}")]
    public async Task<ActionResult<PhotoRecord>> DeleteReviewPhoto([FromRoute] long eventId,
        [FromRoute] long reviewId, [FromRoute] long photoId)
    {
        return await _deleteEventReviewPhotoUseCase.DeleteAsync(eventId, photoId, User);
    }
}