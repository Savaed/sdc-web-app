using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ICustomersController
    {       
        Task<IActionResult> GetAllCustomersAsync();        
        Task<IActionResult> GetCustomerAsync(string id);
    }
}