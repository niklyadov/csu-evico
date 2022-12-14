using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place;

public class AddPlaceUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceService _placeService;
    private readonly PlaceCategoryService _categoryService;

    public AddPlaceUseCase(PlaceService placeService, AuthService authService, PlaceCategoryService categoryService)
    {
        _placeService = placeService;
        _authService = authService;
        _categoryService = categoryService;
    }

    public async Task<ActionResult<PlaceRecord>> AddAsync(AddPlaceInputModel inputModel,
        ClaimsPrincipal claimsPrincipal)
    {
        var currentUserResult = await _authService.GetCurrentUser(claimsPrincipal);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var placeRecord = new PlaceRecord
        {
            LocationLatitude = inputModel.LocationLatitude,
            LocationLongitude = inputModel.LocationLongitude,
            Name = inputModel.Name,
            Description = inputModel.Description,
            Owner = currentUser
        };

        if (inputModel.ParentId.HasValue && inputModel.ParentId.Value > 0)
        {
            var parentPlaceResult = await _placeService.GetByIdAsync(inputModel.ParentId.Value);
            if (parentPlaceResult.IsFailed)
            {
                var parentPlaceError = new Error("Can`t add parent place")
                    .CausedBy(parentPlaceResult.Errors);

                return new BadRequestObjectResult(Result.Fail(parentPlaceError).GetReport());
            }

            var parentPlace = parentPlaceResult.Value;

            placeRecord.Parent = parentPlace;
        }
        
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

            placeRecord.Categories = categoriesResult.Value;
        }

        var canCreateResult = _placeService.CanCreate(placeRecord, currentUser);
        if (canCreateResult.IsFailed)
            return new ObjectResult(canCreateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var placeCreateResult = await _placeService.AddAsync(placeRecord);
        if (placeCreateResult.IsFailed)
            return new BadRequestObjectResult(placeCreateResult.GetReport());

        var createdPlace = placeCreateResult.Value;

        return new OkObjectResult(createdPlace);
    }
}