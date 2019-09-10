using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using SDCWebApp.Auth;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using SDCWebApp.Helpers.Constants;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// API controller that allows exchanging expired access tokens using refresh tokens.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomApiController, IAuthController
    {
        private readonly IRefreshTokenManager _refreshTokenManager;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;


        public AuthController(IJwtTokenHandler jwtTokenHandler, IRefreshTokenManager refreshTokenManager, ILogger<AuthController> logger, IMapper mapper) : base(logger)
        {
            _refreshTokenManager = refreshTokenManager;
            _jwtTokenHandler = jwtTokenHandler;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Asynchronously exchanges <see cref="RefreshToken"/> and <see cref="AccessToken"/> tokens based on passed refresh token.
        /// Returns <see cref="HttpStatusCode.BadRequest"/> response if the access token is not expired, it is invalid or if the refresh token expired.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="refreshTokenData">The <see cref="RefreshTokenViewModel"/> that contains access and refresh tokens. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>New access and refresh JWT tokens.</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshTokenExchangeAsync([FromBody] RefreshTokenViewModel refreshTokenData)
        {
            _logger.LogInformation($"Starting method '{nameof(RefreshTokenExchangeAsync)}'.");           

            try
            {
                _logger.LogDebug($"Getting principal from passed access token.");
                var claims = _jwtTokenHandler.GetPrincipalFromJwtToken(refreshTokenData.AccessToken, true);

                // Get expiry date of the access token. If it's not before UtcNow then return 400 BadRequest.
                var expiryDate = DateTimeOffset.FromUnixTimeSeconds(int.Parse(claims.FindFirstValue(JwtRegisteredClaimNames.Exp)));
                if (expiryDate.DateTime > DateTime.UtcNow)
                {
                    return OnInvalidParameterError($"The access token passed has not expired.");
                }

                // Check if passed refresh token matches any saved refresh token.
                _logger.LogDebug("Checking refresh token correctness.");
                var savedRefreshToken = await _refreshTokenManager.GetSavedRefreshTokenAsync(refreshTokenData.RefreshToken);

                // Check if refresh token expired.
                if (IsRefreshTokenExpired(savedRefreshToken))
                {
                    return OnInvalidParameterError($"The refresh token passed has expired.");
                }

                // Create temporary user representation based on access token principal.
                var tempUser = new IdentityUser
                {
                    Id = claims.FindFirstValue(JwtRegisteredClaimNames.NameId),
                    UserName = claims.FindFirstValue(JwtRegisteredClaimNames.Sub),
                    Email = claims.FindFirstValue(JwtRegisteredClaimNames.Email)
                };

                string[] userRoles = new string[] { claims.FindFirstValue(ApiConstants.RoleClaimName) };

                // Generate new access and refresh tokens.
                _logger.LogDebug("Generating new access and refresh tokens.");
                var newJwtToken = _jwtTokenHandler.CreateJwtToken(tempUser, userRoles);
                string newAccessToken = _jwtTokenHandler.WriteJwtToken(newJwtToken);
                var newRefreshToken = _refreshTokenManager.GenerateRefreshToken();

                // Add new refresh token to the database for future tokens exchange.
                // Delete the saved token to prevent the use of the refresh token more than once.
                _logger.LogDebug("Adding new refresh token to the database.");
                await _refreshTokenManager.AddRefreshTokenAsync(newRefreshToken);
                _logger.LogDebug("Deleting saved refresh token from the database.");
                await _refreshTokenManager.DeleteRefreshTokenAsync(savedRefreshToken);

                var response = new ResponseWrapper(new RefreshTokensResponseDto
                {
                    AccessToken = new AccessToken(newAccessToken, (int)newJwtToken.Payload.Exp),
                    RefreshToken = MapToDto(newRefreshToken)
                });

                _logger.LogInformation($"Finished method '{nameof(RefreshTokenExchangeAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnInvalidParameterError($"Invalid refresh token.", ex);
            }
            catch (SecurityTokenException ex)
            {
                return OnInvalidParameterError($"Invalid access token.", ex);
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex, ex.Message);
                throw;
            }
        }


        #region Privates

        private RefreshTokenDto MapToDto(RefreshToken refreshToken) => _mapper.Map<RefreshTokenDto>(refreshToken);

        private bool IsRefreshTokenExpired(RefreshToken refreshToken)
        {
            var tokenExpiryDate = DateTimeOffset.FromUnixTimeSeconds(refreshToken.ExpiryIn).UtcDateTime;
            return tokenExpiryDate < DateTime.UtcNow;
        }

        #endregion

    }
}