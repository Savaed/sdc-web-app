using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;

namespace SDCWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : CustomApiController, ITicketsController
    {
        private readonly ILogger<TicketsController> _logger;
        private readonly IMapper _mapper;
        private readonly IIndex<string, IServiceBase> _dbServiceFactory;


        public TicketsController(IIndex<string, IServiceBase> dbServiceFactory, ILogger<TicketsController> logger, IMapper mapper) : base(logger)
        {
            _dbServiceFactory = dbServiceFactory;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTicketsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllTicketsAsync)}'.");

            var dbTicketService = _dbServiceFactory[nameof(ITicketDbService)] as ITicketDbService;

            try
            {
                var tickets = await dbTicketService.GetAllAsync();
                var ticketDtos = MapToDtoEnumerable(tickets);
                var response = new ResponseWrapper(ticketDtos);
                _logger.LogInformation($"Finished method '{nameof(GetAllTicketsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(dbTicketService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        public async Task<IActionResult> GetTicketAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetTicketAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            var ticketDbService = _dbServiceFactory[nameof(ITicketDbService)] as ITicketDbService;

            try
            {
                var ticket = await ticketDbService.GetAsync(id);
                var ticketDto = MapToDto(ticket);
                var response = new ResponseWrapper(ticketDto);
                _logger.LogInformation($"Finished method '{nameof(GetTicketAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(SightseeingTariff).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ticketDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates   

        private IEnumerable<TicketDto> MapToDtoEnumerable(IEnumerable<Ticket> tickets) => _mapper.Map<IEnumerable<TicketDto>>(tickets);
        private TicketDto MapToDto(Ticket ticket) => _mapper.Map<TicketDto>(ticket);
        private Ticket MapToDomainModel(TicketDto ticketDto) => _mapper.Map<Ticket>(ticketDto);

        #endregion
    }
}