using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Models
{
    [TestFixture]
    public class SightseeingTariffTests
    {
        [Test]
        public void Equals__One_sightseeing_tariff_is_reffered_to_second__Should_be_the_same()
        {
            var sightseeingTariff1 = new SightseeingTariff { Id = "1", Name = "test" };
            var sightseeingTariff2 = sightseeingTariff1;

            bool isEqual = sightseeingTariff1.Equals(sightseeingTariff2);

            isEqual.Should().BeTrue();
        }

        // For equality Id, Name must be the same.
        [Test]
        public void Equals__Two_sightseeing_tariff_with_the_same_properties_value__Should_be_the_same()
        {
            var sightseeingTariff1 = new SightseeingTariff { Id = "1", Name = "test" };
            var sightseeingTariff2 = new SightseeingTariff { Id = "1", Name = "test" };

            bool isEqual = sightseeingTariff1.Equals(sightseeingTariff2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var sightseeingTariff1 = new SightseeingTariff { Id = "1", Name = "test" };
            var sightseeingTariff2 = new SightseeingTariff { Id = "1", Name = "other test" };

            bool isEqual = sightseeingTariff1.Equals(sightseeingTariff2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_sightseeing_tariff_is_null__Should_not_be_the_same()
        {
            SightseeingTariff SightseeingTariff1 = null;
            var sightseeingTariff2 = new SightseeingTariff { Id = "1", Name = "test" };

            bool isEqual = sightseeingTariff2.Equals(SightseeingTariff1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var sightseeingTariff1 = new SightseeingTariff { Id = "1", Name = "test" };

            bool isEqual = sightseeingTariff1.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_sightseeing_tariff__Should_be_the_same()
        {
            var sightseeingTariff1 = new SightseeingTariff { Id = "1", Name = "test" };

            bool isEqual = sightseeingTariff1.Equals(sightseeingTariff1);

            isEqual.Should().BeTrue();
        }
    }
}
