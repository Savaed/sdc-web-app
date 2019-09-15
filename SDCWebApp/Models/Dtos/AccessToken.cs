using System;

namespace SDCWebApp.Models.Dtos
{
    public class AccessToken : IAuthToken
    {
        public string Token { get; set; }
        public DateTime ExpiryIn { get; set; }


        public AccessToken(string token, long expiryInUnixEpochFormat)
        {
            Token = token;
            ExpiryIn = DateTimeOffset.FromUnixTimeSeconds(expiryInUnixEpochFormat).UtcDateTime;
        }
    }
}
