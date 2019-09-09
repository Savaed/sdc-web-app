using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Data.Validation;
using System.Net;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace SDCWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomApiController, IOrdersController
    {
        private const string ControllerPrefix = "orders";
        private readonly ApplicationDbContext _dbContext;
        private readonly IIndex<string, IServiceBase> _dbServiceFactory;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;


        public OrdersController(ApplicationDbContext dbContext, IIndex<string, IServiceBase> dbServiceFactory, ILogger<OrdersController> logger, IMapper mapper) : base(logger)
        {
            _dbContext = dbContext;
            _dbServiceFactory = dbServiceFactory;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously creates ticket order that contains info about customer and tickets.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="Discount"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="order">The <see cref="OrderDto"/> order representation to be created. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="DiscountDto"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAsync(OrderDto order)
        {
            _logger.LogInformation($"Starting method '{nameof(CreateOrderAsync)}'.");

            try
            {
                _logger.LogDebug("Creating customer based on data incoming from the ticket order.");
                var customer = CreateCustomer(order);
                _logger.LogDebug("Creating tickets based on data incoming from the ticket order.");
                var orderedTickets = await CreateTicketsAsync(order);            

                var customerDbService = (ICustomerDbService)_dbServiceFactory[nameof(ICustomerDbService)];
                var savedCustomer = await GetSavedCustomerAsync(customerDbService, customer);

                if (savedCustomer is null)
                {
                    _logger.LogDebug("Adding new customer to the database.");
                    savedCustomer = await customerDbService.AddAsync(customer);
                }

                AddCustomerToTickets(savedCustomer, orderedTickets);

                _logger.LogDebug("Adding ordered tickets to the database.");
                var addedTickets = await AddTicketsToDbAsync(orderedTickets);

                _logger.LogDebug("Mapping to DTOs.");
                var ticketsDto = MapToTicketDtoEnumerable(addedTickets);
                var customerDto = MapToCustomerDto(savedCustomer);

                // Generate order id from customer and ticket id. Format of this id is:
                //      encoded - Base64String (eg. MzU4ZDAxZDgtYzY1NC00MWQxLTliYzctYzk0OGE5M2RmNDFkO2NhMThlZGI1LTU3NzMtNGI5OC1hY2YyLTViMDk2ZDFmZGFhOA==)
                //      decoded - customerId;ticketId (eg. 358d01d8-c654-41d1-9bc7-c948a93df41d;ca18edb5-5773-4b98-acf2-5b096d1fdaa8)
                //string encodedOrderId = EncodeOrderId(customerDto.Id, ticketDto.Id);

                string orderUrl = $"{ControllerPrefix}/test";
                var response = new ResponseWrapper(new OrderDto { Id = "test", Customer = customerDto, Tickets = ticketsDto.ToList() });
                _logger.LogInformation($"Finished method '{nameof(CreateOrderAsync)}'");

                return Created(orderUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"{ex.Message}");
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(null, ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(string id)
        {
            throw new NotImplementedException();

            //try
            //{
            //    // Decode order id to customer and ticket id.
            //    // ids[0] - customer id.
            //    // ids[1] - ticket id.
            //    string[] ids = DecodeOrderId(id);

            //    var customer = await (_dbServiceFactory[nameof(ICustomerDbService)] as ICustomerDbService).GetAsync(ids[0]);
            //    var ticket = await (_dbServiceFactory[nameof(ITicketDbService)] as ITicketDbService).GetAsync(ids[1]);

            //    var customerDto = MapToCustomerDto(customer);
            //    var ticketDto = MapToTicketDto(ticket);

            //    var order = new OrderDto { Id = id, Customer = customerDto, Ticket = ticketDto };
            //    var response = new ResponseWrapper(order);

            //    return Ok(response);
            //}
            //catch (FormatException ex)
            //{
            //    return OnInvalidParameterError($"Passed '{nameof(id)}' has invalid format. {ex.Message}", ex);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return OnNotFoundError($"'{nameof(Customer)}' or '{nameof(Ticket)}' not found.", ex);
            //}
            //catch (InternalDbServiceException ex)
            //{
            //    LogInternalDbServiceException(null, ex);
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    LogUnexpectedException(ex);
            //    throw;
            //}
        }


        #region Privates

        private void AddCustomerToTickets(Customer customer, IEnumerable<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                ticket.Customer = customer;
            }
        }


        private async Task<IEnumerable<Ticket>> AddTicketsToDbAsync(IEnumerable<Ticket> tickets)
        {
            List<Ticket> tmpTickets = new List<Ticket>();

            foreach (var ticket in tickets)
            {
                tmpTickets.Add(await (_dbServiceFactory[nameof(ITicketDbService)] as ITicketDbService).AddAsync(ticket));
            }

            return tmpTickets.AsEnumerable();
        }


        private string EncodeOrderId(string customerId, string ticketId)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{customerId};{ticketId}"));
        }

        private string[] DecodeOrderId(string encodedOrderId)
        {
            try
            {
                string decodedOrderId = Encoding.UTF8.GetString(Convert.FromBase64String(encodedOrderId));
                return decodedOrderId.Split(';', StringSplitOptions.RemoveEmptyEntries);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, $"Encoding failed. Invalid format. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Encoding failed. {ex.Message}");
                throw;
            }
        }

        private IEnumerable<TicketDto> MapToTicketDtoEnumerable(IEnumerable<Ticket> tickets) => _mapper.Map<IEnumerable<TicketDto>>(tickets);        

        private TicketDto MapToTicketDto(Ticket ticket) => _mapper.Map<TicketDto>(ticket);

        private CustomerDto MapToCustomerDto(Customer customer) => _mapper.Map<CustomerDto>(customer);

        private async Task<IEnumerable<Ticket>> CreateTicketsAsync(OrderDto order)
        {
            List<Ticket> tickets = new List<Ticket>();

            foreach (var ticket in order.Tickets)
            {
                tickets.Add(new Ticket
                {
                    PurchaseDate = ticket.PurchaseDate,
                    TicketUniqueId = Guid.NewGuid().ToString(),
                    ValidFor = ticket.ValidFor,

                    Discount = await (_dbServiceFactory[nameof(IDiscountDbService)] as IDiscountDbService).GetAsync(ExtractIdFromTicketLinks(ticket, nameof(Discount))),
                    Tariff = await (_dbServiceFactory[nameof(ITicketTariffDbService)] as ITicketTariffDbService).GetAsync(ExtractIdFromTicketLinks(ticket, nameof(TicketTariff))),
                    Group = await (_dbServiceFactory[nameof(ISightseeingGroupDbService)] as ISightseeingGroupDbService).GetAsync(ExtractIdFromTicketLinks(ticket, nameof(SightseeingGroup)))
                });
            }

            return tickets;
        }

        private Customer CreateCustomer(OrderDto order)
        {
            return new Customer
            {
                DateOfBirth = order.Customer.DateOfBirth,
                EmailAddress = order.Customer.EmailAddress,
                HasFamilyCard = order.Customer.HasFamilyCard,
                IsChild = order.Customer.IsChild,
                IsDisabled = order.Customer.IsDisabled
            };
        }

        private async Task<Customer> GetSavedCustomerAsync(ICustomerDbService customerDbService, Customer customer)
        {
            var result = await customerDbService.GetByAsync(c => c.EmailAddress.Equals(customer.EmailAddress));
            return result.Count() == 0 ? null : result.Single();
        }

        private string ExtractIdFromTicketLinks(TicketDto ticket, string resourceName)
        {
            return ticket.Links.Single(link => link.Rel.Equals(resourceName, StringComparison.InvariantCultureIgnoreCase)).Href.Split('/')[1];
        }

        #endregion

    }
}