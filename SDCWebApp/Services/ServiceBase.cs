using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SDCWebApp.Data;
using SDCWebApp.Models;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Helpers.Extensions;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides basic operations on database services.
    /// </summary>
    public abstract class ServiceBase
    {
        private readonly string[] _readOnlyPropertiesNames = new string[]
        {
            $"{nameof(BasicEntity.Id)}",
            $"{nameof(BasicEntity.CreatedAt)}",
            $"{nameof(BasicEntity.ConcurrencyToken)}",
            $"{nameof(BasicEntity.UpdatedAt)}"
        };
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;


        public ServiceBase(ApplicationDbContext context, ILogger logger)
        {
            _dbContext = context;
            _logger = logger;
        }


        /// <summary>
        /// Updates an entity by ignoring some properties (all <see cref="BasicEntity"/> properties)  
        /// because they are read-only and cannot be changed externally. Returns updated entity with new values.
        /// </summary>
        /// <param name="originalEntity">The original value of updating entity retrieved from the database.</param>
        /// <param name="entityToBeUpdated">The entity to be updated.</param>
        /// <returns>Updated entity.</returns>
        protected virtual BasicEntity BasicRestrictedUpdate(BasicEntity originalEntity, BasicEntity entityToBeUpdated)
        {
            _logger.LogInformation($"Starting method '{nameof(BasicRestrictedUpdate)}'.");

            _dbContext.Attach(originalEntity);
            var properties = originalEntity.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (_readOnlyPropertiesNames.Any(x => x.Equals(property.Name)))
                    _logger.LogWarning($"Encouneterd property '{property.Name}' when updating '{typeof(BasicEntity).Name}'. This property is read-only and any attempts to change its value will be ignored.");
                else
                    property.SetValue(originalEntity, property.GetValue(entityToBeUpdated));
            }

            originalEntity.UpdatedAt = DateTime.UtcNow;
            _logger.LogInformation($"Finished method '{nameof(BasicRestrictedUpdate)}'.");

            return originalEntity;
        }

        /// <summary>
        /// Ensures if database created. If not it will be created. Note that the database will be created but not using migrations so it cannot be updating using migrations later.
        /// </summary>
        protected virtual async Task EnsureDatabaseCreatedAsync()
        {
            if (await _dbContext.Database.EnsureCreatedAsync() == false)
                _logger.LogWarning($"Database with provider '{_dbContext.Database.ProviderName}' does not exist. It will be created but not using migrations so it cannot be updating using migrations later.");
        }

        /// <summary>
        /// Checks if <see cref="BasicEntity"/> entity exists using custom equality comparer.
        /// </summary>
        /// <param name="entity">The <see cref="BasicEntity"/> entity to check if there is the same entity in the database.</param>
        /// <returns></returns>
        protected abstract Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity);

        /// <summary>
        /// Filters set of data of type <typeparamref name="T"/>. Returns filterd data set. Throws <see cref="ArgumentNullException"/> if <paramref name="predicate"/> is null 
        /// or <see cref="InvalidOperationException"/> if cannot filter data.
        /// </summary>
        /// <typeparam name="T">The type of entity to set be filtered.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Filterd <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">If cannot filter data.</exception>
        protected virtual IEnumerable<BasicEntity> GetByPredicate<T>(Expression<Func<T, bool>> predicate) where T : BasicEntity
        {
            _logger.LogDebug($"Starting method '{nameof(GetByPredicate)}'.");

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate), $"Argument '{nameof(predicate)}' cannot be null.");
            }

            try
            {
                IEnumerable<T> result;

                if (typeof(T) == typeof(Ticket))
                {
                    result = (IEnumerable<T>)_dbContext.Tickets.IncludeDetails().Where(predicate as Expression<Func<Ticket, bool>>).AsEnumerable();
                }
                else
                {
                    result = _dbContext.Set<T>().Where(predicate).AsEnumerable();
                }

                _logger.LogDebug($"Finished method '{nameof(GetByPredicate)}'.");
                return result;
            }
            catch (ArgumentNullException ex)
            {
                var exception = new InvalidOperationException($"Cannot apply '{nameof(predicate)}' to filter data. See the inner exception for more details.", ex);
                _logger.LogError($"{exception.GetType().Name} - {exception.Message}", exception);
                throw exception;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
        }
    }
}