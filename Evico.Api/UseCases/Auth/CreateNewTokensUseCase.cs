using Evico.Api.Extensions;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Evico.Api.UseCases.Auth;

public class CreateNewTokensUseCase
{
    private readonly ProfileService _profileService;
    private readonly JwtTokensService _jwtTokensService;

    public CreateNewTokensUseCase(ProfileService profileService, JwtTokensService jwtTokensService)
    {
        _profileService = profileService;
        _jwtTokensService = jwtTokensService;
    }
    
    public async Task<ActionResult<BearerRefreshTokenPair>> CreateNewToken(string username)
    {
        var addWithUsernameResult = await _profileService.AddWithUsernameAsync(username);
        if (addWithUsernameResult.IsFailed)
            return new BadRequestObjectResult(addWithUsernameResult.GetReport());

        var tokensPair = new BearerRefreshTokenPair(
            _jwtTokensService.CreateAccessTokenForUser(addWithUsernameResult.Value), 
                _jwtTokensService.CreateRefreshTokenForUser(addWithUsernameResult.Value));
        
        return new OkObjectResult(tokensPair);
    }
}