using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class DeletePlaceCategoryUseCase
{
    private readonly AuthService _authService;
    private readonly PlaceCategoryService _categoryService;

    public DeletePlaceCategoryUseCase(PlaceCategoryService categoryService, AuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }

    public async Task<ActionResult<List<PlaceCategoryRecord>>> DeleteAsync(long categoryId, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        if (currentUserResult.IsFailed)
            return new UnauthorizedObjectResult(currentUserResult.GetReport());
        var currentUser = currentUserResult.Value;

        var categoryRecordWithIdResult = await _categoryService.GetByIdAsync(categoryId);
        if (categoryRecordWithIdResult.IsFailed)
            return new BadRequestObjectResult(categoryRecordWithIdResult.GetReport());
        var categoryRecord = categoryRecordWithIdResult.Value;

        var canDeleteCategoryResult = _categoryService.CanDelete(categoryRecord, currentUser);
        if (canDeleteCategoryResult.IsFailed)
            return new ObjectResult(canDeleteCategoryResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var deleteCategoryRecordResult = await _categoryService.DeleteAsync(categoryRecord);
        if (categoryRecordWithIdResult.IsFailed)
            return new BadRequestObjectResult(categoryRecordWithIdResult.GetReport());
        var deletedCategoryRecord = deleteCategoryRecordResult.Value;

        return new OkObjectResult(deletedCategoryRecord);
    }
}