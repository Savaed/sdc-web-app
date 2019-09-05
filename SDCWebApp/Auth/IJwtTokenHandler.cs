using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SDCWebApp.Auth
{
    public interface IJwtTokenHandler
    {
        /// <summary>
        /// Creates JWT token for given <paramref name="user"/>. Throws an excpetion if create JWT token failed.
        /// </summary>
        /// <param name="user">The <see cref=" IdentityUser"/> user for which the token creates.</param>
        /// <param name="userRoles">The use roles.</param>
        /// <returns>Created <see cref="JwtSecurityToken"/> token.</returns>
        /// <exception cref="Exception">Create JWT access token failed.</exception>
        JwtSecurityToken CreateJwtToken(IdentityUser user, string[] userRoles);

        string WriteJwtToken(JwtSecurityToken token);

        /// <summary>
        /// Validates JWT token and gets <see cref="ClaimsPrincipal"/> described by it.
        /// Throws an exception if token validtion failed.
        /// </summary>
        /// <param name="token">The token to be validate and from which principal will be retrieved.</param>
        /// <param name="forExpiredToken">Indicates whether validation parameters will be used for validation expired token.</param>
        /// <returns>Returns <see cref="ClaimsPrincipal"/> principal described by the token.</returns>
        /// <exception cref="SecurityTokenException">Token validation failed.</exception>
        ClaimsPrincipal GetPrincipalFromJwtToken(string token, bool forExpiredToken = false);

    }
}