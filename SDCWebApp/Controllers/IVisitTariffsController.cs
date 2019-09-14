using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface IVisitTariffsController
    {
        Task<IActionResult> GetTariffAsync(string id);
        Task<IActionResult> GetCurrentTariffAsync();
        Task<IActionResult> GetAllTariffsAsync();
        Task<IActionResult> AddTariffAsync(VisitTariffDto tariff);
        Task<IActionResult> UpdateTariffAsync(string id, VisitTariffDto tariff);
        Task<IActionResult> DeleteTariffAsync(string id);
    }
}