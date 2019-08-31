using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models;
using System;

namespace UnitTests.Validators
{
    [TestFixture]
    public class CustomerValidatorTests
    {
        private readonly CustomerValidator _validator = new CustomerValidator();


        [Test]
        public void Validate__EmailAddress_is_null_or_empty__Should_be_invalid([Values(null, "")] string email)
        {
            var invalidCustomer = new Customer { EmailAddress = email };

            _validator.ShouldHaveValidationErrorFor(x => x.EmailAddress, invalidCustomer);
        }

        // Invalid email adresses acording https://en.wikipedia.org/wiki/Email_address#Examples
        [Test]
        public void Validate__EmailAddress_is_incorrect__Should_be_invalid([Values(
        "",
        "Abc.example.com)",
        "A@b@c@example.com",
        "a\\\"b(c)d,e:f;g<h>i[j\\k]l@example.com",
        "just\"not\"right@example.com",
        "this is\"not\\allowed@example.com",
        "this\\ still\\\"not\\\\allowed@example.com",
        "1234567890123456789012345678901234567890123456789012345678901234+x@example.com")] string email)
        {

            var invalidCustomer = new Customer { EmailAddress = email };

            _validator.ShouldHaveValidationErrorFor(x => x.EmailAddress, invalidCustomer);
        }

        // Valid email adresses acording https://en.wikipedia.org/wiki/Email_address#Examples
        [Test]
        public void Validate__EmailAddress_is_correct__Should_be_valid([Values(
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
            var validCustomer = new Customer { EmailAddress = email };

            _validator.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, validCustomer);
        }

        // This case is almost impossible due to the time it takes for the client to send a request to the server to process it.      
        [Test]
        public void Validate__DateOfBirth_is_exactly_122_years_and_164_days_ago__Should_be_valid()
        {
            var validCustomer = new Customer { DateOfBirth = DateTime.Now.AddYears(-122).AddDays(-164) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth, validCustomer);
        }

        [Test]
        public void Validate__DateOfBirth_is_more_than_122_years_and_164_days_ago__Should_be_invalid()
        {
            var invalidCustomer = new Customer { DateOfBirth = DateTime.Now.AddYears(-1000) };

            _validator.ShouldHaveValidationErrorFor(x => x.DateOfBirth, invalidCustomer);
        }

        [Test]
        public void Validate__DateOfBirth_is_in_the_future__Should_be_invalid()
        {
            var invalidCustomer = new Customer { DateOfBirth = DateTime.Now.AddYears(122).AddDays(164) };

            _validator.ShouldHaveValidationErrorFor(x => x.DateOfBirth, invalidCustomer);
        }

        [Test]
        public void Validate__DateOfBirth_has_default_value__Should_be_invalid()
        {
            // By default, customer's birth date is DateTime.MinValue = 1.1.0001
            var invalidCustomer = new Customer();

            _validator.ShouldHaveValidationErrorFor(x => x.DateOfBirth, invalidCustomer);
        }

        [Test]
        public void Validate__DateOfBirth_is_between_122_years_and_164_days_ago_and_now__Should_be_valid()
        {
            var validCustomer = new Customer { DateOfBirth = DateTime.Now.AddYears(-23) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth, validCustomer);
        }
    }
}
