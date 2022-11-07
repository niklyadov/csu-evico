using System.IdentityModel.Tokens.Jwt;
using Evico.Api.Entity;
using FluentResults;
using Microsoft.IdentityModel.Tokens;

namespace Evico.Api.Services.Auth;

public class JwtAuthService
{
    private readonly JwtTokensService _jwtTokensService;
    private readonly ProfileService _profileService;

    public JwtAuthService(ProfileService profileService, JwtTokensService jwtTokensService)
    {
        _profileService = profileService;
        _jwtTokensService = jwtTokensService;
    }

    public Result<(string base64Token, JwtSecurityToken parsedToken)> GetSecurityTokenFromHttpRequest(
        HttpRequest httpRequest)
    {
        try
        {
            var token = httpRequest.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
                throw new InvalidOperationException("Bearer Token is not passed.");

            var tokenBody = token.Substring(7);

            var parsedToken = _jwtTokensService.ParseToken(tokenBody);
            if (parsedToken == null)
                throw new SecurityTokenValidationException(
                    "Invalid token provided. Can't parse token. Parsed token is null");

            return Result.Ok((tokenBody, parsedToken));
        }
        catch (Exception exception)
        {
            return Result.Fail(new Error("Some error with parsing JWT token")
                .CausedBy(exception));
        }
    }

    public async Task<Result<ProfileRecord>> GetCurrentUserFromToken(JwtSecurityToken parsedToken)
    {
        try
        {
            if (!long.TryParse(parsedToken.Issuer, out var userId))
                throw new SecurityTokenInvalidIssuerException("Issuer could not be parsed");

            var userWithIdResult = await _profileService.GetByIdAsync(userId);

            if (userWithIdResult.IsFailed)
                return Result.Fail(new Error($"Retrieve one user with id {userId} is failed")
                    .CausedBy(userWithIdResult.Errors));

            return Result.Ok(userWithIdResult.Value);
        }
        catch (SecurityTokenInvalidIssuerException e)
        {
            return Result.Fail(new Error("Invalid issuer").CausedBy(e));
        }
        catch (Exception exception)
        {
            return Result.Fail(new Error("Some error with getting current user from JWT token")
                .CausedBy(exception));
        }
    }
}