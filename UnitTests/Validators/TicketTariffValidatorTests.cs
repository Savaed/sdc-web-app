using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FluentValidation.TestHelper;
using Moq;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models;
using FluentAssertions;

namespace UnitTests.Validators
{
    [TestFixture]
    public class TicketTariffValidatorTests
    {
        private readonly TicketTariffValidator _validator = new TicketTariffValidator();


        [Test]
        public void Validate__DefaultPrice_is_less_than_0_or_equals_to_0__Should_be_invalid([Values(-1.0f, 0.0f)] float price)
        {
            var invalidTicketTariff = new TicketTariff { DefaultPrice = price };

            _validator.ShouldHaveValidationErrorFor(x => x.DefaultPrice, invalidTicketTariff);
        }

        [Test]
        public void Validate__DefaultPrice_is_greater_than_1000__Should_be_invalid()
        {
            var invalidTicketTariff = new TicketTariff { DefaultPrice = 3400.0f };

            _validator.ShouldHaveValidationErrorFor(x => x.DefaultPrice, invalidTicketTariff);
        }

        [Test]
        public void Validate__DefaultPrice_is_exactly_1000__Should_be_valid()
        {
            var validTicketTariff = new TicketTariff { DefaultPrice = 1000.0f };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DefaultPrice, validTicketTariff);
        }

        [Test]
        public void Validate__DefaultPrice_is_greater_than_0_and_less_than_1000__Should_be_valid()
        {
            var validTicketTariff = new TicketTariff { DefaultPrice = 100.0f };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DefaultPrice, validTicketTariff);
        }

        [Test]
        public void Validate__Description_is_null_or_empty__Should_be_invalid([Values(null, "")] string description)
        {
            var invalidTicketTariff = new TicketTariff { Description = description };

            _validator.ShouldHaveValidationErrorFor(x => x.Description, invalidTicketTariff);
        }

        [Test]
        public void Validate__Description_has_more_than_256_characters__Should_be_invalid()
        {
            var invalidTicketTariff = new TicketTariff
            {
                Description = "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901"
            };

            invalidTicketTariff.Description.Length.Should().BeGreaterThan(256);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, invalidTicketTariff);
        }

        [Test]
        public void Validate__Description_has_less_than_256_characters__Should_be_valid()
        {
            var validTicketTariff = new TicketTariff { Description = "123456789" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, validTicketTariff);
        }

        [Test]
        public void Validate__Description_has_exactly_256_characters__Should_be_valid()
        {
            var validTicketTariff = new TicketTariff
            {
                Description = "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901123456789012345678901234567890123456789"
            };

            validTicketTariff.Description.Length.Should().Be(256);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, validTicketTariff);
        }


    }
}
