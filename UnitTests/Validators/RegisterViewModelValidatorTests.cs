using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models.ViewModels;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Validators
{
    [TestFixture]
    public class RegisterViewModelValidatorTests
    {
        private ApplicationDbContext _dbContext;
        private RegisterViewModelValidator _validator;


        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContext = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext))).Object;
            _validator = new RegisterViewModelValidator(_dbContext);
        }


        [Test]
        public void Validate__Role_is_null_or_empty__Should_be_invalid([Values(null, "")] string role)
        {
            var invalidRegisterVM = new RegisterViewModel { Role = role };

            _validator.ShouldHaveValidationErrorFor(x => x.EmailAddress, invalidRegisterVM);
        }

        [Test]
        public async Task Validate__Given_role_doesnt_exist_in_db__should_be_invalid()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Roles.RemoveRange(await context.Roles.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var invalidRegisterVM = new RegisterViewModel { Role = "adasd" };
                    var validator = new RegisterViewModelValidator(context);

                    validator.ShouldHaveValidationErrorFor(x => x.Role, invalidRegisterVM);
                }
            }
        }

        [Test]
        public async Task Validate__Given_role_exist_in_db__Should_be_valid()
        {
            string role = "admin";
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var validRegisterVM = new RegisterViewModel { Role = role };
                    var validator = new RegisterViewModelValidator(context);

                    validator.ShouldNotHaveValidationErrorFor(x => x.Role, validRegisterVM);
                }
            }
        }

        [Test]
        public void Validate__Incorrect_email_format__Should_be_invalid([Values(
        null,
        "",
        "Abc.example.com)",
        "A@b@c@example.com",
        "a\\\"b(c)d,e:f;g<h>i[j\\k]l@example.com",
        "just\"not\"right@example.com",
        "this is\"not\\allowed@example.com",
        "this\\ still\\\"not\\\\allowed@example.com",
        "1234567890123456789012345678901234567890123456789012345678901234+x@example.com")] string email)
        {
            var invalidRegisterVM = new RegisterViewModel { EmailAddress = email };

            _validator.ShouldHaveValidationErrorFor(x => x.EmailAddress, invalidRegisterVM);
        }


        [Test]
        public void Validate__Correct_email_format__Should_be_valid([Values(
        "simple@example.com",
        "very.common@example.com",
        "disposable.style.email.with+symbol@example.com",
        "other.email-with-hyphen@example.com",
        "fully-qualified-domain@example.com",
        "user.name+tag+sorting@example.com",
        "x@example.com",
        "example-indeed@strange-example.com",
        "admin@mailserver1",
        "example@s.example",
        "\" \"@example.org",
        "\"john..doe\"@example.org")] string email)
        {
            var validRegisterVM = new RegisterViewModel { EmailAddress = email };

            _validator.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, validRegisterVM);
        }

        [Test]
        public void Validate__Username_is_null_or_empty__Should_be_invalid([Values(null, "")] string userName)
        {
            var invalidRegisterVM = new RegisterViewModel { UserName = userName };

            _validator.ShouldHaveValidationErrorFor(x => x.UserName, invalidRegisterVM);
        }

        [Test]
        public void Validate__Username_has_less_than_2_or_more_than_20_characters__Should_be_invalid([Values("a", "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqq")] string userName)
        {
            var invalidRegisterVM = new RegisterViewModel { UserName = userName };

            _validator.ShouldHaveValidationErrorFor(x => x.UserName, invalidRegisterVM);
        }

        [Test]
        public void Validate__Username_contains_characters_other_than_lowrcase_letters__Should_be_invalid([Values("ab12", "AB", "a&*", "!##", "123")] string userName)
        {
            var invalidRegisterVM = new RegisterViewModel { UserName = userName };

            _validator.ShouldHaveValidationErrorFor(x => x.UserName, invalidRegisterVM);
        }

        [Test]
        public void Validate__Username_has_more_than_2_and_less_than_20_lowercase_letter__Should_be_valid()
        {
            var validRegisterVM = new RegisterViewModel { UserName = "abcdef" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.UserName, validRegisterVM);
        }

        [Test]
        public void Validate__Password_is_null_or_empty__Should_be_invalid([Values(null, "")] string password)
        {
            var invalidRegisterVM = new RegisterViewModel { Password = password };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_digit__Should_be_invalid()
        {
            var invalidRegisterVM = new RegisterViewModel { Password = "abcABC)_+" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_special_charackter__Should_be_invalid()
        {
            var invalidRegisterVM = new RegisterViewModel { Password = "abcABC123" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_uppercase__Should_be_invalid()
        {
            var invalidRegisterVM = new RegisterViewModel { Password = "abc#$%123" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_not_any_lowercase__Should_be_invalid()
        {
            var invalidRegisterVM = new RegisterViewModel { Password = "ABC#$%123" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_has_less_than_8_characters__Should_be_invalid()
        {
            var invalidRegisterVM = new RegisterViewModel { Password = "aA1)" };

            _validator.ShouldHaveValidationErrorFor(x => x.Password, invalidRegisterVM);
        }

        [Test]
        public void Validate__Password_valid__Should_be_valid()
        {
            var validRegisterVM = new RegisterViewModel { Password = "abcABC123!#" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.Password, validRegisterVM);
        }
    }
}
