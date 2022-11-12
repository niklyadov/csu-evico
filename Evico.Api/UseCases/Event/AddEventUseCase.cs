using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Event;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Event;

public class AddEventUseCase
{
    private readonly AuthService _authService;
    private readonly EventCategoryService _categoryService;
    private readonly EventService _eventService;
    private readonly PlaceService _placeService;

    public AddEventUseCase(EventService eventService, AuthService authService, PlaceService placeService,
        EventCategoryService categoryService)
    {
        _eventService = eventService;
        _authService = authService;
        _placeService = placeService;
        _categoryService = categoryService;
    }

    public async Task<ActionResult<EventRecord>> AddAsync(AddEventInputModel inputModel, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeWithIdResult = await _placeService.GetByIdAsync(inputModel.PlaceId);
        if (placeWithIdResult.IsFailed)
            return new BadRequestObjectResult(placeWithIdResult.GetReport());

        var canCreateResult = _eventService.CanCreate(placeWithIdResult.Value, currentUser);
        if (canCreateResult.IsFailed)
            return new ObjectResult(canCreateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var eventRecord = new EventRecord
        {
            Start = inputModel.Start,
            End = inputModel.End,
            PlaceId = inputModel.PlaceId,
            Name = inputModel.Name,
            Description = inputModel.Description,
            Owner = currentUser
        };

        if (inputModel.CategoryIds != null && inputModel.CategoryIds.Any())
        {
            var categoriesResult = await _categoryService.GetByIdsAsync(inputModel.CategoryIds);
            if (categoriesResult.IsFailed)
            {
                var categoriesResultError =
                    new Error($"Failed to get categories with ids {string.Join(',', inputModel.CategoryIds)}").CausedBy(
                        categoriesResult.Errors);
                return new BadRequestObjectResult(Result.Fail(categoriesResultError));
            }

            eventRecord.Categories = categoriesResult.Value;
        }

        var addEventResult = await _eventService.AddAsync(eventRecord);
        if (addEventResult.IsFailed)
            return new BadRequestObjectResult(addEventResult.GetReport());

        return new OkObjectResult(addEventResult.Value);
    }
}