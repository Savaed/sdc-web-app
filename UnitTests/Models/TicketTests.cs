using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Models
{
    public class TicketTests
    {
        [Test]
        public void Price__Tariff_and_Discount_are_null__Should_return_zero()
        {
            var ticket = new Ticket();
            ticket.Discount = null;
            ticket.Tariff = null;

            float price = ticket.Price;

            price.Should().Equals(0.0f);
        }

        [Test]
        public void Price__Tariff_is_not_null_and_Discount_is_null__Should_return_Ticket_Default_Price()
        {
            var ticket = new Ticket();
            ticket.Discount = null;
            ticket.Tariff = new TicketTariff { DefaultPrice = 20.0f };
            float defaultPrice = ticket.Tariff.DefaultPrice;
            float price = ticket.Price;

            ((int)price).Should().Equals(defaultPrice);
        }

        [Test]
        public void Price__Tariff_is_null_and_Discount_is_not_null__Should_return_zero()
        {
            var ticket = new Ticket();
            ticket.Discount = new Discount { DiscountValueInPercentage = 50 };
            ticket.Tariff = null;

            float price = ticket.Price;

            price.Equals(0.0f);
        }

        [Test]
        public void Price__Tariff_and_Discount_have_value__should_return_calculated_price_diffrent_from_Tariff_Default_Price()
        {
            var ticket = new Ticket();
            ticket.Discount = new Discount { DiscountValueInPercentage = 50 };
            ticket.Tariff = new TicketTariff { DefaultPrice = 20 };
            float discountPrice = 50 / 20f;

            float price = ticket.Price;

            price.Equals(discountPrice);
            price.Should().NotBe(ticket.Tariff.DefaultPrice);
        }
    }
}
