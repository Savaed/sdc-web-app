using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IArticleDbService
    {
        Task<Article> GetArticleAsync(string id);
        Task<IEnumerable<Article>> GetArticlesAsync(IEnumerable<string> ids);
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<IEnumerable<Article>> GetArticlesByAsync(Expression<Func<Article, bool>> predicate);
        Task<Article> AddArticleAsync(Article Article);
        Task<Article> UpdateArticleAsync(Article Article);
        Task DeleteArticleAsync(string id);
    }
}