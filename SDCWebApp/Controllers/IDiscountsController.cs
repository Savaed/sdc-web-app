using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Controllers
{
    public interface IDiscountsController
    {
        Task<IActionResult> GetDiscountAsync(string id);
        Task<IActionResult> GetAllDiscountsAsync();
        Task<IActionResult> AddDiscountAsync(DiscountDto Discount);
        Task<IActionResult> UpdateDiscountAsync(string id, DiscountDto Discount);
        Task<IActionResult> DeleteDiscountAsync(string id);
    }
}
