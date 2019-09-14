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
    /// Provides methods to Http verbs proccessing on <see cref="Ticket"/> entities.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class TicketsController : CustomApiController, ITicketsController
    {
        private readonly ILogger<TicketsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITicketDbService _ticketDbService;


        public TicketsController(ITicketDbService ticketDbService, ILogger<TicketsController> logger, IMapper mapper) : base(logger)
        {
            _ticketDbService = ticketDbService;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{Ticket}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{Ticket}"/>.</returns>
        [HttpGet("tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTicketsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllTicketsAsync)}'.");

            try
            {
                var tickets = await _ticketDbService.GetAllAsync();
                var ticketDtos = MapToDtoEnumerable(tickets);
                var response = new ResponseWrapper(ticketDtos);
                _logger.LogInformation($"Finished method '{nameof(GetAllTicketsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _ticketDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="Ticket"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="Ticket"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Ticket"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searched <see cref="Ticket"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="Ticket"/>.</returns>
        [HttpGet("tickets/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTicketAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetTicketAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");
            }

            try
            {
                var ticket = await _ticketDbService.GetAsync(id);
                var ticketDto = MapToDto(ticket);
                var response = new ResponseWrapper(ticketDto);
                _logger.LogInformation($"Finished method '{nameof(GetTicketAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _ticketDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="Ticket"/> belonged to the <see cref="Customer"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="Ticket"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Customer"/> or <see cref="Ticket"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketId">The id of searched <see cref="Ticket"/>. Cannot be null or empty.</param>
        /// <param name="customerId">The id of specified <see cref="Customer"/> that owns searched <see cref="Ticket"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="Ticket"/>.</returns>
        [HttpGet("customers/{customerId:required}/tickets/{ticketId:required}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerTicketAsync(string customerId, string ticketId)
        {
            _logger.LogInformation($"Starting method '{nameof(GetCustomerTicketsAsync)}'.");

            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(ticketId))
            {
                return OnInvalidParameterError($"Parameter '{customerId}' and '{ticketId}' cannot be null or empty.");
            }

            IEnumerable<Ticket> allCustomerTickets = new Ticket[] { }.AsEnumerable();

            try
            {
                allCustomerTickets = await _ticketDbService.GetByAsync(x => x.Customer.Id.Equals(customerId));
                if (allCustomerTickets.Count() == 0)
                {
                    // The customer must have at least one ticket because it is created if it has ordered at least one ticket.
                    // If none ticket found then customer with passed id doesn't exist.
                    return OnNotFoundError($"Element '{nameof(Customer)}' with specified id: '{customerId}' not found.");
                }

                var customersSearchedTicket = allCustomerTickets.Single(x => x.Id.Equals(ticketId));
                _logger.LogDebug("Data retrieved succeeded");
                var customersSearchedTicketDto = MapToDto(customersSearchedTicket);
                var response = new ResponseWrapper(customersSearchedTicketDto);
                _logger.LogInformation($"Finished method '{nameof(GetCustomerTicketsAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex) when (allCustomerTickets.Count() > 0)
            {
                // If at least one ticket found, but allCustomerTickets.Single(ticketId) thrown exception, specified customer exists, but the ticket not.
                return OnNotFoundError($"Element '{nameof(Ticket)}' with specified id: '{ticketId}' not found.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _ticketDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{Ticket}"/> that belonges to the <see cref="Customer"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the <see cref="Ticket"/> tickets found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Customer"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="customerId">The id of specified <see cref="Customer"/> that owns searched tickets. Cannot be null or empty.</param>
        /// <returns>An <see cref="IEnumerable{Ticket}"/> that belongs to the <see cref="Customer"/>.</returns>
        [HttpGet("customers/{customerId:required}/tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerTicketsAsync(string customerId)
        {
            _logger.LogInformation($"Starting method '{nameof(GetCustomerTicketsAsync)}'.");

            if (string.IsNullOrEmpty(customerId))
            {
                return OnInvalidParameterError($"Parameter '{customerId}' cannot be null or empty.");
            }

            try
            {
                var allCustomerTickets = await _ticketDbService.GetByAsync(x => x.Customer.Id.Equals(customerId));
                if (allCustomerTickets.Count() == 0)
                {
                    // The customer must have at least one ticket because it is created if it has ordered at least one ticket.
                    // If none ticket found then customer with passed id doesn't exist.
                    return OnNotFoundError($"Element '{nameof(Customer)}' with specified id: '{customerId}' not found.");
                }

                _logger.LogDebug("Data retrieved succeeded");
                var allCustomerTicketsDto = MapToDtoEnumerable(allCustomerTickets);
                var response = new ResponseWrapper(allCustomerTicketsDto);
                _logger.LogInformation($"Finished method '{nameof(GetCustomerTicketsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _ticketDbService.GetType());
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
        private TicketDto MapToDto(Ticket ticket) => _mapper.Map<TicketDto>(ticket);

        #endregion

    }
}