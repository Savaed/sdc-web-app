using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IDiscountDbService
    {
        Task<Discount> GetAsync(string id);
        Task<IEnumerable<Discount>> GetAllAsync();
        Task<IEnumerable<Discount>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<Discount> UpdateAsync(Discount discount);
        Task DeleteAsync(string id);
        Task<Discount> AddAsync(Discount discount);
    }
}
