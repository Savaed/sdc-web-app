using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models.ViewModels;

namespace UnitTests.Validators
{
    public class LoginViewModelValidatorTests
    {
        private readonly LoginViewModelValidator _validator = new LoginViewModelValidator();


        [Test]
        public void Validate__Username_is_null_or_empty__Should_be_invalid([Values(null, "")] string userName)
        {
            var invalidRegisterVM = new LoginViewModel { UserName = userName };

            _validator.ShouldHaveValidationErrorFor(x => x.UserName, invalidRegisterVM);
        }

        [Test]
        public void Validate__Username_has_less_than_2_or_more_than_20_characters__Should_be_invalid([Values("a", "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqq")] string userName)
        {
            var invalidRegisterVM = new LoginViewModel { UserName = userName };

            _validator.ShouldHaveValidationErrorFor(x => x.UserName, invalidRegisterVM);
        }

        [Test]
        public void Validate__Username_contains_characters_other_than_lowrcase_letters__Should_be_invalid([Values("ab12", "AB", "a&*", "!##", "123")] string userName)
        {
            var invalidRegisterVM = new LoginViewModel { UserName = userName };

            _validator.ShouldHaveValidationErrorFor(x => x.UserName, invalidRegisterVM);
        }

        [Test]
        public void Validate__Username_has_more_than_2_and_less_than_20_lowercase_letter__Should_be_valid()
        {
            var validRegisterVM = new LoginViewModel { UserName = "abcdef" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.UserName, validRegisterVM);
        }

        [Test]
        public void Validate__Password_is_null_or_empty__Should_be_invalid([Values(null, "")] string password)
        {
            var invalidRegisterVM = new LoginViewModel { Password = password };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_digit__Should_be_invalid()
        {
            var invalidRegisterVM = new LoginViewModel { Password = "abcABC)_+" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_special_charackter__Should_be_invalid()
        {
            var invalidRegisterVM = new LoginViewModel { Password = "abcABC123" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_uppercase__Should_be_invalid()
        {
            var invalidRegisterVM = new LoginViewModel { Password = "abc#$%123" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_lowercase__Should_be_invalid()
        {
            var invalidRegisterVM = new LoginViewModel { Password = "ABC#$%123" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_less_than_8_characters__Should_be_invalid()
        {
            var invalidRegisterVM = new LoginViewModel { Password = "aA1)" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_valid__Should_be_valid()
        {
            var validRegisterVM = new LoginViewModel { Password = "abcABC123!#" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.Password, validRegisterVM);
        }

    }
}
