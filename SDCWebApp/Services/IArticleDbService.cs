using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IArticleDbService
    {
        Task<Article> GetAsync(string id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<Article> UpdateAsync(Article article);
        Task DeleteAsync(string id);
        Task<Article> AddAsync(Article article);
    }
}
