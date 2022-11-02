using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Evico.Api.Entity;
using Microsoft.IdentityModel.Tokens;

namespace Evico.Api.Services.Auth;

public class JwtTokensService
{
    private readonly JwtTokensServiceConfiguration _configuration = new();

    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.JwtSecret));
    }

    public string GenerateNewTokenForUser(ProfileRecord user, string audience = "default", DateTime? expires = null)
    {
        var generationDate = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            user.Id.ToString(),
            audience,
            notBefore: generationDate,
            expires: expires ?? generationDate.Add(_configuration.JwtDefaultLifetime),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<bool> IsValidTokenAsync(ProfileRecord user, string jwtBase64, string audience = "default")
    {
        var validationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(jwtBase64,
            new TokenValidationParameters
            {
                ValidIssuer = user.Id.ToString(),
                ValidateIssuer = true,

                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,

                ValidateLifetime = true,

                ValidAudience = audience,
                ValidateAudience = true
            }
        );

        return validationResult.IsValid;
    }

    public JwtSecurityToken ParseToken(string jwtBase64) => new(jwtBase64);
}