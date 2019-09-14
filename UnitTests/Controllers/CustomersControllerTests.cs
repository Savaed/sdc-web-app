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
    public class CustomersControllerTests
    {
        private Mock<ICustomerDbService> _customerDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<CustomersController> _logger;
        private Customer _validCustomer;
        private Customer[] _customers;
        private CustomerDto[] _customerDtos;


        [OneTimeSetUp]
        public void SetUp()
        {
            _customerDbServiceMock = new Mock<ICustomerDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<CustomersController>>();
            _validCustomer = new Customer
            {
                Id = "15891fb0-faec-43c6-9e83-04a4a17c3660",
                DateOfBirth = DateTime.Now.AddYears(-23),
                EmailAddress = "sample@mail.com"
            };

            _customers = new Customer[]
            {
                new Customer { Id = "1", UpdatedAt = DateTime.MinValue, EmailAddress = "example@mail.com" },
                new Customer { Id = "2", UpdatedAt = DateTime.Now.AddMinutes(30), EmailAddress = "email@mail.com" }
            };

            _customerDtos = new CustomerDto[]
            {
                new CustomerDto { Id = "1", DateOfBirth = DateTime.Now.AddYears(-63), UpdatedAt = null, EmailAddress = "other_example@mail.com" },
                new CustomerDto { Id = "2", UpdatedAt = DateTime.Now.AddMinutes(30), EmailAddress = "other_other_example@mail.com" }
            };
        }


        #region GetCustomerAsync(string id) 
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Specified ticket not found -> 404NotFound
        // Argument id is null or empty -> 400BadRequest
        // Ticket found -> 200Ok, return this ticket 

        [Test]
        public async Task GetCustomerAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _customerDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetCustomerAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetCustomerAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _customerDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetCustomerAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetCustomerAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _customerDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetCustomerAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetCustomerAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _customerDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetCustomerAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetCustomerAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _customerDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validCustomer);
            _mapperMock.Setup(x => x.Map<CustomerDto>(It.IsNotNull<Customer>())).Returns(new CustomerDto { Id = id, EmailAddress = "newmapped@Customer.com" });
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetCustomerAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region GetAllCustomersAsync();
        // Internal error refferd to the db -> throw InternalDbServiceException
        // Unexpected internal error -> throw Exception
        // Resource is empty -> 200Ok, return empty IEnumerable
        // At least one ticket found -> 200Ok, return not empty IEnumerable        

        [Test]
        public async Task GetAllCustomerAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _customerDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllCustomersAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllCustomerAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _customerDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllCustomersAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllCustomerAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _customerDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Customer>());
            _mapperMock.Setup(x => x.Map<IEnumerable<CustomerDto>>(It.IsNotNull<IEnumerable<Customer>>())).Returns(Enumerable.Empty<CustomerDto>());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllCustomersAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<CustomerDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllCustomerAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _customerDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_customers);
            _mapperMock.Setup(x => x.Map<IEnumerable<CustomerDto>>(It.IsNotNull<IEnumerable<Customer>>())).Returns(_customerDtos.AsEnumerable());
            var controller = new CustomersController(_customerDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllCustomersAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<CustomerDto>).Should().NotBeEmpty();
        }

        #endregion

    }
}
