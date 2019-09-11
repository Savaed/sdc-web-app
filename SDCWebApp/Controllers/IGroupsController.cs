using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface IGroupsController
    {
        Task<IActionResult> GetAvailableGroupDatesAsync();
        Task<IActionResult> GetGroupAsync(string id);
        Task<IActionResult> GetAllGroupsAsync();
    }
}