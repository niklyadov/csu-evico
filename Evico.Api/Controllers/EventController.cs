using Evico.Api.Entity;
using Evico.Api.InputModels.Event;
using Evico.Api.UseCases.Event;
using Evico.Api.UseCases.Event.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : BaseController
{
    private readonly AddEventReviewUseCase _addEventReviewUseCase;
    private readonly AddEventUseCase _addEventUseCase;
    private readonly DeleteEventByIdUseCase _deleteEventByIdUseCase;
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
    }

    [HttpPost]
    public async Task<ActionResult<EventRecord>> Add([FromBody] AddEventInputModel addEventModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _addEventUseCase.AddAsync(addEventModel, User);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<EventRecord>>> GetAll()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getEventsUseCase.GetAllAsync(User);
    }

    [AllowAnonymous]
    [HttpGet("{eventId}")]
    public async Task<ActionResult<EventRecord>> GetById([FromRoute] long eventId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getEventByIdUseCase.GetByIdAsync(eventId, User);
    }

    [HttpPut]
    public async Task<ActionResult<EventRecord>> Update([FromBody] UpdateEventInputModel updateEventModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _updateEventUseCase.UpdateAsync(updateEventModel, User);
    }

    [HttpDelete("{eventId}")]
    public async Task<ActionResult<EventRecord>> DeleteById([FromRoute] long eventId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _deleteEventByIdUseCase.DeleteByIdAsync(eventId, User);
    }

    [HttpPost("{eventId}/review")]
    public async Task<ActionResult<EventReviewRecord>> AddReview([FromRoute] long eventId,
        [FromBody] AddEventReviewInputModel inputModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _addEventReviewUseCase.AddAsync(eventId, inputModel, User);
    }

    [AllowAnonymous]
    [HttpGet("{eventId}/review/{reviewId}")]
    public async Task<ActionResult<EventReviewRecord>> GetReviewById([FromRoute] long eventId,
        [FromRoute] long reviewId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getEventReviewByIdUseCase.GetByIdAsync(eventId, reviewId, User);
    }

    [AllowAnonymous]
    [HttpGet("{eventId}/review")]
    public async Task<ActionResult<List<EventReviewRecord>>> GetReviews([FromRoute] long eventId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _getEventReviewsUseCase.GetAllAsync(eventId, User);
    }

    [HttpPut("{eventId}/review")]
    public async Task<ActionResult<EventReviewRecord>> UpdateReview([FromRoute] long eventId,
        [FromBody] UpdateEventReviewInputModel inputModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _updateEventReviewUseCase.UpdateAsync(eventId, inputModel, User);
    }


    [HttpDelete("{eventId}/review/{reviewId}")]
    public async Task<ActionResult<EventReviewRecord>> DeleteReview([FromRoute] long eventId, [FromRoute] long reviewId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return await _deleteEventReviewUseCase.DeleteByIdAsync(eventId, reviewId, User);
    }
}