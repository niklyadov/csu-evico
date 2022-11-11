using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review;

public class UpdateEventReviewUseCase
{
    private readonly AuthService _authService;
    private readonly EventReviewService _eventReviewService;
    private readonly EventService _eventService;

    public UpdateEventReviewUseCase(EventService eventService, EventReviewService eventReviewService,
        AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<EventReviewRecord>> UpdateAsync(long eventId, UpdateEventReviewInputModel inputModel,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventReviewByIdResult = await _eventReviewService.GetByIdAsync(inputModel.Id);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventReview = eventReviewByIdResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventReviewByIdResult.IsFailed)
            return new BadRequestObjectResult(eventReviewByIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;

        var canUpdateEventReviewResult = _eventReviewService.CanUpdate(eventWithId, eventReview, currentUser);
        if (canUpdateEventReviewResult.IsFailed)
            return new ObjectResult(canUpdateEventReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        if (!string.IsNullOrEmpty(inputModel.Comment))
            eventReview.Comment = inputModel.Comment;

        if (inputModel.Rate != null)
            eventReview.Rate = inputModel.Rate.Value;

        var updateEventResult = await _eventReviewService.Update(eventReview);
        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return new OkObjectResult(updateEventResult.Value);
    }
}