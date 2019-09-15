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
    [Route("api/ticket-tariffs")]
    [ApiController]
    public class TicketTariffsController : CustomApiController, ITicketTariffsController
    {
        private const string ControllerPrefix = "ticket-tariffs";
        private readonly ILogger<TicketTariffsController> _logger;
        private readonly ITicketTariffDbService _ticketTariffDbService;
        private readonly IVisitTariffDbService _visitTariffDbService;
        private readonly IMapper _mapper;


        public TicketTariffsController(ITicketTariffDbService ticketTariffDbService, IVisitTariffDbService visitTariffDbService, ILogger<TicketTariffsController> logger, IMapper mapper) : base(logger)
        {
            _mapper = mapper;
            _logger = logger;
            _ticketTariffDbService = ticketTariffDbService;
            _visitTariffDbService = visitTariffDbService;
        }





        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/> from <see cref="VisitTariff"/> that contain them.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="visitTariffId">The <see cref="VisitTariff"/> parent ID.</param>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        [HttpGet]
        [Route("api/sightseeing-tariffs/{sightseeingTariffId:required}/ticket-tariffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVisitsTicketTariffsAsync(string visitTariffId)
        {
            if (string.IsNullOrEmpty(visitTariffId))
            {
                return OnInvalidParameterError($"Parameter '{nameof(visitTariffId)}' cannot be null or empty.");
            }

            try
            {
                var visitTariff = await _visitTariffDbService.GetAsync(visitTariffId);            
                var visitsTicketTariffDto = MapToDtoEnumerable(visitTariff.TicketTariffs.AsEnumerable());
                var response = new ResponseWrapper(visitsTicketTariffDto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{visitTariffId}'.", ex);
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
        /// Asynchronously adds <see cref="TicketTariff"/> to <see cref="VisitTariff"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="TicketTariff"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariff">The <see cref="TicketTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <param name="visitTariffId">The ID of <see cref="VisitTariff"/> which contains <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An added <see cref="TicketTariffDto"/>.</returns>
        ///  [HttpPost]
        [Route("visit-tariffs/{visitTariffId:required}/ticket-tariffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddVisitsTicketTariffAsync(string visitTariffId, [FromBody] TicketTariffDto ticketTariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddVisitsTicketTariffAsync)}'.");

            if (string.IsNullOrEmpty(visitTariffId))
            {
                return OnInvalidParameterError($"Parameter '{nameof(visitTariffId)}' cannot be null or empty.");
            }

            TicketTariff ticketTariffToBeAdded = null;
            VisitTariff visitTariff = null;

            try
            {
                visitTariff = await _visitTariffDbService.GetAsync(visitTariffId);

                // Ignore Id if the client set it. Id of entity is set internally by the server.
                ticketTariff.Id = null;

                ticketTariffToBeAdded = MapToDomainModel(ticketTariff);
                var addedTicketTariff = await _ticketTariffDbService.RestrictedAddAsync(ticketTariffToBeAdded);

                // Reverse map only for response to the client.
                var addedTariffDto = MapToDto(addedTicketTariff);
                var response = new ResponseWrapper(addedTariffDto);
                string addedTicketTariffUrl = $"{ControllerPrefix}/{addedTicketTariff.Id}";
                _logger.LogInformation($"Finished method '{nameof(AddVisitsTicketTariffAsync)}'.");
                return Created(addedTicketTariffUrl, response);
            }
            catch(InvalidOperationException ex) when (visitTariff is null)
            {
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{visitTariffId}'.", ex);
            }
            catch (InvalidOperationException ex)
            {
                // TicketTariff already exists. Add it to the visitTariff but not to the db.
                var existingTariff = _ticketTariffDbService.GetAllAsync().Result.Single(x => x.Equals(ticketTariffToBeAdded));
                _logger.LogInformation($"{ex.Message}");

                var response = new ResponseWrapper(existingTariff);
                _logger.LogInformation($"Finished method '{nameof(AddVisitsTicketTariffAsync)}'.");
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
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTicketTariffsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllTicketTariffsAsync)}'.");

            try
            {
                var ticketTariffs = await _ticketTariffDbService.GetAllAsync();
                var ticketTariffDtos = MapToDtoEnumerable(ticketTariffs);
                var response = new ResponseWrapper(ticketTariffDtos);
                _logger.LogInformation($"Finished method '{nameof(GetAllTicketTariffsAsync)}'.");
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
        /// Asynchronously gets specified <see cref="TicketTariff"/> by <paramref name="id"/> from <see cref="VisitTariff"/> that owns it.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="TicketTariff"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The ID of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="visitTariffId">The ID of <see cref="VisitTariff"/> which owns for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="TicketTariff"/>.</returns>
        /// 
        [HttpGet]
        [Route("visit-tariffs/{visitTariffId:required}/ticket-tariffs/{ticketTariffId:required}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId)
        {
            if (string.IsNullOrEmpty(visitTariffId) || string.IsNullOrEmpty(ticketTariffId))
            {
                return OnInvalidParameterError($"Parameters '{nameof(visitTariffId)}' and '{nameof(ticketTariffId)}' cannot be null or empty.");
            }

            VisitTariff visitTariff = null;

            try
            {
                visitTariff = await _visitTariffDbService.GetAsync(visitTariffId);
                var ticketTariff = visitTariff.TicketTariffs.Single(x => x.Id.Equals(ticketTariffId));
                var ticketTariffDto = MapToDto(ticketTariff);
                var response = new ResponseWrapper(ticketTariffDto);
                return Ok(response);
            }
            catch (InvalidOperationException ex) when (visitTariff is null)
            {
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{visitTariffId}'.", ex);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(TicketTariff).Name}' with specified id: '{ticketTariffId}'.", ex);
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
        /// Asynchronously updates <see cref="TicketTariff"/> in <see cref="VisitTariff"/> that owns it.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The ID of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="visitTariffsId">The ID of <see cref="VisitTariff"/> which owns <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <param name="ticketTariff">The <see cref="TicketTariffDto"/> tariff to be updated. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="TicketTariff"/>.</returns>
        public async Task<IActionResult> UpdateVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId, [FromBody] TicketTariffDto ticketTariff)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateVisitsTicketTariffAsync)}'.");

            if (string.IsNullOrEmpty(visitTariffId) || string.IsNullOrEmpty(ticketTariffId))
            {
                return OnInvalidParameterError($"Arguments '{nameof(visitTariffId)}' and '{nameof(ticketTariffId)}' cannot be null or empty.");
            }

            if (!ticketTariffId.Equals(ticketTariff.Id))
            {
                return OnMismatchParameterError($"An '{nameof(ticketTariffId)}' in URL end field '{nameof(ticketTariff.Id)}' in request body mismatches. Value in URL: '{ticketTariffId}'. " +
                    $"Value in body: '{ticketTariff.Id}'.");
            }

            try
            {
                var tariffToBeUpdated = MapToDomainModel(ticketTariff);
                var updatedInfo = await _ticketTariffDbService.RestrictedUpdateAsync(tariffToBeUpdated);

                // Revers map for client response.
                var updatedTariffDto = MapToDto(updatedInfo);
                var response = new ResponseWrapper(updatedTariffDto);
                _logger.LogInformation($"Finished method '{nameof(UpdateVisitsTicketTariffAsync)}'");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(VisitInfo).Name}' with specified id: '{ticketTariffId}'.", ex);
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
        /// Asynchronously deletes specified <see cref="TicketTariff"/> by <paramref name="ticketTariffId"/> from <see cref="VisitTariff"/> that owns it.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> delete succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="TicketTariff"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The ID of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="visitTariffId">The ID of <see cref="VisitTariff"/> which owns <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        ///    
        [HttpDelete]
        [Route("visit-tariffs/{visitTariffId:required}/ticket-tariffs/{ticketTariffId:required}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId)
        {
            throw new NotImplementedException();
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