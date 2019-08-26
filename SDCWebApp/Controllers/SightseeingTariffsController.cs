using System;
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
        public Task<IActionResult> GetAllTariffsAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("/current")]
        public Task<IActionResult> GetCurrentTariffAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTariffAsync(string id)
        {
            throw new NotImplementedException();            
        }

        [HttpPost]
        public Task<IActionResult> AddTariffAsync([FromBody] SightseeingTariff tariff)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteTariffAsync(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:required}")]
        public Task<IActionResult> UpdateTariffAsync(string id, [FromBody] SightseeingTariff tariff)
        {
            throw new NotImplementedException();
        }
    }
}