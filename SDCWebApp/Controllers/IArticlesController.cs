using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Controllers
{
    public interface IArticlesController
    {
        Task<IActionResult> GetArticleAsync(string id);
        Task<IActionResult> GetAllArticlesAsync();
        Task<IActionResult> AddArticleAsync(ArticleDto Article);
        Task<IActionResult> UpdateArticleAsync(string id, ArticleDto Article);
        Task<IActionResult> DeleteArticleAsync(string id);
    }
}