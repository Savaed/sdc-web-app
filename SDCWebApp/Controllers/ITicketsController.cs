using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ITicketsController
    {
        Task<IActionResult> GetTicketAsync(string id);
        Task<IActionResult> GetAllTicketsAsync();
    }
}