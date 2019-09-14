using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    /// Provides methods to Http verbs proccessing on <see cref="SightseeingTariff"/> entities.
    /// </summary>
    [Route("api/sightseeing-tariffs")]
    [Authorize(Helpers.Constants.ApiConstants.ApiUserPolicy)]
    [ApiController]
    public class SightseeingTariffsController : CustomApiController, ISightseeingTariffsController
    {
        private const string ControllerPrefix = "sightseeing-tariffs";
        private readonly ILogger<SightseeingTariffsController> _logger;
        private readonly ISightseeingTariffDbService _tariffDbService;
        private readonly IMapper _mapper;


        public SightseeingTariffsController(ISightseeingTariffDbService tariffDbService, ILogger<SightseeingTariffsController> logger, IMapper mapper) : base(logger)
        {
            _mapper = mapper;
            _logger = logger;
            _tariffDbService = tariffDbService;
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{SightseeingTariff}"/> wrapped in <see cref="ResponseWrapper"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{SightseeingTariff}"/>.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTariffsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllTariffsAsync)}'.");

            try
            {
                var tariffs = await _tariffDbService.GetAllAsync();
                var tariffsDto = MapToDtoEnumerable(tariffs);
                var response = new ResponseWrapper(tariffsDto);
                _logger.LogInformation($"Finished method '{nameof(GetAllTariffsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _tariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets the most recent <see cref="SightseeingTariff"/> wrapped in <see cref="ResponseWrapper"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the most recent <see cref="SightseeingTariff"/> found or 
        /// <see cref="HttpStatusCode.NotFound"/> response if none <see cref="SightseeingTariff"/> exist. 
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns>The most recent <see cref="SightseeingTariff"/>.</returns>
        [HttpGet("current")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentTariffAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetCurrentTariffAsync)}'.");

            try
            {
                var tariffs = await _tariffDbService.GetAllAsync();

                var currentTariff = tariffs.OrderByDescending(x => x.UpdatedAt == DateTime.MinValue ? x.CreatedAt : x.UpdatedAt).First();

                var currentTariffDto = MapToDto(currentTariff);
                var response = new ResponseWrapper(currentTariffDto);
                _logger.LogInformation($"Finished method '{nameof(GetCurrentTariffAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element {typeof(SightseeingTariff).Name}.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _tariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="SightseeingTariff"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="SightseeingTariff"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="SightseeingTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searching <see cref="SightseeingTariff"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="SightseeingTariff"/>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTariffAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetTariffAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");
            }

            try
            {
                var tariff = await _tariffDbService.GetAsync(id);
                var tariffDto = MapToDto(tariff);
                var response = new ResponseWrapper(tariffDto);
                _logger.LogInformation($"Finished method '{nameof(GetTariffAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(SightseeingTariff).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _tariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously adds <see cref="SightseeingTariff"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="SightseeingTariff"/> created succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="tariff">The <see cref="SightseeingTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="SightseeingTariff"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTariffAsync([FromBody] SightseeingTariffDto tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddTariffAsync)}'.");

            try
            {
                // Ignore Id if the client set it. Id of entity is set internally by the server.
                tariff.Id = null;

                var tariffToBeAdded = MapToDomainModel(tariff);
                var addedTariff = await _tariffDbService.RestrictedAddAsync(tariffToBeAdded);

                // Reverse map only for response to the client.
                var addedTariffDto = MapToDto(addedTariff);
                var response = new ResponseWrapper(addedTariffDto);
                string addedTariffUrl = $"{ControllerPrefix}/{addedTariff.Id}";
                _logger.LogInformation($"Finished method '{nameof(AddTariffAsync)}'.");
                return Created(addedTariffUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                return OnInvalidParameterError($"Element '{typeof(SightseeingTariff).Name}' already exists. Value of '{nameof(tariff.Name)}' must be unique, but this one is not.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _tariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes specified <see cref="SightseeingTariff"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="SightseeingTariff"/> deleted succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="SightseeingTariff"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="SightseeingTariff"/> to be deleted. Cannot be null or empty.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTariffAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteTariffAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");
            }

            try
            {
                await _tariffDbService.DeleteAsync(id);
                var response = new ResponseWrapper();
                _logger.LogInformation($"Finished method '{nameof(DeleteTariffAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(SightseeingTariff).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _tariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="SightseeingTariff"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="SightseeingTariff"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="SightseeingTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="SightseeingTariff"/> to be updated. Cannot be null or empty. Must match to <paramref name="tariff"/>.Id property.</param>
        /// <param name="tariff">The <see cref="SightseeingTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="SightseeingTariff"/>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTariffAsync(string id, [FromBody] SightseeingTariffDto tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateTariffAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"An argument '{nameof(id)}' cannot be null or empty.");
            }

            if (!id.Equals(tariff.Id))
            {
                return OnMismatchParameterError($"An '{nameof(id)}' in URL end field '{nameof(tariff.Id).ToLower()}' in request body mismatches. Value in URL: '{id}'. Value in body: '{tariff.Id}'.");
            }

            try
            {
                var tariffToBeUpdated = MapToDomainModel(tariff);
                var updatedTariff = await _tariffDbService.RestrictedUpdateAsync(tariffToBeUpdated);

                // Revers map for client response.
                tariff = MapToDto(updatedTariff);
                _logger.LogInformation($"Finished method '{nameof(UpdateTariffAsync)}'.");
                var response = new ResponseWrapper(tariff);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(SightseeingTariff).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _tariffDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private SightseeingTariff MapToDomainModel(SightseeingTariffDto tariffDto) => _mapper.Map<SightseeingTariff>(tariffDto);
        private SightseeingTariffDto MapToDto(SightseeingTariff tariff) => _mapper.Map<SightseeingTariffDto>(tariff);
        private IEnumerable<SightseeingTariffDto> MapToDtoEnumerable(IEnumerable<SightseeingTariff> tariff) => _mapper.Map<IEnumerable<SightseeingTariffDto>>(tariff);

        #endregion

    }
}