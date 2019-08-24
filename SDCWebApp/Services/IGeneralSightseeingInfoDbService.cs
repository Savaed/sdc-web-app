using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IGeneralSightseeingInfoDbService
    {
        Task<GeneralSightseeingInfo> GetAsync(string id);
        Task<IEnumerable<GeneralSightseeingInfo>> GetAllAsync();
        Task<IEnumerable<GeneralSightseeingInfo>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<GeneralSightseeingInfo> UpdateAsync(GeneralSightseeingInfo info);
        Task DeleteAsync(string id);
        Task<GeneralSightseeingInfo> AddAsync(GeneralSightseeingInfo info);
    }
}
