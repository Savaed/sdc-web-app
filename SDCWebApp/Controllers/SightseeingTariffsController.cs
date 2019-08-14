using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Helpers;
using SDCWebApp.Models;
using SDCWebApp.Models.ApiDto;
using SDCWebApp.Services;

namespace SDCWebApp.Controllers
{
    [Route("api/sightseeing-tariffs")]
    [ApiController]
    public class SightseeingTariffsController : ControllerBase//, ISightseeingTariffsController
    {
        private readonly ISightseeingTariffDbService _tariffDbService;
        private readonly ILogger<SightseeingTariffsController> _logger;


        public SightseeingTariffsController(ISightseeingTariffDbService tariffDbService, ILogger<SightseeingTariffsController> logger)
        {
            _tariffDbService = tariffDbService;
            _logger = logger;
        }


        // [GET] sightseeing-tariffs/9c29a9d9-6da4-47b9-80f0-361717ccfd73
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTariff(string id)
        {
            SightseeingTariff tariff;
            ApiError error;

            try
            {
                tariff = await _tariffDbService.GetSightseeingTariffAsync(id);
                var okResponse = new CommonWrapper
                {
                    Success = true,
                    Data = tariff,
                    Errors = null
                };
                return Ok(okResponse);
            }
            catch (NullReferenceException ex)
            {
                error = new ApiError
                {
                    ErrorCode = Errors.InternalServerErrorCode,
                    ErrorMessage = Errors.InternalServerErrorResourceNotExistMessage
                };
                var errorResponseValue = new CommonWrapper
                {
                    Success = false,
                    Data = null,
                    Errors = new List<ApiError>().Append(error).ToList()
                };

                var errorResponse = new ObjectResult(errorResponseValue)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                return errorResponse;
            }
            catch (InvalidOperationException ex)
            {
                error = new ApiError
                {
                    ErrorCode = Errors.NotFoundCode,
                    ErrorMessage = Errors.NotFoundMessage
                };
                var errorResponseValue = new CommonWrapper
                {
                    Success = false,
                    Data = null,
                    Errors = new List<ApiError>().Append(error).ToList()
                };

                var errorResponse = new ObjectResult(errorResponseValue)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
                return errorResponse;
            }      
        }

        // [GET] sightseeing-tariffs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetTariffs()
        {
            throw new NotImplementedException();
        }
    }
}