﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="Article"/> entities in the database.
    /// </summary>
    public class ArticleDbService : ServiceBase, IArticleDbService
    {
        private readonly ILogger<ArticleDbService> _logger;
        private readonly ApplicationDbContext _context;


        public ArticleDbService(ApplicationDbContext context, ILogger<ArticleDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="Article"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="article">The article to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="article"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Article"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<Article> AddAsync(Article article)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");
            // Call normal add mode.
            return await AddBaseAsync(article);
        }

        /// <summary>
        /// Asynchronously adds <see cref="Article"/> entity to the database. Do not allow to add an entity with the same Title, Text and Author properties.  
        /// Throws an exception if already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="article">The article to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="article"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Article"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<Article> RestrictedAddAsync(Article article)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedAddAsync)}'.");
            // Call restricted add mode.
            return await AddBaseAsync(article, true);
        }

        /// <summary>
        /// Asynchronously deletes <see cref="Article"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Article"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Articles ?? throw new InternalDbServiceException($"Table of type '{typeof(Article).Name}' is null.");

            try
            {
                if (_context.Articles.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.Articles.GetType().Name} does not contain any element.");

                if (await _context.Articles.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var articleToBeDeleted = await _context.Articles.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove article with id '{articleToBeDeleted.Id}'.");
                _context.Articles.Remove(articleToBeDeleted);
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Remove data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(DeleteAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element. See exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing article with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="Article"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="Article"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Articles ?? throw new InternalDbServiceException($"Table of type '{typeof(Article).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all articles from database.");
                var articles = await _context.Articles.ToArrayAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {articles.Count()} elements.");
                return articles.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all articles from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs <see cref="Article"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<Article> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Articles ?? throw new InternalDbServiceException($"Table of type '{typeof(Article).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve article with id: '{id}' from database.");
                var tariff = await _context.Articles.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return tariff;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.Articles.Count() == 0 ? $"Element not found because resource {_context.Articles.GetType().Name} does contain any elements. See the inner exception for more details."
                    : "Element not found. See the inner exception for more details.";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving article with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="Article"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="Article"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Article>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");

            // TODO Create only for unit tests purposes. In debug and later should be Migrate()!!!
            await EnsureDatabaseCreatedAsync();
            _ = _context?.Articles ?? throw new InternalDbServiceException($"Table of type '{typeof(Article).Name}' is null.");

            try
            {
                IEnumerable<Article> articles = new Article[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.Articles.CountAsync();
                int numberOfElementsOnLastPage = numberOfResourceElements % pageSize;
                int numberOfFullPages = (numberOfResourceElements - numberOfElementsOnLastPage) / pageSize;

                if (numberOfElementsOnLastPage > 0)
                {
                    maxNumberOfPageWithData = ++numberOfFullPages;
                    _logger.LogWarning($"Last page of data contain {numberOfElementsOnLastPage} elements which is less than specified in {nameof(pageSize)}: {pageSize}.");
                }
                else
                    maxNumberOfPageWithData = numberOfFullPages;

                if (numberOfResourceElements == 0 || pageSize == 0 || pageNumber > maxNumberOfPageWithData)
                {
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {articles.Count()} elements.");
                    return articles;
                }

                _logger.LogDebug($"Starting retrieve data. '{nameof(pageNumber)}': {pageNumber.ToString()}, '{nameof(pageSize)}': {pageSize.ToString()}.");
                articles = _context.Articles.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return articles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving articles from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="Article"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="article">The article to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="article"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="article"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<Article> UpdateAsync(Article article)
        {
            // If _context.Articles does not null, but does not exist (as table in database, not as object using by EF Core)
            // following if statement (exactly Count method) will throw exception about this table ("no such table: 'Articles'." or something like that).
            // So you can catch this exception and re-throw in InternalDbServiceException to next handling in next level layer e.g Controller.

            // Maybe throwing exception in try block seems to be bad practice and a little bit tricky, but in this case is neccessery.
            // Refference to Groups while it does not exist cause throwing exception and without this 2 conditions below you cannot check 
            // is there any element for update in database.

            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");

            // Call normal update mode.
            return await UpdateBaseAsync(article);
        }

        /// <summary>
        /// Asynchronously updates <see cref="Article"/> entity ignoring read-only properties like as Id, CreatedAt, UpdatedAt, ConcurrencyToken. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="article">The article to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="article"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="article"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<Article> RestrictedUpdateAsync(Article article)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdateAsync)}'.");
            // Call restricted update mode.
            return await UpdateBaseAsync(article, true);
        }    


        #region Privates

        /// <summary>
        /// Asynchronously adds <see cref="Article"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'Title', 'Text' and 'Author' value.
        /// </summary>
        /// <param name="article"><see cref="Article"/> to be added.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'Title', 'Text' and 'Author' value.</param>
        /// <returns>Added <see cref="Article"/> entity.</returns>
        private async Task<Article> AddBaseAsync(Article article, bool isRestrict = false)
        {
            _logger.LogInformation($"Starting method '{nameof(AddBaseAsync)}'.");

            if (article is null)
                throw new ArgumentNullException($"Argument '{nameof(article)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Articles ?? throw new InternalDbServiceException($"Table of type '{typeof(Article).Name}' is null.");

            try
            {
                if (isRestrict)
                {
                    // Resticted add mode that use custom equality comparer. Articles are equal if they have the same Title, Text and Author.

                    // Check if exist in db article with the same Title, Text and Author as adding.
                    if (await IsEntityAlreadyExistsAsync(article))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. The value of '{nameof(article.Title)}', " +
                            $"'{nameof(article.Text)}' and '{nameof(article.Author)}' are not unique.");
                }
                else
                {
                    // Normal add mode without any additional restrictions.
                    if (_context.Articles.Contains(article))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{article.Id}'.");
                }

                _logger.LogDebug($"Starting add tariff with id '{article.Id}'.");
                var addedArticle = _context.Articles.Add(article).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddBaseAsync)}'.");
                return addedArticle;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - Changes made by add operations cannot be saved properly. See the inner exception for more details. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception for more details.", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding an article to the database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="Article"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used and update allow entirely entity updating. 
        /// Otherwise the restricted mode will be using. It will ignore updating some read-only properties.
        /// </summary>
        /// <param name="article"><see cref="Article"/> to be updated.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will ignore some read-only properties changes.</param>
        /// <returns>Updated <see cref="Article"/> entity.</returns>
        private async Task<Article> UpdateBaseAsync(Article article, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(UpdateBaseAsync)}'.");

            _ = article ?? throw new ArgumentNullException(nameof(article), $"Argument '{nameof(article)}' cannot be null.");

            if (string.IsNullOrEmpty(article.Id))
                throw new ArgumentException($"Argument '{nameof(article.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Articles ?? throw new InternalDbServiceException($"Table of type '{typeof(Article).Name}' is null.");

            try
            {
                if (_context.Articles.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{article.Id}' for update. Resource {_context.Articles.GetType().Name} does not contain any element.");

                if (await _context.Articles.ContainsAsync(article) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{article.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update article with id '{article.Id}'.");

                Article updatedArticle = null;
                article.UpdatedAt = DateTime.UtcNow;

                if (isRestrict)
                {
                    // Resticted update mode that ignores all changes in read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken.
                    var originalArticle = await _context.Articles.SingleAsync(x => x.Id.Equals(article.Id));
                    updatedArticle = BasicRestrictedUpdate(originalArticle, article) as Article;
                }
                else
                {
                    // Normal update mode without any additional restrictions.
                    updatedArticle = _context.Articles.Update(article).Entity;
                }

                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(UpdateBaseAsync)}'.");
                return updatedArticle;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating article with id '{article.Id}'. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allArticles = await _context.Articles.ToArrayAsync();
            return allArticles.Any(x => x.Equals(entity as Article));
        }

        #endregion

    }
}
