//using System;
//using System.Collections.Generic;
//using System.Text;
//using NUnit.Framework;
//using Moq;
//using FluentAssertions;
//using SDCWebApp.Services;
//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using SDCWebApp.Controllers;
//using System.Threading.Tasks;
//using UnitTests.Helpers;
//using Microsoft.AspNetCore.Mvc;
//using SDCWebApp.Models;
//using SDCWebApp.Models.Dtos;
//using System.Linq;
//using SDCWebApp.ApiErrors;

//namespace UnitTests.Controllers
//{
//    [TestFixture]
//    public class SightseeingTariffsControllerTests
//    {
//        private Mock<ISightseeingTariffDbService> _tariffDbServiceMock;
//        private Mock<IMapper> _mapperMock;
//        private ILogger<SightseeingTariffsController> _logger;
//        private SightseeingTariff _responseTariff;


//        [OneTimeSetUp]
//        public void SetUp()
//        {
//            _tariffDbServiceMock = new Mock<ISightseeingTariffDbService>();
//            _mapperMock = new Mock<IMapper>();
//            _mapperMock.Setup(x => x.Map<SightseeingTariff, SighseeingTariffDto>(It.IsNotNull<SightseeingTariff>())).Returns(new SighseeingTariffDto());
//            _logger = Mock.Of<ILogger<SightseeingTariffsController>>();
//            _responseTariff = new SightseeingTariff
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
//        }

//        #region GetTariffAsync(string id);
//        // [GET] sightseeing-tariffs/{id}
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

//            Mock.Verify(_tariffDbServiceMock);
//            (result as NotFoundObjectResult).StatusCode.Should().Be(404);
//            ((result as NotFoundObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetTariffAsync__Parameter_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
//        {
//            _tariffDbServiceMock.Setup(x => x.GetAsync(It.Is<string>(s => s == id))).ThrowsAsync(new ArgumentException()).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTariffAsync(id);

//            Mock.Verify(_tariffDbServiceMock);
//            (result as BadRequestObjectResult).StatusCode.Should().Be(400);
//            ((result as BadRequestObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
//        }

//        [Test]
//        public async Task GetTariffAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
//        {

//            _tariffDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_responseTariff).Verifiable();
//            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger, _mapperMock.Object);

//            var result = await controller.GetTariffAsync("15891fb0-faec-43c6-9e83-04a4a17c3660");

//            Mock.Verify(_tariffDbServiceMock);
//            (result as OkObjectResult).StatusCode.Should().Be(200);
//            ((result as OkObjectResult).Value as ResponseWrapper).Data.Should().NotBe(new SightseeingTariff());
//        }



//        #endregion


//        #region GetCurrentTariffAsync();
//        // [GET] sightseeing-tariffs/current
//        // pusta lista i nie mozna pobrac -> 200 ok, error, ze pusta lista
//        // exception przy pobieraniu -> 500 internal server error (po prosty throw;)
//        // znaleziono -> 200 ok, najbardziej aktualny cennik wraz z lista cennikow biletow
//        #endregion


//        #region GetAllTariffsAsync();
//        // [GET] sightseeing-tariffs/
//        // exception przy pobieraniu -> 500 internal server error (po prosty throw;)
//        // pusty zasob -> 200 ok, pusta lista
//        // znalazlo min 1 element -> 200 ok i niepusta lista cennikow
//        #endregion


//        #region AddTariffAsync(SightseeingTariff tariff);
//        // [POST] sightseeing-tariffs/
//        // exception przy dodawaniu -> 500 internal server error (po prosty throw;)
//        // istnieje juz taki sam cennik w bazie -> 400 bad request instance already exists, details 'objname', 'id'
//        // jakies dane cenniku sa niepoprawne -> 400 bad request i info co jest nie tak
//        // dodano poprawnie -> 201 created, zwraca dodany element
//        #endregion


//        #region UpdateTariffAsync(string id, SightseeingTariff tariff);
//        // [PUT] sightseeing-tariffs/{id}
//        // exception przy edycji -> 500 internal server error (po prosty throw;)
//        // nie ma takiego elementu o podanym id -> not found
//        // niepoprawne dane w dodawanym elemencie -> 400 bad request i info co jest nie tak
//        // id w url i id w body sie nie zgadzaja -> 400 bad request
//        // update poprawny -> 200 ok
//        #endregion


//        #region DeleteTariffAsync(string id);
//        // [DELETE] sightseeing-tariffs/{id}
//        // exception przy usuwaniu -> 500 internal server error (po prosty throw;)
//        // nie ma takiego cennika o podanym id -> 404 not found
//        // usunieto poprawnie -> 200 ok
//        #endregion
//    }
//}