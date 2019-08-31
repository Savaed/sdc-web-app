using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Models
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Equals__One_customer_is_reffered_to_second__Should_be_the_same()
        {
            var customer1 = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };
            var customer2 = customer1;

            bool isEqual = customer1.Equals(customer2);

            isEqual.Should().BeTrue();
        }

        // For equality Id and EmailAddress must be the same.
        [Test]
        public void Equals__Two_Customers_with_the_same_properties_value__Should_be_the_same()
        {
            var customer1 = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };
            var customer2 = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };

            bool isEqual = customer1.Equals(customer2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var customer1 = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };
            var customer2 = new Customer { Id = "1", EmailAddress = null, DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };

            bool isEqual = customer1.Equals(customer2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_Customer_is_null__Should_not_be_the_same()
        {
            Discount customer1 = null;
            var customer2 = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };

            bool isEqual = customer2.Equals(customer1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var customer2 = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };

            bool isEqual = customer2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            var customer = new Customer { Id = "1", EmailAddress = "test", DateOfBirth = new DateTime(1990, 1, 1), HasFamilyCard = false };

            bool isEqual = customer.Equals(customer);

            isEqual.Should().BeTrue();
        }
    }
}
