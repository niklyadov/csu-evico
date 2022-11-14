using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class AddPlaceCategoryUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceCategoryService _categoryService;

    public AddPlaceCategoryUseCase(PlaceCategoryService categoryService, AuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }

    public async Task<ActionResult<PlaceCategoryRecord>> AddAsync(AddPlaceCategoryInputModel inputModel,
        ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var canCreateResult = _categoryService.CanCreate(currentUser);
        if (canCreateResult.IsFailed)
            return new ObjectResult(canCreateResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var categoryRecord = new PlaceCategoryRecord
        {
            Name = inputModel.Name,
            Description = inputModel.Description
        };

        if (inputModel.ParentCategoryId.HasValue && inputModel.ParentCategoryId.Value > 0)
        {
            var parentCategoryRecordResult = await _categoryService.GetByIdAsync(inputModel.ParentCategoryId.Value);
            if (parentCategoryRecordResult.IsFailed)
            {
                var parentCategoryRecordResultError = new Error(
                        $"Failed to set parent category id {inputModel.ParentCategoryId.Value} to category")
                    .CausedBy(parentCategoryRecordResult.Errors);

                return new BadRequestObjectResult(Result.Fail(parentCategoryRecordResultError).GetReport());
            }

            categoryRecord.Parent = parentCategoryRecordResult.Value;
        }

        var createCategoryResult = await _categoryService.AddAsync(categoryRecord);
        if (createCategoryResult.IsFailed)
            return new BadRequestObjectResult(createCategoryResult.GetReport());
        var createdCategory = createCategoryResult.Value;

        return new OkObjectResult(createdCategory);
    }
}