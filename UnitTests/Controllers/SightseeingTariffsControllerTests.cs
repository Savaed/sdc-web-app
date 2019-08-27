//using AutoMapper;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using NUnit.Framework;
//using SDCWebApp.Controllers;
//using SDCWebApp.Models;
//using SDCWebApp.Models.Dtos;
//using SDCWebApp.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace UnitTests.Controllers
//{
//    [TestFixture]
//    public class SightseeingTariffsControllerTests
//    {
//        private Mock<ISightseeingTariffDbService> _tariffDbServiceMock;
//        private Mock<IMapper> _mapperMock;
//        private ILogger<SightseeingTariffsController> _logger;
//        private SightseeingTariff _validTriff;
//        private SightseeingTariff[] _tariffs;


//        [OneTimeSetUp]
//        public void SetUp()
//        {
//            _tariffDbServiceMock = new Mock<ISightseeingTariffDbService>();
//            _mapperMock = new Mock<IMapper>();
//            _mapperMock.Setup(x => x.Map<SightseeingTariff, SightseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(new SightseeingTariffDto());
//            _logger = Mock.Of<ILogger<SightseeingTariffsController>>();
//            _validTriff = new SightseeingTariff
//            {
//                Id = "15891fb0-faec-43c6-9e83-04a4a17c3660",
//                Name = "Sample sightseeing tariff name for test",
//                TicketTariffs = new TicketTariff[]
//                {
//                    new TicketTariff
//                    {
//                        Id = "123456789",
//                        Description = "Sample ticket tariff for test",
//                        DefaultPrice = 23,
//                        UpdatedAt = DateTime.Now
//                    }
//                }
//            };
//            _tariffs = new SightseeingTariff[]
//            {
//                new SightseeingTariff
//                {
//                    UpdatedAt = null,
//                    Name = "Not updated since created"
//                },
//                new SightseeingTariff
//                {
//                    UpdatedAt = DateTime.Now.AddMinutes(30),
//                    Name = "Updated in future. It's the latest tariff and should be retrieved"
//                }
//           };
//        }

//        #region GetTariffAsync(string id);
//        // brak cennika o podanym id/pusty zasob -> 404 not found
//        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
//        // znaleziono -> 200 ok, szukany cennik wraz z lista cennikow biletow
//        // jakikolwiek error -> response zawiera error

//        [Test]
//        public async Task GetTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException()).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTariffAsync("1");
            
//            (result as NotFoundObjectResult).StatusCode.Should().Be(404);
//            ((result as NotFoundObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetTariffAsync__Arguments_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTariffAsync(id);
            
//            (result as BadRequestObjectResult).StatusCode.Should().Be(400);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validTriff).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTariffAsync("15891fb0-faec-43c6-9e83-04a4a17c3660");
            
//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            ((result as OkObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
//        }

//        #endregion


//        #region GetCurrentTariffAsync();
//        // pusta lista i nie mozna pobrac -> 200 ok, error ze pusta lista
//        // znaleziono -> 200 ok, najbardziej aktualny cennik wraz z lista cennikow biletow
//        // aktualny cennik jest najpozniej stworzony, ale najwczesniej zaktualizowany (chodzi o to, ze powinno patrzec po update, jesli update jest null, to wtedy na created)

//        [Test]
//        public async Task GetCurrentTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InvalidOperationException()).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetCurrentTariffAsync();
            
//            (result as NotFoundObjectResult).StatusCode.Should().Be(404);
//            ((result as NotFoundObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetCurrentTariffAsync_Some_tariffs_have_been_edited_since_the_creation_and_are_the_most_current__Should_return_200Ok_response_with_most_current_tariff()
//        {
//            // NOTE Some tariffs may have been edited after creation, but there are other newer, unedited tariffs.
//            // Regardless of that the latest updated tariff is the most current even if there are other, newer creted, but not updated (or updated latter) tariffs
//            // Example :
//            //          tariff 1: created at: 12.12.2018, updated at: 12.6.2019  --> THIS IS THE MOST CURRENT TARIFF
//            //          tariff 2: created at: 23.3.2019,  updated at: null       --> THIRD
//            //          tariff 3: created at: 23.12.2015, updated at 11.6.2019   --> SECOND 

//            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_tariffs.AsEnumerable()).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetCurrentTariffAsync();

//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            ((result as OkObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(_tariffs[1]);
//        }

//        [Test]
//        public async Task GetCurrentTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_tariffs.AsEnumerable()).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetCurrentTariffAsync();
            
//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            ((result as OkObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
//        }
//        #endregion


//        #region GetAllTariffsAsync();
//        // pusty zasob -> 200 ok, pusta lista
//        // znalazlo min 1 element -> 200 ok i niepusta lista cennikow

//        [Test]
//        public async Task GetAllTariffAsync__Element_not_found__Should_return_200OK_response_with_empty_IEnumerable()
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new SightseeingTariff[] { }.AsEnumerable());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetAllTariffsAsync();

//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            (((result as OkObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingTariff>).Count().Should().Be(0);
//        }

//        [Test]
//        public async Task GetAllTariffAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_tariffs);
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetAllTariffsAsync();

//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            (((result as OkObjectResult).Value as ResponseWrapper).Data as IEnumerable<SightseeingTariff>).Count().Should().NotBe(0);
//        }

//        #endregion


//        #region AddTariffAsync(SightseeingTariff tariff);
//        // istnieje juz taki sam cennik w bazie -> 400 bad request instance already exists, details 'objname', 'id'
//        // tariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
//        // jakies dane cenniku sa niepoprawne -> 400 bad request i info co jest nie tak
//        // dodano poprawnie -> 201 created, zwraca dodany element

//        [Test]
//        public async Task AddTariffAsync__Already_there_is_the_same_element_in_database__Should_return_400BadRequest_response()
//        {
//            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.AddTariffAsync(new SightseeingTariffDto());

//            (result as BadRequestObjectResult).StatusCode.Should().Be(400);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task AddTariffAsync__Argument_is_null__Should_return_400BadRequest_response()
//        {
//            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new ArgumentNullException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.AddTariffAsync(null as SightseeingTariffDto);

//            (result as BadRequestObjectResult).StatusCode.Should().Be(400);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task AddTariffAsync__Argument_is_not_null_but_invalid__Should_return_400BadRequest_response()
//        {
//            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new ArgumentNullException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);
//            var invalidTariff = new SightseeingTariffDto { Name = null };   // Propert Name is required. Max length = 50.

//            var result = await controller.AddTariffAsync(invalidTariff);

//            (result as BadRequestObjectResult).StatusCode.Should().Be(400);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task AddTariffAsync__Add_succeeded__Should_return_200OK_response_with_added_element()
//        {
//            _tariffDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new ArgumentNullException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);
//            var validTariff = new SightseeingTariffDto
//            {
//                Name = "Valid name",
//                Id = "12312321321321"
//            };

//            var result = await controller.AddTariffAsync(validTariff);

//            (result as BadRequestObjectResult).StatusCode.Should().Be(201);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
//        }
//        #endregion


//        #region UpdateTariffAsync(string id, SightseeingTariff tariff);
//        // nie ma takiego elementu o podanym id -> not found
//        // niepoprawne dane w dodawanym elemencie -> 400 bad request i info co jest nie tak
//        // id w url i id w body sie nie zgadzaja -> 400 bad request
//        // id / tariff jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
//        // update poprawny -> 200 ok
//        #endregion


//        #region DeleteTariffAsync(string id);
//        // nie ma takiego cennika o podanym id -> 404 not found
//        // argument jest pusty albo null -> 400 bad request
//        // usunieto poprawnie -> 200 ok

//        [Test]
//        public async Task DeleteTariffAsync__Element_not_found__Should_return_404NotFound_response_with_error()
//        {
//            string id = "-1";
//            _tariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.DeleteTariffAsync(id);

//            (result as NotFoundObjectResult).StatusCode.Should().Be(404);
//            ((result as NotFoundObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task DeleteTariffAsync__Arguments_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
//        {
//            _tariffDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.DeleteTariffAsync(id);

//            (result as BadRequestObjectResult).StatusCode.Should().Be(400);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task DeleteTariffAsync__Delete_succeeded__Should_return_200OK_response()
//        {
//            _tariffDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.DeleteTariffAsync("123");

//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            ((result as OkObjectResult).Value as ResponseWrapper).Error.Should().BeNull();
//        }
//        #endregion
//    }
//}