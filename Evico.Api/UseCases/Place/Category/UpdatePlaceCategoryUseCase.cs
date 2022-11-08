using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.InputModels.Place;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class UpdatePlaceCategoryUseCase
{
    private readonly PlaceCategoryService _categoryService;
    private readonly AuthService _authService;

    public UpdatePlaceCategoryUseCase(PlaceCategoryService categoryService, AuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }
    
    public async Task<ActionResult<List<PlaceCategoryRecord>>> UpdateAsync(UpdatePlaceCategoryInputModel inputModel, ClaimsPrincipal userClaims)
    {
                var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var getCategoryByIdResult = await _categoryService.GetByIdAsync(inputModel.Id);
        if (getCategoryByIdResult.IsFailed)
            return new BadRequestObjectResult(getCategoryByIdResult.GetReport());
        var categoryRecord = getCategoryByIdResult.Value;
        
        var canUpdateCategoryResult = _categoryService.CanView(categoryRecord, currentUser);
        if (canUpdateCategoryResult.IsFailed)
            return new ObjectResult(canUpdateCategoryResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        if (!String.IsNullOrEmpty(inputModel.Name))
            categoryRecord.Name = inputModel.Name;

        if (!String.IsNullOrEmpty(inputModel.Description))
            categoryRecord.Description = inputModel.Description;
        
        if (inputModel.ParentCategoryId.HasValue)
        {
            var parentCategoryRecordResult = await _categoryService.GetByIdAsync(inputModel.ParentCategoryId.Value);
            if (parentCategoryRecordResult.IsFailed)
            {
                var parentCategoryRecordResultError = new Error(
                        $"Failed to set parent category id {inputModel.ParentCategoryId.Value} " +
                        $"to category with id {inputModel.Id}")
                    .CausedBy(parentCategoryRecordResult.Errors);
                
                return new BadRequestObjectResult(Result.Fail(parentCategoryRecordResultError).GetReport());
            }

            categoryRecord.Parent = parentCategoryRecordResult.Value;
        }

        var updateCategoryResult = await _categoryService.UpdateAsync(categoryRecord);
        if (updateCategoryResult.IsFailed)
            return new BadRequestObjectResult(updateCategoryResult.GetReport());
        var updatedCategory = updateCategoryResult.Value;

        return new OkObjectResult(updatedCategory);
    }
}