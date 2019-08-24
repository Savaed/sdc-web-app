using System.Collections.Generic;
using System.Threading.Tasks;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public interface ISightseeingGroupDbService
    {
        Task<SightseeingGroup> GetAsync(string id);
        Task<IEnumerable<SightseeingGroup>> GetAllAsync();
        Task<IEnumerable<SightseeingGroup>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<SightseeingGroup> UpdateAsync(SightseeingGroup group);
        Task DeleteAsync(string id);
        Task<SightseeingGroup> AddAsync(SightseeingGroup group);
    }
}
