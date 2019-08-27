using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Validators
{
    public class TicketTariffDtoValidatorTests
    {
        private readonly TicketTariffDtoValidator _validator = new TicketTariffDtoValidator();


        [Test]
        public void Validate__DefaultPrice_is_less_than_0_or_equals_to_0__Should_be_invalid([Values(-1.0f, 0.0f)] float price)
        {
            var invalidTicketTariffDto = new TicketTariffDto { DefaultPrice = price };

            _validator.ShouldHaveValidationErrorFor(x => x.DefaultPrice, invalidTicketTariffDto);
        }

        [Test]
        public void Validate__DefaultPrice_is_greater_than_1000__Should_be_invalid()
        {
            var invalidTicketTariffDto = new TicketTariffDto { DefaultPrice = 3400.0f };

            _validator.ShouldHaveValidationErrorFor(x => x.DefaultPrice, invalidTicketTariffDto);
        }

        [Test]
        public void Validate__DefaultPrice_is_exactly_1000__Should_be_valid()
        {
            var validTicketTariffDto = new TicketTariffDto { DefaultPrice = 1000.0f };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DefaultPrice, validTicketTariffDto);
        }

        [Test]
        public void Validate__DefaultPrice_is_greater_than_0_and_less_than_1000__Should_be_valid()
        {
            var validTicketTariffDto = new TicketTariffDto { DefaultPrice = 100.0f };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DefaultPrice, validTicketTariffDto);
        }

        [Test]
        public void Validate__Description_is_null_or_empty__Should_be_invalid([Values(null, "")] string description)
        {
            var invalidTicketTariffDto = new TicketTariffDto { Description = description };

            _validator.ShouldHaveValidationErrorFor(x => x.Description, invalidTicketTariffDto);
        }

        [Test]
        public void Validate__Description_has_more_than_256_characters__Should_be_invalid()
        {
            var invalidTicketTariffDto = new TicketTariffDto
            {
                Description = "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901"
            };

            invalidTicketTariffDto.Description.Length.Should().BeGreaterThan(256);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, invalidTicketTariffDto);
        }

        [Test]
        public void Validate__Description_has_less_than_256_characters__Should_be_valid()
        {
            var validTicketTariffDto = new TicketTariffDto { Description = "123456789" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, validTicketTariffDto);
        }

        [Test]
        public void Validate__Description_has_exactly_256_characters__Should_be_valid()
        {
            var validTicketTariffDto = new TicketTariffDto
            {
                Description = "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901123456789012345678901234567890123456789"
            };

            validTicketTariffDto.Description.Length.Should().Be(256);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, validTicketTariffDto);
        }
    }
}
