using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using SDCWebApp.Helpers.Constants;

namespace SDCWebApp.Auth
{
    /// <summary>
    /// Provides methods to create and validation JWT tokens.
    /// </summary>
    public sealed class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly ILogger<JwtTokenHandler> _logger;
        private readonly IOptions<JwtSettings> _jwtOptions;


        public JwtTokenHandler(IOptions<JwtSettings> options, ILogger<JwtTokenHandler> logger)
        {
            if (_jwtSecurityTokenHandler is null)
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            _logger = logger;
            _jwtOptions = options;
            ThrowIfInvalidOptions(_jwtOptions);
        }


        /// <summary>
        /// Creates <see cref="JwtSecurityToken"/> token for given <paramref name="user"/>. Throws an excpetion if create JWT token failed.
        /// </summary>
        /// <param name="user">The <see cref=" IdentityUser"/> user for which the token is created.</param>
        /// <param name="userRoles">The use roles.</param>
        /// <returns>Created <see cref="JwtSecurityToken"/> token.</returns>
        /// <exception cref="Exception">Create JWT access token failed.</exception>
        public JwtSecurityToken CreateJwtToken(IdentityUser user, string[] userRoles)
        {
            _logger.LogInformation($"Starting method '{nameof(CreateJwtToken)}'.");

            try
            {
                _logger.LogDebug($"Creating token descriptor.");
                var tokenDescriptor = CreateTokenDescriptor(user, userRoles);
                _logger.LogDebug($"Creating JWT token.");
                var jwtToken = _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                _logger.LogInformation($"Finished method '{nameof(CreateJwtToken)}'.");
                return jwtToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Create JWT access token failed: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Validates JWT token and gets <see cref="ClaimsPrincipal"/> described by it.
        /// Throws an exception if token validtion failed.
        /// </summary>
        /// <param name="token">The token to be validate and from which principal will be retrieved.</param>
        /// <param name="forExpiredToken">Indicates whether validation parameters will be used for validation expired token.</param>
        /// <returns>Returns <see cref="ClaimsPrincipal"/> principal described by the token.</returns>
        /// <exception cref="SecurityTokenException">Token validation failed.</exception>
        public ClaimsPrincipal GetPrincipalFromJwtToken(string token, bool forExpiredToken = false)
        {
            _logger.LogInformation($"Starting method '{nameof(GetPrincipalFromJwtToken)}'.");

            try
            {
                _logger.LogDebug($"Getting validation parameters.");
                var validationParameters = _jwtOptions.Value.GetValidationParameters();

                if (forExpiredToken)
                {
                    // Validate only expired tokens here. Validation of the token lifetime is not needed.
                    validationParameters.ValidateLifetime = false;
                }

                var principal = ValidateToken(token, validationParameters);
                _logger.LogInformation($"Finished method '{nameof(GetPrincipalFromJwtToken)}'.");
                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Token validation failed: {ex.Message}");
                var exception = new SecurityTokenException($"Token validation failed: { ex.Message }", ex);
                throw exception;
            }
        }

        public string WriteJwtToken(JwtSecurityToken token)
        {
            _logger.LogInformation($"Starting method '{nameof(WriteJwtToken)}'.");
            return _jwtSecurityTokenHandler.WriteToken(token);
        }


        #region Privates        

        private SecurityTokenDescriptor CreateTokenDescriptor(IdentityUser user, string[] userRoles)
        {
            _logger.LogDebug($"Starting method '{nameof(CreateTokenDescriptor)}'.");

            _logger.LogDebug($"Getting secrest key.");
            byte[] signingKey = Encoding.ASCII.GetBytes(_jwtOptions.Value.SecretKey);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id),

                        // In this project only one user role will be used.
                        new Claim(ApiConstants.RoleClaim, userRoles.FirstOrDefault()),
                    }),
                Audience = _jwtOptions.Value.Audience,
                Issuer = _jwtOptions.Value.Issuer,
                Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_jwtOptions.Value.ExpiryIn)),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
            };
        }

        private void ThrowIfInvalidOptions(IOptions<JwtSettings> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.Value.ExpiryIn <= 0)
            {
                throw new ArgumentException("Must be a non-zero, positive time interval in seconds.", nameof(options.Value.ExpiryIn));
            }

            if (options.Value.SecretKey is null)
            {
                throw new ArgumentNullException(nameof(options.Value.SecretKey), $"Argument {nameof(options.Value.SecretKey)} cannot be null."); ;
            }

            if (options.Value.Issuer == null)
            {
                throw new ArgumentNullException(nameof(options.Value.Issuer), $"Argument {nameof(options.Value.Issuer)} cannot be null."); ;
            }
            if (options.Value.Audience == null)
            {
                throw new ArgumentNullException(nameof(options.Value.Audience), $"Argument {nameof(options.Value.Audience)} cannot be null."); ;
            }
        }

        private ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            _logger.LogInformation($"Starting method '{nameof(ValidateToken)}'.");

            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                // Check the type of the token and algorithm used to encode token. It must be SecurityAlgorithms.HmacSha256.
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token.");

                _logger.LogInformation($"Finished method '{nameof(ValidateToken)}'.");
                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token validation failed: {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}
