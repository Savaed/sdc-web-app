using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using SDCWebApp.Data.Validation;
using SDCWebApp.Models;

namespace UnitTests.Validation
{
    [TestFixture]
    public class ArticleValidatorTests
    {
        private readonly ArticleValidator _validator = new ArticleValidator();


        [Test]
        public void Validate__Title_is_null_or_empty__Should_be_invalid([Values(null, "")] string title)
        {
            var invalidArticle = new Article { Title = title };

            _validator.ShouldHaveValidationErrorFor(x => x.Title, invalidArticle);
        }

        [Test]
        public void Validate__Title_lenght_is_greater_than_50__Should_be_invalid()
        {
            var invalidArticle = new Article { Title = "123456789012345678901234567890123456789012345678901" };

            invalidArticle.Title.Length.Should().BeGreaterThan(50);
            _validator.ShouldHaveValidationErrorFor(x => x.Title, invalidArticle);
        }

        [Test]
        public void Validate__Title_lenght_is_exactly_50__Should_be_valid()
        {
            var validArticle = new Article { Title = "12345678901234567890123456789012345678901234567890" };

            validArticle.Title.Length.Should().Be(50);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Title, validArticle);
        }

        [Test]
        public void Validate__Title_lenght_is_less_than_50__Should_be_valid()
        {
            var validArticle = new Article { Title = "1234567890123456789012345678901234567890123456789" };

            validArticle.Title.Length.Should().BeLessThan(50);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Title, validArticle);
        }

        [Test]
        public void Validate__Text_is_null_or_empty__Should_be_invalid([Values(null, "")] string text)
        {
            var invalidArticle = new Article { Text = text };

            _validator.ShouldHaveValidationErrorFor(x => x.Text, invalidArticle);
        }


        [Test]
        public void Validate__Author_lenght_is_greater_than_50__Should_be_invalid()
        {
            var invalidArticle = new Article { Author = "123456789012345678901234567890123456789012345678901" };

            invalidArticle.Author.Length.Should().BeGreaterThan(50);
            _validator.ShouldHaveValidationErrorFor(x => x.Author, invalidArticle);
        }

        [Test]
        public void Validate__Author_lenght_is_exactly_50__Should_be_valid()
        {
            var validArticle = new Article { Author = "12345678901234567890123456789012345678901234567890" };

            validArticle.Author.Length.Should().Be(50);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Author, validArticle);
        }

        [Test]
        public void Validate__Author_lenght_is_less_than_50__Should_be_valid()
        {
            var validArticle = new Article { Author = "1234567890123456789012345678901234567890123456789" };

            validArticle.Author.Length.Should().BeLessThan(50);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Author, validArticle);
        }

        [Test]
        public void Validate__Author_is_null_or_empty__Should_be_invalid([Values(null, "")] string author)
        {
            var invalidArticle = new Article { Author = author };

            _validator.ShouldHaveValidationErrorFor(x => x.Author, invalidArticle);
        }
    }
}
