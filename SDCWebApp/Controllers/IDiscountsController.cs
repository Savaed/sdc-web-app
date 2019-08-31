using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
