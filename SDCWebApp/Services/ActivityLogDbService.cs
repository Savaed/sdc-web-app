using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public class ActivityLogDbService : ServiceBase, IActivityLogDbService
    {
        private readonly ILogger<ActivityLogDbService> _logger;
        private readonly ApplicationDbContext _dbContext;


        public ActivityLogDbService(ApplicationDbContext dbContext, ILogger<ActivityLogDbService> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// Asynchronously adds <see cref="ActivityLog"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="log">The activity log to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="log"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="log"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<ActivityLog> AddAsync(ActivityLog log)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (log is null)
                throw new ArgumentNullException($"Argument '{nameof(log)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.ActivityLogs ?? throw new InternalDbServiceException($"Table of type '{typeof(ActivityLog).Name}' is null.");

            try
            {
                if (_dbContext.ActivityLogs.Contains(log))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{log.Id}'.");

                _logger.LogDebug($"Starting add activity log with id '{log.Id}'.");
                var addedlog = _dbContext.ActivityLogs.Add(log).Entity;
                await _dbContext.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedlog;
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
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding an activity log with id: '{log?.Id}' to the database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="ActivityLog"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<ActivityLog> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.ActivityLogs ?? throw new InternalDbServiceException($"Table of type '{typeof(ActivityLog).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve activity log with id: '{id}' from the database.");
                var log = await _dbContext.ActivityLogs.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return log;
            }
            catch (InvalidOperationException ex)
            {
                string message = _dbContext.ActivityLogs.Count() == 0 ? $"Element not found because resource {_dbContext.ActivityLogs.GetType().Name} does contain any elements. See the inner exception for more details."
                    : "Element not found. See the inner exception for more details.";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving activity log with id '{id}' from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="ActivityLog"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="ActivityLog"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<ActivityLog>> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 30)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.ActivityLogs ?? throw new InternalDbServiceException($"Table of type '{typeof(ActivityLog).Name}' is null.");

            try
            {
                IEnumerable<ActivityLog> logs = new ActivityLog[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _dbContext.ActivityLogs.CountAsync();
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
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {logs.Count()} elements.");
                    return logs;
                }

                _logger.LogDebug($"Starting retrieve data. '{nameof(pageNumber)}': {pageNumber.ToString()}, '{nameof(pageSize)}': {pageSize.ToString()}.");
                logs = _dbContext.ActivityLogs.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return logs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving activity logs from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }


        #region Privates

        protected async override Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            return await _dbContext.ActivityLogs.ContainsAsync(entity as ActivityLog);
        }

        #endregion

    }
}
