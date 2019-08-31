using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Models
{
    [TestFixture]
    public class SightseeingGroupTests
    {
        public object SightseeingGroup1 { get; private set; }

        [Test]
        public void Equals__One_sightseeing_group_is_reffered_to_second__Should_be_the_same()
        {
            var sightseeingGroup1 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 9, 9), MaxGroupSize = 30 };
            var sightseeingGroup2 = sightseeingGroup1;

            bool isEqual = sightseeingGroup1.Equals(sightseeingGroup2);

            isEqual.Should().BeTrue();
        }

        // For equality Id, Description, Title, Text, Author must be the same
        [Test]
        public void Equals__Two_sightseeing_groups_with_the_same_properties_value__Should_be_the_same()
        {
            var sightseeingGroup1 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 9, 9), MaxGroupSize = 30 };
            var sightseeingGroup2 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 9, 9), MaxGroupSize = 30 };

            bool isEqual = sightseeingGroup1.Equals(sightseeingGroup2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var sightseeingGroup1 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 9, 9), MaxGroupSize = 30 };
            var sightseeingGroup2 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 8, 8), MaxGroupSize = 30 };

            bool isEqual = sightseeingGroup1.Equals(sightseeingGroup2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_sightseeing_group_is_null__Should_not_be_the_same()
        {
            SightseeingGroup sightseeingGroup1 = null;
            var sightseeingGroup2 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 8, 8), MaxGroupSize = 30 };

            bool isEqual = sightseeingGroup2.Equals(sightseeingGroup1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var sightseeingGroup2 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 8, 8), MaxGroupSize = 30 };

            bool isEqual = sightseeingGroup2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            var sightseeingGroup1 = new SightseeingGroup { Id = "1", SightseeingDate = new DateTime(2019, 8, 8), MaxGroupSize = 30 };

            bool isEqual = sightseeingGroup1.Equals(sightseeingGroup1);

            isEqual.Should().BeTrue();
        }

    }
}
