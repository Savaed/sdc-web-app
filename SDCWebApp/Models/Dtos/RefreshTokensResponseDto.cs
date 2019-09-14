namespace SDCWebApp.Models.Dtos
{
    public class RefreshTokensResponseDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}
