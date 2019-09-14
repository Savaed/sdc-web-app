using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ISightseeingTariffsController
    {
        Task<IActionResult> GetTariffAsync(string id);
        Task<IActionResult> GetCurrentTariffAsync();
        Task<IActionResult> GetAllTariffsAsync();
        Task<IActionResult> AddTariffAsync(SightseeingTariffDto tariff);
        Task<IActionResult> UpdateTariffAsync(string id, SightseeingTariffDto tariff);
        Task<IActionResult> DeleteTariffAsync(string id);
    }
}