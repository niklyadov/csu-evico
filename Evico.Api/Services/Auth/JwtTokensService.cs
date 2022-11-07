using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Evico.Api.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Evico.Api.Services.Auth;

public class JwtTokensService
{
    private readonly JwtConfiguration _configuration;

    public JwtTokensService(IOptions<JwtConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public string CreateAccessTokenForUser(ProfileRecord user)
    {
        return CreateTokenForUser(user,
            DateTime.UtcNow + _configuration.JwtDefaultLifetime);
    }

    public string CreateRefreshTokenForUser(ProfileRecord user)
    {
        return CreateTokenForUser(user,
            DateTime.UtcNow + _configuration.JwtRefreshLifetime);
    }

    private string CreateTokenForUser(User user, DateTime expires)
    {
        var issuer = _configuration.Issuer;
        var audience = _configuration.Audience;
        var key = Encoding.ASCII.GetBytes(_configuration.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.Id}"),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    public JwtSecurityToken ParseToken(string jwtBase64)
    {
        return new(jwtBase64);
    }
}