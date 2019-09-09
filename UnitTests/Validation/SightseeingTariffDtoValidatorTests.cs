using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validation;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Validation
{
    public class SightseeingTariffDtoValidatorTests
    {
        private readonly SightseeingTariffDtoValidator _validator = new SightseeingTariffDtoValidator();


        [Test]
        public void Validate__Name_is_null_or_empty__Should_be_invalid([Values(null, "")] string name)
        {
            var invalidTariff = new SightseeingTariffDto { Name = name };

            _validator.ShouldHaveValidationErrorFor(x => x.Name, invalidTariff);
        }

        [Test]
        public void Validate__Name_lenght_is_greater_than_50__Should_be_invalid()
        {
            var invalidTariff = new SightseeingTariffDto { Name = "123456789012345678901234567890123456789012345678901" };

            invalidTariff.Name.Length.Should().BeGreaterThan(50);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, invalidTariff);
        }

        [Test]
        public void Validate__Name_lenght_is_exactly_50__Should_be_valid()
        {
            var validTariff = new SightseeingTariffDto { Name = "12345678901234567890123456789012345678901234567890" };

            validTariff.Name.Length.Should().Be(50);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, validTariff);
        }

        [Test]
        public void Validate__Name_lenght_is_less_than_50__Should_be_valid()
        {
            var validTariff = new SightseeingTariffDto { Name = "1234567890123456789012345678901234567890123456789" };

            validTariff.Name.Length.Should().BeLessThan(50);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, validTariff);
        }
    }
}
