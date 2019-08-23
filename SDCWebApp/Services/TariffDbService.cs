using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public class TariffDbService : ITariffDbService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TariffDbService> _logger;


        public TariffDbService(ApplicationDbContext context, ILogger<TariffDbService> logger)
        {
            _logger = logger;
            _context = context;
        }


        public Task<TicketTariff> AddAsync(TicketTariff tariff)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketTariff>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TicketTariff> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketTariff>> GetCurrentTariffsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TicketTariff> UpdateAsync(TicketTariff tariff)
        {
            throw new NotImplementedException();
        }
    }
}
