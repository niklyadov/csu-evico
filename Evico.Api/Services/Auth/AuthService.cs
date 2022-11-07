using System.Security.Claims;
using Evico.Api.Entity;
using Evico.Api.Extensions;
using FluentResults;

namespace Evico.Api.Services.Auth;

public class AuthService
{
    private readonly ProfileService _profileService;

    public AuthService(ProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task<Result<ProfileRecord>> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
    {
        return await Result.Try(async () => await RequireCurrentUser(claimsPrincipal));
    }

    public async Task<ProfileRecord> RequireCurrentUser(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.Identity == null)
            throw new InvalidOperationException("Identity is null");

        if (!claimsPrincipal.Identity.IsAuthenticated)
            throw new InvalidOperationException("User is not authorized");

        if (string.IsNullOrEmpty(claimsPrincipal.Identity.Name))
            throw new InvalidOperationException("Username is empty");

        var username = claimsPrincipal.Identity.Name!;

        var userWithUsernameResult = await _profileService.GetByUsernameAsync(username);

        if (userWithUsernameResult.IsFailed)
            throw new InvalidOperationException($"Retrieve user with username: {username} was failed",
                userWithUsernameResult.GetReportException());

        return userWithUsernameResult.Value;
    }
}