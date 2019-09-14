namespace SDCWebApp.Models.Dtos
{
    public class RefreshTokenDto : IAuthToken
    {
        public string Token { get; set; }
        public int ExpiryIn { get; set; }
    }
}
