using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ITicketTariffsController
    {
        Task<IActionResult> GetVisitsTicketTariffsAsync(string visitTariffId);
        Task<IActionResult> AddVisitsTicketTariffAsync(string visitTariffId, [FromBody] TicketTariffDto ticketTariff);
        Task<IActionResult> DeleteVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId);
        Task<IActionResult> GetAllTicketTariffsAsync();
        Task<IActionResult> GetVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId);
        Task<IActionResult> UpdateVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId, [FromBody] TicketTariffDto ticketTariff);
    }
}