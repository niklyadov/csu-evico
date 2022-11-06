using Evico.Api.Entity;
using Evico.Api.Services.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.Extensions;

public static class ControllerBaseGetCurrentUser
{
    public static async Task<Result<ProfileRecord>> GetCurrentUserAsync(this ControllerBase controller)
    {
        var svc = controller.HttpContext.RequestServices;

        var tokensService = svc.GetRequiredService<JwtTokensService>();
        var authService = svc.GetRequiredService<JwtAuthService>();

        var tokenResult = authService.GetSecurityTokenFromHttpRequest(controller.Request);
        if (tokenResult.IsFailed)
            return Result.Fail(new Error("Unable to validate token")
                .CausedBy(tokenResult.Errors));

        var parsedToken = tokenResult.Value.parsedToken;
        var tokenBody = tokenResult.Value.base64Token;
            
        var userResult = await authService.GetCurrentUserFromToken(parsedToken);
        if(userResult.IsFailed)
            return Result.Fail(new Error("Error with getting user")
                .CausedBy(userResult.Errors));

        var validationTokenResult = await tokensService.IsValidTokenAsync(userResult.Value, tokenBody);
        if (validationTokenResult.IsFailed)
            return Result.Fail(new Error("Token is invalid")
                .CausedBy(validationTokenResult.Errors));


        return userResult.Value;
    }
}