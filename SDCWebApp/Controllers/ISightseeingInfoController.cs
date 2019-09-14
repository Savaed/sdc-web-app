using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ISightseeingInfoController
    {
        Task<IActionResult> AddInfoAsync(SightseeingInfoDto info);
        Task<IActionResult> DeleteInfoAsync(string id);
        Task<IActionResult> UpdateInfoAsync(string id, SightseeingInfoDto info);
        Task<IActionResult> GetAllInfoAsync();
        Task<IActionResult> GetInfoAsync(string id);
    }
}