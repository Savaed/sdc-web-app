using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides methods to Http verbs to maintain ticket orders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomApiController, IOrdersController
    {
        private const string ControllerPrefix = "orders";
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly ITicketOrderHandler _orderHandler;


        public OrdersController(ITicketOrderHandler orderHandler, ILogger<OrdersController> logger, IMapper mapper) : base(logger)
        {
            _orderHandler = orderHandler;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Asynchronously creates ticket order that contains info about customer and ordered tickets.
        /// Returns <see cref="HttpStatusCode.Created"/> response if order create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="order">The <see cref="OrderRequestDto"/> order representation to be created. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="DiscountDto"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAsync(OrderRequestDto order)
        {
            _logger.LogInformation($"Starting method '{nameof(CreateOrderAsync)}'.");

            try
            {
                await _orderHandler.CreateOrderAsync(order);
                await _orderHandler.HandleOrderAsync();
                var customer = _orderHandler.Customer;
                var orderedTickets = _orderHandler.Tickets;
                string orderId = _orderHandler.OrderId;

                _logger.LogDebug("Mapping to DTOs.");
                var orderedTicketsDto = MapToDtoEnumerable(orderedTickets);
                var customerDto = MapToDto(customer);

                string orderUrl = $"{ControllerPrefix}/{orderId}";
                var response = new ResponseWrapper(new OrderResponseDto { Id = orderId, Customer = customerDto, Tickets = orderedTicketsDto });
                _logger.LogInformation($"Finished method '{nameof(CreateOrderAsync)}'");

                return Created(orderUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                return OnInvalidParameterError($"The order cannot be properly handled. {ex.Message}");
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves data about ordered <see cref="Ticket"/> tickets and <see cref="Customer"/> customer associated with this particular order.
        /// Returns <see cref="HttpStatusCode.OK"/> if retrieve succeeded, <see cref="HttpStatusCode.BadRequest"/> if the passed id is invalid and
        /// <see cref="HttpStatusCode.NotFound"/> if cannot found customer and tickets related to the order with the given <paramref name="id"/>.</summary>
        /// <param name="id">The ticket order ID.</param>
        /// <returns><see cref="OrderResponseDto"/> that contain data about ticket order.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderAsync(string id)
        {
            try
            {
                var customer = await _orderHandler.GetCustomerAsync(id);
                var tickets = await _orderHandler.GetOrderedTicketsAsync(id);

                if (tickets.Count() == 0)
                {
                    return OnNotFoundError($"No tickets found related to the order with the given ID: '{id}'.");
                }

                var customerDto = MapToDto(customer);
                var ticketDto = MapToDtoEnumerable(tickets);

                var order = new OrderResponseDto { Id = id, Customer = customerDto, Tickets = ticketDto };
                var response = new ResponseWrapper(order);

                return Ok(response);
            }
            catch (ArgumentNullException ex)
            {
                return OnInvalidParameterError($"Passed '{nameof(id)}' is invalid", ex);
            }
            catch (FormatException ex)
            {
                return OnInvalidParameterError($"Passed '{nameof(id)}' has invalid format. {ex.Message}", ex);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"'{nameof(Customer)}' or '{nameof(Ticket)}' not found.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private IEnumerable<TicketDto> MapToDtoEnumerable(IEnumerable<Ticket> tickets) => _mapper.Map<IEnumerable<TicketDto>>(tickets);

        private CustomerDto MapToDto(Customer customer) => _mapper.Map<CustomerDto>(customer);

        #endregion

    }
}