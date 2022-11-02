namespace Evico;

public class BearerRefreshTokenPair
{
    public BearerRefreshTokenPair(String bearerToken, String refreshToken)
    {
        BearerToken = bearerToken;
        RefreshToken = refreshToken;
    }
    
    public String BearerToken { get; set; }
    public String RefreshToken { get; set; }
}