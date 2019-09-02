using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Models;
using SDCWebApp.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class ArticleDbServiceTests
    {
        private static readonly Article[] _articleForRestrictedUpdateCases = new Article[]
        {
            new Article{ ConcurrencyToken = Encoding.ASCII.GetBytes("Updated ConcurrencyToken") },    // Attempt to change 'ConcurrencyToken' which is read-only property.
            new Article{ UpdatedAt = DateTime.Now.AddYears(100) }                                     // Attempt to change 'UpdatedAt' which is read-only property.
        };
        private Mock<ApplicationDbContext> _dbContextMock;
        private ILogger<ArticleDbService> _logger;
        private readonly Article _validArticle = new Article
        {
            Id = "1",
            Author = "Mr Test",
            Text = "TestTestTest",
            Title = "Id TDD worth?",
            UpdatedAt = DateTime.Now.AddDays(-1)
        };

        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext)));
            _logger = Mock.Of<ILogger<ArticleDbService>>();
        }


        #region GetAsync(string id)
        // nie istnieje tabela -> internal ex
        // tabela jest nullem -> internal ex
        // id jest nullem/pusty -> arg exc
        // 0 znaleziono -> invalid oper exc
        // zasob jest pusty -> invalid oper exc
        // znalazlo -> zwraca tylko jeden element

        [Test]
        public async Task GetAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_doesnt_exit__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new ArticleDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetAsync(id);

            await action.Should().ThrowExactlyAsync<ArgumentException>("Because id cannot be null or empty string.");
        }

        [Test]
        public async Task GetAsync__Found_zero_matching_Article__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because Article not found.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.RemoveRange(await context.Articles.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of Article.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Article_found__Should_return_this_article()
        {
            Article expectedArticle;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedArticle = await context.Articles.FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.GetAsync(expectedArticle.Id);
                    result.Should().BeEquivalentTo(expectedArticle);
                }
            }
        }

        #endregion


        #region GetAllAsync()
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // znalazlo -> ienum<Article> dla wszystkich z zasobu

        [Test]
        public async Task GetAllAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_doesnt_exit__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_is_empty__Should_return_empty_IEnumerable()
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
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetAllAsync__All_Articles_found__Should_return_IEnumerable_for_all_ticket_groups()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Articles.ToArray().Length;
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        #endregion


        #region Add(Article Article)
        // zasob nie istnieje -> exc z msg
        // zasob jest nullem -> null ref exc
        // problem z zapisaniem zmian -> inter | Nie mam pojecia jak to przetestowac xD
        // arg jest nullem -> arg null exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

        [Test]
        public async Task AddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task AddAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new ArticleDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.AddAsync(null as Article);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Article' is null.");
        }


        [Test]
        public async Task AddAsync__In_resource_exists_the_same_Article_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Add(_validArticle);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    // Testing method
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validArticle);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same Article as this one to be added.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Should_return_added_article()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.AddAsync(_validArticle);

                    result.Should().BeEquivalentTo(_validArticle);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Articles.Count() + 1;
                    var service = new ArticleDbService(context, _logger);

                    await service.AddAsync(_validArticle);

                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_contains_added_article()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    await service.AddAsync(_validArticle);

                    context.Articles.Contains(_validArticle).Should().BeTrue();
                }
            }
        }

        #endregion

        #region RestrictedAddAsync(Article Article)
        // zasob nie istnieje -> exc z msg
        // zasob jest nullem -> null ref exc
        // problem z zapisaniem zmian -> inter | Nie mam pojecia jak to przetestowac xD
        // arg jest nullem -> arg null exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac (takie samo id, wartosci ) -> invalid oper exc
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet
        // proba doda

        [Test]
        public async Task RestrictedAddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedAddAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedAddAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new ArticleDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.RestrictedAddAsync(null as Article);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'article' is null.");
        }


        [Test]
        public async Task RestrictedAddAsync__In_resource_exists_article_with_the_same_id_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Add(_validArticle);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validArticle.Title = "changed title";
                    _validArticle.Text = "changed text";
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedAddAsync(_validArticle);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same Article as this one to be added.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Attempt_to_add_entity_with_the_same_values_as_existing_one_but_with_different_id__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Add(_validArticle);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validArticle.Id += "_changed_id";
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedAddAsync(_validArticle);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because this method DOES NOT allow to add a new entity with the same properties value (Title, Text, Author) " +
                        "but different 'Id'. It is intentional behaviour.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Should_return_added_article()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedAddAsync(_validArticle);

                    result.Should().BeEquivalentTo(_validArticle);
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Articles.Count() + 1;
                    var service = new ArticleDbService(context, _logger);

                    await service.RestrictedAddAsync(_validArticle);

                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Resource_contains_added_article()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    await service.RestrictedAddAsync(_validArticle);

                    context.Articles.Contains(_validArticle).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateAsync(Article tariff)
        // arg jest nullem -> arg null exc
        // arg ma id ktore jest nullem albo pusty -> arg exc   
        // zasob jest nullem -> inter exc
        // zasob nie istnieje -> inter exc
        // zasob jest pusty -> invalid oper exc
        // nie znaleziono podanego biletu -> invalid oper exc
        // update udal sie -> tyle samo biletow co przed operacja 
        // update sie udal -> w zasobie istnieje zmodyfikowany bilet
        // update sie udal -> w zasobie nie istnieje poprzednia wersja biletu
        // update sie udal -> zwraca zmodyfikowany bilet
        // update sie udal -> zmieniona data modyfikacji

        [Test]
        public async Task UpdateAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(null as Article);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Article' cannot be null.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidArticle = new Article { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(invalidArticle);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Article' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    articleBeforUpdate = await context.Articles.FirstAsync();
                    article = articleBeforUpdate.Clone() as Article;
                    context.Articles.RemoveRange(await context.Articles.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(article);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Matching_Article_not_found__Should_throw_InvalidOperationException()
        {
            Article article = new Article
            {
                Id = "0",
                Text = "Sample text for only this test.",
                Author = "Joe Doe",
                Title = "SJKC",
                UpdatedAt = DateTime.Now.AddHours(-3)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(article);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching article not found.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Should_return_updated_article()
        {
            Article articleBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    articleBeforUpdate = await context.Articles.FirstAsync();
                    Article article = articleBeforUpdate;
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.UpdateAsync(article);

                    result.Should().BeEquivalentTo(article);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_contains_updated_article()
        {
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.UpdateAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Contains(article).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_article()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.UpdateAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Single(x => x == article).Should().NotBeSameAs(articleBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_length_should_be_unchanged()
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
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);
                    expectedLength = await context.Articles.CountAsync();

                    await service.UpdateAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MaxValue()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    articleBeforUpdate.UpdatedAt = DateTime.MinValue;
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.UpdateAsync(article);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    articleBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.UpdateAsync(article);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)articleBeforUpdate.UpdatedAt);
                }
            }
        }

        #endregion


        #region RestrictedUpdateAsync(Article article)
        // wszystko co w normalnym update
        // proba zmian readonly properties -> metoda niczego nie zmieni i zaloguje warny


        [Test]
        public async Task RestrictedUpdateAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedUpdateAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedUpdateAsync(_validArticle);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(null as Article);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Article' cannot be null.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidArticle = new Article { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(invalidArticle);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Article' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            Article ArticleBeforUpdate;
            Article Article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ArticleBeforUpdate = await context.Articles.FirstAsync();
                    Article = ArticleBeforUpdate.Clone() as Article;
                    context.Articles.RemoveRange(await context.Articles.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    Article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(Article);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because esource is empty.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Matching_Article_not_found__Should_throw_InvalidOperationException()
        {
            Article Article = new Article
            {
                Id = "0",
                Title = "Sample title for only this test.",
                UpdatedAt = DateTime.Now.AddHours(-3)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    // In db does not matching Article to belowe disount.
                    Func<Task> result = async () => await service.RestrictedUpdateAsync(Article);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching Article not found.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Should_return_updated_sightseeing_tariff()
        {
            Article articleBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    articleBeforUpdate = await context.Articles.FirstAsync();
                    Article article = articleBeforUpdate;
                    article.Title = "Changed name.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(article);

                    result.Should().BeEquivalentTo(article);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_should_contains_updated_sightseeing_tariff()
        {
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Contains(article).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_sightseeing_tariff()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Single(x => x == article).Should().NotBeSameAs(articleBeforUpdate);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_length_should_be_unchanged()
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
                    article.Title = "Changed title.";
                    var service = new ArticleDbService(context, _logger);
                    expectedLength = await context.Articles.CountAsync();

                    await service.RestrictedUpdateAsync(article);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MaxValue()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    articleBeforUpdate.UpdatedAt = DateTime.MinValue;
                    article.Title = "Changed title";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(article);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            Article articleBeforUpdate;
            Article article;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    article = await context.Articles.FirstAsync();
                    articleBeforUpdate = article.Clone() as Article;
                    articleBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    article.Title = "Changed title";
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(article);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)articleBeforUpdate.UpdatedAt);
                }
            }
        }

        [TestCaseSource(nameof(_articleForRestrictedUpdateCases))]
        public async Task RestrictedUpdateAsync__Attempt_to_update_readonly_properties__These_changes_will_be_ignored(Article updatedArticleCase)
        {
            // In fact, any read-only property changes will be ignored and no exception will be thrown, but the method will log any of these changes as warning.

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var articleBeforeUpdate = await context.Articles.FirstAsync();
                    updatedArticleCase.Id = articleBeforeUpdate.Id;
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(updatedArticleCase);

                    // Those properties should be unchanged since they are readonly.
                    result.CreatedAt.Should().BeSameDateAs(articleBeforeUpdate.CreatedAt);
                    result.ConcurrencyToken.Should().BeSameAs(articleBeforeUpdate.ConcurrencyToken);
                }
            }
        }
        #endregion


        #region DeleteAsync(string id)
        // zasob jest nullem -> internal
        // zasob nie istnieje -> intern
        // id jest nullem albo pusty -> arg exc
        // zasob jest pusty -> invalid oper exc
        // nie ma takiego biletu jak podany -> invalid oper exc
        // usuwanie sie udalo -> nie w zasobie usunietego biletu
        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

        [Test]
        public async Task DeleteAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new ArticleDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.DeleteAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task DeleteAsync__Resource_is_empty__Should_throw_InvalidOperationException()
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
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> result = async () => await service.DeleteAsync("1");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot delete any element.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Article_to_be_deleted_not_found__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    // Only positive Id is valid so there is not Id = '-100'.
                    Func<Task> result = async () => await service.DeleteAsync("-100");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because Article to be deleted not found.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_length_should_be_less_by_1()
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
                    var service = new ArticleDbService(context, _logger);
                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_should_not_contain_deleted_article()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    Article articleToBeDeleted = context.Articles.First();
                    id = articleToBeDeleted.Id;
                    var service = new ArticleDbService(context, _logger);

                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
                }
            }
        }

        #endregion


        #region GetWithPagination(int pageNumber, int pageSize)
        // zasob jest nullem -> internal
        // zasob nie istnieje -> internal
        // page number/ size jest nullem -> argument null exc
        // page nie moze byc < 1 -> arg out of range
        // size nie moze byc < 0 -> arg out of range
        // przypadek: jest 201 elementow, bierzemy nr 3 i size 100 el, to skip 200 i 1 el sie wyswietli -> zwracamy liste z tym jednym elementem
        // jest 200 ele nr jest 3, size 100 -> pusta lista
        // tabela jest pusta -> jw

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Articles = null as DbSet<Article>;
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop Articles table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Articles]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Article. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_number_is_less_than_1__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new ArticleDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(0, 10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page number cannot be less than 1.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_size_is_negative__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new ArticleDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(3, -10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page size cannot be negative number.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Number_of_elements_on_single_page_is_less_than_page_size__Should_return_all_these_elements()
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
                    for (int i = 0; i < 10; i++)
                    {
                        await context.Articles.AddAsync(new Article { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    // Page will be first with 6 elements and second with 4 elements. Second page will be return.
                    var result = await service.GetWithPaginationAsync(2, 6);

                    // More generic solution is: Count() should be count of elements in resource % pageSize.
                    result.Count().Should().Be(4);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__There_are_not_elements_on_single_page__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.Articles.AddAsync(new Article { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ArticleDbService(context, _logger);

                    // In this case is only 2 pages with any data.
                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_empty__Should_return_empty_IEnumerable()
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
                    var service = new ArticleDbService(context, _logger);

                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
