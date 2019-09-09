using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using SDCWebApp.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Controllers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SDCWebApp.ApiErrors;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class TicketTariffsControllerTests
    {
        private const string _sightseeingTariffId = "1";
        private Mock<ITicketTariffDbService> _ticketTariffDbServiceMock;
        private Mock<ISightseeingTariffDbService> _sightseeingTariffDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<TicketTariffsController> _logger;
        private TicketTariff _validTicketTariff;
        private TicketTariff[] _ticketTariffs;
        private TicketTariffDto[] _ticketTariffDtos;
        private SightseeingTariff _parentSightseeingTariff;


        // TODO Redesign all tests and controller.


        [OneTimeSetUp]
        public void SetUp()
        {
            _ticketTariffDbServiceMock = new Mock<ITicketTariffDbService>();
            _sightseeingTariffDbServiceMock = new Mock<ISightseeingTariffDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<TicketTariffsController>>();

            _ticketTariffs = new TicketTariff[]
            {
                 new TicketTariff
                 {
                    Id = "1",
                    Description = "This is DTO",
                    DefaultPrice = 40,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 },
                 new TicketTariff
                 {
                    Id = "2",
                    Description = "This id DTO",
                    DefaultPrice = 35,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 }
            };

            _parentSightseeingTariff = new SightseeingTariff
            {
                Id = _sightseeingTariffId,
                UpdatedAt = new DateTime(2019, 1, 1),
                Name = "sample parent sightseeing tariff name",
                TicketTariffs = new List<TicketTariff>(_ticketTariffs) as ICollection<TicketTariff>
            };

            _validTicketTariff = new TicketTariff
            {
                Id = "3",
                Description = "Sample test description",
                DefaultPrice = 20,
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            };

            _ticketTariffDtos = new TicketTariffDto[]
            {
                 new TicketTariffDto
                 {
                    Id = "4",
                    Description = "This is DTO",
                    DefaultPrice = 23,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 },
                 new TicketTariffDto
                 {
                    Id = "5",
                    Description = "This id DTO",
                    DefaultPrice = 50,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 }
            };

            
        }


        #region GetTicketTariffFromSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId)
        // brak s-t -> 404
        // istnieje s-t, ale wgl brak t-t -> 404
        // ogolnie istnieje taki t-t, ale nie nalezy do konkretnego s-t -> 404
        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
        // znaleziono -> 200 ok
        // jakikolwiek error -> response zawiera error
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetTicketTariffFromSightseeingTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _ticketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(_parentSightseeingTariff);
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetTicketTariffFromSightseeingTariffAsync(_sightseeingTariffId, "1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetTicketTariffFromSightseeingTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _ticketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(_ticketTariffs[0]);
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetTicketTariffFromSightseeingTariffAsync(_sightseeingTariffId, "1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetTicketTariffFromSightseeingTariffAsync__Sightseeing_tariff_not_found__Should_return_404NotFound_response_with_error()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTicketTariffFromSightseeingTariffAsync(_sightseeingTariffId, "1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetTicketTariffFromSightseeingTariffAsync__Sightseeing_tariff_found_but_searching_ticket_tariff_does_not_belong_to_this__Should_return_404NotFound_response_with_error()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            // SightseeingTariff found, but TicketTariff with id = 111 doesn't belong to this. But may exists in db.
            var result = await controller.GetTicketTariffFromSightseeingTariffAsync(_sightseeingTariffId, "111");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("", null)]
        [TestCase(null, null)]
        public async Task GetTicketTariffFromSightseeingTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response(string sightseeingTariffId, string ticketTariffId)
        {
            _ticketTariffDbServiceMock.Setup(x => x.GetAsync(ticketTariffId)).ThrowsAsync(new ArgumentException());
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(sightseeingTariffId)).ThrowsAsync(new ArgumentException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTicketTariffFromSightseeingTariffAsync(sightseeingTariffId, ticketTariffId);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetTicketTariffFromSightseeingTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            _mapperMock.Setup(x => x.Map<TicketTariffDto>(It.IsNotNull<TicketTariff>())).Returns(CreateTicketTariffDto(_parentSightseeingTariff.TicketTariffs.ToArray()[0]));
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            // Get first TicketTariff from _parentSightseeingTariff.
            var result = await controller.GetTicketTariffFromSightseeingTariffAsync(_sightseeingTariffId, _parentSightseeingTariff.TicketTariffs.ElementAt(0).Id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion

        #region GetAllTicketTariffsFromSightseeingTariffAsync(string sightseeingTariffId)
        // nie znalazlo s-t -> 404
        // pusty zasob -> 200 ok, pusta lista
        // znalazlo min 1 element -> 200 ok i niepusta lista
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetAllTicketTariffsFromSightseeingTariffAsync__Sightseeing_tariff_not_found__Should_return_404NotFound_response_with_error()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketTariffsFromSightseeingTariffAsync(_sightseeingTariffId);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetAllTicketTariffsFromSightseeingTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTicketTariffsFromSightseeingTariffAsync(_sightseeingTariffId);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllTicketTariffsFromSightseeingTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new Exception());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTicketTariffsFromSightseeingTariffAsync(_sightseeingTariffId);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllTicketTariffsFromSightseeingTariffAsync__Sightseeing_tariff_does_not_contain_any_ticket_tariffs__Should_return_200OK_response_with_empty_IEnumerable()
        {
            // Create parent SightseeingTariff with empty TicketTariffs.
            var parentSightseeingTariff = _parentSightseeingTariff.Clone() as SightseeingTariff;
            parentSightseeingTariff.TicketTariffs = Enumerable.Empty<TicketTariff>().ToArray();

            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(parentSightseeingTariff);
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketTariffDto>>(It.IsNotNull<IEnumerable<TicketTariff>>())).Returns(Enumerable.Empty<TicketTariffDto>());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketTariffsFromSightseeingTariffAsync(_sightseeingTariffId);

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketTariffDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllTicketTariffsFromSightseeingTariffAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketTariffDto>>(It.IsNotNull<IEnumerable<TicketTariff>>())).Returns(_ticketTariffDtos.AsEnumerable());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketTariffsFromSightseeingTariffAsync(_sightseeingTariffId);

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketTariffDto>).Should().NotBeEmpty();
        }

      


        #endregion


        #region GetAllTicketTariffsAsync();
        // pusty zasob -> 200 ok, pusta lista
        // znalazlo min 1 element -> 200 ok i niepusta lista
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetAllTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _ticketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTicketTariffsAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _ticketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTicketTariffsAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllTicketTariffAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _ticketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<TicketTariff>());
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketTariffDto>>(It.IsNotNull<IEnumerable<TicketTariff>>())).Returns(Enumerable.Empty<TicketTariffDto>());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketTariffsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketTariffDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllTicketTariffAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _ticketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_ticketTariffs);
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketTariffDto>>(It.IsNotNull<IEnumerable<TicketTariff>>())).Returns(_ticketTariffDtos.AsEnumerable());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketTariffsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketTariffDto>).Should().NotBeEmpty();
        }

        #endregion


        #region AddTicketTariffToSightseeingTariffAsync(string sightseeingTariffId, TicketTariffDto ticketTariff)
        // brak s-t o takim id
        // istnieje juz taki sam cennik w bazie -> 200 i dodaje do s-t
        // dodano poprawnie calkiem nowy t-t -> 201 created, dodaje do bazy i do s-t        
        // ticketTariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task AddTicketTariffToSightseeingTariffAsync__Sightseeing_tariff_not_found__Should_return_404NotFound_response_with_error()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddTicketTariffToSightseeingTariffAsync(_sightseeingTariffId, _ticketTariffDtos[0]);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task AddTicketTariffToSightseeingTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InternalDbServiceException());
            _ticketTariffDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.AddTicketTariffToSightseeingTariffAsync(_sightseeingTariffId, _ticketTariffDtos[0]);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task AddTicketTariffToSightseeingTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new Exception());
            _ticketTariffDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new Exception());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.AddTicketTariffToSightseeingTariffAsync(_sightseeingTariffId, _ticketTariffDtos[0]);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task AddTicketTariffToSightseeingTariffAsync__Already_there_is_the_same_element_in_database__Should_return_200Ok_response_and_add_ticket_tariff_to_parent()
        {
            // If in db exists the same TicketTariff as the one to be added then this tariff should be add to the parent SightseeingTariff and 200 OK response should be return.
            var ticketTariffDto = CreateTicketTariffDto(_ticketTariffs[0]);
            _mapperMock.Setup(x => x.Map<TicketTariff>(ticketTariffDto)).Returns(_ticketTariffs[0]);
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            _ticketTariffDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsNotNull<TicketTariff>())).ThrowsAsync(new InvalidOperationException());
            _ticketTariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_ticketTariffs);

            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddTicketTariffToSightseeingTariffAsync(_sightseeingTariffId, ticketTariffDto);

            (result as ObjectResult).StatusCode.Should().Be(200);
          // ((result as ObjectResult).Value as ResponseWrapper).Data.Should().(ticketTariffDto);
        }

        [Test]
        public async Task AddTicketTariffToSightseeingTariffAsync__Add_succesful__Should_return_201Created_response_and_add_ticket_tariff_to_parent()
        {
            // If in db doesn't exist the same TicketTariff as the one to be added then this tariff should be add to the parent SightseeingTariff and 201 Created response should be return.
            var ticketTariffDto = CreateTicketTariffDto(_ticketTariffs[0]);
            _mapperMock.Setup(x => x.Map<TicketTariff>(ticketTariffDto)).Returns(_ticketTariffs[0]);
            _mapperMock.Setup(x => x.Map<TicketTariffDto>(_ticketTariffs[0])).Returns(ticketTariffDto);
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            _ticketTariffDbServiceMock.Setup(x => x.RestrictedAddAsync(_ticketTariffs[0])).ReturnsAsync(_ticketTariffs[0]);
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddTicketTariffToSightseeingTariffAsync(_sightseeingTariffId, ticketTariffDto);

            (result as ObjectResult).StatusCode.Should().Be(201);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(ticketTariffDto);
        }

        #endregion

        #region UpdateTicketTariffInSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId, [FromBody] TicketTariffDto ticketTariff)
        // nie ma takiego elementu o podanym id -> 404 not found
        // id w url i id w body sie nie zgadzaja -> 400 bad request, mismatch
        // id / TicketTariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // update poprawny -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__Sightseeing_tariff_not_found__Should_return_404NotFound_response_with_error()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "4" ,_ticketTariffDtos[0]);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__Ticket_tariff_not_found__Should_return_404NotFound_response_with_error()
        {
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            _ticketTariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<TicketTariff>())).ThrowsAsync(new InvalidOperationException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "4", _ticketTariffDtos[0]);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _ticketTariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new InternalDbServiceException());
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "4", _ticketTariffDtos[0]);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _ticketTariffDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<TicketTariff>())).ThrowsAsync(new Exception());
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "4", _ticketTariffDtos[0]);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("", null)]
        [TestCase(null, null)]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response(string sightseeingTariffId, string ticketTariffId)
        {
            _ticketTariffDbServiceMock.Setup(x => x.GetAsync(ticketTariffId)).ThrowsAsync(new ArgumentException());
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(sightseeingTariffId)).ThrowsAsync(new ArgumentException());
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "4", _ticketTariffDtos[0]);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__Argument_id_and_id_property_in_element_to_be_updated_mismatches__Should_return_400BadRequest_response()
        {
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "100", _ticketTariffDtos[0]);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateTicketTariffInSightseeingTariffAsync__Update_succeeded__Should_return_200OK_response_with_updated_element()
        {
            var validTicketTariff = new TicketTariff { Id = "4", Description = "Valid description updated" };
            var validTicketTariffDto = CreateTicketTariffDto(validTicketTariff);
            _mapperMock.Setup(x => x.Map<TicketTariff>(It.IsNotNull<TicketTariffDto>())).Returns(validTicketTariff);
            _mapperMock.Setup(x => x.Map<TicketTariffDto>(It.IsNotNull<TicketTariff>())).Returns(validTicketTariffDto);
            _ticketTariffDbServiceMock.Setup(x => x.UpdateAsync(validTicketTariff)).ReturnsAsync(validTicketTariff);
            _sightseeingTariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_parentSightseeingTariff);
            var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateTicketTariffInSightseeingTariffAsync(_sightseeingTariffId, "4", validTicketTariffDto);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(validTicketTariffDto);
        }

        #endregion

        //#region DeleteTicketTariffAsync(string id);
        //// nie ma takiego elementu o podanym id -> 404 not found
        //// argument jest pusty albo null -> 400 bad request
        //// usunieto poprawnie -> 200 ok
        //// any internal error refferd to the db occurred -> throws internal db service exc
        //// any unexpected internal error occurred -> throws exc

        //[Test]
        //public async Task DeleteTicketTariffAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        //{
        //    // Example of these errors: database does not exist, table does not exist etc.

        //    _ticketTariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
        //    var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

        //    Func<Task> result = async () => await controller.DeleteTicketTariffAsync("1");

        //    await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        //}

        //[Test]
        //public async Task DeleteTicketTariffAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        //{
        //    _ticketTariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
        //    var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

        //    Func<Task> result = async () => await controller.DeleteTicketTariffAsync("1");

        //    await result.Should().ThrowExactlyAsync<Exception>();
        //}

        //[Test]
        //public async Task DeleteTicketTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        //{
        //    string id = "-1";
        //    _ticketTariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
        //    var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

        //    var result = await controller.DeleteTicketTariffAsync(id);

        //    (result as ObjectResult).StatusCode.Should().Be(404);
        //    ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        //}

        //[Test]
        //public async Task DeleteTicketTariffAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        //{
        //    _ticketTariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
        //    var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

        //    var result = await controller.DeleteTicketTariffAsync(id);

        //    (result as ObjectResult).StatusCode.Should().Be(400);
        //    ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        //}

        //[Test]
        //public async Task DeleteTicketTariffAsync__Delete_succeeded__Should_return_200OK_response()
        //{
        //    _ticketTariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
        //    var controller = new TicketTariffsController(_ticketTariffDbServiceMock.Object, _sightseeingTariffDbServiceMock.Object, _logger, _mapperMock.Object);

        //    var result = await controller.DeleteTicketTariffAsync("1");

        //    (result as ObjectResult).StatusCode.Should().Be(200);
        //    ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
        //}

        //#endregion


        #region Privates

        private TicketTariffDto CreateTicketTariffDto(TicketTariff ticketTariff)
        {
            return new TicketTariffDto
            {
                Id = ticketTariff.Id,
                Description = ticketTariff.Description,
                DefaultPrice = ticketTariff.DefaultPrice,
                IsPerHour = ticketTariff.IsPerHour,
                CreatedAt = ticketTariff.CreatedAt,
                IsPerPerson = ticketTariff.IsPerPerson,
                UpdatedAt = ticketTariff.UpdatedAt
            };
        }

        #endregion

    }
}
