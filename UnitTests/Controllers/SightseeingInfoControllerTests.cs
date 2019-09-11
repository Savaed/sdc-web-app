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
using UnitTests.Helpers;

using SDCWebApp.ApiErrors;
using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class SightseeingInfoControllerTests
    {
        private Mock<IGeneralSightseeingInfoDbService> _infoDbServiceMock;
        private ILogger<SightseeingInfoController> _logger;
        private Mock<IMapper> _mapperMock;
        private SightseeingInfoDto[] _infoDtos;
        private GeneralSightseeingInfo _info;
        private SightseeingInfoDto _infoDto;


        [OneTimeSetUp]
        public void SetUp()
        {
            _infoDbServiceMock = new Mock<IGeneralSightseeingInfoDbService>();
            _logger = Mock.Of<ILogger<SightseeingInfoController>>();
            _mapperMock = new Mock<IMapper>();
            _info = Helpers.CreateModel.CreateInfo();
            _infoDto = CreateInfoDto(_info);
            _infoDtos = new SightseeingInfoDto[] { _infoDto };
        }


        #region GetInfoAsync(string id);
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Specified SightseeingInfo not found -> 404NotFound
        // Argument id is null or empty -> 400BadRequest
        // GeneralSightseeingInfo found -> 200Ok, return this SightseeingGroup        

        [Test]
        public async Task GetInfoAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _infoDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetInfoAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetInfoAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _infoDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetInfoAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetInfoAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _infoDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException()).Verifiable();
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetInfoAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetInfoAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _infoDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetInfoAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetInfoAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _infoDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_info);
            _mapperMock.Setup(x => x.Map<SightseeingInfoDto>(It.IsNotNull<GeneralSightseeingInfo>())).Returns(_infoDtos[0]);
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetInfoAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region GetAllInfoAsync();
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Resource is empty -> 200OK, return empty IEnumerable
        // At least one SightseeingGroup found -> 200OK, return not empty IEnumerable 

        [Test]
        public async Task GetAllinfoAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllInfoAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllinfoAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllInfoAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllinfoAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<GeneralSightseeingInfo>());
            _mapperMock.Setup(x => x.Map<IEnumerable<SightseeingInfoDto>>(It.IsNotNull<IEnumerable<GeneralSightseeingInfo>>())).Returns(Enumerable.Empty<SightseeingInfoDto>());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllInfoAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingInfoDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllinfoAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new GeneralSightseeingInfo[] { _info });
            _mapperMock.Setup(x => x.Map<IEnumerable<SightseeingInfoDto>>(It.IsNotNull<IEnumerable<GeneralSightseeingInfo>>())).Returns(_infoDtos.AsEnumerable());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllInfoAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingInfoDto>).Should().NotBeEmpty();
        }

        #endregion


        #region AddInfoAsync(GeneralSightseeingInfo info);
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Already there is the same info -> return 400BadRequest
        // Add succeeded -> return 200OK, with added info

        [Test]
        public async Task AddInfoAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _infoDbServiceMock.Setup(x => x.AddAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new InternalDbServiceException());
            _infoDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);
            var infoDto = CreateInfoDto(CreateModel.CreateInfo());

            Func<Task> result = async () => await controller.AddInfoAsync(infoDto);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task AddInfoAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _infoDbServiceMock.Setup(x => x.AddAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new Exception());
            _infoDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new Exception());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);
            var infoDto = CreateInfoDto(CreateModel.CreateInfo());

            Func<Task> result = async () => await controller.AddInfoAsync(infoDto);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task AddInfoAsync__Already_there_is_the_same_element_in_database__Should_return_400BadRequest_response()
        {
            _mapperMock.Setup(x => x.Map<GeneralSightseeingInfo>(_infoDto)).Returns(_info);
            _infoDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<GeneralSightseeingInfo>())).ThrowsAsync(new InvalidOperationException());
            _infoDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsNotNull<GeneralSightseeingInfo>())).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);
            var infoDto = CreateInfoDto(CreateModel.CreateInfo());

            var result = await controller.AddInfoAsync(infoDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task AddInfoAsync__Add_succeeded__Should_return_201OK_response_with_added_element()
        {
            _mapperMock.Setup(x => x.Map<GeneralSightseeingInfo>(It.IsNotNull<SightseeingInfoDto>())).Returns(_info);
            _mapperMock.Setup(x => x.Map<SightseeingInfoDto>(It.IsNotNull<GeneralSightseeingInfo>())).Returns(_infoDto);
            _infoDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<GeneralSightseeingInfo>())).ReturnsAsync(_info);
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);
            var infoDto = CreateInfoDto(CreateModel.CreateInfo());

            var result = await controller.AddInfoAsync(infoDto);

            (result as ObjectResult).StatusCode.Should().Be(201);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
        }

        #endregion


        #region UpdateInfoAsync(string id, GeneralSightseeingInfo info);
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Update succeeded -> return 200OK, with updated info
        // Info to be updated not found -> 404NotFound
        // Id is null or empty -> 400BadRequest
        // Id and info.Id mismatches -> 400BadRequest
        
        [Test]
        public async Task UpdateInfoAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _mapperMock.Setup(x => x.Map<GeneralSightseeingInfo>(It.IsNotNull<SightseeingInfoDto>())).Returns(_info);
            _mapperMock.Setup(x => x.Map<SightseeingInfoDto>(It.IsNotNull<GeneralSightseeingInfo>())).Returns(_infoDto);
            _infoDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<GeneralSightseeingInfo>())).ThrowsAsync(new InvalidOperationException());
            _infoDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsNotNull<GeneralSightseeingInfo>())).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateInfoAsync(_infoDto.Id, _infoDto);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task UpdateInfoAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _infoDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new InternalDbServiceException());
            _infoDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new InternalDbServiceException());
            _mapperMock.Setup(x => x.Map<GeneralSightseeingInfo>(It.IsNotNull<SightseeingInfoDto>())).Returns(_info);
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateInfoAsync(_infoDto.Id, _infoDto);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task UpdateInfoAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _infoDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new Exception());
            _infoDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<GeneralSightseeingInfo>())).ThrowsAsync(new Exception());
            _mapperMock.Setup(x => x.Map<GeneralSightseeingInfo>(It.IsNotNull<SightseeingInfoDto>())).Returns(_info);
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateInfoAsync(_infoDto.Id, _infoDto);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task UpdateInfoAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);
            var infoDto = CreateInfoDto(CreateModel.CreateInfo());
            infoDto.Id = id;

            var result = await controller.UpdateInfoAsync(id, infoDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateInfoAsync__Argument_id_and_id_property_in_element_to_be_updated_mismatches__Should_return_400BadRequest_response()
        {
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);
            var infoDto = _infoDtos[0];
            string id = infoDto.Id + "_mismatched_id";

            var result = await controller.UpdateInfoAsync(id, infoDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateInfoAsync__Update_succeeded__Should_return_200OK_response_with_updated_element()
        {
            _mapperMock.Setup(x => x.Map<GeneralSightseeingInfo>(It.IsNotNull<SightseeingInfoDto>())).Returns(_info);
            _mapperMock.Setup(x => x.Map<SightseeingInfoDto>(It.IsNotNull<GeneralSightseeingInfo>())).Returns(_infoDto);
            _infoDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<GeneralSightseeingInfo>())).ReturnsAsync(_info);
            _infoDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsNotNull<GeneralSightseeingInfo>())).ReturnsAsync(_info);
            _mapperMock.Setup(x => x.Map<SightseeingInfoDto>(It.IsNotNull<GeneralSightseeingInfo>())).Returns(_infoDto);
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateInfoAsync(_infoDto.Id, _infoDto);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(_infoDto);
        }

        #endregion


        #region DeleteInfoAsync(string id)
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Info to be deleted not found -> 404NotFound
        // Id is null or empty -> 400BadRequest
        // Delete succeeded -> return 200OK

        [Test]
        public async Task DeleteInfoAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _infoDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteInfoAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task DeleteInfoAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _infoDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteInfoAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task DeleteInfoAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            string id = "-1";
            _infoDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteInfoAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteInfoAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _infoDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteInfoAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteInfoAsync__Delete_succeeded__Should_return_200OK_empty_response()
        {
            var emptyResponse = new ResponseWrapper();
            _infoDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
            var controller = new SightseeingInfoController(_infoDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteInfoAsync("123");

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Should().BeEquivalentTo(emptyResponse);
        }

        #endregion


        #region Privates

        private SightseeingInfoDto CreateInfoDto(GeneralSightseeingInfo info)
        {
            var infoDto = new SightseeingInfoDto
            {
                Id = info.Id,
                Description = info.Description,
                MaxAllowedGroupSize = info.MaxAllowedGroupSize,
                MaxChildAge = info.MaxChildAge,
                MaxTicketOrderInterval = info.MaxTicketOrderInterval,
                SightseeingDuration = info.SightseeingDuration,
                OpeningHours = new OpeningHoursDto[] { }
            };

            foreach (var openingHour in info.OpeningHours)
            {
                infoDto.OpeningHours.ToList().Add(new OpeningHoursDto
                {
                    OpeningHour = openingHour.OpeningHour,
                    ClosingHour = openingHour.ClosingHour,
                    DayOfWeek = openingHour.DayOfWeek
                });
            }

            return infoDto;
        }

        #endregion

    }
}
