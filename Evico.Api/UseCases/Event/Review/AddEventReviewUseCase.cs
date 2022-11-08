using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event.Review;

public class AddEventReviewUseCase
{
    private readonly AuthService _authService;
    private readonly EventReviewService _eventReviewService;
    private readonly EventService _eventService;

    public AddEventReviewUseCase(EventService eventService, EventReviewService eventReviewService,
        AuthService authService)
    {
        _eventService = eventService;
        _eventReviewService = eventReviewService;
        _authService = authService;
    }

    public async Task<ActionResult<EventReviewRecord>> AddAsync(long eventId, AddEventReviewInputModel inputModel,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(eventId);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(
                eventWithIdResult.GetReport());
        var eventWithId = eventWithIdResult.Value;

        var canCreateEventReviewResult = _eventReviewService.CanCreate(eventWithId, currentUser);
        if (canCreateEventReviewResult.IsFailed)
            return new ObjectResult(canCreateEventReviewResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var createdReviewResult = await _eventReviewService.Add(new EventReviewRecord
        {
            Comment = inputModel.Comment,
            Author = currentUser,
            Rate = inputModel.Rate,
            Photos = inputModel.Photos,
            Event = eventWithId
        });
        if (createdReviewResult.IsFailed)
            return new BadRequestObjectResult(
                createdReviewResult.GetReport());

        return new OkObjectResult(createdReviewResult.Value);
    }
}