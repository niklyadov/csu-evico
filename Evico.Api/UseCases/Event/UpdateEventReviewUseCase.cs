using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class UpdateEventReviewUseCase
{
    private readonly EventService _eventService;
    private readonly EventReviewService _eventReviewService;
    private readonly AuthService _authService;

    public UpdateEventReviewUseCase(EventService eventService, EventReviewService eventReviewService, AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }
    public async Task<ActionResult<EventReviewRecord>> UpdateAsync(long eventId, UpdateEventReviewInputModel inputModel, ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventReviewByIdResult = await _eventReviewService.GetById(inputModel.Id);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventReview = eventReviewByIdResult.Value;

        var canUpdateEventReviewResult = _eventReviewService.CanUpdate(eventReview, currentUser);
        if (canUpdateEventReviewResult.IsFailed)
            return new ObjectResult(canUpdateEventReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        if (!String.IsNullOrEmpty(inputModel.Comment))
            eventReview.Comment = inputModel.Comment;
        
        if (inputModel.Rate != null)
            eventReview.Rate = inputModel.Rate.Value;
        
        eventReview.Photos = inputModel.Photos;

        var updateEventResult = await _eventReviewService.Update(eventReview);
        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return new OkObjectResult(updateEventResult.Value);
    }
}