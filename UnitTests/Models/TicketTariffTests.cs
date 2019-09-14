using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;

namespace UnitTests.Models
{
    internal class TicketTariffTests
    {
        [Test]
        public void Equals__One_ticket_tariff_is_reffered_to_second__Should_be_the_same()
        {
            var ticketTariff1 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };
            var ticketTariff2 = ticketTariff1;

            bool isEqual = ticketTariff1.Equals(ticketTariff2);

            isEqual.Should().BeTrue();
        }

        // For equality Id, Description, DefaultPrice, IdPerHour, IsPerPerson
        [Test]
        public void Equals__Two_ticket_tariffs_with_the_same_properties_value__Should_be_the_same()
        {
            var ticketTariff1 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };
            var ticketTariff2 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };

            bool isEqual = ticketTariff1.Equals(ticketTariff2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var ticketTariff1 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };
            var ticketTariff2 = new TicketTariff { Id = "1", Description = "other description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };

            bool isEqual = ticketTariff1.Equals(ticketTariff2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_ticket_tariff_is_null__Should_not_be_the_same()
        {
            TicketTariff ticketTariff1 = null;
            var ticketTariff2 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };

            bool isEqual = ticketTariff2.Equals(ticketTariff1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var ticketTariff2 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };

            bool isEqual = ticketTariff2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            var ticketTariff1 = new TicketTariff { Id = "1", Description = "description", DefaultPrice = 20, IsPerHour = true, IsPerPerson = true };

            bool isEqual = ticketTariff1.Equals(ticketTariff1);

            isEqual.Should().BeTrue();
        }
    }
}
