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
    /// Provides methods to Http verbs proccessing on <see cref="TicketTariff"/> entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTariffsController : CustomApiController, ITicketTariffsController
    {
        private const string ControllerPrefix = "ticket-tariffs";
        private readonly ILogger<TicketTariffsController> _logger;
        private readonly ITicketTariffDbService _ticketTariffDbService;
        private readonly IVisitTariffDbService _sightseeingTariffDbService;
        private readonly IMapper _mapper;


        public TicketTariffsController(ITicketTariffDbService ticketTariffDbService, IVisitTariffDbService sightseeingTariffDbService, ILogger<TicketTariffsController> logger, IMapper mapper) : base(logger)
        {
            _mapper = mapper;
            _logger = logger;
            _ticketTariffDbService = ticketTariffDbService;
            _sightseeingTariffDbService = sightseeingTariffDbService;
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/> from parent <see cref="SightseeingTariff"/>
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="sightseeingTariffId">The <see cref="SightseeingTariff"/> parent id.</param>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        [HttpGet]
        [Route("api/sightseeing-tariffs/{sightseeingTariffId:required}/ticket-tariffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTicketTariffsFromSightseeingTariffAsync(string sightseeingTariffId)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllTicketTariffsFromSightseeingTariffAsync)}'.");

            if (string.IsNullOrEmpty(sightseeingTariffId))
            {
                return OnInvalidParameterError($"Parameter '{nameof(sightseeingTariffId)}' cannot be null or empty.");
            }

            try
            {
                var sightseeingTariff = await _sightseeingTariffDbService.GetAsync(sightseeingTariffId);
                var ticketTariffs = sightseeingTariff.TicketTariffs.ToArray();
                var ticketTariffDtos = MapToDtoEnumerable(ticketTariffs);
                var response = new ResponseWrapper(ticketTariffDtos);
                _logger.LogInformation($"Finished method '{nameof(GetAllTicketTariffsFromSightseeingTariffAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{sightseeingTariffId}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _sightseeingTariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }

        }

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> to parent <see cref="SightseeingTariff"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="TicketTariff"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariff">The <see cref="TicketTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An added <see cref="TicketTariffDto"/>.</returns>
        [HttpPost]
        [Route("sightseeing-tariffs/{sightseeingTariffId:required}/ticket-tariffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTicketTariffToSightseeingTariffAsync(string sightseeingTariffId, [FromBody] TicketTariffDto ticketTariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddTicketTariffToSightseeingTariffAsync)}'.");

            if (string.IsNullOrEmpty(sightseeingTariffId))
            {
                return OnInvalidParameterError($"Parameter '{nameof(sightseeingTariffId)}' cannot be null or empty.");
            }

            TicketTariff ticketTariffToBeAdded = null;
            VisitTariff sightseeingTariff = null;








            // TODO      JAKOS TO ZMIENIC ZEBY MIALO RECE I NOGI BO TERAZ TO TROCHE LIPA











            try
            {
                sightseeingTariff = await _sightseeingTariffDbService.GetAsync(sightseeingTariffId);

                // Ignore Id if the client set it. Id of entity is set internally by the server.
                ticketTariff.Id = null;

                ticketTariffToBeAdded = MapToDomainModel(ticketTariff);
                var addedTicketTariff = await _ticketTariffDbService.RestrictedAddAsync(ticketTariffToBeAdded);

                // Reverse map only for response to the client.
                var addedTariffDto = MapToDto(addedTicketTariff);
                var response = new ResponseWrapper(addedTariffDto);
                string addedTicketTariffUrl = $"{ControllerPrefix}/{addedTicketTariff.Id}";
                _logger.LogInformation($"Finished method '{nameof(AddTicketTariffToSightseeingTariffAsync)}'.");
                return Created(addedTicketTariffUrl, response);
            }
            catch (InvalidOperationException)
            {
                // TicketTariff already exists. Add it to the sightseeingTariff but not to the db.
                var existingTariff = _ticketTariffDbService.GetAllAsync().Result.Single(x => x.Equals(ticketTariffToBeAdded));
                _logger.LogInformation($"Already there is the same '{typeof(TicketTariff).Name}' in the database. Id: '{existingTariff.Id}'.");

                var response = new ResponseWrapper(existingTariff);
                _logger.LogInformation($"Finished method '{nameof(AddTicketTariffToSightseeingTariffAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _ticketTariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes specified <see cref="TicketTariff"/> by <paramref name="ticketTariffId"/> from <see cref="SightseeingTariff"/> parent.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> delete succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="TicketTariff"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The id of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        [Route("sightseeing-tariffs/{sightseeingTariffId:required}/ticket-tariffs/{ticketTariffId:required}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicketTariffFromSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId)
        {
            throw new NotImplementedException();

            //_logger.LogInformation($"Starting method '{nameof(DeleteTicketTariffFromSightseeingTariffAsync)}'.");

            //if (string.IsNullOrEmpty(ticketTariffId))
            //    return OnInvalidParameterError($"Parameter '{nameof(ticketTariffId)}' cannot be null or empty.");

            //try
            //{
            //    await _ticketTariffDbService.DeleteAsync(ticketTariffId);
            //    var response = new ResponseWrapper();
            //    _logger.LogInformation($"Finished method '{nameof(DeleteTicketTariffFromSightseeingTariffAsync)}'.");
            //    return Ok(response);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return OnNotFoundError($"Cannot found element '{typeof(TicketTariff).Name}' with specified id: '{ticketTariffId}'.", ex);
            //}
            //catch (InternalDbServiceException ex)
            //{
            //    LogInternalDbServiceException(_ticketTariffDbService.GetType(), ex);
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    LogUnexpectedException(ex);
            //    throw;
            //}
        }

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTicketTariffsAsync()
        {
            throw new NotImplementedException();

            //_logger.LogInformation($"Starting method '{nameof(GetAllTicketTariffsAsync)}'.");

            //try
            //{
            //    var ticketTariffs = await _ticketTariffDbService.GetAllAsync();
            //    var ticketTariffDtos = MapToDtoEnumerable(ticketTariffs);
            //    var response = new ResponseWrapper(ticketTariffDtos);
            //    _logger.LogInformation($"Finished method '{nameof(GetAllTicketTariffsAsync)}'.");
            //    return Ok(response);
            //}
            //catch (InternalDbServiceException ex)
            //{
            //    LogInternalDbServiceException(_ticketTariffDbService.GetType(), ex);
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    LogUnexpectedException(ex);
            //    throw;
            //}
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="TicketTariff"/> by <paramref name="id"/> from <see cref="SightseeingTariff"/> parent.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="TicketTariff"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The id of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="TicketTariff"/>.</returns>
        [HttpGet("sightseeing-tariffs/{sightseeingTariffId:required}/ticket-tariffs/{ticketTariffId:required}")]
        [Route("")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTicketTariffFromSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId)
        {
            throw new NotImplementedException();

            //_logger.LogInformation($"Starting method '{nameof(GetTicketTariffAsync)}'.");

            //if (string.IsNullOrEmpty(ticketTariffId))
            //    return OnInvalidParameterError($"Parameter '{nameof(ticketTariffId)}' cannot be null or empty.");

            //try
            //{
            //    var ticketTariff = await _ticketTariffDbService.GetAsync(ticketTariffId);
            //    var ticketTariffDto = MapToDto(ticketTariff);
            //    var response = new ResponseWrapper(ticketTariffDto);
            //    _logger.LogInformation($"Finished method '{nameof(GetTicketTariffAsync)}'.");
            //    return Ok(response);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return OnNotFoundError($"Cannot found element '{typeof(TicketTariff).Name}' with specified id: '{ticketTariffId}'.", ex);
            //}
            //catch (InternalDbServiceException ex)
            //{
            //    LogInternalDbServiceException(_ticketTariffDbService.GetType(), ex);
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    LogUnexpectedException(ex);
            //    throw;
            //}
        }

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> in parent <see cref="SightseeingTariff"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The id of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An updated <see cref="TicketTariff"/>.</returns>
        [HttpPut("sightseeing-tariffs/{sightseeingTariffId:required}/ticket-tariffs/{ticketTariffId:required}")]
        [Route("")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTicketTariffInSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId, [FromBody] TicketTariffDto ticketTariff)
        {
            throw new NotImplementedException();
            //_logger.LogInformation($"Starting method '{nameof(UpdateTicketTariffAsync)}'.");

            //if (string.IsNullOrEmpty(ticketTariffId))
            //    return OnInvalidParameterError($"An argument '{nameof(ticketTariffId)}' cannot be null or empty.");

            //if (!ticketTariffId.Equals(ticketTariff.Id))
            //    return OnMismatchParameterError($"An '{nameof(ticketTariffId)}' in URL end field '{nameof(ticketTariff.Id).ToLower()}' in request body mismatches. Value in URL: '{ticketTariffId}'. Value in body: '{ticketTariff.Id}'.");

            //try
            //{
            //    var ticketTariffToBeUpdated = MapToDomainModel(ticketTariff);
            //    var updatedTicketTariff = await _ticketTariffDbService.RestrictedUpdateAsync(ticketTariffToBeUpdated);

            //    // Revers map for client response.
            //    ticketTariff = MapToDto(updatedTicketTariff);
            //    var response = new ResponseWrapper(ticketTariff);
            //    _logger.LogInformation($"Finished method '{nameof(UpdateTicketTariffAsync)}'");
            //    return Ok(response);
            //}
            //catch (InvalidOperationException ex)
            //{
            //    return OnNotFoundError($"Cannot found element '{typeof(TicketTariff).Name}' with specified id: '{ticketTariffId}'.", ex);
            //}
            //catch (InternalDbServiceException ex)
            //{
            //    LogInternalDbServiceException(_ticketTariffDbService.GetType(), ex);
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    LogUnexpectedException(ex);
            //    throw;
            //}
        }


        #region Privates

        private TicketTariff MapToDomainModel(TicketTariffDto tariffDto) => _mapper.Map<TicketTariff>(tariffDto);
        private TicketTariffDto MapToDto(TicketTariff tariff) => _mapper.Map<TicketTariffDto>(tariff);
        private IEnumerable<TicketTariffDto> MapToDtoEnumerable(IEnumerable<TicketTariff> tariff) => _mapper.Map<IEnumerable<TicketTariffDto>>(tariff);

        private async Task<IEnumerable<TicketTariffDto>> GetAllAsync()
        {
            _logger.LogDebug($"Starting method '{nameof(GetAllAsync)}'.");

            try
            {
                var ticketTariffs = await _ticketTariffDbService.GetAllAsync();
                var ticketTariffDtos = MapToDtoEnumerable(ticketTariffs);
                _logger.LogDebug($"Finished method '{nameof(GetAllAsync)}'.");
                return ticketTariffDtos;
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _ticketTariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        #endregion

    }
}