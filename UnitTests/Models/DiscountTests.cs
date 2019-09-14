using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;

namespace UnitTests.Models
{
    [TestFixture]
    public class DiscountTests
    {
        [Test]
        public void Equals__One_discount_is_reffered_to_second__Should_be_the_same()
        {
            var discount1 = new Discount { Id = "1", Description = "test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };
            var discount2 = discount1;

            bool isEqual = discount1.Equals(discount2);

            isEqual.Should().BeTrue();
        }

        // For equality Id, Description, GroupSizeForDiscount, DiscountValueInPercentage, Type must be the same
        [Test]
        public void Equals__Two_discount_with_the_same_properties_value__Should_be_the_same()
        {
            var discount1 = new Discount { Id = "1", Description = "test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };
            var discount2 = new Discount { Id = "1", Description = "test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };

            bool isEqual = discount1.Equals(discount2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var discount1 = new Discount { Id = "1", Description = "test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };
            var discount2 = new Discount { Id = "1", Description = null, GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };

            bool isEqual = discount1.Equals(discount2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_discount_is_null__Should_not_be_the_same()
        {
            Discount discount1 = null;
            var discount2 = new Discount { Id = "1", Description = "other test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };

            bool isEqual = discount2.Equals(discount1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var discount2 = new Discount { Id = "1", Description = "other test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };

            bool isEqual = discount2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            var discount1 = new Discount { Id = "1", Description = "other test", GroupSizeForDiscount = 20, DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForChild };

            bool isEqual = discount1.Equals(discount1);

            isEqual.Should().BeTrue();
        }
    }
}
