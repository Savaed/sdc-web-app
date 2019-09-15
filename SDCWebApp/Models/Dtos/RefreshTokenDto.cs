using System;

namespace SDCWebApp.Models.Dtos
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiryIn { get; set; }

        public RefreshTokenDto() { }

        public RefreshTokenDto(string token, int expiryInUnixEpochFormat)
        {
            Token = token;
            ExpiryIn = DateTimeOffset.FromUnixTimeSeconds(expiryInUnixEpochFormat).UtcDateTime;
        }
    }
}