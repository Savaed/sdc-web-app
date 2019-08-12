using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    /// <summary>
    /// A generic repository for database CRUD operations. Methods from this class should not be used directly for database operations on specific data types.
    /// For operations on specific data types you need to create database services for all of those data types.
    /// Note that any changes obtained using the methods of this class will be saved only after <seealso cref="DbContext.SaveChangesAsync()"/> or <see cref="DbContext.SaveChanges()"/> is called.
    /// </summary>
    public sealed class DbRepository : IDbRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DbRepository> _logger;


        public DbRepository(ApplicationDbContext dbContext, ILogger<DbRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Asynchronously retrives matching entity from database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entity to be retrived.</typeparam>
        /// <param name="id">Id of entity.</param>
        /// <returns>Matching <see cref="{TEntity}"/> entity.</returns>
        /// <exception cref="ArgumentException">Argument id is null or empty.</exception>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="InvalidOperationException">Element not found.</exception>
        public async Task<TEntity> GetByIdAsync<TEntity>(string id) where TEntity : class, IEntity
        {
            if (string.IsNullOrEmpty(id))
            {
                var e = new ArgumentException($"Argument { nameof(id) } can not be null or empty string. Value of {nameof(id)}: {id}");
                _logger.LogError(e, $"Encountered { e.GetType() }. Parameter: { nameof(id) } can not be null or empty string.");
                throw e;
            }

            if (_dbContext.Set<TEntity>() is null)
            {
                var e = new NullReferenceException($"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                _logger.LogError(e, $"Encountered { e.GetType() }. The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                throw e;
            }

            TEntity searchingElement;

            try
            {
                searchingElement = await _dbContext.Set<TEntity>().SingleAsync(x => x.Id.Equals(id));
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, $"Searching element not found");
                throw;
            }
            return searchingElement;
        }

        /// <summary>
        /// Retrives matching entities from database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entities to be retrived.</typeparam>
        /// <param name="ids">Entities ids.</param>
        /// <returns>An <see cref="IEnumerable{TEntity}"/> of matching entities</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentException">An argument <paramref name="ids"/> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The length of <paramref name="ids"/> is grater than overall count of entities in resource or resource is empty.</exception>
        public IEnumerable<TEntity> GetByIds<TEntity>(IEnumerable<string> ids) where TEntity : class, IEntity
        {
            if (_dbContext.Set<TEntity>() is null)
            {
                var e = new NullReferenceException($"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                _logger.LogError(e, $"Encountered { e.GetType() }. The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                throw e;
            }

            if (ids is null || ids.Count() == 0)
            {
                var e = new ArgumentException($"Argument { nameof(ids) } can not be null or empty string array. Current value of {nameof(ids)}: {ids?.Count().ToString()}");
                _logger.LogError(e, $"Encountered {e.GetType()}. Parameter: {nameof(ids)} can not be null or empty string array.");
                throw e;
            }

            int allElementsCount = _dbContext.Set<TEntity>().Count();

            if (ids.Count() > allElementsCount)
            {
                var e = new ArgumentOutOfRangeException($"The length of an argument {nameof(ids)} is outside the allowable range of values. Length of {nameof(ids)}: {ids.Count().ToString()} but " +
                    $"maximum allowable length is {allElementsCount.ToString()}. This LogError may ocure when more ids than all elements in resource is passing to method or if resource is empty.");
                _logger.LogError(e, $"Encountered {e.GetType()}. The length of an argument {nameof(ids)} is outside the allowable range of values.");
                throw e;
            }

            var searchingResource = _dbContext.Set<TEntity>();
            var result = new List<TEntity>();

            foreach (var id in ids)
            {
                var elements = searchingResource.Where(x => x.Id.Equals(id)).AsEnumerable();
                result.AddRange(elements);
            }

            return result.OrderBy(x => x.Id).AsEnumerable();
        }

        /// <summary>
        /// Asynchronously retrives all entities from database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entities to be retrived.</typeparam>
        /// <returns>An <see cref="IEnumerable{TEntity}"/> of matching entities</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IEntity
        {
            if (_dbContext.Set<TEntity>() is null)
            {
                var e = new NullReferenceException($"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                _logger.LogError(e, $"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                throw e;
            }
            var result = await _dbContext.Set<TEntity>().ToListAsync();
            return result.AsEnumerable();
        }

        /// <summary>
        /// Adds given entity to database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entity to be added.</typeparam>
        /// <param name="entityToAdd">An entity to be added.</param>
        /// <returns>An entity which was added.</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentNullException">An arguemnt <paramref name="entityToAdd"/> is null.</exception>
        /// <exception cref="InvalidOperationException">An entity to be added already exists in resource</exception>
        public TEntity Add<TEntity>(TEntity entityToAdd) where TEntity : class, IEntity
        {
            if (entityToAdd is null)
            {
                var e = new ArgumentNullException($"Argument { nameof(entityToAdd) } can not be null.");
                _logger.LogError(e, $"Encountered { e.GetType() }. Parameter: { nameof(entityToAdd) } can not be null.");
                throw e;
            }

            if (_dbContext.Set<TEntity>() is null)
            {
                var e = new NullReferenceException($"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                _logger.LogError(e, $"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                throw e;
            }

            var searchingResource = _dbContext.Set<TEntity>();
            bool isResourceAlreadyContainsElement = searchingResource.Contains(entityToAdd);

            if (isResourceAlreadyContainsElement)
            {
                var e = new InvalidOperationException("Element to be added already exists in resource.");
                _logger.LogError(e, "Can not add element which already exists in rsource.");
                throw e;
            }

            var result = searchingResource.Add(entityToAdd);
            return result.Entity;
        }

        /// <summary>
        /// Asynchronously updates given entity with matching entity in database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entity to be updated.</typeparam>
        /// <param name="entityToUpdate">An entity to be updated.</param>
        /// <returns>An entity which was updated.</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentNullException">An argument <paramref name="entityToUpdate"/> is null.</exception>
        /// <exception cref="ArgumentException">A value of Id in given entity is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Resource is empty or matching element not found.</exception>
        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class, IEntity
        {
            if (entityToUpdate is null)
            {
                var e = new ArgumentNullException($"Argument { nameof(entityToUpdate) } can not be null.");
                _logger.LogError(e, $"Encountered { e.GetType() }. Parameter: { nameof(entityToUpdate) } can not be null.");
                throw e;
            }

            if (string.IsNullOrEmpty(entityToUpdate.Id))
            {
                var e = new ArgumentException($"Value of { nameof(entityToUpdate.Id) } can not be null or empty string.");
                _logger.LogError(e, $"Encountered {e.GetType()}. Parameter: {nameof(entityToUpdate.Id)} can not be null or empty string.");
                throw e;
            }

            if (_dbContext.Set<TEntity>() is null)
            {
                var e = new NullReferenceException($"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                _logger.LogError(e, $"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                throw e;
            }

            var searchingResource = _dbContext.Set<TEntity>();
            int resourceLength = searchingResource.Count();
            bool isIdMatches = await searchingResource.AnyAsync(x => x.Id.Equals(entityToUpdate.Id));

            if (resourceLength == 0 || isIdMatches == false)
            {
                var e = new InvalidOperationException($"Update operations is invalid due to querying resource is empty or matching element not found. Resource: {searchingResource.GetType().ToString()} is empty?: " +
                    $"{ (resourceLength == 0).ToString() }");
                _logger.LogError(e, "Update operations is invalid due to querying resource is empty or matching element not found.");
                throw e;
            }

            (entityToUpdate as BasicEntity).UpdatedAt = DateTime.UtcNow;
            var result = searchingResource.Update(entityToUpdate);
            return result.Entity;
        }

        /// <summary>
        /// Asynchronously deletes entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to be deleted.</typeparam>
        /// <param name="id">Id of entity to be deleted.</param>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentException">An arguement <paramref name="id"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Matching element not found.</exception>
        public async Task DeleteAsync<TEntity>(string id) where TEntity : class, IEntity
        {
            if (string.IsNullOrEmpty(id))
            {
                var e = new ArgumentException($"Argument{ nameof(id) } can not be null or empty string.");
                _logger.LogError(e, $"Encountered {e.GetType()}. Parameter: {nameof(id)} can not be null or empty string.");
                throw e;
            }

            if (_dbContext.Set<TEntity>() is null)
            {
                var e = new NullReferenceException($"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                _logger.LogError(e, $"The resource which is querying is null. In this case resource is: {typeof(TEntity)}s.");
                throw e;
            }

            var searchingResource = _dbContext.Set<TEntity>();

            // Because argument of SingleAsync() is id, it is not possible that SingleAsync() method throws exception.
            var elementToBeDeleted = await searchingResource.SingleAsync(x => x.Id.Equals(id));

            if (elementToBeDeleted is null)
            {
                var e = new InvalidOperationException("Matching element not found and delete operation terminated.");
                _logger.LogError(e, "Matching element not found and delete operation terminated.");
                throw e;
            }

            searchingResource.Remove(elementToBeDeleted);
        }
    }
}
