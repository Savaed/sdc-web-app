using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Moq.Language.Flow;
using FluentAssertions;
using SDCWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SDCWebApp.Models;
using NLog;
using UnitTests.Helpers;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.TestHelper;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data.Validators;
using SDCWebApp.Services;

namespace UnitTests.Services
{
    [TestFixture]
    public class ArticleDbServiceTests
    {
        private ApplicationDbContext _dbContext;
        private Mock<IDbRepository> _repoMock;
        private ILogger<ArticleDbService> _logger;
        private Mock<ICustomValidator<Article>> _validatorMock;


        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContext = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext))).Object;
            _repoMock = new Mock<IDbRepository>();
            _logger = Mock.Of<ILogger<ArticleDbService>>();
            _validatorMock = new Mock<ICustomValidator<Article>>();
            _validatorMock.Setup(v => v.Validate(It.IsAny<Article>())).Returns(new ValidationResult());
        }


        #region GetArticleAsync(string id)
        // id jest nullem/pusty -> arg exc
        // nie istnieje bilet z takim id -> invalid oper exc
        // zasob jest pusty -> invalid oper exc i repo rzuci wyjatek 
        // zaosb nie istnieje -> null  ref exc
        // znalazlo -> zwraca tylko jeden element

        [Test]
        public async Task GetArticleAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            _repoMock.Setup(r => r.GetByIdAsync<Article>(id)).ThrowsAsync(new ArgumentException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticleAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'id' is null ore empty string.");
        }

        [Test]
        public async Task GetArticleAsync__Searching_Article_not_found__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<Article>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticleAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching element not found.");
        }

        [Test]
        public async Task GetArticleAsync__The_resource_is_empty__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<Article>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticleAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because the resource is empty.");
        }

        [Test]
        public async Task GetArticleAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<Article>(id)).ThrowsAsync(new NullReferenceException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticleAsync(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetArticleAsync__Searching_Article_found__Should_return_this_Article()
        {
            Article expectedArticle;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedArticle = await context.Articles.FirstOrDefaultAsync();
                }
            }
            _repoMock.Setup(r => r.GetByIdAsync<Article>(expectedArticle.Id)).ReturnsAsync(expectedArticle);
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetArticleAsync(expectedArticle.Id);

            result.Should().BeEquivalentTo(expectedArticle);
        }

        #endregion


        #region GetAllArticlesAsync()
        // zasob nie istnieje -> null ref exc
        // zasob jest pusty -> pusty ienumer i repo rzuci wyjatek 
        // znalazlo -> ienum<Article> dla wszystkich z zasobu


        [Test]
        public async Task GetAllArticlesAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            _repoMock.Setup(r => r.GetAllAsync<Article>()).ThrowsAsync(new NullReferenceException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetAllArticlesAsync();

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetAllArticlesAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
        {
            _repoMock.Setup(r => r.GetAllAsync<Article>()).ReturnsAsync(new Article[] { }.AsEnumerable());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetAllArticlesAsync();

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetAllArticlesAsync__All_Articles_found__Should_return_IEnumerable_for_all_Articles()
        {
            var expectedArticles = new Article[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedArticles = await context.Articles.ToArrayAsync();
                }
            }
            _repoMock.Setup(r => r.GetAllAsync<Article>()).ReturnsAsync(expectedArticles);
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetAllArticlesAsync();

            result.Count().Should().Be(expectedArticles.Count());
        }

        #endregion


        #region GetArticlesAsync(IEnumerable<string> ids)
        // zaosb nie istnieje -> null  ref exc
        // zasob jest pusty -> arg out of range
        // podano wiecej ids niz jest elementow w bazie -> arg out of range
        // ids sa nullem albo pusta tablica -> arg exc
        // jakies id nie puasuje -> ienum z mniejsza iloscia biletow
        // zadne id nie pasuje -> pusty ienumer
        // wszystkie id pasuja -> ienumer dla wszysztkich

        [Test]
        public async Task GetArticlesAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string[] ids = { "1", "2" };
            _repoMock.Setup(r => r.GetByIds<Article>(It.IsNotNull<IEnumerable<string>>())).Throws<NullReferenceException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticlesAsync(ids);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetArticlesAsync__The_resource_which_is_quering_is_empty__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = { "1", "2" };
            _repoMock.Setup(r => r.GetByIds<Article>(It.IsNotNull<IEnumerable<string>>())).Throws<ArgumentOutOfRangeException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = () => service.GetArticlesAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task GetArticlesAsync__Amount_od_ids_is_greater_than_Articles_number_in_resource__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = new string[] { "1", "2", "3", "4", "5" };
            _repoMock.Setup(r => r.GetByIds<Article>(ids)).Throws<ArgumentOutOfRangeException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticlesAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because amount of ids id greater than overall number of Articles in quering resource.");
        }

        [Test]
        public async Task GetArticlesAsync__Ids_are_null_or_empty__Should_throw_ArgumentException([Values(null, new string[] { "", "", "" })] string[] ids)
        {
            _repoMock.Setup(r => r.GetByIds<Article>(ids)).Throws<ArgumentException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticlesAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because arguemnt 'ids' is null or empty.");
        }

        [Test]
        public async Task GetArticlesAsync__At_least_one_id_dont_match__Should_return_IEnumerable_for_all_other_Articles()
        {
            var exptectedArticles = new Article[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    exptectedArticles = await context.Articles.ToArrayAsync();
                }
            }
            List<string> ids = new List<string>();
            foreach (Article Article in exptectedArticles)
            {
                ids.Add(Article.Id);
            }
            // This one id will be not matching.
            ids[0] = "1";
            _repoMock.Setup(r => r.GetByIds<Article>(ids)).Returns(exptectedArticles.Skip(1));
            int expectedLength = exptectedArticles.Count() - 1;
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetArticlesAsync(ids);

            result.Count().Should().Be(expectedLength);
        }

        [Test]
        public async Task GetArticlesAsync__Any_id_dont_match__Should_return_empty_IEnumerable()
        {
            string[] ids = new string[] { "-1", "-2" };
            _repoMock.Setup(r => r.GetByIds<Article>(ids)).Returns(new List<Article> { }.AsEnumerable());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetArticlesAsync(ids);

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetArticlesAsync__All_od_ids_matches__Should_return_IEnumerable_for_all_Articles()
        {
            var exptectedArticles = new Article[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    exptectedArticles = await context.Articles.ToArrayAsync();
                }
            }
            List<string> ids = new List<string>();
            foreach (Article article in exptectedArticles)
            {
                ids.Add(article.Id);
            }
            _repoMock.Setup(r => r.GetByIds<Article>(ids)).Returns(exptectedArticles);
            int expectedLength = exptectedArticles.Count();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetArticlesAsync(ids);

            result.Count().Should().Be(expectedLength);
        }
        #endregion


        #region GetArticlesByAsync(Expression<Func<Article, bool>> predicate)
        // predicate jest nullem -> arg null exc
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> pusty ienumer
        // zaden element nie spelnia predykatu -> pusty ienumer
        // wiecej niz jeden element spelnia predykat -> ienumer z dl. min. 2
        // tylko jeden element spelnia predykat -> ienum z dl. = 1

        [Test]
        public async Task GetArticlesByAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbOptions = Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext));
            var dbContextMock = new Mock<ApplicationDbContext>(dbOptions);
            dbContextMock.Setup(c => c.Articles).Returns(null as DbSet<Article>);
            var service = new ArticleDbService(_logger, dbContextMock.Object, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticlesByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetArticlesByAsync__The_predicate_is_null__Should_throw_ArgumentNullException()
        {
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetArticlesByAsync(null as Expression<Func<Article, bool>>);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'predicate' is null.");
        }

        [Test]
        public async Task GetArticlesByAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.RemoveRange(await context.Articles.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetArticlesByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetArticlesByAsync__Any_Article_satisfy_predicate__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetArticlesByAsync(x => x.CreatedAt == DateTime.Now.AddYears(-1000));

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetArticlesByAsync__Only_one_Article_satisfy_predicate__Should_return_IEnumerable_with_length_equals_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var expectedArticle = await context.Articles.FirstAsync();
                    var expectedArticleCreatedAtDate = expectedArticle.CreatedAt;
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetArticlesByAsync(x => x.CreatedAt == expectedArticleCreatedAtDate);

                    result.Count().Should().Be(1);
                }
            }
        }

        [Test]
        public async Task GetArticlesByAsync__More_that_one_Article_satisfy_predicate__Should_return_IEnumerable_for_matches_Articles()
        {
            Article[] articles = new Article[]
            {
                new Article{ Id = "1", Author= "Kowalski", Title = "Sample1", Text = "sample1" },
                new Article{ Id = "2", Author= "Kowalski", Title = "Sample2", Text = "sample2" },
                new Article{ Id = "3", Author= "Kowalski", Title = "Sample3", Text = "sample3" }
            };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.RemoveRange(await context.Articles.ToArrayAsync());
                    await context.Articles.AddRangeAsync(articles);
                    await context.SaveChangesAsync();

                }
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetArticlesByAsync(x => x.Author.Equals("Kowalski"));

                    result.Count().Should().Be(3);
                }
            }
        }

        #endregion


        #region AddArticle(Article article)
        // zasob jest nullem -> null ref exc
        // arg nie przechodzi validacji -> arg exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
        // arg jest nullem -> arg exc
        // validacja biletu sie nie udala (bledne dane) -> arg exc z powodem blednej validacji
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

        [Test]
        public async Task AddArticle__Argument_is_null__Should_throw_ArgumentNullException()
        {
            _repoMock.Setup(r => r.Add<Article>(It.IsAny<Article>())).Throws<ArgumentNullException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddArticleAsync(null as Article);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'article' is null.");
        }

        //[Test]
        //public async Task AddArticle__Argument_is_invalid__Should_throw_ArgumentException()
        //{
        //    Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = null };
        //    var validatorMock = new Mock<IArticleValidator>();
        //    validatorMock.Setup(v => v.Validate(article)).Returns<ValidationResult>(x => x = new ValidationResult());
        //    var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, validatorMock.Object);

        //    Func<Task> result = async () => await service.AddArticleAsync(article);

        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'article' is invalid.").WithMessage("Text");
        //}

        [Test]
        public async Task AddArticle__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.Add<Article>(It.IsNotNull<Article>())).Throws<NullReferenceException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddArticleAsync(article);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task AddArticle__In_resource_exists_the_same_Article_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.Add<Article>(It.IsNotNull<Article>())).Throws<InvalidOperationException>();
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddArticleAsync(article);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same Article as this one to be added.");
        }

        [Test]
        public async Task AddArticle__Add_successful__Should_return_added_Article()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.Add<Article>(article)).Returns(article);
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.AddArticleAsync(article);

            result.Should().BeEquivalentTo(article);
        }

        [Test]
        public async Task AddArticle__Add_successful__Resource_length_should_be_greater_by_1()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Articles.Count() + 1;
                    _repoMock.Setup(r => r.Add<Article>(It.IsNotNull<Article>())).Callback(() => context.Articles.Add(article)).Returns(article);
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.AddArticleAsync(article);

                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddArticle__Add_successful__Resource_contains_added_Article()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    _repoMock.Setup(r => r.Add<Article>(It.IsNotNull<Article>())).Callback(() => context.Articles.Add(article)).Returns(article);
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.AddArticleAsync(article);

                    context.Articles.Contains(article).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateArticleAsync(Article article)
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> invalid oper exc
        // nie znaleziono podanego biletu -> invalid oper exc
        // arg jest nullem -> arg null exc
        // arg ma id ktore jest nullem albo pusty -> arg exc   
        // validacja sie nie udala (bledne dane) -> arg exc
        // update udal sie -> tyle samo biletow co przed operacja 
        // update sie udal -> w zasobie istnieje zmodyfikowany bilet
        // update sie udal -> w zasobie nie istnieje poprzednia wersja biletu
        // update sie udal -> zwraca zmodyfikowany bilet

        [Test]
        public async Task UpdateArticleAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.UpdateAsync<Article>(It.IsNotNull<Article>())).ThrowsAsync(new NullReferenceException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateArticleAsync(article);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }


        [Test]
        public async Task UpdateArticleAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
        {
            Article article = new Article { Id = "1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.UpdateAsync<Article>(It.IsNotNull<Article>())).ThrowsAsync(new InvalidOperationException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateArticleAsync(article);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task UpdateArticleAsync__Matching_Article_not_found__Should_throw_InvalidOperationException()
        {
            Article article = new Article { Id = "-1", Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.UpdateAsync<Article>(It.IsNotNull<Article>())).ThrowsAsync(new InvalidOperationException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateArticleAsync(article);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching Article not found.");
        }

        [Test]
        public async Task UpdateArticleAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            _repoMock.Setup(r => r.UpdateAsync<Article>(null)).ThrowsAsync(new ArgumentNullException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateArticleAsync(null as Article);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Article' cannot be null.");
        }

        [Test]
        public async Task UpdateArticleAsync__Arguments_Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            Article article = new Article { Id = id, Author = "Kowalski", Title = "Sample", Text = "sample" };
            _repoMock.Setup(r => r.UpdateAsync<Article>(It.IsNotNull<Article>())).ThrowsAsync(new ArgumentException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateArticleAsync(article);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Article' has property 'Id' set to null or empty string.");
        }

        //[Test]
        //public async Task UpdateArticleAsync__Validation_failed__Should_throw_ArgumentException()
        //{
        //    Article Article = new Article { Id = "1", ArticleUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000) };
        //    _ArticleValidatorMock.Setup(v => v.Validate(Article));
        //    var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validator);

        //    Func<Task> result = async () => await service.UpdateArticleAsync(Article);

        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Article' has property 'Id' set to null or empty string.");
        //}

        [Test]
        public async Task UpdateArticleAsync__Update_successful__Should_return_updated_Article()
        {
            Article articleBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    articleBeforUpdate = await context.Articles.FirstAsync();
                    Article article = articleBeforUpdate;
                    article.Text = "sample2222";
                    article.Title = "xyz";
                    _repoMock.Setup(r => r.UpdateAsync<Article>(article)).Callback(() => context.Articles.Update(article)).ReturnsAsync(article);
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateArticleAsync(article);

                    result.Should().BeEquivalentTo(articleBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateArticleAsync__Update_successful__Resource_should_contains_updated_Article()
        {
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    article.Text = "sample2222";
                    article.Title = "xyz";
                    _repoMock.Setup(r => r.UpdateAsync<Article>(article)).Callback(() => context.Articles.Update(article)).ReturnsAsync(article);
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateArticleAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Contains(article).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateArticleAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_Article()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    article.Text = "sample2222";
                    article.Title = "xyz";
                    _repoMock.Setup(r => r.UpdateAsync<Article>(article)).Callback(() => context.Articles.Update(article)).ReturnsAsync(article);
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateArticleAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Single(x => x == article).Should().NotBeSameAs(articleBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateArticleAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            Article articleBeforUpdate;
            Article article;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    articleBeforUpdate = await context.Articles.FirstAsync();
                    article = articleBeforUpdate;
                    article.Text = "sample2222";
                    article.Title = "xyz";
                    _repoMock.Setup(r => r.UpdateAsync<Article>(article)).Callback(() => context.Articles.Update(article)).ReturnsAsync(article);
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
                    expectedLength = await context.Articles.CountAsync();

                    await service.UpdateArticleAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }
        #endregion


        #region DeleteArticleAsync(string id)
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> invalid oper exc
        // nie ma takiego biletu jak podany -> invalid oper exc
        // id jest nullem albo pusty -> arg exc
        // usuwanie sie udalo -> nie w zasobie usunietego biletu
        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

        [Test]
        public async Task DeleteArticleAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "1";
            _repoMock.Setup(r => r.DeleteAsync<Article>(id)).ThrowsAsync(new NullReferenceException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteArticleAsync(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }


        [Test]
        public async Task DeleteArticleAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
        {
            string id = "1";
            _repoMock.Setup(r => r.DeleteAsync<Article>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteArticleAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task DeleteArticleAsync__Searching_Article_not_found__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.DeleteAsync<Article>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteArticleAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching Article not found");
        }

        [Test]
        public async Task DeleteArticleAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            _repoMock.Setup(r => r.DeleteAsync<Article>(id)).ThrowsAsync(new ArgumentException());
            var service = new ArticleDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteArticleAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because searching Article not found");
        }

        [Test]
        public async Task DeleteArticleAsync__Delete_successful__Resources_length_should_be_less_by_1()
        {
            string id;
            int expectedLength;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Articles.Count() - 1;
                    Article articleToBeDeleted = context.Articles.First();
                    id = articleToBeDeleted.Id;
                    _repoMock.Setup(r => r.DeleteAsync<Article>(id)).Callback(() => context.Articles.Remove(articleToBeDeleted));
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
                    await service.DeleteArticleAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteArticleAsync__Delete_successful__Resources_should_not_contain_deleted_Article()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    Article articleToBeDeleted = context.Articles.First();
                    id = articleToBeDeleted.Id;
                    _repoMock.Setup(r => r.DeleteAsync<Article>(id)).Callback(() => context.Articles.Remove(articleToBeDeleted));
                    var service = new ArticleDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.DeleteArticleAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
