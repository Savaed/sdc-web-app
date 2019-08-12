﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FluentValidation.TestHelper;
using Moq;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models;
using FluentAssertions;

namespace UnitTests
{
    [TestFixture]
    public class DiscountValidatorTests
    {
        private readonly DiscountValidator _validator = new DiscountValidator();


        [Test]
        public void Validate__GroupSizeForDiscount_is_set_while_Type_is_different_from_ForGroup__Should_be_invalid([Values(
            Discount.DiscountType.ForChild,
            Discount.DiscountType.ForDisabled,
            Discount.DiscountType.ForFamily,
            Discount.DiscountType.ForPensioner,
            Discount.DiscountType.ForStudent)]
        Discount.DiscountType type)
        {
            var invalidDiscount = new Discount { GroupSizeForDiscount = 20, Type = type };

            _validator.ShouldHaveValidationErrorFor(x => x.GroupSizeForDiscount, invalidDiscount);
        }

        [Test]
        public void Validate__GroupSizeForDiscount_is_set_while_Type_is_ForGroup__Should_be_valid()
        {
            var validDiscount = new Discount { GroupSizeForDiscount = 20, Type = Discount.DiscountType.ForGroup };

            _validator.ShouldNotHaveValidationErrorFor(x => x.GroupSizeForDiscount, validDiscount);
        }

        [Test]
        public void Validate__Discount_value_is_less_than_0__Should_be_invalid()
        {
            var invalidDiscount = new Discount { DiscountValueInPercentage = -32 };

            _validator.ShouldHaveValidationErrorFor(x => x.DiscountValueInPercentage, invalidDiscount);
        }

        [Test]
        public void Validate__Discount_value_is_between_0_and_100__Should_be_valid()
        {
            var validDiscount = new Discount { DiscountValueInPercentage = 32 };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DiscountValueInPercentage, validDiscount);
        }

        [Test]
        public void Validate__Discount_value_is_greater_than_100__Should_be_invalid()
        {
            var invalidDiscount = new Discount { DiscountValueInPercentage = 101 };

            _validator.ShouldHaveValidationErrorFor(x => x.DiscountValueInPercentage, invalidDiscount);
        }

        [Test]
        public void Validate__Discount_value_is_0_or_100__Should_be_valid([Values(0, 100)] int discountValue)
        {
            var validDiscount = new Discount { DiscountValueInPercentage = discountValue };

            _validator.ShouldNotHaveValidationErrorFor(x => x.DiscountValueInPercentage, validDiscount);
        }

        [Test]
        public void Validate__Description_is_null_or_empty__Should_be_invalid([Values(null, "")] string description)
        {
            var invalidDiscount = new Discount { Description = description };

            _validator.ShouldHaveValidationErrorFor(x => x.Description, invalidDiscount);
        }

        [Test]
        public void Validate__Description_has_more_than_256_characters__Should_be_invalid()
        {
            var invalidDiscount = new Discount
            {
                Description = "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901"
            };

            invalidDiscount.Description.Length.Should().BeGreaterThan(256);
            _validator.ShouldHaveValidationErrorFor(x => x.Description, invalidDiscount);
        }

        [Test]
        public void Validate__Description_has_less_than_256_characters__Should_be_valid()
        {
            var validDiscount = new Discount { Description = "123456789" };

            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, validDiscount);
        }

        [Test]
        public void Validate__Description_has_exactly_256_characters__Should_be_valid()
        {
            var validDiscount = new Discount
            {
                Description = "1234567890123456789012345678901123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901" +
                "123456789012345678901234567890112345678901234567890123456789011234567890123456789012345678901123456789012345678901234567890123456789"
            };

            validDiscount.Description.Length.Should().Be(256);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Description, validDiscount);
        }
    }
}
