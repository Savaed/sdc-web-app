using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.ViewModels;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface IUsersController
    {
        Task<IActionResult> RegisterAsync(RegisterViewModel registerData);
        Task<IActionResult> LoginAsync(LoginViewModel loginData);
        //Task<IActionResult> LogoutAsync(LogoutViewModel logoutData);
    }
}