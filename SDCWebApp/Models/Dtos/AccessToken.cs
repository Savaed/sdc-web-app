namespace SDCWebApp.Models.Dtos
{
    public class AccessToken : IAuthToken
    {
        public string Token { get; set; }
        public int ExpiryIn { get; set; }


        public AccessToken(string token, int expiryIn)
        {
            Token = token;
            ExpiryIn = expiryIn;
        }
    }
}
