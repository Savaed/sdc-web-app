using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Data.Validators;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public class ArticleDbService : IArticleDbService
    {
        private readonly ILogger<ArticleDbService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbRepository _repository;
        private readonly ICustomValidator<Article> _validator;


        public ArticleDbService(ILogger<ArticleDbService> logger, ApplicationDbContext dbContext, IDbRepository repository, ICustomValidator<Article> validator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _repository = repository;
            _validator = validator;
        }


        public async Task<Article> AddArticleAsync(Article article)
        {
            Article result;

            var validationResult = _validator.Validate(article);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                var ex = new ArgumentException(nameof(article));
                _logger.LogError(ex, $"Argument '{nameof(article)}' is invalid. {error.ErrorMessage}.");
                throw ex;
            }

            try
            {
                result = _repository.Add<Article>(article);
                await _dbContext.TrySaveChangesAsync<Article>();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, $"The resource of type {typeof(DbSet<Article>)} is null.");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "An entity to be added already exists in resource.");
                throw;
            }
            catch (NotSupportedException ex)
            {
                _logger.LogError(ex, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(article)}' is null.");
                throw;
            }
            return result;
        }

        public async Task DeleteArticleAsync(string id)
        {
            try
            {
                await _repository.DeleteAsync<Article>(id);
                await _dbContext.TrySaveChangesAsync<Article>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Article>)} is null.");
                throw;
            }
            catch (ArgumentException e2)
            {
                _logger.LogError(e2, $"The argument '{nameof(id)}' is null or empty.");
                throw;
            }
            catch (InvalidOperationException e3)
            {
                _logger.LogError(e3, "The quering resource is empty or matching element not found.");
                throw;
            }
            catch (NotSupportedException e4)
            {
                _logger.LogError(e4, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            IEnumerable<Article> result;
            try
            {
                result = await _repository.GetAllAsync<Article>();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e, $"The resource of type {typeof(DbSet<Article>)} doesnt exist.");
                throw;
            }
            return result;
        }

        public async Task<Article> GetArticleAsync(string id)
        {
            Article result;
            try
            {
                result = await _repository.GetByIdAsync<Article>(id);
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Article>)} doesnt exist.");
                throw;
            }
            catch (ArgumentException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(id)}' is null ore empty string.");
                throw;
            }
            catch (InvalidOperationException e3)
            {
                _logger.LogError(e3, "Searching element not found or resource is empty. See exception for more information.");
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync(IEnumerable<string> ids)
        {
            IEnumerable<Article> result;
            try
            {
                result = _repository.GetByIds<Article>(ids);
                await _dbContext.TrySaveChangesAsync<Article>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Article>)} doesnt exist.");
                throw;
            }
            catch (ArgumentOutOfRangeException e2)
            {
                _logger.LogError(e2, $"The length of '{nameof(ids)}' is grater than overall count of entities in resource or resource is empty.");
                throw;
            }
            catch (ArgumentException e3)
            {
                _logger.LogError(e3, $"Argument '{nameof(ids)}' is null ore empty.");
                throw;
            }
            catch (NotSupportedException e4)
            {
                _logger.LogError(e4, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Article>> GetArticlesByAsync(Expression<Func<Article, bool>> predicate)
        {
            var result = new Article[] { }.AsEnumerable();
            if (predicate is null)
            {
                var e = new ArgumentNullException(nameof(predicate), $"Argument '{nameof(predicate)}' cannot be null.");
                _logger.LogError(e, $"Argument '{nameof(predicate)}' is null.");
                throw e;
            }

            if (_dbContext.Set<Article>() is null)
            {
                var e = new NullReferenceException($"The resource of type {typeof(DbSet<Article>)} doesnt exist.");
                _logger.LogError(e, $"The resource of type {typeof(DbSet<Article>)} doesnt exist.");
                throw e;
            }

            if (_dbContext.Set<Article>().Count() == 0)
                return result;

            result = await _dbContext.Set<Article>().Where(predicate).ToListAsync();
            return result.AsEnumerable();
        }

        public async Task<Article> UpdateArticleAsync(Article article)
        {
            Article result;

            var validationResult = _validator.Validate(article);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                var ex = new ArgumentException(nameof(article));
                _logger.LogError(ex, $"Argument '{nameof(article)}' is invalid. {error.ErrorMessage}.");
                throw ex;
            }

            try
            {
                result = await _repository.UpdateAsync<Article>(article);
                await _dbContext.TrySaveChangesAsync<Article>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Article>)} is null.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(article)}' is null.");
                throw;
            }
            catch (ArgumentException e3)
            {
                _logger.LogError(e3, $"Value of '{nameof(article.Id)}' is null or empty.");
                throw;
            }
            catch (InvalidOperationException e4)
            {
                _logger.LogError(e4, "The quering resource is empty or matching element not found.");
                throw;
            }
            catch (NotSupportedException e5)
            {
                _logger.LogError(e5, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            return result;
        }
    }
}
