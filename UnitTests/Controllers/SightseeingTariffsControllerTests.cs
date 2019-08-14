using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using SDCWebApp.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SDCWebApp.Controllers;
using System.Threading.Tasks;
using UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.ApiDto;
using SDCWebApp.Models;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class SightseeingTariffsControllerTests
    {
        private Mock<ISightseeingTariffDbService> _tariffDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<SightseeingTariffsController> _logger;


        [OneTimeSetUp]
        public void SetUp()
        {
            _tariffDbServiceMock = new Mock<ISightseeingTariffDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<SightseeingTariffsController>>();
        }


        #region GetTariff()
        // wgl nie istnieje zasob, w ktorym moze znajdowac sie cennik -> 500 internal error, nie ma takiej tabeli w bazie
        // jw success = false, data = null
        // zle id, zasob istnieje -> 404 not found, error, ze tabela istnieje, ale nie ma w niej elementu o takim id, success = false
        // znaleziono element -> 200 ok , bez errorow, z danymi jako pojedynczy element, success = true

        [Test]
        public async Task GetTariffAsync__Resource_which_is_reffered_doesnt_exist__Should_return_500InternalServerError_with_explanation()
        {
            _tariffDbServiceMock.Setup(x => x.GetSightseeingTariffAsync(It.IsNotNull<string>())).ThrowsAsync(new NullReferenceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger);

            ObjectResult result = await controller.GetTariff("id") as ObjectResult;

            result.StatusCode.Should().Be(500);
            (result.Value as CommonWrapper).Errors.Should().NotBeNull();
        }

        [Test]
        public async Task GetTariffAsync__Resource_which_is_reffered_doesnt_exist__Should_return_500InternalServerError_without_data_and_success_be_false()
        {
            _tariffDbServiceMock.Setup(x => x.GetSightseeingTariffAsync(It.IsNotNull<string>())).ThrowsAsync(new NullReferenceException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger);

            ObjectResult result = await controller.GetTariff("id") as ObjectResult;

            result.StatusCode.Should().Be(500);
            (result.Value as CommonWrapper).Data.Should().BeNull();
            (result.Value as CommonWrapper).Success.Should().BeFalse();
        }

        [Test]
        public async Task GetTariffAsync__Element_not_found__Should_return_404NotFound_with_explanation()
        {
            _tariffDbServiceMock.Setup(x => x.GetSightseeingTariffAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger);

            ObjectResult result = await controller.GetTariff("id") as ObjectResult;

            result.StatusCode.Should().Be(404);
            (result.Value as CommonWrapper).Errors.Should().NotBeNull();
        }

        [Test]
        public async Task GetTariffAsync__Element_not_found__Should_return_404NotFound_without_data_and_success_be_false()
        {
            _tariffDbServiceMock.Setup(x => x.GetSightseeingTariffAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger);

            ObjectResult result = await controller.GetTariff("id") as ObjectResult;

            result.StatusCode.Should().Be(404);
            (result.Value as CommonWrapper).Data.Should().BeNull();
            (result.Value as CommonWrapper).Success.Should().BeFalse();

        }

        [Test]
        public async Task GetTariffAsync__Element_found__Should_return_200OK_with_data()
        {
            _tariffDbServiceMock.Setup(x => x.GetSightseeingTariffAsync(It.IsNotNull<string>())).ReturnsAsync(new SightseeingTariff());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger);

            ObjectResult result = await controller.GetTariff("id") as ObjectResult;

            result.StatusCode.Should().Be(200);
            (result.Value as CommonWrapper).Data.Should().NotBeNull();
        }

        [Test]
        public async Task GetTariffAsync__Element_found__Should_return_200OK_without_error_and_success_be_true()
        {
            _tariffDbServiceMock.Setup(x => x.GetSightseeingTariffAsync(It.IsNotNull<string>())).ReturnsAsync(new SightseeingTariff());
            var controller = new SightseeingTariffsController(_tariffDbServiceMock.Object, _logger);

            ObjectResult result = await controller.GetTariff("id") as ObjectResult;

            result.StatusCode.Should().Be(200);
            (result.Value as CommonWrapper).Errors.Should().BeNull();
            (result.Value as CommonWrapper).Success.Should().BeTrue();
        }

        #endregion

        #region GetTariffs()
        // wgl nie istnieje zasob, w ktorym moze znajdowac sie cennik -> 500 internal error, error, ze nie istnieje taki zasob, success = false
        // zasob jest pusty -> 200 ok, pusta list, success = true
        // cos tam w tym zasobie jest -> 200 ok, niepusta lista, success = true


        #endregion

    }
}
