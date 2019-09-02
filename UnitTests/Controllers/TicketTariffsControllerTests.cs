//using System;
//using System.Collections.Generic;
//using System.Text;
//using NUnit.Framework;
//using FluentAssertions;
//using Moq;
//using SDCWebApp.Services;
//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using SDCWebApp.Models;
//using SDCWebApp.Models.Dtos;
//using SDCWebApp.Controllers;
//using System.Threading.Tasks;

//namespace UnitTests.Controllers
//{
//    [TestFixture]
//    public class TicketTariffsControllerTests
//    {


//        private Mock<ITicketTariffDbService> _TicketTariffDbServiceMock;
//        private Mock<IMapper> _mapperMock;
//        private ILogger<TicketTariffsController> _logger;
//        private TicketTariff _validTicketTariff;
//        private TicketTariff[] _TicketTariffs;
//        private TicketTariffDto[] _TicketTariffDtos;


//        [OneTimeSetUp]
//        public void SetUp()
//        {
//            _TicketTariffDbServiceMock = new Mock<ITicketTariffDbService>();
//            _mapperMock = new Mock<IMapper>();
//            _logger = Mock.Of<ILogger<TicketTariffsController>>();

//            _validTicketTariff = new TicketTariff
//            {
//                Id = "1",
//                Description = "Sample test description",
//                DefaultPrice = 20,
//                UpdatedAt = DateTime.UtcNow.AddDays(-1)
//            };

//            _TicketTariffDtos = new TicketTariffDto[]
//            {
//                 new TicketTariffDto
//                 {
//                    Id = "1",
//                    Description = "This is DTO",
//                    Type = TicketTariff.TicketTariffType.ForChild,
//                    GroupSizeForTicketTariff = 0,
//                    TicketTariffValueInPercentage = 23,
//                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
//                 },
//                 new TicketTariffDto
//                 {
//                    Id = "2",
//                    Description = "This id DTO",
//                    Type = TicketTariff.TicketTariffType.ForChild,
//                    GroupSizeForTicketTariff = 0,
//                    TicketTariffValueInPercentage = 23,
//                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
//                 }
//            };

//            _TicketTariffs = new TicketTariff[]
//            {
//                 new TicketTariff
//                 {
//                    Id = "1",
//                    Description = "This is DTO",
//                    Type = TicketTariff.TicketTariffType.ForChild,
//                    GroupSizeForTicketTariff = 0,
//                    TicketTariffValueInPercentage = 23,
//                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
//                 },
//                 new TicketTariff
//                 {
//                    Id = "2",
//                    Description = "This id DTO",
//                    Type = TicketTariff.TicketTariffType.ForChild,
//                    GroupSizeForTicketTariff = 0,
//                    TicketTariffValueInPercentage = 23,
//                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
//                 }
//            };
//        }


//        #region GetTicketTariffAsync(string id);
//        // brak elementu o podanym id/pusty zasob -> 404 not found
//        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
//        // znaleziono -> 200 ok
//        // jakikolwiek error -> response zawiera error
//        // any internal error refferd to the db occurred -> throws internal db service exc
//        // any unexpected internal error occurred -> throws exc

//        [Test]
//        public async Task GetTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
//        {
//            // Example of these errors: database does not exist, table does not exist etc.

//            _TicketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.GetTicketTariffAsync("1");

//            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
//        }

//        [Test]
//        public async Task GetTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.GetTicketTariffAsync("1");

//            await result.Should().ThrowExactlyAsync<Exception>();
//        }

//        [Test]
//        public async Task GetTicketTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTicketTariffAsync("1");

//            (result as ObjectResult).StatusCode.Should().Be(404);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetTicketTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTicketTariffAsync(id);

//            (result as ObjectResult).StatusCode.Should().Be(400);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetTicketTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
//        {
//            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
//            _TicketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariffDto>(It.IsNotNull<TicketTariff>())).Returns(new TicketTariffDto { Id = id, Description = "new mapped TicketTariff" });
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTicketTariffAsync(id);

//            (result as ObjectResult).StatusCode.Should().Be(200);
//            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
//        }

//        #endregion

//        #region GetAllTicketTariffsAsync();
//        // pusty zasob -> 200 ok, pusta lista
//        // znalazlo min 1 element -> 200 ok i niepusta lista
//        // any internal error refferd to the db occurred -> throws internal db service exc
//        // any unexpected internal error occurred -> throws exc

//        [Test]
//        public async Task GetAllTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
//        {
//            // Example of these errors: database does not exist, table does not exist etc.

//            _TicketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.GetAllTicketTariffsAsync();

//            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
//        }

//        [Test]
//        public async Task GetAllTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.GetAllTicketTariffsAsync();

//            await result.Should().ThrowExactlyAsync<Exception>();
//        }

//        [Test]
//        public async Task GetAllTicketTariffAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<TicketTariff>());
//            _mapperMock.Setup(x => x.Map<IEnumerable<TicketTariffDto>>(It.IsNotNull<IEnumerable<TicketTariff>>())).Returns(Enumerable.Empty<TicketTariffDto>());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetAllTicketTariffsAsync();

//            (result as ObjectResult).StatusCode.Should().Be(200);
//            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketTariffDto>).Should().BeEmpty();
//        }

//        [Test]
//        public async Task GetAllTicketTariffAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_TicketTariffs);
//            _mapperMock.Setup(x => x.Map<IEnumerable<TicketTariffDto>>(It.IsNotNull<IEnumerable<TicketTariff>>())).Returns(_TicketTariffDtos.AsEnumerable());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetAllTicketTariffsAsync();

//            (result as ObjectResult).StatusCode.Should().Be(200);
//            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketTariffDto>).Should().NotBeEmpty();
//        }

//        #endregion

//        #region AddTicketTariffAsync(TicketTariff TicketTariff);
//        // istnieje juz taki sam cennik w bazie -> 400 bad request instance already exists, details 'objname', 'id'
//        // TicketTariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
//        // jakies dane cenniku sa niepoprawne -> 400 bad request i info co jest nie tak
//        // dodano poprawnie -> 201 created, zwraca dodany element
//        // any internal error refferd to the db occurred -> throws internal db service exc
//        // any unexpected internal error occurred -> throws exc

//        [Test]
//        public async Task AddTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
//        {
//            // Example of these errors: database does not exist, table does not exist etc.

//            _TicketTariffDbServiceMock.Setup(x => x.AddAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new InternalDbServiceException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.AddTicketTariffAsync(_TicketTariffDtos[0]);

//            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
//        }

//        [Test]
//        public async Task AddTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.AddAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new Exception());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.AddTicketTariffAsync(_TicketTariffDtos[0]);

//            await result.Should().ThrowExactlyAsync<Exception>();
//        }

//        [Test]
//        public async Task AddTicketTariffAsync__Already_there_is_the_same_element_in_database__Should_return_400BadRequest_response()
//        {
//            var TicketTariffDto = CreateTicketTariffDto(_TicketTariffs[0]);
//            _mapperMock.Setup(x => x.Map<TicketTariff>(TicketTariffDto)).Returns(_TicketTariffs[0]);
//            _TicketTariffDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<TicketTariff>())).ThrowsAsync(new InvalidOperationException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.AddTicketTariffAsync(TicketTariffDto);

//            (result as ObjectResult).StatusCode.Should().Be(400);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task AddTicketTariffAsync__Add_succeeded__Should_return_200OK_response_with_added_element()
//        {
//            var validTicketTariff = new TicketTariff { Id = "12312321321321", Description = "Valid description" };
//            var validTicketTariffDto = CreateTicketTariffDto(validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariff>(It.IsNotNull<TicketTariffDto>())).Returns(validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariffDto>(It.IsNotNull<TicketTariff>())).Returns(validTicketTariffDto);
//            _TicketTariffDbServiceMock.Setup(x => x.AddAsync(validTicketTariff)).ReturnsAsync(validTicketTariff);
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.AddTicketTariffAsync(validTicketTariffDto);

//            (result as ObjectResult).StatusCode.Should().Be(201);
//            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
//        }

//        #endregion

//        #region UpdateTicketTariffAsync(string id, TicketTariff TicketTariff);
//        // nie ma takiego elementu o podanym id -> 404 not found
//        // id w url i id w body sie nie zgadzaja -> 400 bad request, mismatch
//        // id / TicketTariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
//        // update poprawny -> 200 ok
//        // any internal error refferd to the db occurred -> throws internal db service exc
//        // any unexpected internal error occurred -> throws exc

//        [Test]
//        public async Task UpdateTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
//        {
//            // Example of these errors: database does not exist, table does not exist etc.

//            _TicketTariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new InternalDbServiceException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.UpdateTicketTariffAsync(_TicketTariffDtos[0].Id, _TicketTariffDtos[0]);

//            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
//        }

//        [Test]
//        public async Task UpdateTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new Exception());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.UpdateTicketTariffAsync(_TicketTariffDtos[0].Id, _TicketTariffDtos[0]);

//            await result.Should().ThrowExactlyAsync<Exception>();
//        }

//        [Test]
//        public async Task UpdateTicketTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
//        {
//            var validTicketTariff = new TicketTariff { Id = "12312321321321", Description = "Valid description" };
//            var validTicketTariffDto = CreateTicketTariffDto(validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariff>(It.IsNotNull<TicketTariffDto>())).Returns(validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariffDto>(It.IsNotNull<TicketTariff>())).Returns(validTicketTariffDto);
//            _TicketTariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<TicketTariff>())).ThrowsAsync(new InvalidOperationException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.UpdateTicketTariffAsync(validTicketTariffDto.Id, validTicketTariffDto);

//            (result as ObjectResult).StatusCode.Should().Be(404);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task UpdateTicketTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
//        {
//            var TicketTariffDto = new TicketTariffDto { Id = id, Description = "Test dto description" };
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.UpdateTicketTariffAsync(id, TicketTariffDto);

//            (result as ObjectResult).StatusCode.Should().Be(400);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task UpdateTicketTariffAsync__Argument_id_and_id_property_in_element_to_be_updated_mismatches__Should_return_400BadRequest_response()
//        {
//            var TicketTariffDto = new TicketTariffDto { Id = "1", Description = "Test dto description" };
//            string id = TicketTariffDto.Id + "_mismatched_id";
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.UpdateTicketTariffAsync(id, TicketTariffDto);

//            (result as ObjectResult).StatusCode.Should().Be(400);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task UpdateTicketTariffAsync__Update_succeeded__Should_return_200OK_response_with_updated_element()
//        {
//            var validTicketTariff = new TicketTariff { Id = "12312321321321", Description = "Valid description" };
//            var validTicketTariffDto = CreateTicketTariffDto(validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariff>(It.IsNotNull<TicketTariffDto>())).Returns(validTicketTariff);
//            _mapperMock.Setup(x => x.Map<TicketTariffDto>(It.IsNotNull<TicketTariff>())).Returns(validTicketTariffDto);
//            _TicketTariffDbServiceMock.Setup(x => x.UpdateAsync(validTicketTariff)).ReturnsAsync(validTicketTariff);
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.UpdateTicketTariffAsync(validTicketTariffDto.Id, validTicketTariffDto);

//            (result as ObjectResult).StatusCode.Should().Be(200);
//            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(validTicketTariffDto);
//        }

//        #endregion

//        #region DeleteTicketTariffAsync(string id);
//        // nie ma takiego elementu o podanym id -> 404 not found
//        // argument jest pusty albo null -> 400 bad request
//        // usunieto poprawnie -> 200 ok
//        // any internal error refferd to the db occurred -> throws internal db service exc
//        // any unexpected internal error occurred -> throws exc

//        [Test]
//        public async Task DeleteTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
//        {
//            // Example of these errors: database does not exist, table does not exist etc.

//            _TicketTariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.DeleteTicketTariffAsync("1");

//            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
//        }

//        [Test]
//        public async Task DeleteTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            Func<Task> result = async () => await controller.DeleteTicketTariffAsync("1");

//            await result.Should().ThrowExactlyAsync<Exception>();
//        }

//        [Test]
//        public async Task DeleteTicketTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
//        {
//            string id = "-1";
//            _TicketTariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.DeleteTicketTariffAsync(id);

//            (result as ObjectResult).StatusCode.Should().Be(404);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task DeleteTicketTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.DeleteTicketTariffAsync(id);

//            (result as ObjectResult).StatusCode.Should().Be(400);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task DeleteTicketTariffAsync__Delete_succeeded__Should_return_200OK_response()
//        {
//            _TicketTariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
//            var controller = new TicketTariffsController(_TicketTariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.DeleteTicketTariffAsync("1");

//            (result as ObjectResult).StatusCode.Should().Be(200);
//            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
//        }

//        #endregion


//        #region Privates

//        private TicketTariffDto CreateTicketTariffDto(TicketTariff TicketTariff)
//        {
//            return new TicketTariffDto
//            {
//                Id = TicketTariff.Id,
//                Description = TicketTariff.Description,
//                TicketTariffValueInPercentage = TicketTariff.TicketTariffValueInPercentage,
//                GroupSizeForTicketTariff = TicketTariff.GroupSizeForTicketTariff,
//                Type = TicketTariff.Type
//            };
//        }

//        #endregion

//    }
//}
