namespace SDCWebApp.Models
{
    public class RefreshToken : BasicEntity, IAuthToken
    {
        public string Token { get; set; }
        public int ExpiryIn { get; set; }
    }
}
