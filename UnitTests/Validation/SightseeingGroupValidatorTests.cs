using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Data.Validation;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using UnitTests.Helpers;

namespace UnitTests.Validation
{
    public class SightseeingGroupValidatorTests
    {
        private SightseeingGroupValidator _validator;


        [OneTimeSetUp]
        public void SetUp()
        {
            var info = new GeneralSightseeingInfo[]
           {
                new GeneralSightseeingInfo
                {
                    Id = "1",
                    OpeningHour = new TimeSpan(10, 0, 0),
                    ClosingHour = new TimeSpan(18, 0, 0),
                    Description = "test",
                    MaxAllowedGroupSize = 35,
                    MaxChildAge = 5,
                    MaxTicketOrderInterval = 4
                }
           };
            var _dbContextMock = new Mock<ApplicationDbContext>();
            _dbContextMock.Setup(x => x.GeneralSightseeingInfo).Returns(CreateMock.CreateDbSetMock<GeneralSightseeingInfo>(info).Object);
            _validator = new SightseeingGroupValidator(_dbContextMock.Object);           
        }


        [Test]
        public void Validate__Sightseeing_hour_is_is_not_during_opening_hours__Should_be_invalid()
        {
            // Opening hours are not at night. Note, that hour is half past which is valid be is not during opening hours.
            var invalidGroup = new SightseeingGroup { SightseeingDate = new DateTime(2019, 4, 4, 23, 30, 00) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__Sigthseeing_hour_is_not_full__Should_be_invalid()
        {
            var invalidGroup = new SightseeingGroup { SightseeingDate = new DateTime(2019, 9, 9, 12, 23, 0) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__Sigthseeing_hour_full__Should_be_valid()
        {
            var validGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(1).Truncate(TimeSpan.FromHours(1)) };

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
            var invalidGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddYears(2) };

            _validator.ShouldHaveValidationErrorFor(x => x.SightseeingDate, invalidGroup);
        }

        [Test]
        public void Validate__SightseeingDate_is_in_the_future_less_than_or_equal_to_max_ticket_order_interval__Should_be_valid()
        {
            // This max ticket order interval is 4 weeks by default.
            var validGroup = new SightseeingGroup { SightseeingDate = DateTime.Now.AddDays(28).Truncate(TimeSpan.FromHours(1)) };

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
