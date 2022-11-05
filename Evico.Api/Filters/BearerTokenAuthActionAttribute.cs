using System.Net;
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

            var token = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
                throw new InvalidOperationException("Bearer Token is not passed.");

            var tokenBody = token.Substring(7);

            var parsedToken = tokensService.ParseToken(tokenBody);
            if (parsedToken == null)
                throw new InvalidOperationException("Invalid token provided. Can't parse token. Parsed token is null");

            var initUserResult = authService.InitInstanceWithToken(parsedToken);
            if (initUserResult.IsFailed)
                throw new InvalidOperationException(string.Join(',', initUserResult.Errors));

            var userResult = await authService.GetCurrentUser();
            if (userResult.IsFailed)
                throw new SecurityTokenInvalidIssuerException("Can't get current user. Maybe provided token is invalid.");

            var user = userResult.Value;

            if (user is null)
                throw new SecurityTokenInvalidIssuerException("Can't get current user. Maybe provided token is invalid.");

            if (!await tokensService.IsValidTokenAsync(user, tokenBody))
                throw new SecurityTokenValidationException("Token is invalid. Please, update the token.");
            
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

        var result = JsonConvert.SerializeObject(new { error = ex });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}