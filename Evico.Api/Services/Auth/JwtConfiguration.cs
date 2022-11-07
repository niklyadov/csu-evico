namespace Evico.Api.Services.Auth;

public class JwtConfiguration
{
    public TimeSpan JwtDefaultLifetime { get; set; } = TimeSpan.FromDays(7);
    public TimeSpan JwtRefreshLifetime { get; set; } = TimeSpan.FromDays(14);
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}