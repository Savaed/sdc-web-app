using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class SightseeingTariffsControllerTests
    {
        private Mock<ISightseeingTariffDbService> _tariffDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<SightseeingTariffsController> _logger;
        private SightseeingTariff _validTariff;
        private SightseeingTariff[] _tariffs;
        private SightseeingTariffDto[] _tariffDtos;


        [OneTimeSetUp]
        public void SetUp()
        {
            _tariffDbServiceMock = new Mock<ISightseeingTariffDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<SightseeingTariffsController>>();
            _validTariff = new SightseeingTariff
            {
                Id = "15891fb0-faec-43c6-9e83-04a4a17c3660",
                Name = "Sample sightseeing tariff name for test",
                TicketTariffs = new TicketTariff[]
                {
                    new TicketTariff
                    {
                        Id = "84b9aa90-faec-43c6-9e83-15891fb0205a",
                        Description = "Sample ticket tariff for test",
                        DefaultPrice = 23,
                        UpdatedAt = DateTime.Now
                    }
                }
            };

            _tariffs = new SightseeingTariff[]
            {
                new SightseeingTariff { Id = "1", UpdatedAt = DateTime.MinValue, Name = "Not updated since created" },
                new SightseeingTariff { Id = "2", UpdatedAt = DateTime.Now.AddMinutes(30), Name = "Updated in future. It's the latest tariff and should be retrieved" }
            };

            _tariffDtos = new SightseeingTariffDto[]
            {
                new SightseeingTariffDto { Id = "1", UpdatedAt = null, Name = "Not updated since created" },
                new SightseeingTariffDto { Id = "2", UpdatedAt = DateTime.Now.AddMinutes(30), Name = "Updated in future. It's the latest tariff and should be retrieved" }
            };
        }


        #region GetTariffAsync(string id);
        // brak cennika o podanym id/pusty zasob -> 404 not found
        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
        // znaleziono -> 200 ok, szukany cennik wraz z lista cennikow biletow
        // jakikolwiek error -> response zawiera error
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetTariffAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetTariffAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException()).Verifiable();
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTariffAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _tariffDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTariffAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(new SightseeingTariffDto { Id = id, Name = "new mapped sightseeing tariff" });
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTariffAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region GetCurrentTariffAsync();
        // pusta lista i nie mozna pobrac -> 404
        // znaleziono -> 200 ok, najbardziej aktualny cennik wraz z lista cennikow biletow
        // aktualny cennik jest najpozniej stworzony, ale najwczesniej zaktualizowany (chodzi o to, ze powinno patrzec po update, jesli update jest null, to wtedy na created)
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetCurrentTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetCurrentTariffAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetCurrentTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetCurrentTariffAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetCurrentTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InvalidOperationException()).Verifiable();
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetCurrentTariffAsync();

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetCurrentTariffAsync__Some_tariffs_have_been_edited_since_the_creation_and_are_the_most_current__Should_return_200Ok_response_with_the_most_current_tariff()
        {
            // NOTE Some tariffs may have been edited after creation, but there are other newer, unedited tariffs.
            // Regardless of that the latest updated tariff is the most current even if there are other, newer creted, but not updated (or updated latter) tariffs.
            // Example :
            //          tariff 1: created at: 12.12.2018, updated at: 12.6.2019  --> THIS IS THE MOST CURRENT TARIFF
            //          tariff 2: created at: 23.3.2019,  updated at: null       --> THIRD
            //          tariff 3: created at: 23.12.2015, updated at 11.6.2019   --> SECOND 

            // _tariffs[1] is the most current in this test case.
            var currentTariffDto = CreateSightseeingTariffDto(_tariffs[1]);
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_tariffs.AsEnumerable()).Verifiable();
            _mapperMock.Setup(x => x.Map<SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(currentTariffDto);
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetCurrentTariffAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(currentTariffDto);
        }

        [Test]
        public async Task GetCurrentTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_tariffs.AsEnumerable()).Verifiable();
            _mapperMock.Setup(x => x.Map<SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(new SightseeingTariffDto { Id = "1", Name = "mapped from tariff to DTO" });
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetCurrentTariffAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region GetAllTariffsAsync();
        // pusty zasob -> 200 ok, pusta lista
        // znalazlo min 1 element -> 200 ok i niepusta lista cennikow
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetAllTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTariffsAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTariffsAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllTariffAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<SightseeingTariff>());
            _mapperMock.Setup(x => x.Map<IEnumerable<SightseeingTariffDto>>(It.IsNotNull<IEnumerable<SightseeingTariff>>())).Returns(Enumerable.Empty<SightseeingTariffDto>());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTariffsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingTariffDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllTariffAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_tariffs);
            _mapperMock.Setup(x => x.Map<IEnumerable<SightseeingTariffDto>>(It.IsNotNull<IEnumerable<SightseeingTariff>>())).Returns(_tariffDtos.AsEnumerable());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTariffsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingTariffDto>).Should().NotBeEmpty();
        }

        #endregion


        #region AddTariffAsync(SightseeingTariff tariff);
        // istnieje juz taki sam cennik w bazie -> 400 bad request instance already exists, details 'objname', 'id'
        // tariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // jakies dane cenniku sa niepoprawne -> 400 bad request i info co jest nie tak
        // dodano poprawnie -> 201 created, zwraca dodany element
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task AddTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new InternalDbServiceException());
            _tariffDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new InternalDbServiceException());
            var tariff = new SightseeingTariffDto { Id = "1", Name = "test" };
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.AddTariffAsync(tariff);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task AddTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new Exception());
            _tariffDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new Exception());
            var tariff = new SightseeingTariffDto { Id = "1", Name = "test" };
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.AddTariffAsync(tariff);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task AddTariffAsync__Already_there_is_the_same_element_in_database__Should_return_400BadRequest_response()
        {
            var tariffDto = CreateSightseeingTariffDto(_tariffs[0]);
            _mapperMock.Setup(x => x.Map<SightseeingTariff>(tariffDto)).Returns(_tariffs[0]);
            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
            _tariffDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddTariffAsync(tariffDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task AddTariffAsync__Add_succeeded__Should_return_200OK_response_with_added_element()
        {
            var validTariff = new SightseeingTariff { Id = "12312321321321", Name = "Valid name" };
            var validTariffDto = CreateSightseeingTariffDto(validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariff>(It.IsNotNull<SightseeingTariffDto>())).Returns(validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(validTariffDto);
            _tariffDbServiceMock.Setup(x => x.AddAsync(validTariff)).ReturnsAsync(validTariff);
            _tariffDbServiceMock.Setup(x => x.RestrictedAddAsync(validTariff)).ReturnsAsync(validTariff);
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddTariffAsync(validTariffDto);

            (result as ObjectResult).StatusCode.Should().Be(201);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region UpdateTariffAsync(string id, SightseeingTariff tariff);
        // nie ma takiego elementu o podanym id -> 404 not found
        // id w url i id w body sie nie zgadzaja -> 400 bad request, mismatch
        // id / tariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // update poprawny -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task UpdateTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            var validTariff = new SightseeingTariff { Id = "12312321321321", Name = "Valid name" };
            var validTariffDto = CreateSightseeingTariffDto(validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariff>(It.IsNotNull<SightseeingTariffDto>())).Returns(validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(validTariffDto);
            _tariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
            _tariffDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateTariffAsync(validTariffDto.Id, validTariffDto);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _tariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new InternalDbServiceException());
            _tariffDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateTariffAsync(_tariffDtos[0].Id, _tariffDtos[0]);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task UpdateTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _tariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new Exception());
            _tariffDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<SightseeingTariff>())).ThrowsAsync(new Exception());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateTariffAsync(_tariffDtos[0].Id, _tariffDtos[0]);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task UpdateTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);
            var tariffDto = _tariffDtos[0];
            tariffDto.Id = id;

            var result = await controller.UpdateTariffAsync(id, tariffDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateTariffAsync__Argument_id_and_id_property_in_element_to_be_updated_mismatches__Should_return_400BadRequest_response()
        {
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);
            var tariffDto = _tariffDtos[0];
            string id = tariffDto.Id + "_mismatched_id";

            var result = await controller.UpdateTariffAsync(id, tariffDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateTariffAsync__Update_succeeded__Should_return_200OK_response_with_updated_element()
        {
            var validTariff = new SightseeingTariff { Id = "12312321321321", Name = "Valid name" };
            var validTariffDto = CreateSightseeingTariffDto(validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariff>(It.IsNotNull<SightseeingTariffDto>())).Returns(validTariff);
            _mapperMock.Setup(x => x.Map<SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(validTariffDto);
            _tariffDbServiceMock.Setup(x => x.UpdateAsync(validTariff)).ReturnsAsync(validTariff);
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);
            _tariffDbServiceMock.Setup(x => x.RestrictedUpdateAsync(validTariff)).ReturnsAsync(validTariff);

            var result = await controller.UpdateTariffAsync(validTariffDto.Id, validTariffDto);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(validTariffDto);
        }


        #endregion


        #region DeleteTariffAsync(string id);
        // nie ma takiego cennika o podanym id -> 404 not found
        // argument jest pusty albo null -> 400 bad request
        // usunieto poprawnie -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task DeleteTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _tariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteTariffAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task DeleteTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _tariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteTariffAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task DeleteTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            string id = "-1";
            _tariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteTariffAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _tariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteTariffAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteTariffAsync__Delete_succeeded__Should_return_200OK_empty_response()
        {
            var emptyResponse = new ResponseWrapper();
            _tariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteTariffAsync("123");

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Should().BeEquivalentTo(emptyResponse);
        }

        #endregion


        #region Privates

        private SightseeingTariffDto CreateSightseeingTariffDto(SightseeingTariff tariff)
        {
            return new SightseeingTariffDto
            {
                Id = tariff.Id,
                Name = tariff.Name,
                CreatedAt = tariff.CreatedAt,
                UpdatedAt = tariff.UpdatedAt,
                TicketTariffs = tariff.TicketTariffs as ICollection<TicketTariffDto>
            };
        }

        #endregion

    }
}