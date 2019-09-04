using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using System.Linq.Expressions;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="SightseeingTariff"/> entities in the database.
    /// </summary>
    public class SightseeingTariffDbService : ServiceBase, ISightseeingTariffDbService
    {
        private readonly ILogger<SightseeingTariffDbService> _logger;
        private readonly ApplicationDbContext _context;


        public SightseeingTariffDbService(ApplicationDbContext context, ILogger<SightseeingTariffDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="SightseeingTariff"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="tariff"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that the one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="SightseeingTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<SightseeingTariff> AddAsync(SightseeingTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");
            // Call normal add mode.
            return await AddBaseAsync(tariff);
        }

        /// <summary>
        /// Asynchronously adds <see cref="SightseeingTariff"/> entity to the database. Do not allow add entity with the same Description property. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="tariff"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="SightseeingTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<SightseeingTariff> RestrictedAddAsync(SightseeingTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedAddAsync)}'.");
            // Call restricted add mode.
            return await AddBaseAsync(tariff, true);
        }

        /// <summary>
        /// Asynchronously deletes <see cref="SightseeingTariff"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="SightseeingTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                if (await _context.SightseeingTariffs.AnyAsync(x => x.Id.Equals(id)) == false)
                {
                    if (_context.SightseeingTariffs.Count() == 0)
                        throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.SightseeingTariffs.GetType().Name} does not contain any element.");
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");
                }

                var tariffToBeDeleted = await _context.SightseeingTariffs.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove sightseeing tariff with id '{tariffToBeDeleted.Id}'.");
                _context.SightseeingTariffs.Remove(tariffToBeDeleted);
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Remove data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(DeleteAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Cannot found element. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing sightseeing tariff with id '{id}' from database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="SightseeingTariff"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="SightseeingTariff"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<SightseeingTariff>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all sightseeing tariffs from database.");
                var tariffs = await _context.SightseeingTariffs.Include(x => x.TicketTariffs).ToArrayAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning '{tariffs.Count()}' elements.");
                return tariffs.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all sightseeing tariffs from database. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="SightseeingTariff"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<SightseeingTariff> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve sighseeing tariff with id: '{id}' from database.");
                var tariff = await _context.SightseeingTariffs.Include(x => x.TicketTariffs).SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return tariff;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.SightseeingTariffs.Count() == 0 ? $"Element not found because resource '{_context.SightseeingTariffs.GetType().Name}' does contain any elements."
                    : "Element not found";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving sighseeing tariff with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="SightseeingTariff"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="SightseeingTariff"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<SightseeingTariff>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                IEnumerable<SightseeingTariff> tariffs = Enumerable.Empty<SightseeingTariff>();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.SightseeingTariffs.CountAsync();
                int numberOfElementsOnLastPage = numberOfResourceElements % pageSize;
                int numberOfFullPages = (numberOfResourceElements - numberOfElementsOnLastPage) / pageSize;

                if (numberOfElementsOnLastPage > 0)
                {
                    maxNumberOfPageWithData = ++numberOfFullPages;
                    _logger.LogWarning($"Last page of data contain {numberOfElementsOnLastPage} elements which is less than specified in '{nameof(pageSize)}': {pageSize}.");
                }
                else
                    maxNumberOfPageWithData = numberOfFullPages;

                if (numberOfResourceElements == 0 || pageSize == 0 || pageNumber > maxNumberOfPageWithData)
                {
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {tariffs.Count()} elements.");
                    return tariffs;
                }

                _logger.LogDebug($"Starting retrieve data. '{nameof(pageNumber)}': {pageNumber.ToString()}, '{nameof(pageSize)}': {pageSize.ToString()}.");
                tariffs = _context.SightseeingTariffs.Include(x => x.TicketTariffs).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return tariffs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing tariffs from database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="SightseeingTariff"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<SightseeingTariff> UpdateAsync(SightseeingTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");
            // Call normal update mode.
            return await UpdateBaseAsync(tariff);
        }

        /// <summary>
        /// Asynchronously updates <see cref="SightseeingTariff"/> entity ignoring read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<SightseeingTariff> RestrictedUpdateAsync(SightseeingTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdateAsync)}'.");
            // Call restricted update mode.
            return await UpdateBaseAsync(tariff, true);
        }

        /// <summary>
        /// Filters set of data of type <see cref="SightseeingTariff"/>. Returns filtered data set. Throws an exception if <paramref name="predicate"/> is null, 
        /// or if cannot filter data due to any internal problem.
        /// </summary>
        /// <typeparam name="T">The type of entity to set be filtered.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Filterd <see cref="SightseeingTariff"/> set.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot filter data.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>        
        public async Task<IEnumerable<SightseeingTariff>> GetByAsync(Expression<Func<SightseeingTariff, bool>> predicate)
        {
            _logger.LogInformation($"Starting method '{nameof(GetByAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                var result = GetByPredicate<SightseeingTariff>(predicate) as IEnumerable<SightseeingTariff>;
                _logger.LogInformation($"Finished method '{nameof(GetByAsync)}'.");
                return result;
            }
            catch (ArgumentNullException)
            {
                _logger.LogError($"Argument '{nameof(predicate)}' cannot be null.");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - {ex.Message} See the exception for more details.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing tariffs from database using {nameof(predicate)}. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }


        #region Privates       

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allTariffs = await _context.SightseeingTariffs.ToArrayAsync();
            return allTariffs.Any(x => x.Equals(entity as SightseeingTariff));
        }

        /// <summary>
        /// Asynchronously updates <see cref="SightseeingTariff"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used and update allow entirely entity updating. 
        /// Otherwise the restricted mode will be using. It will ignore updating some read-only properties.
        /// </summary>
        /// <param name="tariff">SightseeingTariff To be updated</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will ignore some read-only properties changes.</param>
        /// <returns>Updated <see cref="SightseeingTariff"/> entity.</returns>
        private async Task<SightseeingTariff> UpdateBaseAsync(SightseeingTariff tariff, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(UpdateBaseAsync)}'.");

            _ = tariff ?? throw new ArgumentNullException(nameof(tariff), $"Argument '{nameof(tariff)}' cannot be null.");

            if (string.IsNullOrEmpty(tariff.Id))
                throw new ArgumentException($"Argument '{nameof(tariff.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                if (_context.SightseeingTariffs.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{tariff.Id}' for update. Resource {_context.Groups.GetType().Name} does not contain any element.");

                if (await _context.SightseeingTariffs.ContainsAsync(tariff) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{tariff.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update tariff with id '{tariff.Id}'.");

                SightseeingTariff updatedTariff = null;
                tariff.UpdatedAt = DateTime.UtcNow;

                if (isRestrict)
                {
                    // Resticted update mode that ignores all changes in read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken and navigation properties like TicketTariffs.
                    var originalTariff = await _context.SightseeingTariffs.SingleAsync(x => x.Id.Equals(tariff.Id));

                    // Set the TicketTariffs navigation property to the original value, because restricted update mode does not allow updating of TicketTariffs.
                    // For any changes from the client in TicketTariff objects, use the TicketTairiffsController methods.
                    tariff.TicketTariffs = originalTariff.TicketTariffs;

                    updatedTariff = BasicRestrictedUpdate(originalTariff, tariff) as SightseeingTariff;
                }
                else
                {
                    // Normal update mode without any additional restrictions.
                    updatedTariff = _context.SightseeingTariffs.Update(tariff).Entity;
                }

                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(UpdateBaseAsync)}'.");
                return updatedTariff;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating sighseeing tariff with id '{tariff.Id}'. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously adds <see cref="SightseeingTariff"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'Name' value. Moreover it will does not allow to add navigation property while adding <see cref="SightseeingTariff"/>.
        /// </summary>
        /// <param name="tariff"><see cref="SightseeingTariff"/> to be added.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'Name' value. Moreover it will does not allow to add navigation property while adding <see cref="SightseeingTariff"/>.</param>
        /// <returns>Added <see cref="SightseeingTariff"/> entity.</returns>
        private async Task<SightseeingTariff> AddBaseAsync(SightseeingTariff tariff, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(AddBaseAsync)}'.");

            if (tariff is null)
                throw new ArgumentNullException($"Argument '{nameof(tariff)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                if (isRestrict)
                {
                    // Resticted add mode that use custom equality comparer. The sightseeing tariffs are equal if they have the same Name.
                    // Moreover this mode does not allow adding navigation property togather with parent entity (SightseeinTariff -> TicketTariffs).
                    tariff.TicketTariffs = null;

                    // Check if exist in db tariff with the same 'Name' as adding.
                    if (await IsEntityAlreadyExistsAsync(tariff))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. The value of '{nameof(tariff.Name)}' is not unique.");
                }
                else
                {
                    // Normal add mode without any additional restrictions.
                    if (_context.SightseeingTariffs.Contains(tariff))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{tariff.Id}'.");
                }

                _logger.LogDebug($"Starting add tariff with id '{tariff.Id}'.");
                var addedTariff = _context.SightseeingTariffs.Add(tariff).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(AddBaseAsync)}'.");
                return addedTariff;
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
                var internalException = new InternalDbServiceException($"Encountered problem when adding sighseeing tarifff to the database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        #endregion

    }
}
