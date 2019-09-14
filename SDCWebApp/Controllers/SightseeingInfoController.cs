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
using System.Net;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides methods to Http verbs proccessing on <see cref="GeneralSightseeingInfo"/> entities.
    /// </summary>
    [Route("api/info")]
    [ApiController]
    public class SightseeingInfoController : CustomApiController, ISightseeingInfoController
    {
        private const string ControllerPrefix = "info";
        private readonly IGeneralSightseeingInfoDbService _infoDbService;
        private readonly ILogger<SightseeingInfoController> _logger;
        private readonly IMapper _mapper;


        public SightseeingInfoController(IGeneralSightseeingInfoDbService infoDbService, ILogger<SightseeingInfoController> logger, IMapper mapper) : base(logger)
        {
            _logger = logger;
            _mapper = mapper;
            _infoDbService = infoDbService;
        }


        /// <summary>
        /// Asynchronously adds <see cref="GeneralSightseeingInfo"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="SightseeingInfo"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="info">The <see cref="SightseeingInfoDto"/> info to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="GeneralSightseeingInfo"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddInfoAsync([FromBody] SightseeingInfoDto info)
        {
            _logger.LogInformation($"Starting method '{nameof(AddInfoAsync)}'.");

            try
            {
                // Ignore Id if the client set it. Id of entity is set internally by the server.
                info.Id = null;

                var infoToBeAdded = MapToDomainModel(info);
                var addedInfo = await _infoDbService.AddAsync(infoToBeAdded);

                // Reverse map only for response to the client.
                var addedInfoDto = MapToDto(addedInfo);
                var response = new ResponseWrapper(addedInfoDto);
                string addedInfoUrl = $"{ControllerPrefix}/{addedInfo.Id}";
                _logger.LogInformation($"Finished method '{nameof(addedInfo)}'.");
                return Created(addedInfoUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                return OnInvalidParameterError($"Element '{typeof(GeneralSightseeingInfo).Name}' already exists.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _infoDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes specified <see cref="GeneralSightseeingInfo"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="GeneralSightseeingInfo"/> delete succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="GeneralSightseeingInfo"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="GeneralSightseeingInfo"/> to be deleted. Cannot be null or empty.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteInfoAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteInfoAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");
            }

            try
            {
                await _infoDbService.DeleteAsync(id);
                var response = new ResponseWrapper();
                _logger.LogInformation($"Finished method '{nameof(DeleteInfoAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(GeneralSightseeingInfo).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _infoDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{GeneralSightseeingInfo}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{GeneralSightseeingInfo}"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInfoAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllInfoAsync)}'.");

            try
            {
                var allInfo = await _infoDbService.GetAllAsync();
                var allInfoDto = MapToDtoEnumerable(allInfo);
                var response = new ResponseWrapper(allInfoDto);
                _logger.LogInformation($"Finished method '{nameof(GetAllInfoAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _infoDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="GeneralSightseeingInfo"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="GeneralSightseeingInfo"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="GeneralSightseeingInfo"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searched <see cref="GeneralSightseeingInfo"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="GeneralSightseeingInfo"/>.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetInfoAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");
            }

            try
            {
                var info = await _infoDbService.GetAsync(id);
                var infoDto = MapToDto(info);
                var response = new ResponseWrapper(infoDto);
                _logger.LogInformation($"Finished method '{nameof(GetInfoAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(GeneralSightseeingInfo).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _infoDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="GeneralSightseeingInfo"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="GeneralSightseeingInfo"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="GeneralSightseeingInfo"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="GeneralSightseeingInfo"/> to be updated. Cannot be null or empty. Must match to <paramref name="info"/>.Id property.</param>
        /// <param name="info">The <see cref="SightseeingInfoDto"/> info to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="GeneralSightseeingInfo"/>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInfoAsync(string id, [FromBody] SightseeingInfoDto info)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateInfoAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"An argument '{nameof(id)}' cannot be null or empty.");
            }

            if (!id.Equals(info.Id))
            {
                return OnMismatchParameterError($"An '{nameof(id)}' in URL end field '{nameof(info.Id).ToLower()}' in request body mismatches. Value in URL: '{id}'. Value in body: '{info.Id}'.");
            }

            try
            {
                var infoToBeUpdated = MapToDomainModel(info);
                var updatedInfo = await _infoDbService.RestrictedUpdateAsync(infoToBeUpdated);

                // Revers map for client response.
                info = MapToDto(updatedInfo);
                var response = new ResponseWrapper(info);
                _logger.LogInformation($"Finished method '{nameof(UpdateInfoAsync)}'");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(GeneralSightseeingInfo).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, _infoDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private GeneralSightseeingInfo MapToDomainModel(SightseeingInfoDto tariffDto) => _mapper.Map<GeneralSightseeingInfo>(tariffDto);
        private SightseeingInfoDto MapToDto(GeneralSightseeingInfo tariff) => _mapper.Map<SightseeingInfoDto>(tariff);
        private IEnumerable<SightseeingInfoDto> MapToDtoEnumerable(IEnumerable<GeneralSightseeingInfo> tariff) => _mapper.Map<IEnumerable<SightseeingInfoDto>>(tariff);

        #endregion
    }
}