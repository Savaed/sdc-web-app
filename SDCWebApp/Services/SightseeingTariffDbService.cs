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
    /// <summary>
    /// Provides methods for get, add, update and delete operations for <see cref="SightseeingTariff"/> entities in the database.
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
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="SightseeingTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<SightseeingTariff> AddAsync(SightseeingTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (tariff is null)
                throw new ArgumentNullException($"Argument '{nameof(tariff)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");

            try
            {
                if (_context.SightseeingTariffs.Contains(tariff))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{tariff.Id}'.");

                _logger.LogDebug($"Starting add tariff with id '{tariff.Id}'.");
                var addedTariff = _context.SightseeingTariffs.Add(tariff).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedTariff;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See inner exception for more details.", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. Id of this element: '{tariff.Id}'.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding sighseeing tarifff with id '{tariff?.Id}' to the database. See inner excpetion for more details.", ex);
                throw internalException;
            }
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
                if (_context.SightseeingTariffs.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.SightseeingTariffs.GetType().Name} does not contain any element.");

                if (await _context.SightseeingTariffs.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var tariffToBeDeleted = await _context.SightseeingTariffs.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove sightseeing tariff with id '{tariffToBeDeleted.Id}'.");
                _context.SightseeingTariffs.Remove(tariffToBeDeleted);
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
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing sightseeing tariff with id '{id}' from database. See inner excpetion for more details.", ex);
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
                var tariffs = await _context.SightseeingTariffs.Include(x => x.TicketTariffs).ToListAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {tariffs.Count} elements.");
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
        /// Asynchronously retrievs <see cref="SightseeingTariff"/> entity with given <paramref name="id"/> from the database. 
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
                string message = _context.SightseeingTariffs.Count() == 0 ? $"Element not found because resource {_context.SightseeingTariffs.GetType().Name} does contain any elements. See inner exception for more details."
                    : "Element not found. See inner exception for more details.";
                _logger.LogError(ex, $"{ex.GetType().Name} {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving sighseeing tariff with id '{id}' from database. See inner exception for more details.", ex);
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
                    _logger.LogWarning($"Last page of data contain {numberOfElementsOnLastPage} elements which is less than specified in {nameof(pageSize)}: {pageSize}.");
                }
                else
                    maxNumberOfPageWithData = numberOfFullPages;

                if (numberOfResourceElements == 0 || pageSize == 0 || pageNumber > maxNumberOfPageWithData)
                {
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {tariffs.Count()} elements.");
                    return tariffs;
                }

                _logger.LogDebug($"Starting retrieve data. {nameof(pageNumber)} '{pageNumber.ToString()}', {nameof(pageSize)} '{pageSize.ToString()}'.");
                tariffs = _context.SightseeingTariffs.Include(x => x.TicketTariffs).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return tariffs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing tariffs from database. See inner excpetion for more details.", ex);
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
                tariff.UpdatedAt = DateTime.UtcNow;
                var updatedTariff = _context.SightseeingTariffs.Update(tariff).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(UpdateAsync)}'.");
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
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");

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
                var originalTariff = await _context.SightseeingTariffs.SingleAsync(x => x.Id.Equals(tariff.Id));
                var updatedTariff = RestrictedUpdate(originalTariff, tariff) as SightseeingTariff;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(UpdateAsync)}'.");
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
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (tariff is null)
                throw new ArgumentNullException($"Argument '{nameof(tariff)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.SightseeingTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingTariff).Name}' is null.");
            
            try
            {
                // Check if exist in db tariff with the same 'Name' as adding.
                if (await IsEntityAlreadyExistsAsync(tariff))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. The value of '{nameof(tariff.Name)}' is not unique.");

                _logger.LogDebug($"Starting add tariff with id '{tariff.Id}'.");
                tariff.TicketTariffs = null;
                var addedTariff = _context.SightseeingTariffs.Add(tariff).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedTariff;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See inner exception for more details.", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. The value of '{nameof(tariff.Name)}' is not unique.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding sighseeing tarifff with id '{tariff?.Id}' to the database. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }


        #region Privates

        private async Task EnsureDatabaseCreatedAsync()
        {
            if (await _context.Database.EnsureCreatedAsync() == false)
                _logger.LogWarning($"Database with provider '{_context.Database.ProviderName}' does not exist. It will be created but not using migrations so it cannot be updating using migrations later.");
        }

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allTariffs = await _context.SightseeingTariffs.ToArrayAsync();
            return allTariffs.Any(x => x.Equals(entity as SightseeingTariff));
        }

        #endregion

    }
}
