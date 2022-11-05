using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Evico.Api.Entity;
using FluentResults;

namespace Evico.Api.Services.Auth;

public class JwtAuthService
{
    private const string Audience = "default";
    private readonly JwtTokensService _jwtTokensService;
    private readonly ProfileService _usersService;
    private int? _userId;

    public JwtAuthService(ProfileService usersService, JwtTokensService jwtTokensService)
    {
        _usersService = usersService;
        _jwtTokensService = jwtTokensService;
    }

    public Result InitInstanceWithToken(JwtSecurityToken token)
    {
        try
        {
            if (!int.TryParse(token.Issuer, out var userId))
                throw new Exception("The token is not attached on any user.");

            _userId = userId;

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
    

    public async Task<Result<ProfileRecord>> GetCurrentUser()
    {
        try
        {
            if (!_userId.HasValue || _userId.Value == 0)
                throw new Exception("User id is not assigned!");

            var userByUsernameResult = await _usersService.GetByIdAsync(_userId.Value);

            if (userByUsernameResult.IsFailed)
                return Result.Fail(userByUsernameResult.Errors);

            var user = userByUsernameResult.Value;

            return Result.Ok(user);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private string CalculateHash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = SHA256.HashData(bytes);

        return Encoding.UTF8.GetString(hashedBytes);
    }

    private string GetHashForUser(ProfileRecord user)
    {
        var expiresAt = DateTime.UtcNow.AddDays(1);

        return _jwtTokensService.GenerateNewTokenForUser(user, Audience, expiresAt);
    }
}