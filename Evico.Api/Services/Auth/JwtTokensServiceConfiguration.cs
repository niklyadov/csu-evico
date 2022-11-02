namespace Evico.Api.Services.Auth;

public class JwtTokensServiceConfiguration
{
    public TimeSpan JwtDefaultLifetime { get; set; } = TimeSpan.FromDays(7);
    public string JwtSecret { get; set; } = "B?E(H+MbQeThWmZq";
}