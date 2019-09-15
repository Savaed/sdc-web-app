namespace SDCWebApp.Models
{
    public class RefreshToken : BasicEntity
    {
        public string Token { get; set; }
        public int ExpiryIn { get; set; }
    }
}
