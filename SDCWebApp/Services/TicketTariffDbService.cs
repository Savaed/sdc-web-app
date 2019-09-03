using Microsoft.EntityFrameworkCore;
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
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="TicketTariff"/> entities in the database.
    /// </summary>
    public class TicketTariffDbService : ServiceBase, ITicketTariffDbService
    {
        private readonly ILogger<TicketTariffDbService> _logger;
        private readonly ApplicationDbContext _context;


        public TicketTariffDbService(ApplicationDbContext context, ILogger<TicketTariffDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="tariff"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in the database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="TicketTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<TicketTariff> AddAsync(TicketTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");
            // Call normal add mode.
            return await AddBaseAsync(tariff);
        }

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> entity to the database. Do not allow add entity with the same Description, IsPerHour, IsPerPerson and DefaultPrice. 
        /// Throws an exception if already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="tariff"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="TicketTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<TicketTariff> RestrictedAddAsync(TicketTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedAddAsync)}'.");
            // Call restricted add mode.
            return await AddBaseAsync(tariff, true);
        }

        /// <summary>
        /// Asynchronously deletes <see cref="TicketTariff"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="TicketTariff"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.TicketTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(TicketTariff).Name}' is null.");

            try
            {
                if (_context.TicketTariffs.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.TicketTariffs.GetType().Name} does not contain any element.");

                if (await _context.TicketTariffs.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var tariffToBeDeleted = await _context.TicketTariffs.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove ticket tariff with id '{tariffToBeDeleted.Id}'.");
                _context.TicketTariffs.Remove(tariffToBeDeleted);
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Remove data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(DeleteAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Cannot found element. See the exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing ticket tariff with id '{id}' from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="TicketTariff"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="TicketTariff"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<TicketTariff>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.TicketTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(TicketTariff).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all ticket tariffs from the database.");
                var tariffs = await _context.TicketTariffs.ToArrayAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {tariffs.Count()} elements.");
                return tariffs.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all ticket tariffs from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="TicketTariff"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrieved. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<TicketTariff> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.TicketTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(TicketTariff).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve ticket tariff with id: '{id}' from the database.");
                var tariff = await _context.TicketTariffs.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return tariff;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.TicketTariffs.Count() == 0 ? $"Element not found because resource {_context.TicketTariffs.GetType().Name} does contain any elements. See the inner exception."
                    : "Element not found. See the inner exception.";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving sighseeing tariff with id '{id}' from database. See the inner exception for mre details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="TicketTariff"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="TicketTariff"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<TicketTariff>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equals to 1 is valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equals to 0 is valid.");

            // TODO Create only for unit tests purposes. In debug and later should be Migrate()!!!
            await EnsureDatabaseCreatedAsync();
            _ = _context?.TicketTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(TicketTariff).Name}' is null.");

            try
            {
                IEnumerable<TicketTariff> tariffs = new TicketTariff[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.TicketTariffs.CountAsync();
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

                _logger.LogDebug($"Starting retrieve data. '{nameof(pageNumber)}': {pageNumber.ToString()}, '{nameof(pageSize)}': {pageSize.ToString()}.");
                tariffs = _context.TicketTariffs.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return tariffs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving ticket tariffs from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<TicketTariff> UpdateAsync(TicketTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");
            // Call normal update mode.
            return await UpdateBaseAsync(tariff);
        }     

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> entity ignoring read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="tariff">The tariff to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tariff"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tariff"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<TicketTariff> RestrictedUpdateAsync(TicketTariff tariff)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdateAsync)}'.");
            // Call restricted update mode.
            return await UpdateBaseAsync(tariff, true);
        }


        #region Privates      

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used and update allow entirely entity updating. 
        /// Otherwise the restricted mode will be using. It will ignore updating some read-only properties.
        /// </summary>
        /// <param name="tariff"><see cref="TicketTariff"/> To be updated</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will ignore some read-only properties changes.</param>
        /// <returns>Updated <see cref="TicketTariff"/> entity.</returns>
        private async Task<TicketTariff> UpdateBaseAsync(TicketTariff tariff, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(UpdateBaseAsync)}'.");

            _ = tariff ?? throw new ArgumentNullException(nameof(tariff), $"Argument '{nameof(tariff)}' cannot be null.");

            if (string.IsNullOrEmpty(tariff.Id))
                throw new ArgumentException($"Argument '{nameof(tariff.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.TicketTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(TicketTariff).Name}' is null.");

            try
            {
                if (_context.TicketTariffs.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{tariff.Id}' for update. Resource {_context.Groups.GetType().Name} does not contain any element.");

                if (await _context.TicketTariffs.ContainsAsync(tariff) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{tariff.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update ticket tariff with id '{tariff.Id}'.");

                TicketTariff updatedTariff = null;
                tariff.UpdatedAt = DateTime.UtcNow;

                if (isRestrict)
                {
                    // Resticted update mode that ignores all changes in read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken and navigation properties like TicketTariffs.
                    var originalTariff = await _context.TicketTariffs.SingleAsync(x => x.Id.Equals(tariff.Id));
                    updatedTariff = BasicRestrictedUpdate(originalTariff, tariff) as TicketTariff;
                }
                else
                {
                    // Normal update mode without any additional restrictions.
                    updatedTariff = _context.TicketTariffs.Update(tariff).Entity;
                }

                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(UpdateBaseAsync)}'.");
                return updatedTariff;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Cannot found element for update. See the exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating ticket tariff with id '{tariff.Id}'. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'Description', 'IsPerHour', 'IsPerPerson' and 'DefaultPrice' value.
        /// </summary>
        /// <param name="tariff"><see cref="TicketTariff"/> to be added.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'Description', 'IsPerHour', 'IsPerPerson' and 'DefaultPrice' value.</param>
        /// <returns>Added <see cref="TicketTariff"/> entity.</returns>
        private async Task<TicketTariff> AddBaseAsync(TicketTariff tariff, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(AddBaseAsync)}'.");

            if (tariff is null)
                throw new ArgumentNullException($"Argument '{nameof(tariff)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.TicketTariffs ?? throw new InternalDbServiceException($"Table of type '{typeof(TicketTariff).Name}' is null.");

            try
            {
                if (isRestrict)
                {
                    // Resticted add mode that use custom equality comparer. The ticket tariffs are equal if they have the same Description, IsPerHour, IsPerPerson and DefaultPrice.

                    // Check if exist in db tariff with the same Description, IsPerHour, IsPerPerson and DefaultPrice as adding.
                    if (await IsEntityAlreadyExistsAsync(tariff))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. The value of '{nameof(tariff.Description)}', " +
                            $"'{nameof(tariff.IsPerHour)}', '{nameof(tariff.IsPerPerson)}' and '{nameof(tariff.DefaultPrice)}' are not unique.");
                }
                else
                {
                    // Normal add mode without any additional restrictions.
                    if (_context.TicketTariffs.Contains(tariff))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{tariff.Id}'.");
                }

                _logger.LogDebug($"Starting add tariff with id '{tariff.Id}'.");
                var addedTariff = _context.TicketTariffs.Add(tariff).Entity;
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
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding ticket tarifff with id: '{tariff.Id}' to the database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allTariffs = await _context.TicketTariffs.ToArrayAsync();
            return allTariffs.Any(x => x.Equals(entity as TicketTariff));

        }

        #endregion
    }
}
