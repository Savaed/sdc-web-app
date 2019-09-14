using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SDCWebApp.Auth
{
    /// <summary>
    /// DTO class for retriving Jason Web Token settings from settings file.
    /// </summary>
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryIn { get; set; }   // Time span in seconds
        public string SecretKey { get; set; }
        public int RefreshTokenExpiryIn { get; set; }   // Time span in seconds


        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                // Clock skew compensates for server time drift.
                ClockSkew = TimeSpan.FromMinutes(5),

                // Ensure that tokent hasn't expired.
                RequireExpirationTime = true,
                ValidateLifetime = true,

                // Ensure the token audience matches server audience.
                ValidAudience = Audience,
                ValidateAudience = true,

                // Ensure the token was issued by trusted authorization server (in this case by this server),
                ValidIssuer = Issuer,
                ValidateIssuer = true,

                // Specify the key used to signed the token.
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                RequireSignedTokens = true
            };
        }
    }
}
