namespace SDCWebApp.Models
{
    public interface IAuthToken
    {
        string Token { get; set; }
        int ExpiryIn { get; set; }
    }
}
