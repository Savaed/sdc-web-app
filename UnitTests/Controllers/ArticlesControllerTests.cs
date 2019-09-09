using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SDCWebApp.ApiErrors;
using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ArticlesControllerTests
    {
        private Mock<IArticleDbService> _articleDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<ArticlesController> _logger;
        private Article _validArticle;
        private Article[] _articles;
        private ArticleDto[] _articleDtos;


        [OneTimeSetUp]
        public void SetUp()
        {
            _articleDbServiceMock = new Mock<IArticleDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<ArticlesController>>();

            _validArticle = new Article
            {
                Id = "1",
                Title = "title",
                Text = "text",
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            };

            _articleDtos = new ArticleDto[]
            {
                new ArticleDto
                {
                    Id = "1",
                    Title = "title dto",
                    Text = "text",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 },
                 new ArticleDto
                 {
                     Id = "2",
                     Title = "title dto",
                     Text = "text",
                     UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 }
            };

            _articles = new Article[]
            {
                 new Article
                 {
                    Id = "1",
                    Title = "title",
                    Text = "text",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 },
                 new Article
                 {
                    Id = "1",
                    Title = "title",
                    Text = "text",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 }
            };
        }


        #region GetArticleAsync(string id);
        // brak elementu o podanym id/pusty zasob -> 404 not found
        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
        // znaleziono -> 200 ok
        // jakikolwiek error -> response zawiera error
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetArticleAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _articleDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetArticleAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetArticleAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _articleDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetArticleAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetArticleAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _articleDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetArticleAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetArticleAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _articleDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetArticleAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetArticleAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _articleDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validArticle);
            _mapperMock.Setup(x => x.Map<ArticleDto>(It.IsNotNull<Article>())).Returns(new ArticleDto { Id = id, Title = "new mapped Article" });
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetArticleAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion

        #region GetAllArticlesAsync();
        // pusty zasob -> 200 ok, pusta lista
        // znalazlo min 1 element -> 200 ok i niepusta lista
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetAllArticleAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _articleDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllArticlesAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllArticleAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _articleDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllArticlesAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllArticleAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _articleDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Article>());
            _mapperMock.Setup(x => x.Map<IEnumerable<ArticleDto>>(It.IsNotNull<IEnumerable<Article>>())).Returns(Enumerable.Empty<ArticleDto>());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllArticlesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<ArticleDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllArticleAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _articleDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_articles);
            _mapperMock.Setup(x => x.Map<IEnumerable<ArticleDto>>(It.IsNotNull<IEnumerable<Article>>())).Returns(_articleDtos.AsEnumerable());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllArticlesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<ArticleDto>).Should().NotBeEmpty();
        }

        #endregion

        #region AddArticleAsync(Article Article);
        // istnieje juz taki sam cennik w bazie -> 400 bad request instance already exists, details 'objname', 'id'
        // Article jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // jakies dane cenniku sa niepoprawne -> 400 bad request i info co jest nie tak
        // dodano poprawnie -> 201 created, zwraca dodany element
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task AddArticleAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _articleDbServiceMock.Setup(x => x.AddAsync(It.IsAny<Article>())).ThrowsAsync(new InternalDbServiceException());
            _articleDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<Article>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);


            Func<Task> result = async () => await controller.AddArticleAsync(_articleDtos[0]);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task AddArticleAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _articleDbServiceMock.Setup(x => x.AddAsync(It.IsAny<Article>())).ThrowsAsync(new Exception());
            _articleDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<Article>())).ThrowsAsync(new Exception());

            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.AddArticleAsync(_articleDtos[0]);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task AddArticleAsync__Already_there_is_the_same_element_in_database__Should_return_400BadRequest_response()
        {
            var ArticleDto = CreateArticleDto(_articles[0]);
            _mapperMock.Setup(x => x.Map<Article>(ArticleDto)).Returns(_articles[0]);
            _articleDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<Article>())).ThrowsAsync(new InvalidOperationException());
            _articleDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsNotNull<Article>())).ThrowsAsync(new InvalidOperationException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddArticleAsync(ArticleDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task AddArticleAsync__Add_succeeded__Should_return_200OK_response_with_added_element()
        {
            var validArticle = new Article { Id = "12312321321321", Title = "Valid title" };
            var validArticleDto = CreateArticleDto(validArticle);
            _mapperMock.Setup(x => x.Map<Article>(It.IsNotNull<ArticleDto>())).Returns(validArticle);
            _mapperMock.Setup(x => x.Map<ArticleDto>(It.IsNotNull<Article>())).Returns(validArticleDto);
            _articleDbServiceMock.Setup(x => x.AddAsync(validArticle)).ReturnsAsync(validArticle);
            _articleDbServiceMock.Setup(x => x.RestrictedAddAsync(validArticle)).ReturnsAsync(validArticle);

            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddArticleAsync(validArticleDto);

            (result as ObjectResult).StatusCode.Should().Be(201);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion

        #region UpdateArticleAsync(string id, Article Article);
        // nie ma takiego elementu o podanym id -> 404 not found
        // id w url i id w body sie nie zgadzaja -> 400 bad request, mismatch
        // id / Article jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // update poprawny -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task UpdateArticleAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _articleDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Article>())).ThrowsAsync(new InternalDbServiceException());
            _articleDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<Article>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateArticleAsync(_articleDtos[0].Id, _articleDtos[0]);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task UpdateArticleAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _articleDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Article>())).ThrowsAsync(new Exception());
            _articleDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<Article>())).ThrowsAsync(new Exception());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateArticleAsync(_articleDtos[0].Id, _articleDtos[0]);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task UpdateArticleAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            var validArticle = new Article { Id = "12312321321321", Title = "Valid Title" };
            var validArticleDto = CreateArticleDto(validArticle);
            _mapperMock.Setup(x => x.Map<Article>(It.IsNotNull<ArticleDto>())).Returns(validArticle);
            _mapperMock.Setup(x => x.Map<ArticleDto>(It.IsNotNull<Article>())).Returns(validArticleDto);
            _articleDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<Article>())).ThrowsAsync(new InvalidOperationException());
            _articleDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsNotNull<Article>())).ThrowsAsync(new InvalidOperationException());

            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateArticleAsync(validArticleDto.Id, validArticleDto);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateArticleAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            var articleDto = new ArticleDto { Id = id, Title = "Test dto Title" };
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateArticleAsync(id, articleDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateArticleAsync__Argument_id_and_id_property_in_element_to_be_updated_mismatches__Should_return_400BadRequest_response()
        {
            var articleDto = new ArticleDto { Id = "1", Title = "Test dto Title" };
            string id = articleDto.Id + "_mismatched_id";
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateArticleAsync(id, articleDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateArticleAsync__Update_succeeded__Should_return_200OK_response_with_updated_element()
        {
            var validArticle = new Article { Id = "12312321321321", Title = "Valid Title" };
            var validArticleDto = CreateArticleDto(validArticle);
            _mapperMock.Setup(x => x.Map<Article>(It.IsNotNull<ArticleDto>())).Returns(validArticle);
            _mapperMock.Setup(x => x.Map<ArticleDto>(It.IsNotNull<Article>())).Returns(validArticleDto);
            _articleDbServiceMock.Setup(x => x.UpdateAsync(validArticle)).ReturnsAsync(validArticle);
            _articleDbServiceMock.Setup(x => x.RestrictedUpdateAsync(validArticle)).ReturnsAsync(validArticle);

            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateArticleAsync(validArticleDto.Id, validArticleDto);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(validArticleDto);
        }

        #endregion

        #region DeleteArticleAsync(string id);
        // nie ma takiego elementu o podanym id -> 404 not found
        // argument jest pusty albo null -> 400 bad request
        // usunieto poprawnie -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task DeleteArticleAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _articleDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteArticleAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task DeleteArticleAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _articleDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteArticleAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task DeleteArticleAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            string id = "-1";
            _articleDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteArticleAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteArticleAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _articleDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteArticleAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteArticleAsync__Delete_succeeded__Should_return_200OK_response()
        {
            _articleDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
            var controller = new ArticlesController(_articleDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteArticleAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
        }

        #endregion


        #region Privates
        private ArticleDto CreateArticleDto(Article article)
        {
            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text               
            };
        }

        #endregion
    }


}

