using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

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