using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Models
{
    [TestFixture]
    public class GeneralSightseeingInfoTests
    {
        [Test]
        public void Equals__One_info_is_reffered_to_second__Should_be_the_same()
        {
            var info1 = new GeneralSightseeingInfo { Id = "1", Description = "description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };
            var info2 = info1;

            bool isEqual = info1.Equals(info2);

            isEqual.Should().BeTrue();
        }

        // For equality Id, Description, MaxChildAge, MacAllowedGroupSize, OpeningHour, ClosingHour
        [Test]
        public void Equals__Two_info_with_the_same_properties_value__Should_be_the_same()
        {
            var info1 = new GeneralSightseeingInfo { Id = "1", Description = "description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };
            var info2 = new GeneralSightseeingInfo { Id = "1", Description = "description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };

            bool isEqual = info1.Equals(info2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var info1 = new GeneralSightseeingInfo { Id = "1", Description = "description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };
            var info2 = new GeneralSightseeingInfo { Id = "1", Description = "other description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };

            bool isEqual = info1.Equals(info2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_info_is_null__Should_not_be_the_same()
        {
            Discount info1 = null;
            var info2 = new GeneralSightseeingInfo { Id = "1", Description = "other description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };

            bool isEqual = info2.Equals(info1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var info2 = new GeneralSightseeingInfo { Id = "1", Description = "other description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };

            bool isEqual = info2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            var info1 = new GeneralSightseeingInfo { Id = "1", Description = "other description", MaxChildAge = 5, MaxAllowedGroupSize = 30, OpeningHour = 10, ClosingHour = 18 };

            bool isEqual = info1.Equals(info1);

            isEqual.Should().BeTrue();
        }
    }
}
