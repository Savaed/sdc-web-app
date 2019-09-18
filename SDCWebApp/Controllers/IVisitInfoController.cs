using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface IVisitInfoController
    {
        Task<IActionResult> AddInfoAsync(VisitInfoDto info);
        Task<IActionResult> DeleteInfoAsync(string id);
        Task<IActionResult> UpdateInfoAsync(string id, VisitInfoDto info);
        Task<IActionResult> GetAllInfoAsync();
        Task<IActionResult> GetInfoAsync(string id);
        Task<IActionResult> GetRecentInfoAsync();
    }
}