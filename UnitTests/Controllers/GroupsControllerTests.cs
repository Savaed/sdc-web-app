using Autofac.Features.Indexed;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.ApiErrors;
using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class GroupsControllerTests
    {
        private Mock<IIndex<string, IServiceBase>> _dbServiceFactoryMock;
        private Mock<ISightseeingGroupDbService> _groupDbServiceMock;
        private Mock<IVisitInfoDbService> _infoDbServiceMock;
        private ILogger<GroupsController> _logger;
        private Mock<IMapper> _mapperMock;
        private SightseeingGroup _validSightseeingGroup;
        private SightseeingGroupDto _validSightseeingGroupDto;
        private VisitInfo _info;


        [OneTimeSetUp]
        public void SetUp()
        {
            _groupDbServiceMock = new Mock<ISightseeingGroupDbService>();
            _infoDbServiceMock = new Mock<IVisitInfoDbService>();

            _dbServiceFactoryMock = new Mock<IIndex<string, IServiceBase>>();
            _dbServiceFactoryMock.Setup(x => x["ISightseeingGroupDbService"]).Returns(_groupDbServiceMock.Object);
            _dbServiceFactoryMock.Setup(x => x["IVisitInfoDbService"]).Returns(_infoDbServiceMock.Object);

            _validSightseeingGroup = new SightseeingGroup
            {
                Id = "1",
                MaxGroupSize = 30,
                SightseeingDate = new DateTime(2019, 12, 12, 12, 0, 0)
            };
            _validSightseeingGroupDto = new SightseeingGroupDto
            {
                Id = "1",
                MaxGroupSize = 30,
                SightseeingDate = new DateTime(2019, 12, 12, 12, 0, 0),
                IsAvailablePlace = true,
                CurrentGroupSize = 20
            };

            _info = CreateModel.CreateInfo();

            _logger = Mock.Of<ILogger<GroupsController>>();

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map<SightseeingGroupDto>(It.IsAny<SightseeingGroup>())).Returns(_validSightseeingGroupDto);
            _mapperMock.Setup(x => x.Map<SightseeingGroup>(It.IsAny<SightseeingGroupDto>())).Returns(_validSightseeingGroup);
        }


        #region GetAllGroupsAsync()
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Resource is empty -> return empty IEnumerable
        // At least one SightseeingGroup found -> 200Ok, return not empty IEnumerable 

        [Test]
        public async Task GetAllGroupsAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _groupDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllGroupsAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllGroupsAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _groupDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllGroupsAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllGroupsAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _groupDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<SightseeingGroup>());
            _mapperMock.Setup(x => x.Map<IEnumerable<SightseeingGroupDto>>(It.IsNotNull<IEnumerable<SightseeingGroup>>())).Returns(Enumerable.Empty<SightseeingGroupDto>());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllGroupsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingGroupDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllGroupsAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _groupDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new SightseeingGroup[] { _validSightseeingGroup }.AsEnumerable());
            _mapperMock.Setup(x => x.Map<IEnumerable<SightseeingGroupDto>>(It.IsNotNull<IEnumerable<SightseeingGroup>>())).Returns(new SightseeingGroupDto[] { _validSightseeingGroupDto }.AsEnumerable());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllGroupsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingGroupDto>).Should().NotBeEmpty();
        }

        #endregion


        #region GetGroupAsync(string id)
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Specified SightseeingGroup not found -> 404NotFound
        // Argument id is null or empty -> 400BadRequest
        // SightseeingGroup found -> 200Ok, return this SightseeingGroup 

        [Test]
        public async Task GetGroupAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.
            _groupDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetGroupAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetGroupAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _groupDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetGroupAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetGroupAsync__Element_not_found__Should_return_404NotFound_response()
        {
            _groupDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetGroupAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetGroupAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _groupDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetGroupAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetGroupAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _groupDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validSightseeingGroup);
            _mapperMock.Setup(x => x.Map<SightseeingGroupDto>(It.IsNotNull<SightseeingGroup>())).Returns(_validSightseeingGroupDto);
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetGroupAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(_validSightseeingGroupDto);
        }

        #endregion


        #region GetAvailableGroupDatesAsync()
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Resource is empty -> return empty IEnumerable
        // At least one SightseeingGroup found -> 200Ok, return not empty IEnumerable 
        // There is sightseeing group in the db with no available places -> not return date of this group

        [Test]
        public async Task GetAvailableGroupDatesAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ThrowsAsync(new InternalDbServiceException());
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAvailableGroupDatesAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAvailableGroupDatesAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ThrowsAsync(new Exception());
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAvailableGroupDatesAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAvailableGroupDatesAsync__Resource_is_empty__Should_return_200OK_response_with__not_empty_IEnumerable()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new VisitInfo[] { _info }.AsEnumerable());
            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(Enumerable.Empty<SightseeingGroup>());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAvailableGroupDatesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<GroupInfo>).Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAvailableGroupDatesAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new VisitInfo[] { _info }.AsEnumerable());
            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(new SightseeingGroup[] { _validSightseeingGroup }.AsEnumerable());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAvailableGroupDatesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<GroupInfo>).Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAvailableGroupDatesAsync__There_is_group_without_available_places__Returned_IEnumerable_should_not_contain_date_of_this_group()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new VisitInfo[] { _info }.AsEnumerable());
            var notAvailableGroup = new SightseeingGroup { Id = "2", SightseeingDate = DateTime.Now.AddDays(1), MaxGroupSize = 1, Tickets = new Ticket[] { new Ticket { Id = "only_for_this_test" } } };
            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>()))
                .ReturnsAsync(new SightseeingGroup[]
                {
                    _validSightseeingGroup,
                    notAvailableGroup
                }.AsEnumerable());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAvailableGroupDatesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<GroupInfo>).Any(x => x.SightseeingDate == notAvailableGroup.SightseeingDate).Should().BeFalse();
        }

        [Test]
        public async Task GetAvailableGroupDatesAsync__Sightseeing_info_not_found__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<VisitInfo>());
            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(new SightseeingGroup[] { _validSightseeingGroup }.AsEnumerable());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAvailableGroupDatesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<GroupInfo>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAvailableGroupDatesAsync__At_least_one_element_found__Should_return_200OK_response_with_distinct_groups()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new VisitInfo[] { _info }.AsEnumerable());
            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(new SightseeingGroup[] { _validSightseeingGroup }.AsEnumerable());
            var controller = new GroupsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAvailableGroupDatesAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            var resultData = ((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<GroupInfo>;

            // Each group should be different each other.
            resultData.ToList().Distinct().Count().Should().Be(resultData.Count());
        }

        #endregion

    }
}
