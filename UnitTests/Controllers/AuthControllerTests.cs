using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using SDCWebApp.ApiErrors;
using SDCWebApp.Auth;
using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IJwtTokenHandler> _jwtTokenHandlerMock;
        private Mock<IRefreshTokenManager> _refreshTokenManagerMock;
        private ILogger<AuthController> _logger;
        private Mock<IMapper> _mapperMock;
        private IdentityUser _user;
        private string _validAccessToken;
        private string _invalidAccessToken;
        private string _refreshToken;
        private ClaimsIdentity _claims;
        private ClaimsPrincipal _expiredTokenPrincipal;


        [OneTimeSetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _jwtTokenHandlerMock = new Mock<IJwtTokenHandler>();
            _refreshTokenManagerMock = new Mock<IRefreshTokenManager>();
            _logger = Mock.Of<ILogger<AuthController>>();
            string[] userRoles = new string[] { "admin" };
            _user = new IdentityUser
            {
                Id = "1",
                Email = "sample@mail.com",
                UserName = "username"
            };
            _claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, "1"),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddDays(-2)).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, _user.Id),

                // In this project only one user role will be used.
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault())
            });
            _validAccessToken = "validAccessToken";
            _invalidAccessToken = "invalidAccessToken";
            _refreshToken = "refreshToken";
            _expiredTokenPrincipal = new ClaimsPrincipal();
            _expiredTokenPrincipal.AddIdentity(_claims);
        }


        #region RefreshTokenExchangeAsync(string expiredAccessToken, string refreshToken)
        // Invalid access token passed -> 400, invalid access token
        // Access token doesn't expire -> 400, invalid access token (it's not expired)
        // Refresh token not found -> 404, refresh token not found
        // Tokens are null or empty -> 400
        // Access and refresh tokens are valid -> 200, new access and refresh tokens are returned    

        [Test]
        public async Task RefreshTokenExchangeAsync__Refresh_token_expired__Should_return_400BadRequest_response()
        {
            var notExpiredTokenPrincipal = GetNotExpiredTokenPrincipal();
            var notExpiredJwtToken = new JwtSecurityToken(expires: DateTime.UtcNow.AddDays(1));
            string notExpiredAccessToken = new JwtSecurityTokenHandler().WriteToken(notExpiredJwtToken);
            var expiredRefreshToken = new RefreshToken { Id = "2", Token = "newRefreshToken", ExpiryIn = (int)new DateTimeOffset(DateTime.UtcNow.AddDays(-2)).ToUnixTimeSeconds() };
            _refreshTokenManagerMock.Setup(x => x.GetSavedRefreshTokenAsync(It.IsNotNull<string>())).ReturnsAsync(expiredRefreshToken);
            _jwtTokenHandlerMock.Setup(x => x.GetPrincipalFromJwtToken(It.IsNotNull<string>(), true)).Returns(notExpiredTokenPrincipal);
            var refreshRequestBody = new RefreshTokenViewModel { AccessToken = notExpiredAccessToken, RefreshToken = _refreshToken };
            var controller = new AuthController(_jwtTokenHandlerMock.Object, _refreshTokenManagerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.RefreshTokenExchangeAsync(refreshRequestBody);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task RefreshTokenExchangeAsync__Invalid_access_token_was_passed__Should_return_400BadRequest_response()
        {
            _jwtTokenHandlerMock.Setup(x => x.GetPrincipalFromJwtToken(_invalidAccessToken, true)).Throws<SecurityTokenException>();
            var controller = new AuthController(_jwtTokenHandlerMock.Object, _refreshTokenManagerMock.Object, _logger, _mapperMock.Object);
            var refreshRequestBody = new RefreshTokenViewModel { AccessToken = _invalidAccessToken, RefreshToken = _refreshToken };

            var result = await controller.RefreshTokenExchangeAsync(refreshRequestBody);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task RefreshTokenExchangeAsync__Access_token_does_not_expiry__Should_return_400BadRequest_response()
        {
            var notExpiredTokenPrincipal = GetNotExpiredTokenPrincipal();
            var notExpiredJwtToken = new JwtSecurityToken(expires: DateTime.UtcNow.AddDays(1));
            string notExpiredAccessToken = new JwtSecurityTokenHandler().WriteToken(notExpiredJwtToken);
            _jwtTokenHandlerMock.Setup(x => x.GetPrincipalFromJwtToken(It.IsNotNull<string>(), true)).Returns(notExpiredTokenPrincipal);
            var controller = new AuthController(_jwtTokenHandlerMock.Object, _refreshTokenManagerMock.Object, _logger, _mapperMock.Object);
            var refreshRequestBody = new RefreshTokenViewModel { AccessToken = notExpiredAccessToken, RefreshToken = _refreshToken };

            var result = await controller.RefreshTokenExchangeAsync(refreshRequestBody);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task RefreshTokenExchangeAsync__Passed_refresh_token_not_found__Should_return_400BadRequest_response()
        {
            _jwtTokenHandlerMock.Setup(x => x.GetPrincipalFromJwtToken(_validAccessToken, true)).Returns(_expiredTokenPrincipal);
            _refreshTokenManagerMock.Setup(x => x.GetSavedRefreshTokenAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new AuthController(_jwtTokenHandlerMock.Object, _refreshTokenManagerMock.Object, _logger, _mapperMock.Object);
            var refreshRequestBody = new RefreshTokenViewModel { AccessToken = _validAccessToken, RefreshToken = _refreshToken };

            var result = await controller.RefreshTokenExchangeAsync(refreshRequestBody);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task RefreshTokenExchangeAsync__Passed_access_and_refresh_tokens_are_valid__Should_return_200OK_response_with_new_tokens()
        {
            var newJwtToken = new JwtSecurityToken(issuer: "example.com", expires: DateTime.UtcNow.AddDays(1));
            string newAccessToken = new JwtSecurityTokenHandler().WriteToken(newJwtToken);
            var savedRefreshToken = new RefreshToken { Id = "1", Token = _refreshToken, ExpiryIn = (int)new DateTimeOffset(DateTime.UtcNow.AddDays(2)).ToUnixTimeSeconds() };
            var newRefreshToken = new RefreshToken { Id = "2", Token = "newRefreshToken", ExpiryIn = (int)new DateTimeOffset(DateTime.UtcNow.AddDays(2)).ToUnixTimeSeconds() };
            SetUpMocksForSucceed(newJwtToken, newAccessToken, savedRefreshToken, newRefreshToken);
            _mapperMock.Setup(x => x.Map<RefreshTokenDto>(It.IsNotNull<RefreshToken>())).Returns(new RefreshTokenDto(newRefreshToken.Token, newRefreshToken.ExpiryIn));
            var controller = new AuthController(_jwtTokenHandlerMock.Object, _refreshTokenManagerMock.Object, _logger, _mapperMock.Object);
            var refreshRequestBody = new RefreshTokenViewModel { AccessToken = _validAccessToken, RefreshToken = _refreshToken };

            var result = await controller.RefreshTokenExchangeAsync(refreshRequestBody);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
            (((result as ObjectResult).Value as ResponseWrapper).Data as RefreshTokensResponseDto).AccessToken.Should().NotBeNull();
            (((result as ObjectResult).Value as ResponseWrapper).Data as RefreshTokensResponseDto).RefreshToken.Should().NotBeNull();

        }

        #endregion


        #region Privates

        private void SetUpMocksForSucceed(JwtSecurityToken newJwtToken, string newAccessToken, RefreshToken savedRefreshToken, RefreshToken newRefreshToken)
        {
            _jwtTokenHandlerMock.Setup(x => x.GetPrincipalFromJwtToken(_validAccessToken, true)).Returns(_expiredTokenPrincipal);
            _jwtTokenHandlerMock.Setup(x => x.CreateJwtToken(It.IsNotNull<IdentityUser>(), It.IsNotNull<string[]>())).Returns(newJwtToken);
            _jwtTokenHandlerMock.Setup(x => x.WriteJwtToken(It.IsNotNull<JwtSecurityToken>())).Returns(newAccessToken);
            _refreshTokenManagerMock.Setup(x => x.GetSavedRefreshTokenAsync(It.IsNotNull<string>())).ReturnsAsync(savedRefreshToken);
            _refreshTokenManagerMock.Setup(x => x.GenerateRefreshToken(32)).Returns(newRefreshToken);
            _refreshTokenManagerMock.Setup(x => x.AddRefreshTokenAsync(It.IsNotNull<RefreshToken>()));
            _refreshTokenManagerMock.Setup(x => x.DeleteRefreshTokenAsync(It.IsNotNull<RefreshToken>()));
        }

        private ClaimsPrincipal GetNotExpiredTokenPrincipal()
        {

            string[] userRoles = new string[] { "admin" };
            var notExpiredTokenPrincipal = new ClaimsPrincipal();
            notExpiredTokenPrincipal.AddIdentity(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, "1"),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddDays(2)).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, _user.Id),

                // In this project only one user role will be used.
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault())
            }));

            return notExpiredTokenPrincipal;
        }

        #endregion
    }
}
