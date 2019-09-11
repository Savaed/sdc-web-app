using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using UnitTests.Helpers;

using SDCWebApp.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class GeneralSightseeingInfoTests
    {
        private GeneralSightseeingInfo _info;


        [OneTimeSetUp]
        public void SetUp()
        {
            _info = CreateModel.CreateInfo();
        }


        [Test]
        public void Equals__One_info_is_reffered_to_second__Should_be_the_same()
        {
            var info2 = _info;

            bool isEqual = _info.Equals(info2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__Two_info_with_the_same_properties_value_except_opening_hours__Should_be_the_same()
        {
            var info2 = CreateModel.CreateInfo();

            bool isEqual = _info.Equals(info2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            // Different Description and MaxAllowedGroupSize.
            var info2 = CreateModel.CreateInfo(description: "other_test", maxAllowedGroupSize: 20);

            bool isEqual = _info.Equals(info2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_info_is_null__Should_not_be_the_same()
        {
            GeneralSightseeingInfo info1 = null;
            var info2 = _info;

            bool isEqual = info2.Equals(info1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var info2 = _info;

            bool isEqual = info2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            bool isEqual = _info.Equals(_info);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void OpeningDateTime__OpeningHour_is_10_30__Should_return_date_time_with_hour_equals_10_30()
        {
            int hour = 10;
            int minute = 30;
            var now = DateTime.Now;
            _info.OpeningHours.ToArray().First(x => x.DayOfWeek == now.DayOfWeek).OpeningHour = new TimeSpan(hour, minute, 0);

            var datetime = _info.GetOpeningDateTime(now);

            datetime.Hour.Should().Be(hour);
            datetime.Minute.Should().Be(minute);
            datetime.Date.Should().BeSameDateAs(now.Date);
        }

        [Test]
        public void ClosingDateTime__ClosingHour_is_18_00__Should_return_date_time_with_hour_equals_18_00()
        {
            int hour = 18;
            int minute = 0;
            var now = DateTime.Now;
            _info.OpeningHours.ToArray().First(x => x.DayOfWeek == now.DayOfWeek).ClosingHour = new TimeSpan(hour, minute, 0);

            var datetime = _info.GetClosingDateTime(now);

            datetime.Hour.Should().Be(hour);
            datetime.Minute.Should().Be(minute);
            datetime.Date.Should().BeSameDateAs(now.Date);
        }
    }
}
