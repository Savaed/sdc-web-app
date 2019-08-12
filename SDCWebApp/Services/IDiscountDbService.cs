using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IDiscountDbService
    {
        Task<Discount> GetDiscountAsync(string id);
        Task<IEnumerable<Discount>> GetDiscountsAsync(IEnumerable<string> ids);
        Task<IEnumerable<Discount>> GetAllDiscountsAsync();
        Task<IEnumerable<Discount>> GetDiscountsByAsync(Expression<Func<Discount, bool>> predicate);
        Task<Discount> AddDiscountAsync(Discount discount);
        Task<Discount> UpdateDiscountAsync(Discount discount);
        Task DeleteDiscountAsync(string id);
    }
}