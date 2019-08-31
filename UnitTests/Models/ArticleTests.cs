using FluentAssertions;
using NUnit.Framework;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Models
{
    [TestFixture]
    public class ArticleTests
    {
        [Test]
        public void Equals__One_article_is_reffered_to_second__Should_be_the_same()
        {
            var article1 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };
            var article2 = article1;

            bool isEqual = article1.Equals(article2);

            isEqual.Should().BeTrue();
        }

        // For equality Id, Description, Title, Text, Author must be the same
        [Test]
        public void Equals__Two_articles_with_the_same_properties_value__Should_be_the_same()
        {
            var article1 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };
            var article2 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };

            bool isEqual = article1.Equals(article2);

            isEqual.Should().BeTrue();
        }

        [Test]
        public void Equals__At_least_one_property_value_is_different__Should_not_be_the_same()
        {
            var article1 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };
            var article2 = new Article { Id = "1", Title = "test", Text = "sample", Author = "joe doe" };

            bool isEqual = article1.Equals(article2);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__One_article_is_null__Should_not_be_the_same()
        {
            Article article1 = null;
            var article2 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };

            bool isEqual = article2.Equals(article1);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_of_two_different_types__Should_not_be_the_same()
        {
            DateTime? date = null;
            var article2 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };

            bool isEqual = article2.Equals(date);

            isEqual.Should().BeFalse();
        }

        [Test]
        public void Equals__Check_equality_the_same_single_discount__Should_be_the_same()
        {
            var article1 = new Article { Id = "1", Title = "other test", Text = "sample", Author = "joe doe" };

            bool isEqual = article1.Equals(article1);

            isEqual.Should().BeTrue();
        }
    }
}
