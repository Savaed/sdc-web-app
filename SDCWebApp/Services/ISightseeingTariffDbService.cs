using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ISightseeingTariffDbService
    {
        Task<SightseeingTariff> GetAsync(string id);
        Task<IEnumerable<SightseeingTariff>> GetAllAsync();
        Task<IEnumerable<SightseeingTariff>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<SightseeingTariff> UpdateAsync(SightseeingTariff tariff);
        Task DeleteAsync(string id);
        Task<SightseeingTariff> AddAsync(SightseeingTariff tariff);
    }
}
