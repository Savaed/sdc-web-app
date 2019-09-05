using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface IAuthController
    {
        Task<IActionResult> RefreshTokenExchangeAsync([FromBody] RefreshTokenViewModel refreshTokenData);
    }
}