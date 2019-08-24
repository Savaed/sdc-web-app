using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ITicketTariffDbService
    {
        Task<TicketTariff> GetAsync(string id);
        Task<IEnumerable<TicketTariff>> GetAllAsync();
        Task<IEnumerable<TicketTariff>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<TicketTariff> UpdateAsync(TicketTariff tariff);
        Task DeleteAsync(string id);
        Task<TicketTariff> AddAsync(TicketTariff tariff);
    }
}
