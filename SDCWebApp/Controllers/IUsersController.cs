using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using SDCWebApp.Models.ViewModels;

namespace SDCWebApp.Controllers
{
    public  interface IUsersController
    {
        Task<IActionResult> RegisterAsync(RegisterViewModel registerData);
        Task<IActionResult> LoginAsync(LoginViewModel loginData);
        Task<IActionResult> LogoutAsync(LogoutViewModel logoutData);
    }
}