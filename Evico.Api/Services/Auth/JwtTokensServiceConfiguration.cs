namespace Evico.Api.Services.Auth;

public class JwtTokensServiceConfiguration
{
    public TimeSpan JwtDefaultLifetime { get; set; } = TimeSpan.FromDays(7);
    public TimeSpan JwtRefreshLifetime { get; set; } = TimeSpan.FromDays(14);
    public string JwtSecret { get; set; } = "B?E(H+MbQeThWmZq";
}