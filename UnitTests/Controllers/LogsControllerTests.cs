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

using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace UnitTests.Controllers
{
    public class LogsControllerTests
    {
        private Mock<IActivityLogDbService> _logDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<LogsController> _logger;
        private ActivityLog _validActivityLog;
        private ActivityLog[] _activityLogs;
        private ActivityLogDto[] _activityLogsDto;


        [OneTimeSetUp]
        public void SetUp()
        {
            _logDbServiceMock = new Mock<IActivityLogDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<LogsController>>();
            _validActivityLog = new ActivityLog { Id = "1", User = "user", Type = ActivityLog.ActivityType.CreateResource, Description = "log message" };

            _activityLogs = new ActivityLog[]
            {
                _validActivityLog,
                new ActivityLog { Id = "2", User = "user2", Type = ActivityLog.ActivityType.DeleteResource, Description = "log message 2" }
            };

            _activityLogsDto = new ActivityLogDto[]
            {
                new ActivityLogDto { Id = "1", User = "user1", Type = ActivityLog.ActivityType.CreateResource, Description = "log message " },
                new ActivityLogDto { Id = "2", User = "user2", Type = ActivityLog.ActivityType.DeleteResource, Description = "log message 2" }
            };
        }


        #region GetLogAsync(string id);
        // brak elementu o podanym id/pusty zasob -> 404 not found
        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
        // znaleziono -> 200 ok
        // jakikolwiek error -> response zawiera error
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetLogAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _logDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetLogAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetLogAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _logDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetLogAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetLogAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _logDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetLogAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetLogAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _logDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetLogAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetLogAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _logDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validActivityLog);
            _mapperMock.Setup(x => x.Map<ActivityLogDto>(It.IsNotNull<ActivityLog>())).Returns(new ActivityLogDto { Id = id, Description = "new mapped ActivityLog" });
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetLogAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region GetAllLogsAsync();
        // pusty zasob -> 200 ok, pusta lista
        // znalazlo min 1 element -> 200 ok i niepusta lista
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetLogsAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _logDbServiceMock.Setup(x => x.GetWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetLogsAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetLogsAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _logDbServiceMock.Setup(x => x.GetWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetLogsAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetLogsAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _logDbServiceMock.Setup(x => x.GetWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Enumerable.Empty<ActivityLog>());
            _mapperMock.Setup(x => x.Map<IEnumerable<ActivityLogDto>>(It.IsNotNull<IEnumerable<ActivityLog>>())).Returns(Enumerable.Empty<ActivityLogDto>());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetLogsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<ActivityLogDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetLogsAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _logDbServiceMock.Setup(x => x.GetWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_activityLogs);
            _mapperMock.Setup(x => x.Map<IEnumerable<ActivityLogDto>>(It.IsNotNull<IEnumerable<ActivityLog>>())).Returns(_activityLogsDto.AsEnumerable());
            var controller = new LogsController(_logDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetLogsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<ActivityLogDto>).Should().NotBeEmpty();
        }

        #endregion
    }
}
