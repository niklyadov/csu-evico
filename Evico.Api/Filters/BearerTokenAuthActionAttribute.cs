using System.Net;
using Evico.Api.Extensions;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Evico.Api.Filters;

public class BearerTokenAuthActionAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            var svc = context.HttpContext.RequestServices;

            var tokensService = svc.GetRequiredService<JwtTokensService>();
            var authService = svc.GetRequiredService<JwtAuthService>();

            var tokenResult = authService.GetSecurityTokenFromHttpRequest(context.HttpContext.Request);

            if (tokenResult.IsFailed)
                throw new SecurityTokenValidationException("Unable to validate token", 
                    tokenResult.GetReportException());

            var parsedToken = tokenResult.Value.parsedToken;
            var tokenBody = tokenResult.Value.base64Token;
            
            var userResult = await authService.GetCurrentUserFromToken(parsedToken);
            if (userResult.IsFailed)
                throw new SecurityTokenInvalidIssuerException(
                    "Can't get current user. Maybe provided token is invalid.", 
                    userResult.GetReportException());

            var user = userResult.Value;

            if (user is null)
                throw new SecurityTokenInvalidIssuerException(
                    "Can't get current user. Maybe provided token is invalid.");

            var validationTokenResult = await tokensService.IsValidTokenAsync(user, tokenBody);
            
            if (validationTokenResult.IsFailed)
                throw new SecurityTokenValidationException("Token is invalid. Please, update the token.", 
                    validationTokenResult.GetReportException());

            // success, passed
            await next();
        }
        catch (Exception ex)
        {
            await HandleError(context.HttpContext, ex);
        }
    }

    private static Task HandleError(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new {error = ex});

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        return context.Response.WriteAsync(result);
    }
}