using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using SDCWebApp.Helpers.Constants;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides methods to Http verbs proccessing on <see cref="Discount"/> entities.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Strings.ApiUserPolicyName)]
    [ApiController]
    public class DiscountsController : CustomApiController, IDiscountsController
    {
        private const string ControllerPrefix = "discounts";
        private readonly ILogger<DiscountsController> _logger;
        private readonly IDiscountDbService _discountDbService;
        private readonly IMapper _mapper;


        public DiscountsController(IDiscountDbService discountDbService, ILogger<DiscountsController> logger, IMapper mapper) : base(logger)
        {
            _mapper = mapper;
            _logger = logger;
            _discountDbService = discountDbService;
        }


        /// <summary>
        /// Asynchronously adds <see cref="Discount"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="Discount"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="discount">The <see cref="DiscountDto"/> discount to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="DiscountDto"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDiscountAsync([FromBody] DiscountDto discount)
        {
            _logger.LogInformation($"Starting method '{nameof(AddDiscountAsync)}'.");

            try
            {
                // Ignore Id if the client set it. Id of entity is set internally by the server.
                discount.Id = null;

                var discountToBeAdded = MapToDomainModel(discount);
                var addedDiscount = await _discountDbService.RestrictedAddAsync(discountToBeAdded);

                // Reverse map only for response to the client.
                var addedTariffDto = MapToDto(addedDiscount);
                var response = new ResponseWrapper(addedTariffDto);
                string addedDiscountUrl = $"{ControllerPrefix}/{addedDiscount.Id}";
                _logger.LogInformation($"Finished method '{nameof(addedDiscount)}'.");
                return Created(addedDiscountUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                return OnInvalidParameterError($"Element '{typeof(Discount).Name}' already exists.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_discountDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes specified <see cref="Discount"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="Discount"/> delete succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="Discount"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="Discount"/> to be deleted. Cannot be null or empty.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDiscountAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteDiscountAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            try
            {
                await _discountDbService.DeleteAsync(id);
                var response = new ResponseWrapper();
                _logger.LogInformation($"Finished method '{nameof(DeleteDiscountAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Discount).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_discountDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{Discount}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{Discount}"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDiscountsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllDiscountsAsync)}'.");

            try
            {
                var discounts = await _discountDbService.GetAllAsync();
                var discountDtos = MapToDtoEnumerable(discounts);
                var response = new ResponseWrapper(discountDtos);
                _logger.LogInformation($"Finished method '{nameof(GetAllDiscountsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_discountDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="Discount"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="Discount"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Discount"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searching <see cref="Discount"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="Discount"/>.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDiscountAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetDiscountAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            try
            {
                var discount = await _discountDbService.GetAsync(id);
                var discountDto = MapToDto(discount);
                var response = new ResponseWrapper(discountDto);
                _logger.LogInformation($"Finished method '{nameof(GetDiscountAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Discount).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_discountDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="Discount"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="Discount"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Discount"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="Discount"/> to be updated. Cannot be null or empty. Must match to <paramref name="discount"/>.Id property.</param>
        /// <param name="discount">The <see cref="DiscountDto"/> discount to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="Discount"/>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDiscountAsync(string id, [FromBody] DiscountDto discount)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateDiscountAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"An argument '{nameof(id)}' cannot be null or empty.");

            if (!id.Equals(discount.Id))
                return OnMismatchParameterError($"An '{nameof(id)}' in URL end field '{nameof(discount.Id).ToLower()}' in request body mismatches. Value in URL: '{id}'. Value in body: '{discount.Id}'.");

            try
            {
                var discountToBeUpdated = MapToDomainModel(discount);
                var updatedDiscount = await _discountDbService.RestrictedUpdateAsync(discountToBeUpdated);

                // Revers map for client response.
                discount = MapToDto(updatedDiscount);
                var response = new ResponseWrapper(discount);
                _logger.LogInformation($"Finished method '{nameof(UpdateDiscountAsync)}'");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Discount).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_discountDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private Discount MapToDomainModel(DiscountDto tariffDto) => _mapper.Map<Discount>(tariffDto);
        private DiscountDto MapToDto(Discount tariff) => _mapper.Map<DiscountDto>(tariff);
        private IEnumerable<DiscountDto> MapToDtoEnumerable(IEnumerable<Discount> tariff) => _mapper.Map<IEnumerable<DiscountDto>>(tariff);

        #endregion
    }
}