namespace Evico;

public class BearerRefreshTokenPair
{
    public BearerRefreshTokenPair(string bearerToken, string refreshToken)
    {
        BearerToken = bearerToken;
        RefreshToken = refreshToken;
    }

    public string BearerToken { get; set; }
    public string RefreshToken { get; set; }
}