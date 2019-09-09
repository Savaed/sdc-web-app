using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface IOrdersController
    {
        Task<IActionResult> CreateOrderAsync(OrderDto order);
        Task<IActionResult> GetOrderAsync(string id);
    }
}