
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Helpers.Constants;
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
    /// Provides methods to Http verbs proccessing on <see cref="VisitTariff"/> entities.
    /// </summary>
    [Route("api/visit-tariffs")]
    //[Authorize(ApiConstants.ApiUserPolicy)]
    [ApiController]
    public class VisitTariffsController : CustomApiController, IVisitTariffsController
    {
        private const string ControllerPrefix = "visit-tariffs";
        private readonly ILogger<VisitTariffsController> _logger;
        private readonly IVisitTariffDbService _tariffDbService;
        private readonly IMapper _mapper;


        public VisitTariffsController(IVisitTariffDbService tariffDbService, ILogger<VisitTariffsController> logger, IMapper mapper) : base(logger)
        {
            _mapper = mapper;
            _logger = logger;
            _tariffDbService = tariffDbService;
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{VisitTariff}"/> wrapped in <see cref="ResponseWrapper"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{VisitTariff}"/>.</returns>
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
        /// Asynchronously gets the most recent <see cref="VisitTariff"/> wrapped in <see cref="ResponseWrapper"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the most recent <see cref="VisitTariff"/> found or 
        /// <see cref="HttpStatusCode.NotFound"/> response if none <see cref="VisitTariff"/> exist. 
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns>The most recent <see cref="VisitTariff"/>.</returns>
        [HttpGet("recent")]
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
                return OnNotFoundError($"Cannot found element {typeof(VisitTariff).Name}.", ex);
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
        /// Asynchronously gets specified <see cref="VisitTariff"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="VisitTariff"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="VisitTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searching <see cref="VisitTariff"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="VisitTariff"/>.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
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
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{id}'.", ex);
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
        /// Asynchronously adds <see cref="VisitTariff"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="VisitTariff"/> created succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="tariff">The <see cref="VisitTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="VisitTariff"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTariffAsync([FromBody] VisitTariffDto tariff)
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
                return OnInvalidParameterError($"Element '{typeof(VisitTariff).Name}' already exists. Value of '{nameof(tariff.Name)}' must be unique, but this one is not.", ex);
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
        /// Asynchronously deletes specified <see cref="VisitTariff"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="VisitTariff"/> deleted succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="VisitTariff"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="VisitTariff"/> to be deleted. Cannot be null or empty.</param>
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
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{id}'.", ex);
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
        /// Asynchronously updates <see cref="VisitTariff"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="VisitTariff"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="VisitTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="VisitTariff"/> to be updated. Cannot be null or empty. Must match to <paramref name="tariff"/>.Id property.</param>
        /// <param name="tariff">The <see cref="VisitTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="VisitTariff"/>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTariffAsync(string id, [FromBody] VisitTariffDto tariff)
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
                return OnNotFoundError($"Cannot found element '{typeof(VisitTariff).Name}' with specified id: '{id}'.", ex);
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

        private VisitTariff MapToDomainModel(VisitTariffDto tariffDto) => _mapper.Map<VisitTariff>(tariffDto);
        private VisitTariffDto MapToDto(VisitTariff tariff) => _mapper.Map<VisitTariffDto>(tariff);
        private IEnumerable<VisitTariffDto> MapToDtoEnumerable(IEnumerable<VisitTariff> tariff) => _mapper.Map<IEnumerable<VisitTariffDto>>(tariff);

        #endregion

    }
}