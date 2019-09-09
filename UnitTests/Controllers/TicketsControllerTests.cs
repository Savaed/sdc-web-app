using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SDCWebApp.Controllers;
using System.Threading.Tasks;
using SDCWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Models;
using SDCWebApp.ApiErrors;
using System.Linq;
using Autofac.Features.Indexed;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class TicketsControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private ILogger<TicketsController> _logger;
        private Mock<ITicketDbService> _ticketDbServiceMock;
        private Mock<ICustomerDbService> _customerDbSerivce;
        private Mock<IDiscountDbService> _discountDbServiceMock;
        private Mock<ITicketTariffDbService> _ticketTariffDbSerivce;
        private Mock<ISightseeingGroupDbService> _groupDbSerivce;
        private Ticket _validTicket;
        private TicketDto _validTicketDto;
        private Mock<IIndex<string, IServiceBase>> _dbServiceFactoryMock;

        //TODO Add tests and support for indexer mocks.

        [OneTimeSetUp]
        public void SetUp()
        {
            _ticketDbServiceMock = new Mock<ITicketDbService>();
            _customerDbSerivce = new Mock<ICustomerDbService>();
            _ticketTariffDbSerivce = new Mock<ITicketTariffDbService>();
            _groupDbSerivce = new Mock<ISightseeingGroupDbService>();
            _discountDbServiceMock = new Mock<IDiscountDbService>();

            _dbServiceFactoryMock = new Mock<IIndex<string, IServiceBase>>();

            // _dbServiceFactoryMock.Setup(x => x[It.Is<string>(s => s == nameof(ITicketDbService))]).Returns(_ticketDbServiceMock.Object as ServiceBase);
            //_dbServiceFactoryMock.Setup(x => x[nameof(ICustomerDbService)]).Returns(_customerDbSerivce.Object as ServiceBase);
            //_dbServiceFactoryMock.Setup(x => x[nameof(ITicketTariffDbService)]).Returns(_ticketTariffDbSerivce.Object as ServiceBase);
            //_dbServiceFactoryMock.Setup(x => x[nameof(ISightseeingGroupDbService)]).Returns(_groupDbSerivce.Object as ServiceBase);
            //_dbServiceFactoryMock.Setup(x => x[nameof(IDiscountDbService)]).Returns(_discountDbServiceMock.Object as ServiceBase);

            _validTicketDto = new TicketDto
            {
                //Id = "1",
                //customer = new CustomerDto { Id = "1", EmailAddress = "samplecustomer@mail.com" },
                //Links = new ApiLink[]
                //{
                //    new ApiLink("discount", "discounts/1", "GET"),
                //    new ApiLink("ticketTariff", "ticket-tariffs/1", "GET"),
                //    new ApiLink("sightseeingGroup", "groups/1", "GET")
                //},
                //PurchaseDate = DateTime.Now.AddDays(-1),
                //TicketUniqueId = Guid.NewGuid().ToString()
            };
            _validTicket = new Ticket
            {
                Id = "1",
                Customer = new Customer { Id = "1", EmailAddress = "samplecustomer@mail.com" },
                Discount = new Discount { Id = "1", Description = "discount description", DiscountValueInPercentage = 25, Type = Discount.DiscountType.ForChild },
                Group = new SightseeingGroup { Id = "1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(1) },
                Tariff = new TicketTariff { Id = "1", DefaultPrice = 30, Description = "ticket price list description" },
                PurchaseDate = DateTime.Now.AddDays(-1),
                TicketUniqueId = Guid.NewGuid().ToString()
            };
            _logger = Mock.Of<ILogger<TicketsController>>();
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map<TicketDto>(It.IsAny<Ticket>())).Returns(_validTicketDto);
            _mapperMock.Setup(x => x.Map<Ticket>(It.IsAny<TicketDto>())).Returns(_validTicket);
        }


        #region GetTicketAsync(string id)
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Specified ticket not found -> 404NotFound
        // Argument id is null or empty -> 400BadRequest
        // Ticket found -> 200Ok, return this ticket 

        [Test]
        public async Task GetTicketAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _ticketDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetTicketAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetTicketAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _ticketDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetTicketAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetTicketAsync__Element_not_found__Should_return_404NotFound_response()
        {
            _ticketDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTicketAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetTicketAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _ticketDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTicketAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetTicketAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _ticketDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validTicket);
            _mapperMock.Setup(x => x.Map<TicketDto>(It.IsNotNull<Ticket>())).Returns(_validTicketDto);
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetTicketAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(_validTicketDto);
        }


        #endregion


        #region GetAllTicketsAsync();
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Resource is empty -> return empty IEnumerable
        // At least one ticket found -> 200Ok, return not empty IEnumerable 

        [Test]
        public async Task GetAllTicketAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _ticketDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            _dbServiceFactoryMock.Setup(x => x[It.IsAny<string>()]).Returns(_ticketDbServiceMock.Object as IServiceBase);

            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTicketsAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllTicketAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _ticketDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllTicketsAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllTicketAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _ticketDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Ticket>());
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketDto>>(It.IsNotNull<IEnumerable<Ticket>>())).Returns(Enumerable.Empty<TicketDto>());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllTicketAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _ticketDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new Ticket[] { _validTicket }.AsEnumerable());
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketDto>>(It.IsNotNull<IEnumerable<Ticket>>())).Returns(new TicketDto[] { _validTicketDto }.AsEnumerable());
            var controller = new TicketsController(_dbServiceFactoryMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllTicketsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<TicketDto>).Should().NotBeEmpty();
        }

        #endregion


        #region GetCustomerTicket(string customerId, string ticketId)

        #endregion


        #region GetCustomerTickets(string customerId)

        #endregion

    }
}
