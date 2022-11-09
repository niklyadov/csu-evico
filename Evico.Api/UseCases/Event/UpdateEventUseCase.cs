using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class UpdateEventUseCase
{
    private readonly AuthService _authService;
    private readonly EventService _eventService;
    private readonly PlaceService _placeService;
    private readonly EventCategoryService _categoryService;

    public UpdateEventUseCase(EventService eventService, AuthService authService, PlaceService placeService,
        EventCategoryService categoryService)
    {
        _eventService = eventService;
        _authService = authService;
        _placeService = placeService;
        _categoryService = categoryService;
    }

    public async Task<ActionResult<EventRecord>> UpdateAsync(UpdateEventInputModel inputModel,
        ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var eventWithIdResult = await _eventService.GetByIdAsync(inputModel.Id);
        if (eventWithIdResult.IsFailed)
            return new BadRequestObjectResult(eventWithIdResult.GetReport());
        var eventRecord = eventWithIdResult.Value;

        var canUpdateEventResult = _eventService.CanUpdate(eventRecord, currentUser);
        if (canUpdateEventResult.IsFailed)
            return new ObjectResult(canUpdateEventResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        if (inputModel.Start != null)
            eventRecord.Start = inputModel.Start;

        if (inputModel.End != null)
            eventRecord.End = inputModel.End;

        if (inputModel.PlaceId != null)
        {
            var placeResult = await _placeService.GetByIdAsync(inputModel.PlaceId.Value);
            if (placeResult.IsFailed)
                return new BadRequestObjectResult(placeResult.GetReport());
            var place = placeResult.Value;

            if (place.IsDeleted)
                return new BadRequestObjectResult($"Place with id {place.Id} was deleted");

            eventRecord.Place = place;
        }

        if (!String.IsNullOrEmpty(inputModel.Name))
            eventRecord.Name = inputModel.Name;

        if (!String.IsNullOrEmpty(inputModel.Description))
            eventRecord.Description = inputModel.Description;

        if (inputModel.CategoryIds != null)
        {
            if (!inputModel.CategoryIds.Any())
            {
                eventRecord.Categories = new List<EventCategoryRecord>();
            }
            else
            {
                var categoriesResult = await _categoryService.GetByIdsAsync(inputModel.CategoryIds);
                if (categoriesResult.IsFailed)
                {
                    var categoriesResultError =
                        new Error($"Failed to get categories with ids {String.Join(',', inputModel.CategoryIds)}").CausedBy(
                            categoriesResult.Errors);
                    return new BadRequestObjectResult(Result.Fail(categoriesResultError));
                }

                eventRecord.Categories = categoriesResult.Value;
            } 
        }

        var updateEventResult = await _eventService.UpdateAsync(eventRecord);

        if (updateEventResult.IsFailed)
            return new BadRequestObjectResult(updateEventResult.GetReport());

        return new OkObjectResult(updateEventResult.Value);
    }
}