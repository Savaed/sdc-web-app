using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Data.Validation;
using SDCWebApp.Models;
using System;
using UnitTests.Helpers;

namespace UnitTests.Validation
{
    public class SightseeingGroupValidatorTests
    {
        private const int MaxAllowedGroupSize = 30;
        private SightseeingGroupValidator _validator;
        private VisitInfo _info;
        private int _maxDaysForOrder;


        [OneTimeSetUp]
        public void SetUp()
        {
            _info = CreateModel.CreateInfo(maxAllowedGroupSize: MaxAllowedGroupSize);
            var _dbContextMock = new Mock<ApplicationDbContext>();
            _dbContextMock.Setup(x => x.Info).Returns(CreateMock.CreateDbSetMock<VisitInfo>(new VisitInfo[] { _info }).Object);
            _validator = new SightseeingGroupValidator(_dbContextMock.Object);
            _maxDaysForOrder = _info.MaxTicketOrderInterval * 7;
        }


        [Test]
        public void Validate__Sightseeing_hour_is_is_not_during_opening_hours__Should_be_invalid()
        {
            // DateTime is 15 minutes after closing.
            var invalidGroup = new SightseeingGroup { SightseeingDate = _info.GetClosingDateTime(DateTime.Now).AddMinutes(15) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__Sigthseeing_hour_is_not_full__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(_maxDaysForOrder - 1).Date.AddHours(13).AddMinutes(23) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__Sigthseeing_hour_full__Should_be_valid()
        {
            // SightseeingDate is set to 28 days since now, 13 p.m. It's valid because company is open between 10 and 18. 
            var validGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(_maxDaysForOrder - 1).Date.AddHours(13) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.SightseeingDate, validGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_past__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddYears(-23) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_future_more_than_max_ticket_order_interval_specifies__Should_be_invalid()
        {
            // This max ticket order interval is 4 weeks by default.
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(_maxDaysForOrder + 1) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_future_equal_to_max_ticket_order_interval__Should_be_invalid()
        {
            // This max ticket order interval is 4 weeks by default.
            // SightseeingDate is set to 28 days since now, 13 p.m. It's valid because company is open between 10 and 18. 
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(_maxDaysForOrder) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_future_less_than_max_ticket_order_interval__Should_be_valid()
        {
            // This max ticket order interval is 4 weeks by default.
            // SightseeingDate is set to 27:23:59:59 days since now, 13 p.m. It's valid because company is open between 10 and 18. 
            var validGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(_maxDaysForOrder - 1).Date.AddHours(13) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.SightseeingDate, validGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_less_than_0__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { MaxGroupSize = -1 };

            _validator.ShouldHaveValidationErrorFor(x => x.MaxGroupSize, invalidGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_greater_than_max_allowed__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { MaxGroupSize = _info.MaxAllowedGroupSize + 1 };

            _validator.ShouldHaveValidationErrorFor(x => x.MaxGroupSize, invalidGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_0_or_max_allowed__Should_be_valid([Values(0, MaxAllowedGroupSize)] int groupSize)
        {
            var validGroup = new SightseeingGroup { MaxGroupSize = groupSize };

            _validator.ShouldNotHaveValidationErrorFor(x => x.MaxGroupSize, validGroup);
        }

        [Test]
        public void Validate__MaxGroupSize_is_between_0_and_max_allowed__Should_be_valid()
        {
            var validGroup = new SightseeingGroup { MaxGroupSize = MaxAllowedGroupSize - 1 };

            _validator.ShouldNotHaveValidationErrorFor(x => x.MaxGroupSize, validGroup);
        }
    }
}
