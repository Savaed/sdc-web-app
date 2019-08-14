using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public  interface ISightseeingTariffsController
    {
        Task<IActionResult> GetTariffs();
        Task<IActionResult> GetTariff(string id);
    }
}