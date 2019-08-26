using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ISightseeingTariffsController
    {
        Task<IActionResult> GetTariffAsync(string id);
        Task<IActionResult> GetCurrentTariffAsync();
        Task<IActionResult> GetAllTariffsAsync();
        Task<IActionResult> AddTariffAsync(SightseeingTariff tariff);
        Task<IActionResult> UpdateTariffAsync(string id, SightseeingTariff tariff);
        Task<IActionResult> DeleteTariffAsync(string id);
    }
}