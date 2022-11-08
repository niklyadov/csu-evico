using System.Security.Claims;
using Evico.Api.Entities;
using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Place.Category;

public class GetPlaceCategoriesUseCase
{
    private readonly PlaceCategoryService _categoryService;
    private readonly AuthService _authService;

    public GetPlaceCategoriesUseCase(PlaceCategoryService categoryService, AuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }
    
    public async Task<ActionResult<List<PlaceCategoryRecord>>> GetAllAsync(ClaimsPrincipal userClaims)
    {
        var currentUserResult = await _authService.GetCurrentUser(userClaims);
        var currentUser = currentUserResult.ValueOrDefault;

        var canViewCategoriesResult = _categoryService.CanViewAll(currentUser);
        if (canViewCategoriesResult.IsFailed)
            return new ObjectResult(canViewCategoriesResult.GetReport())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

        var getCategoriesResult = await _categoryService.GetAllAsync();
        if (getCategoriesResult.IsFailed)
            return new BadRequestObjectResult(getCategoriesResult.GetReport());
        var categories = getCategoriesResult.Value;

        return new OkObjectResult(categories);
    }
}