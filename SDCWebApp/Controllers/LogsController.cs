using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SDCWebApp.Helpers.Constants;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace SDCWebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Strings.ApiUserPolicyName)]
    [ApiController]
    public class LogsController : CustomApiController, ILogsController
    {
        private readonly ILogger<LogsController> _logger;
        private readonly IActivityLogDbService _logDbService;
        private readonly IMapper _mapper;


        public LogsController(IActivityLogDbService logDbService, ILogger<LogsController> logger, IMapper mapper) : base(logger)
        {
            _logger = logger;
            _logDbService = logDbService;
            _mapper = mapper;
        }
        

        /// <summary>
        /// Asynchronously gets specified <see cref="ActivityLog"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="ActivityLog"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="ActivityLog"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searching <see cref="ActivityLog"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="ActivityLog"/>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLogAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetLogAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            try
            {
                var log = await _logDbService.GetAsync(id);
                var logDto = MapToDto(log);
                var response = new ResponseWrapper(logDto);
                _logger.LogInformation($"Finished method '{nameof(GetLogAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(ActivityLog).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_logDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{ActivityLog}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{ActivityLog}"/>.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLogsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetLogsAsync)}'.");

            try
            {
                var logs = await _logDbService.GetWithPaginationAsync();
                var logsDto = MapToDtoEnumerable(logs);
                var response = new ResponseWrapper(logsDto);
                _logger.LogInformation($"Finished method '{nameof(GetLogsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_logDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private ActivityLogDto MapToDto(ActivityLog log) => _mapper.Map<ActivityLogDto>(log);
        private IEnumerable<ActivityLogDto> MapToDtoEnumerable(IEnumerable<ActivityLog> logs) => _mapper.Map<IEnumerable<ActivityLogDto>>(logs);

        #endregion
    }
}