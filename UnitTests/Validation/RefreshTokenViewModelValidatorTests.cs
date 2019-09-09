using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validation;
using SDCWebApp.Models.Dtos;

namespace UnitTests.Validation
{
    [TestFixture]
    public class RefreshTokenViewModelValidatorTests
    {
        [Test]
        public void AccessToken__Is_null_or_empty__Should_be_invalid([Values(null, "")] string token)
        {
            var invalidVieModel = new RefreshTokenViewModel { AccessToken = token };

            var validator = new RefreshTokenViewModelValidator();

            validator.ShouldHaveValidationErrorFor(x => x.AccessToken, invalidVieModel);
        }

        [Test]
        public void RefreshToken__Is_null_or_empty__Should_be_invalid([Values(null, "")] string token)
        {
            var invalidVieModel = new RefreshTokenViewModel { RefreshToken = token };

            var validator = new RefreshTokenViewModelValidator();

            validator.ShouldHaveValidationErrorFor(x => x.RefreshToken, invalidVieModel);
        }
    }
}
