using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ILogsController
    {
        Task<IActionResult> GetLogAsync(string id);
        Task<IActionResult> GetLogsAsync();
    }
}