using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides methods for get, add, update and delete operations for <see cref="SightseeingTariff"/> entities in the database.
    /// </summary>
    public class SightseeingTariffDbService : ISightseeingTariffDbService
    {
        private readonly ILogger<SightseeingTariffDbService> _logger;
        private readonly ApplicationDbContext _context;


        public SightseeingTariffDbService(ApplicationDbContext context, ILogger<SightseeingTariffDbService> logger)
        {
            _logger = logger;
            _context = context;
        }


        public Task<SightseeingTariff> AddAsync(SightseeingTariff tariff)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SightseeingTariff>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SightseeingTariff> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SightseeingTariff>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<SightseeingTariff> UpdateAsync(SightseeingTariff tariff)
        {
            throw new NotImplementedException();
        }
    }
}
