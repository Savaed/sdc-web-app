using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SDCWebApp.Helpers
{
    public static class JwtValidation
    {
        public static TokenValidationParameters GetValidationParameters(JwtSettings jwtSettings)
        {
            byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = jwtSettings.Audience,
                ValidIssuer = jwtSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            return tokenValidationParameters;
        }
    }
}
