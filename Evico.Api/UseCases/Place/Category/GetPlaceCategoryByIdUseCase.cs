using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class GetPlaceCategoryByIdUseCase
{
    private readonly PlaceCategoryService _categoryService;
    private readonly AuthService _authService;

    public GetPlaceCategoryByIdUseCase(PlaceCategoryService categoryService, AuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }

    public async Task<ActionResult<PlaceCategoryRecord>> GetByIdAsync(long categoryId, ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        var currentUser = currentUserResult.ValueOrDefault;

        var getCategoryByIdResult = await _categoryService.GetByIdAsync(categoryId);
        if (getCategoryByIdResult.IsFailed)
            return new BadRequestObjectResult(getCategoryByIdResult.GetReport());
        var categoryRecord = getCategoryByIdResult.Value;
        
        var canViewCategoryResult = _categoryService.CanView(categoryRecord, currentUser);
        if (canViewCategoryResult.IsFailed)
            return new ObjectResult(canViewCategoryResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        return new OkObjectResult(categoryRecord);
    }
}