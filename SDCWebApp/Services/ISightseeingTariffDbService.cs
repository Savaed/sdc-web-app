using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ISightseeingTariffDbService
    {
        Task<SightseeingTariff> GetSightseeingTariffAsync(string id);
        Task<IEnumerable<SightseeingTariff>> GetSightseeingTariffsAsync(IEnumerable<string> ids);
        Task<IEnumerable<SightseeingTariff>> GetAllSightseeingTariffsAsync();
        Task<IEnumerable<SightseeingTariff>> GetSightseeingTariffsByAsync(Expression<Func<SightseeingTariff, bool>> predicate);
        Task<SightseeingTariff> AddSightseeingTariffAsync(SightseeingTariff SightseeingTariff);
        Task<SightseeingTariff> UpdateSightseeingTariffAsync(SightseeingTariff SightseeingTariff);
        Task DeleteSightseeingTariffAsync(string id);
    }
}