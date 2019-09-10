using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using SDCWebApp.Helpers.Constants;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides methods to Http verbs proccessing on <see cref="Customer"/> entities.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(ApiConstants.ApiUserPolicy)]
    [ApiController]
    public class CustomersController : CustomApiController, ICustomersController
    {
        private readonly ICustomerDbService _customerDbService;
        private readonly ILogger<CustomersController> _logger;
        private readonly IMapper _mapper;


        public CustomersController(ICustomerDbService customerDbService, ILogger<CustomersController> logger, IMapper mapper) : base(logger)
        {
            _customerDbService = customerDbService;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{Customer}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{Customer}"/>.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllCustomersAsync)}'.");

            try
            {
                var customers = await _customerDbService.GetAllAsync();
                var customersDto = MapToDtoEnumerable(customers);
                var response = new ResponseWrapper(customersDto);
                _logger.LogInformation($"Finished method '{nameof(GetAllCustomersAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_customerDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="Customer"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="Customer"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Customer"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searching <see cref="Customer"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="Customer"/>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetCustomerAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            try
            {
                var customer = await _customerDbService.GetAsync(id);
                var customerDto = MapToDto(customer);
                var response = new ResponseWrapper(customerDto);
                _logger.LogInformation($"Finished method '{nameof(GetCustomerAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Customer).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_customerDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private IEnumerable<CustomerDto> MapToDtoEnumerable(IEnumerable<Customer> customers) => _mapper.Map<IEnumerable<CustomerDto>>(customers);
        private CustomerDto MapToDto(Customer customer) => _mapper.Map<CustomerDto>(customer);

        #endregion
    }
}