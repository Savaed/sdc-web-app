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
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private ILogger<OrdersController> _logger;
        private Mock<IMapper> _mapperMock;
        private Mock<ITicketOrderHandler> _orderHandlerMock;
        private OrderRequestDto _orderData;


        [OneTimeSetUp]
        public void SetUp()
        {
            _logger = Mock.Of<ILogger<OrdersController>>();
            _mapperMock = new Mock<IMapper>();
            _orderHandlerMock = new Mock<ITicketOrderHandler>();

            _orderData = new OrderRequestDto
            {
                Customer = new CustomerDto { EmailAddress = "sample@mail.com", DateOfBirth = DateTime.Now.AddYears(-43) },
                Tickets = new ShallowTicket[]
                {
                    new ShallowTicket { DiscountId = "1", TicketTariffId = "1", SightseeingDate = DateTime.Now.AddDays(1).Date.AddHours(14) },
                    new ShallowTicket { DiscountId = "2", TicketTariffId = "1", SightseeingDate = DateTime.Now.AddDays(1).Date.AddHours(16) }
                }
            };
        }


        #region CreateOrderAsync(OrderRequestDto order)
        // Any problem with order handling due to client request -> 400 Bad Request
        // Any problem with order handling due to internal error with processing data in db -> throw InternalDbServiceException
        // Any problem with order handling due to internal error -> throw Exception
        // Order handle succeeded -> 200OK, return order data (customer, tickets and orderId)

        [Test]
        public async Task CreateOrderAsync__Any_problem_with_order_handling_occurred_due_to_client_request__Should_return_400BadRequest_response()
        {
            _orderHandlerMock.Setup(x => x.CreateOrderAsync(It.IsAny<OrderRequestDto>())).ThrowsAsync(new InvalidOperationException());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.CreateOrderAsync(_orderData);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task CreateOrderAsync__Any_problem_with_order_handling_occurred_due_to_internal_data_processing_error__Should_throw_InternalDbServiceException()
        {
            _orderHandlerMock.Setup(x => x.CreateOrderAsync(It.IsAny<OrderRequestDto>()));
            _orderHandlerMock.Setup(x => x.HandleOrderAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            Func<Task> action = async () => await controller.CreateOrderAsync(_orderData);

            await action.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task CreateOrderAsync__Any_problem_with_order_handling_occurred_due_to_unexpected_internal_error__Should_throw_Exception()
        {
            _orderHandlerMock.Setup(x => x.CreateOrderAsync(It.IsAny<OrderRequestDto>()));
            _orderHandlerMock.Setup(x => x.HandleOrderAsync()).ThrowsAsync(new Exception());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            Func<Task> action = async () => await controller.CreateOrderAsync(_orderData);

            await action.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task CreateOrderAsync__Order_handle_succeeded__Should_return_201Created_with_order_data()
        {
            string orderId = "orderid";
            var customerDto = new CustomerDto { Id = "1", DateOfBirth = _orderData.Customer.DateOfBirth, EmailAddress = _orderData.Customer.EmailAddress };
            var ticketDtos = new TicketDto[]
            {
                new TicketDto { Id = "1", Links = new ApiLink[] { new ApiLink("customer","customers/1", "GET") } },
                new TicketDto { Id = "1", Links = new ApiLink[] { new ApiLink("customer","customers/1", "GET") } }
            }.AsEnumerable();
            SetUpForSucceededOrderHandling(orderId, customerDto, ticketDtos);

            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.CreateOrderAsync(_orderData);

            (result as ObjectResult).StatusCode.Should().Be(201);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
            (((result as ObjectResult).Value as ResponseWrapper).Data as OrderResponseDto).Customer.Should().BeEquivalentTo(customerDto);
            (((result as ObjectResult).Value as ResponseWrapper).Data as OrderResponseDto).Tickets.Should().BeEquivalentTo(ticketDtos);
            (((result as ObjectResult).Value as ResponseWrapper).Data as OrderResponseDto).Id.Should().BeEquivalentTo(orderId);
        }

        #endregion

        #region GetOrderAsync(string id)
        // Malformed orderId - >  404 Bad Request
        // Customer not found -> 404 BadRequest, order doesn't exist without Customer
        // Tickets not found -> 404 BadReques, order doesn't exist without ordered tickets
        // Order found -> 200OK, return order data (customer, tickets and orderId)

        [Test]
        public async Task GetOrderAsync__Any_problem_with_order_handling_occurred_due_to_internal_data_processing_error__Should_throw_InternalDbServiceException()
        {
            _orderHandlerMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            _orderHandlerMock.Setup(x => x.GetOrderedTicketsAsync(It.IsAny<string>())).ReturnsAsync(Enumerable.Empty<Ticket>());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            Func<Task> action = async () => await controller.GetOrderAsync("1");

            await action.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetOrderAsync__Any_problem_with_order_handling_occurred_due_to_unexpected_internal_error__Should_throw_Exception()
        {
            _orderHandlerMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            _orderHandlerMock.Setup(x => x.GetOrderedTicketsAsync(It.IsAny<string>())).ReturnsAsync(Enumerable.Empty<Ticket>());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            Func<Task> action = async () => await controller.GetOrderAsync("1");

            await action.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetOrderAsync__OrderId_has_invalid_format__Should_return_400BadRequest_response()
        {
            _orderHandlerMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>())).ThrowsAsync(new FormatException());
            _orderHandlerMock.Setup(x => x.GetOrderedTicketsAsync(It.IsAny<string>())).ReturnsAsync(Enumerable.Empty<Ticket>());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetOrderAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetOrderAsync__Customer_not_found__Should_return_404NotFound_response()
        {
            _orderHandlerMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>())).ThrowsAsync(new InvalidOperationException());
            _orderHandlerMock.Setup(x => x.GetOrderedTicketsAsync(It.IsAny<string>())).ReturnsAsync(Enumerable.Empty<Ticket>());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetOrderAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetOrderAsync__Tickets_not_found__Should_return_404NotFound_response()
        {
            _orderHandlerMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>()));
            _orderHandlerMock.Setup(x => x.GetOrderedTicketsAsync(It.IsAny<string>())).ReturnsAsync(Enumerable.Empty<Ticket>());
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetOrderAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeEquivalentTo(new ApiError());
        }

        [Test]
        public async Task GetOrderAsync__Order_data_retrieve_succeeded__Should_return_200OK_response_with_data()
        {
            string orderId = "orderid";
            var customerDto = new CustomerDto { Id = "1", DateOfBirth = _orderData.Customer.DateOfBirth, EmailAddress = _orderData.Customer.EmailAddress };
            var ticketDtos = new TicketDto[]
            {
                new TicketDto { Id = "1", Links = new ApiLink[] { new ApiLink("customer","customers/1", "GET") } },
                new TicketDto { Id = "1", Links = new ApiLink[] { new ApiLink("customer","customers/1", "GET") } }
            }.AsEnumerable();
            _orderHandlerMock.Setup(x => x.GetCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Customer { Id = "1", EmailAddress = "sample@mail.com" });
            _orderHandlerMock.Setup(x => x.GetOrderedTicketsAsync(It.IsAny<string>())).ReturnsAsync(new Ticket[] { new Ticket { Id = "1" } });
            SetUpMapper(customerDto, ticketDtos);
            var controller = new OrdersController(_orderHandlerMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetOrderAsync(orderId);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
            (((result as ObjectResult).Value as ResponseWrapper).Data as OrderResponseDto).Customer.Should().BeEquivalentTo(customerDto);
            (((result as ObjectResult).Value as ResponseWrapper).Data as OrderResponseDto).Tickets.Should().BeEquivalentTo(ticketDtos);
            (((result as ObjectResult).Value as ResponseWrapper).Data as OrderResponseDto).Id.Should().BeEquivalentTo(orderId);
        }
        #endregion


        #region Privates

        private void SetUpForSucceededOrderHandling(string orderId, CustomerDto customerDto, IEnumerable<TicketDto> ticketDtos)
        {
            var customer = new Customer { Id = "1", DateOfBirth = _orderData.Customer.DateOfBirth, EmailAddress = _orderData.Customer.EmailAddress };
            var tickets = new Ticket[]
            {
                new Ticket { Id = "1", Customer = customer,  PurchaseDate = DateTime.Now },
                new Ticket { Id = "2", Customer = customer,  PurchaseDate = DateTime.Now }
            }.AsEnumerable();
            _orderHandlerMock.Setup(x => x.Customer).Returns(customer);
            _orderHandlerMock.Setup(x => x.Tickets).Returns(tickets);
            _orderHandlerMock.Setup(x => x.OrderId).Returns(orderId);
            _orderHandlerMock.Setup(x => x.CreateOrderAsync(It.IsAny<OrderRequestDto>()));
            _orderHandlerMock.Setup(x => x.HandleOrderAsync());
            SetUpMapper(customerDto, ticketDtos);
        }

        private void SetUpMapper(CustomerDto customerDto, IEnumerable<TicketDto> ticketDtos)
        {
            _mapperMock.Setup(x => x.Map<CustomerDto>(It.IsAny<Customer>())).Returns(customerDto);
            _mapperMock.Setup(x => x.Map<IEnumerable<TicketDto>>(It.IsAny<IEnumerable<Ticket>>())).Returns(ticketDtos);
        }

        #endregion

    }
}
