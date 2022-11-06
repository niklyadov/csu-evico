namespace Evico.Api.Services.Auth;

public class JwtConfiguration
{
    public TimeSpan JwtDefaultLifetime { get; set; } = TimeSpan.FromDays(7);
    public TimeSpan JwtRefreshLifetime { get; set; } = TimeSpan.FromDays(14);
    public string Key { get; set; } = String.Empty;
    public String Issuer { get; set; } = String.Empty;
    public String Audience { get; set; } = String.Empty;
}