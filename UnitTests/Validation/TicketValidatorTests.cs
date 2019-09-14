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
    [TestFixture]
    public class TicketValidatorTests
    {
        private TicketValidator _validator;
        private Mock<ApplicationDbContext> _dbContextMock;


        [OneTimeSetUp]
        public void SetUp()
        {
            var info = CreateModel.CreateInfo();
            _dbContextMock = new Mock<ApplicationDbContext>();
            _dbContextMock.Setup(x => x.GeneralSightseeingInfo).Returns(CreateMock.CreateDbSetMock<GeneralSightseeingInfo>(new GeneralSightseeingInfo[] { info }).Object);
            _validator = new TicketValidator(_dbContextMock.Object);
        }


        [Test]
        public void Validate__TicketUniqueId_is_null_or_empty__Should_be_invalid([Values(null, "")] string id)
        {
            var invalidTicket = new Ticket { TicketUniqueId = id };

            _validator.ShouldHaveValidationErrorFor(x => x.TicketUniqueId, invalidTicket);
        }

        [Test]
        public void Validate__TicketUniqueId_has_incorrect_format__Should_be_invalid(
            [Values("C56A4180-65AA-42EC-A945-5FD21DEC",
            "!",
            "00000000-0000-0000-0000-000000000000", // Null GUID is invalid in this case.
            "00000000000000000000000000000000",     // Null GUID is invalid in this case.
            "                                ",
            "x56a4180-h5aa-42ec-a945-5fd21dec0538",
            "a56a4180-a5aa-42ec-a945--fd21dec0538",
            "x56a4180-h5aa-42ec-a945-5fd21dec0538sdfsdfsdf",
            "{{6a4180-65aa-42ec-a945-5fd21dec053}}",
            "256a4180-d5aa-42ec-a9 5-5fd21dec0538",
            "{c56a4180-65aa-42ec-a9}5-5fd21dec0538}",
            "guid")] string id)
        {
            var invalidTicket = new Ticket { TicketUniqueId = id };

            _validator.ShouldHaveValidationErrorFor(x => x.TicketUniqueId, invalidTicket);
        }

        [Test]
        public void Validate__TicketUniqueId_has_correct_format__Should_be_valid(
            [Values("C56A4180-65AA-42EC-A945-5FD21DEC0538",
            "04a691a795574f4fba7186ece736c878",
            "c56a4180-65aa-42ec-a945-5fd21dec0538",
            "C56a418065aA426ca9455fd21deC0538",
            "{c56a4180-65aa-42ec-a945-5fd21dec0538}",
            "{C56a418065aa426ca9455fd21deC0538}")] string id)
        {
            var validTicket = new Ticket { TicketUniqueId = id };

            _validator.ShouldNotHaveValidationErrorFor(x => x.TicketUniqueId, validTicket);
        }

        [Test]
        public void Validate__PurchaseDate_is_in_the_future__Should_be_invalid()
        {
            var invalidTicket = new Ticket { PurchaseDate = DateTime.Now.AddYears(100) };

            _validator.ShouldHaveValidationErrorFor(x => x.PurchaseDate, invalidTicket);
        }

        // SDC opened on 2.06.2019, so you couldn't buy a ticket before the company was opened.
        [Test]
        public void Validate__PurchaseDate_is_in_the_past_but_before_2_06_2012__Should_be_invalid()
        {
            var invalidTicket = new Ticket { PurchaseDate = new DateTime(2012, 6, 1) };

            _validator.ShouldHaveValidationErrorFor(x => x.PurchaseDate, invalidTicket);
        }

        public void Validate__PurchaseDate_is_exactly_in_2_06_2012__Should_be_valid()
        {
            var validTicket = new Ticket { PurchaseDate = new DateTime(2012, 6, 2) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.PurchaseDate, validTicket);
        }

        [Test]
        public void Validate__PurchaseDate_is_in_the_past__Should_be_valid()
        {
            var validTicket = new Ticket { PurchaseDate = DateTime.Now.AddYears(-1) };

            _validator.ShouldNotHaveValidationErrorFor(x => x.PurchaseDate, validTicket);
        }

        // Due to the time it takes the client to send the request to the server and process it by the server, 
        // the situation where the purchase date is now (in milliseconds) is impossible.
        [Test]
        public void Validate__PurchaseDate_is_right_now__Should_be_invalid()
        {
            var invalidTicket = new Ticket { PurchaseDate = DateTime.Now };

            _validator.ShouldHaveValidationErrorFor(x => x.PurchaseDate, invalidTicket);
        }
    }
}
