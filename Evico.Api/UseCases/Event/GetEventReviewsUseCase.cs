using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class GetEventReviewsUseCase
{
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;

    public GetEventReviewsUseCase(EventService eventService, EventReviewService eventReviewService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
    }
    
    public async Task<ActionResult<List<EventReviewRecord>>> GetAllAsync(long eventId)
    {
        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(
                eventWithIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;
        
        var canGetAllReviewsResult = _eventReviewService.CanViewAll(eventWithId);
        if (canGetAllReviewsResult.IsFailed)
            return new ObjectResult(canGetAllReviewsResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        var getAllEventsResult = await _eventReviewService.GetAll();
        if (getAllEventsResult.IsFailed)
            return new BadRequestObjectResult(
                getAllEventsResult.GetReport());

        return new OkObjectResult(getAllEventsResult.Value);
    }
}