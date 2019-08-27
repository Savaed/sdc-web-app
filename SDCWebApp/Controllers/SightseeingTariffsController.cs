﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Models;
using SDCWebApp.ApiErrors;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace SDCWebApp.Controllers
{
    [Route("api/sightseeing-tariffs")]
    [ApiController]
    public class SightseeingTariffsController : ControllerBase, ISightseeingTariffsController
    {
        private readonly ILogger<SightseeingTariffsController> _logger;
        private readonly ISightseeingTariffDbService _tariffDbService;
        private readonly IMapper _mapper;


        public SightseeingTariffsController(ISightseeingTariffDbService tariffDbService, ILogger<SightseeingTariffsController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _tariffDbService = tariffDbService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTariffsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllTariffsAsync)}'.");

            try
            {
                var tariffs = await _tariffDbService.GetAllAsync();
                var tariffsDto = _mapper.Map<IEnumerable<SightseeingTariffDto>>(tariffs);
                var okResponse = new ResponseWrapper(tariffsDto);
                _logger.LogInformation($"Finished method '{nameof(GetAllTariffsAsync)}'.");
                return Ok(okResponse);
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Error at '{_tariffDbService.GetType().Name}' occurred while database operation was proccessing. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Unexpected error occured. {ex.Message}");
                throw;
            }
        }


        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentTariffAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetCurrentTariffAsync)}'.");

            try
            {
                var tariffs = await _tariffDbService.GetAllAsync();
                var currentTariff = tariffs.OrderByDescending(x => x.UpdatedAt == DateTime.MinValue ? x.CreatedAt : x.UpdatedAt).First();
                var currentTariffDto = _mapper.Map<SightseeingTariffDto>(currentTariff);
                var okResponse = new ResponseWrapper(currentTariffDto);
                _logger.LogInformation($"Finished method '{nameof(GetCurrentTariffAsync)}'.");
                return Ok(okResponse);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"{ex.GetType().Name} Cannot found element {typeof(SightseeingTariff).Name}.");
                var error = new NotFoundError($"Specified '{typeof(SightseeingTariff).Name}' cannot be found.");
                var notFoundResponse = new ResponseWrapper(error);
                _logger.LogInformation($"Finished method '{nameof(GetCurrentTariffAsync)}'.");
                return NotFound(notFoundResponse);
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Error at '{_tariffDbService.GetType().Name}' occurred while database operation was proccessing. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Unexpected error occured. {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves specified <see cref="SightseeingTariff"/>.
        /// Returns <see cref="OkObjectResult"/> with data if searching tariff was found, <see cref="NotFoundObjectResult"/> if cannot found tariff or
        /// <see cref="BadRequestObjectResult"/> if request is malformed.
        /// </summary>
        /// <param name="id">The id of searching element.</param>
        /// <returns>Specified <see cref="JsonResult"/> response.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTariffAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetTariffAsync)}'.");

            var errorResponse = CreateInvalidArgumentErrorResponse(id, nameof(id));
            if (errorResponse != null)
                return BadRequest(errorResponse);

            try
            {
                _logger.LogDebug($"Starting retrieve data of type '{typeof(SightseeingTariff).Name}' with id '{id}'.");
                var tariff = await _tariffDbService.GetAsync(id);
                var tariffDto = _mapper.Map<SightseeingTariffDto>(tariff);
                var okResponse = new ResponseWrapper(tariffDto);
                _logger.LogDebug($"Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetTariffAsync)}'.");
                return Ok(okResponse);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"{ex.GetType().Name} Cannot found element {typeof(SightseeingTariff).Name} with specified id: '{id}'.");
                var error = new NotFoundError($"Specified '{typeof(SightseeingTariff).Name}' with id: '{id}' cannot be found.");
                var notFoundResponse = new ResponseWrapper(error);
                _logger.LogInformation($"Finished method '{nameof(GetTariffAsync)}'.");
                return NotFound(notFoundResponse);
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Error at '{_tariffDbService.GetType().Name}' occurred while database operation was proccessing. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Unexpected error occured. {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTariffAsync([FromBody] SightseeingTariffDto tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddTariffAsync)}'.");

            try
            {
                var tariffToBeAdded = _mapper.Map<SightseeingTariff>(tariff);
                _logger.LogDebug($"Starting add data of type '{typeof(SightseeingTariff).Name}' with id '{tariff.Id}'.");
                var addedTariff = await _tariffDbService.AddAsync(tariffToBeAdded);
                _logger.LogDebug("Add data succeeded.");

                // Reverse maps from SightseeingTariff to SightseeingTariffDto only for response to the client.
                var addedTariffDto = _mapper.Map<SightseeingTariffDto>(addedTariff);
                var response = new ResponseWrapper(addedTariffDto);
                string addedTariffUrl = $"sightseeing-tariffs/{addedTariff.Id}";
                _logger.LogInformation($"Finished method '{nameof(AddTariffAsync)}'.");
                return Created(addedTariffUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"{ex.GetType().Name} Element {typeof(SightseeingTariff).Name} with specified id: '{tariff.Id}' already exists.");
                var error = new InvalidArgumentError($"Element {typeof(SightseeingTariff).Name} with specified id: '{tariff.Id}' already exists.");
                var errorResponse = new ResponseWrapper(error);
                return BadRequest(errorResponse);
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Error at '{_tariffDbService.GetType().Name}' occurred while database operation was proccessing. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Unexpected error occured. {ex.Message}");
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTariffAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteTariffAsync)}'.");

            var errorResponse = CreateInvalidArgumentErrorResponse(id, nameof(id));
            if (errorResponse != null)
                return BadRequest(errorResponse);

            try
            {
                _logger.LogDebug($"Starting delete data of type '{typeof(SightseeingTariff).Name}' with id '{id}'.");
                await _tariffDbService.DeleteAsync(id);
                _logger.LogDebug("Delete data succeeded.");
                var okResponse = new ResponseWrapper(null as object);
                _logger.LogInformation($"Finished method '{nameof(DeleteTariffAsync)}'.");
                return Ok(okResponse);
            }
            catch (InvalidOperationException ex)
            {
                var notFoundResponse = CreateNotFoundErrorResponse(ex, id);
                _logger.LogInformation($"Finished method '{nameof(GetTariffAsync)}'.");
                return notFoundResponse;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Error at '{_tariffDbService.GetType().Name}' occurred while database operation was proccessing. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Unexpected error occured. {ex.Message}");
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTariffAsync(string id, [FromBody] SightseeingTariffDto tariff)
        {
            // nie ma takiego elementu o podanym id -> 404 not found
            // id w url i id w body sie nie zgadzaja -> 400 bad request, mismatch
            // update poprawny -> 200 ok
            // any internal error refferd to the db occurred -> throws internal db service exc
            // any unexpected internal error occurred -> throws exc

            _logger.LogInformation($"Starting method '{nameof(UpdateTariffAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                // wrap this in func eg. CreateInvalidArgumentErrorResponse
                _logger.LogWarning($"An argument '{nameof(id)}' cannot be null or empty.");
                var error = new InvalidArgumentError($"An argument '{nameof(id)}' cannot be null or empty.");
                var errorResponse = new ResponseWrapper(error);
                _logger.LogInformation($"Finished method '{nameof(DeleteTariffAsync)}'.");
                return BadRequest(errorResponse);
            }

            if (!id.Equals(tariff.Id))
            {
                _logger.LogWarning($"An argument '{nameof(id)}' end property '{nameof(tariff)}.{nameof(tariff.Id)}' mismatches. Value of argument: '{id}'. Value of property: '{tariff.Id}'.");
                var error = new MismatchParameterError($"An argument '{nameof(id)}' end property '{nameof(tariff)}.{nameof(tariff.Id)}' mismatches. Value of argument: '{id}'. Value of property: '{tariff.Id}'.");
                var errorResponse = new ResponseWrapper(error);
                _logger.LogInformation($"Finished method '{nameof(DeleteTariffAsync)}'.");
                return BadRequest(errorResponse);
            }

            try
            {
                var tariffToBeUpdated = _mapper.Map<SightseeingTariff>(tariff);
                var updatedTariff = await _tariffDbService.UpdateAsync(tariffToBeUpdated);
                tariff = _mapper.Map<SightseeingTariffDto>(updatedTariff);
                var response = new ResponseWrapper(tariff);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                var notFoundResponse = CreateNotFoundErrorResponse(ex, id);
                _logger.LogInformation($"Finished method '{nameof(GetTariffAsync)}'.");
                return notFoundResponse;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Error at '{_tariffDbService.GetType().Name}' occurred while database operation was proccessing. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Unexpected error occured. {ex.Message}");
                throw;
            }
        }


        #region Privates

        private ResponseWrapper CreateInvalidArgumentErrorResponse(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                _logger.LogWarning($"An argument '{argumentName}' cannot be null or empty.");
                var error = new InvalidArgumentError($"An argument '{argumentName}' cannot be null or empty.");
                return new ResponseWrapper(error);
            }
            return null;
        }


        private NotFoundObjectResult CreateNotFoundErrorResponse(Exception exception, string id)
        {
            _logger.LogWarning(exception, $"{exception.GetType().Name} Cannot found element {typeof(SightseeingTariff).Name} with specified id: '{id}'.");
            var error = new NotFoundError($"Specified '{typeof(SightseeingTariff).Name}' with id: '{id}' cannot be found.");
            var notFoundResponse = new ResponseWrapper(error);
            return NotFound(notFoundResponse);
        }
        #endregion
    }
}