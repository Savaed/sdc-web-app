namespace SDCWebApp.Models.Dtos
{
    public class RefreshTokensResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}
