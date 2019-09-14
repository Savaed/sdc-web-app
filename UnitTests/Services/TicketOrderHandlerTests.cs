using Autofac.Features.Indexed;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace UnitTests.Services
{
    [TestFixture]
    public class TicketOrderHandlerTests
    {
        private Mock<IIndex<string, IServiceBase>> _dbServiceFactoryMock;
        private Mock<ISightseeingGroupDbService> _groupDbServiceMock;
        private Mock<ITicketDbService> _ticketDbServiceMock;
        private Mock<ICustomerDbService> _customerDbServiceMock;
        private Mock<IDiscountDbService> _discountDbServiceMock;
        private Mock<ITicketTariffDbService> _ticketTariffDbServiceMock;
        private Mock<IGeneralSightseeingInfoDbService> _infoDbServiceMock;
        private Mock<IValidator<SightseeingGroup>> _validatorMock;
        private ILogger<TicketOrderHandler> _logger;
        private Customer _customer;
        private Ticket _ticket;
        private TicketTariff _ticketTariff;
        private Discount _discount;
        private ShallowTicket _shallowTicket;


        [OneTimeSetUp]
        public void SetUp()
        {
            _validatorMock = new Mock<IValidator<SightseeingGroup>>();
            _logger = Mock.Of<ILogger<TicketOrderHandler>>();

            var info = new GeneralSightseeingInfo
            {
                Id = "1",
                Description = "recent sightseeing info",
                MaxAllowedGroupSize = 30,
                MaxChildAge = 5,
                MaxTicketOrderInterval = 4,
                SightseeingDuration = 2,
                OpeningHours = new OpeningHours[] { }
            };
            _infoDbServiceMock = new Mock<IGeneralSightseeingInfoDbService>();
            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new GeneralSightseeingInfo[] { info }.AsEnumerable());

            _ticketTariff = new TicketTariff { Id = "1", DefaultPrice = 20, Description = "ticket tariff" };
            _ticketTariffDbServiceMock = new Mock<ITicketTariffDbService>();

            _discount = new Discount { Id = "1", Description = "discount", DiscountValueInPercentage = 20, Type = Discount.DiscountType.ForChild };
            _discountDbServiceMock = new Mock<IDiscountDbService>();

            _ticket = new Ticket
            {
                Id = "1",
                OrderStamp = "stamp",
                PurchaseDate = DateTime.Now.AddDays(-2),
                TicketUniqueId = "uniqu_id"
            };
            _ticketDbServiceMock = new Mock<ITicketDbService>();

            _customer = new Customer
            {
                Id = "1",
                DateOfBirth = DateTime.Now.AddYears(-30),
                EmailAddress = "sample@mail.com",
                HasFamilyCard = false,
                IsChild = false,
                IsDisabled = true,
                Tickets = new Ticket[] { _ticket }
            };
            _customerDbServiceMock = new Mock<ICustomerDbService>();

            _groupDbServiceMock = new Mock<ISightseeingGroupDbService>();
            _dbServiceFactoryMock = new Mock<IIndex<string, IServiceBase>>();

            _shallowTicket = new ShallowTicket { DiscountId = "1", TicketTariffId = "1", SightseeingDate = DateTime.Now.AddDays(3).Date.AddHours(14) };

            _dbServiceFactoryMock.Setup(x => x["ICustomerDbService"]).Returns(_customerDbServiceMock.Object);
            _dbServiceFactoryMock.Setup(x => x["IDiscountDbService"]).Returns(_discountDbServiceMock.Object);
            _dbServiceFactoryMock.Setup(x => x["ITicketTariffDbService"]).Returns(_ticketTariffDbServiceMock.Object);
            _dbServiceFactoryMock.Setup(x => x["IGeneralSightseeingInfoDbService"]).Returns(_infoDbServiceMock.Object);
            _dbServiceFactoryMock.Setup(x => x["ISightseeingGroupDbService"]).Returns(_groupDbServiceMock.Object);
            _dbServiceFactoryMock.Setup(x => x["ITicketDbService"]).Returns(_ticketDbServiceMock.Object);
        }


        #region CreateOrderAsync(OrderRequestDto order)
        // Argument order is null -> throw ArgumentNullException
        // Property Tickets is null -> throw ArgumentException
        // Property Customer is null -> throw ArgumentException
        // Problem with get saved customer -> throw InvaidOprationException or InternalDbServiceException
        // Create succeeded -> property return Customer and Tickets customer who ordered tickets and tickets

        [Test]
        public async Task CreateOrderAsync__Argument_order_is_null__Should_throw_ArgumentNullException()
        {
            var order = new OrderRequestDto { Id = "1", Customer = new CustomerDto(), Tickets = new ShallowTicket[] { } };
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.CreateOrderAsync(null);

            await action.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Test]
        public async Task CreateOrderAsync__Tickets_in_argument_order_is_null__Should_throw_ArgumentException()
        {
            var order = new OrderRequestDto { Id = "1", Customer = null, Tickets = new ShallowTicket[] { } };
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.CreateOrderAsync(order);

            await action.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task CreateOrderAsync__Customer_in_argument_order_is_null__Should_throw_ArgumentException()
        {
            var order = new OrderRequestDto { Id = "1", Customer = null, Tickets = new ShallowTicket[] { _shallowTicket } };
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.CreateOrderAsync(order);

            await action.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task CreateOrderAsync__Cannot_get_saved_customer__Should_throw_InvalidOperationException()
        {
            var order = new OrderRequestDto { Id = "1", Customer = new CustomerDto(), Tickets = new ShallowTicket[] { _shallowTicket } };
            _customerDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ThrowsAsync(new InvalidOperationException());
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.CreateOrderAsync(order);

            await action.Should().ThrowExactlyAsync<InvalidOperationException>();
        }

        [Test]
        public async Task CreateOrderAsync__Order_create_succeeded__Property_customer_should_return_correct_data()
        {
            // Property should return Customer who ordered tickets.
            // This method set Customer property.
            var tickets = new ShallowTicket[] { _shallowTicket };
            var order = new OrderRequestDto { Id = "1", Customer = new CustomerDto(), Tickets = tickets };
            _customerDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(new Customer[] { _customer });
            _customerDbServiceMock.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync(_customer);
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            await handler.CreateOrderAsync(order);

            handler.Customer.Should().BeEquivalentTo(_customer);
        }

        #endregion

        #region HandleOrderAsync()
        // Attempt creating new group with number of tickets greater than max allowed -> throw InvalidOperationException
        // Attempt updating existent group by adding number of tickets greater than available places -> throw InvalidOperationException
        // Add ticket to group succeeded -> property Ticket contains those tickets

        // NOTE I'm not sure this is the best idea to test method using another one to set required, private variable.
        // In this case HandleOrderAsync() is using variables that had set before by CreateOrderAsync().
        // Without calls CreateOrderAsync(), a HandleOrderAsync() method is not able to do their job, so
        // I decide to callse CreateOrderAsync() before in this test as well as all next HandleOrderAsync() tests.

        [Test]
        public async Task HandleOrderAsync__Attempt_to_create_new_group_with_to_many_tickets__Should_throw_InvalidOperationException()
        {
            // Attempt adding 2 tickets to new created group, but only 1 place is available.
            SetUpForFailedCreateNewGroup();

            var order = new OrderRequestDto
            {
                Customer = new CustomerDto { EmailAddress = "sample@mail.com" },
                Tickets = new ShallowTicket[]
                {
                    new ShallowTicket { DiscountId = "1", SightseeingDate = DateTime.Now.AddDays(1).Date.AddHours(14), TicketTariffId = "1"},
                    new ShallowTicket { DiscountId = "2", SightseeingDate = DateTime.Now.AddDays(1).Date.AddHours(14), TicketTariffId = "2"}
                }
            };
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            // Set _shallowTickets and _customer private variables.
            await handler.CreateOrderAsync(order);

            Func<Task> action = async () => await handler.HandleOrderAsync();

            await action.Should().ThrowExactlyAsync<InvalidOperationException>();
        }

        [Test]
        public async Task HandleOrderAsync__Attempt_to_update_existent_group_by_adding_to_many_tickets__Should_throw_InvalidOperationException()
        {
            // Attempt to update existent group by adding 2 tickets , but only 1 place is available in existent group.
            var sightseeingDate = DateTime.Now.AddDays(1).Date.AddHours(12);
            SetUpForFailedUpdate(sightseeingDate);

            var order = new OrderRequestDto
            {
                Customer = new CustomerDto { EmailAddress = "sample@mail.com" },
                Tickets = new ShallowTicket[]
                {
                     new ShallowTicket { DiscountId = "1", SightseeingDate = sightseeingDate, TicketTariffId = "1"},
                     new ShallowTicket { DiscountId = "2", SightseeingDate = sightseeingDate, TicketTariffId = "2"}
                }
            };
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            // Set _shallowTickets and _customer private variables.
            await handler.CreateOrderAsync(order);

            Func<Task> action = async () => await handler.HandleOrderAsync();

            await action.Should().ThrowExactlyAsync<InvalidOperationException>();
        }

        [Test]
        public async Task HandleOrderAsync__Order_handle_succeeded__Should_not_throw_any_Exception()
        {
            // Attempt to update existent group by adding 2 tickets and exactly 2 places are available.
            var sightseeingDate = DateTime.Now.AddDays(1).Date.AddHours(12);
            SetUpForSucceededOrderHandle(sightseeingDate, new Ticket[] { _ticket });

            var order = new OrderRequestDto
            {
                Customer = new CustomerDto { EmailAddress = "sample@mail.com" },
                Tickets = new ShallowTicket[]
                {
                     new ShallowTicket { DiscountId = "1", SightseeingDate = sightseeingDate, TicketTariffId = "1"},
                     new ShallowTicket { DiscountId = "2", SightseeingDate = sightseeingDate, TicketTariffId = "2"}
                }
            };
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            // Set _shallowTickets and _customer private variables.
            await handler.CreateOrderAsync(order);

            Func<Task> action = async () => await handler.HandleOrderAsync();

            await action.Should().NotThrowAsync<Exception>();
            handler.Tickets.Should().NotBeEmpty();
        }

        #endregion

        #region GetOrderedTicketsAsync(string orderId)
        // Argument orderId is null -> throw ArgumentNullException
        // Argument orderId has invalid format -> throw FormatException
        // Tickets from order not found -> return empty IEnumerable<Ticket>
        // Tickets from order found -> return IEnumerable<Ticket> with these tickets

        [Test]
        public async Task GetOrderedTicketsAsync__Argument_order_id_has_invalid_format__Should_throw_FormatException()
        {
            string orderId = "1234/23";
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.GetOrderedTicketsAsync(orderId);

            await action.Should().ThrowExactlyAsync<FormatException>();
        }

        [Test]
        public async Task GetOrderedTicketsAsync__Argument_order_id_is_null__Should_throw_ArgumentNullException()
        {
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.GetOrderedTicketsAsync(null);

            await action.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Test]
        public async Task GetOrderedTicketsAsync__Tickets_from_order_not_found__Should_return_empty_IEnumerable()
        {
            _ticketDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Ticket, bool>>>())).ReturnsAsync(Enumerable.Empty<Ticket>());
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            var result = await handler.GetOrderedTicketsAsync("eW91O2dvdHRoaXM=");

            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetOrderedTicketsAsync__Tickets_from_order_found__Should_return_not_empty_IEnumerable()
        {
            _ticketDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Ticket, bool>>>())).ReturnsAsync(new Ticket[] { _ticket }.AsEnumerable());
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            var result = await handler.GetOrderedTicketsAsync("eW91O2dvdHRoaXM=");

            result.Should().NotBeEmpty();
        }

        #endregion

        #region GetCustomerAsync(string orderId)
        // Argument orderId is null -> throw ArgumentNullException
        // Argument orderId has invalid format -> throw FormatException
        // Customer from order not found -> return null
        // Customer from order found -> return this customer

        [Test]
        public async Task GetCustomerAsync__Argument_order_id_has_invalid_format__Should_throw_FormatException()
        {
            string orderId = "1234/23";
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.GetCustomerAsync(orderId);

            await action.Should().ThrowExactlyAsync<FormatException>();
        }

        [Test]
        public async Task GetCustomerAsync__Argument_order_id_is_null__Should_throw_ArgumentNullException()
        {
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            Func<Task> action = async () => await handler.GetCustomerAsync(null);

            await action.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Test]
        public async Task GetCustomerAsync__Customer_from_order_not_found__Should_return_null()
        {
            _customerDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InvalidOperationException());
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            var result = await handler.GetCustomerAsync("eW91O2dvdHRoaXM=");

            result.Should().BeNull();
        }

        [Test]
        public async Task GetCustomerAsync__Customer_from_order_found__Should_return_this_customer()
        {
            _customerDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(_customer);
            var handler = new TicketOrderHandler(_dbServiceFactoryMock.Object, _validatorMock.Object, _logger);

            var result = await handler.GetCustomerAsync("eW91O2dvdHRoaXM=");

            result.Should().BeEquivalentTo(_customer);
        }

        #endregion


        #region Privates

        private void SetUpForFailedUpdate(DateTime sightseeingDate)
        {
            var info = new GeneralSightseeingInfo { MaxAllowedGroupSize = 1 };
            var existentGroup = new SightseeingGroup { MaxGroupSize = 1, SightseeingDate = sightseeingDate, Tickets = new Ticket[] { _ticket } };

            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new GeneralSightseeingInfo[] { info });

            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(new SightseeingGroup[] { existentGroup });

            // Only for CreateOrderAsync() purposes.
            _customerDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(new Customer[] { _customer });
        }

        private void SetUpForFailedCreateNewGroup()
        {
            var info = new GeneralSightseeingInfo { MaxAllowedGroupSize = 1 };

            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new GeneralSightseeingInfo[] { info });

            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(Enumerable.Empty<SightseeingGroup>());

            // Only for CreateOrderAsync() purposes.
            _customerDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(new Customer[] { _customer });
        }

        private void SetUpForSucceededOrderHandle(DateTime sightseeingDate, IEnumerable<Ticket> tickets)
        {
            var info = new GeneralSightseeingInfo { MaxAllowedGroupSize = 3 };
            var existentGroup = new SightseeingGroup { MaxGroupSize = info.MaxAllowedGroupSize, SightseeingDate = sightseeingDate, Tickets = new Ticket[] { _ticket } };
            var newGroup = new SightseeingGroup { MaxGroupSize = info.MaxAllowedGroupSize, SightseeingDate = sightseeingDate, Tickets = (ICollection<Ticket>)tickets };

            _infoDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new GeneralSightseeingInfo[] { info });

            _groupDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<SightseeingGroup, bool>>>())).ReturnsAsync(new SightseeingGroup[] { existentGroup });
            _groupDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<SightseeingGroup>())).ReturnsAsync(newGroup);
            _groupDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<SightseeingGroup>())).ReturnsAsync(existentGroup);

            _discountDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(_discount);

            _ticketTariffDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(_ticketTariff);

            // Only for CreateOrderAsync() purposes.
            _customerDbServiceMock.Setup(x => x.GetByAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(new Customer[] { _customer });
        }

        #endregion

    }
}
