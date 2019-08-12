using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models;

namespace UnitTests.Validators
{
    public class SightseeingGroupValidatorTests
    {
        private readonly SightseeingGroupValidator _validator = new SightseeingGroupValidator();


        [Test]
        public void Validate__Sigthseeing_hour_is_not_full_or_half_past__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { SightseeingDate = new DateTime(2019, 9, 9, 12, 23, 0) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        // This test will fail after 12.12.2019 12:00:00
        [Test]
        public void Validate__Sigthseeing_hour_full_or_half_past__Should_be_valid()
        {
            var validGroup = new SightseeingGroup { SightseeingDate = new DateTime(2019, 12, 12, 12, 0, 0) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.SightseeingDate, validGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_past__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddYears(-23) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_future_more_than_1_year_letter__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddYears(2) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        // This test will fail after 12.12.2019 12:00:00
        [Test]
        public void Validate__SightseeingDate_is_in_the_future_less_than_or_equal_to_1_year_letter__Should_be_valid()
        {
            var validGroup = new SightseeingGroup { SightseeingDate = new DateTime(2019, 12, 12, 12, 0, 0) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.SightseeingDate, validGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_less_than_0__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { MaxGroupSize = -1 };

            _validator.ShouldHaveValidationErrorFor(x => x.MaxGroupSize, invalidGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_greater_than_40__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { MaxGroupSize = 41 };

            _validator.ShouldHaveValidationErrorFor(x => x.MaxGroupSize, invalidGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_0_or_40__Should_be_valid([Values(0, 40)] int groupSize)
        {
            var validGroup = new SightseeingGroup { MaxGroupSize = groupSize };

            _validator.ShouldNotHaveValidationErrorFor(x => x.MaxGroupSize, validGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_between_0_and_40__Should_be_valid()
        {
            var validGroup = new SightseeingGroup { MaxGroupSize = 32 };

            _validator.ShouldNotHaveValidationErrorFor(x => x.MaxGroupSize, validGroup);
        }
    }
}
