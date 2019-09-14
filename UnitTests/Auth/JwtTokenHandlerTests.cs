using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SDCWebApp.Auth;

namespace UnitTests.Auth
{
    [TestFixture]
    public class JwtTokenHandlerTests
    {
        private IOptions<JwtSettings> _jwtOptions;
        private ILogger<JwtTokenHandler> _logger;
        private IdentityUser _user;
        private string[] _roles;


        [OneTimeSetUp]
        public void SetUp()
        {
            _roles = new string[] { "mod" };
            _logger = Mock.Of<ILogger<JwtTokenHandler>>();
            _jwtOptions = Mock.Of<IOptions<JwtSettings>>(x => x.Value.Audience == "google.com"
                                                      && x.Value.SecretKey == "secret_key_which_must_have_more_than_128_bits_length"
                                                      && x.Value.Issuer == "google.com"
                                                      && x.Value.ExpiryIn == 3600);
            _user = new IdentityUser
            {
                UserName = "username",
                Email = "sample@mail.com",
                PasswordHash = "passHash"
            };
        }


        #region CreateJwtToken(IdentityUser user, string[] userRoles)
        // Create JWT succeeded -> return JwtSecurityToken

        [Test]
        public void CreateJwtToken__Jwt_created_succeeded__Should_return_not_null_JwtSecurityToken()
        {
            var handler = new JwtTokenHandler(_jwtOptions, _logger);

            var result = handler.CreateJwtToken(_user, _roles);

            result.Should().NotBeNull();
        }

        #endregion
    }
}
