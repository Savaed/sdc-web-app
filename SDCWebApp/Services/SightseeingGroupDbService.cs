﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides methods for get, add, update and delete operations for <see cref="SightseeingGroup"/> entities in the database.
    /// </summary>
    public class SightseeingGroupDbService : ISightseeingGroupDbService
    {
        private readonly ILogger<SightseeingGroupDbService> _logger;
        private readonly ApplicationDbContext _context;


        public SightseeingGroupDbService(ApplicationDbContext context, ILogger<SightseeingGroupDbService> logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="SightseeingGroup"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="group">The group to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="group"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="SightseeingGroup"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<SightseeingGroup> AddAsync(SightseeingGroup group)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");

            if (group is null)
                throw new ArgumentNullException($"Argument '{nameof(group)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Groups ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingGroup).Name}' is null.");

            try
            {
                if (_context.Groups.Contains(group))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{group.Id}'.");

                _logger.LogDebug($"Starting add group with id '{group.Id}'.");
                var addedGroup = _context.Groups.Add(group).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedGroup;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} Changes made by add operations cannot be saved properly. See inner exception. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See inner exception for more details.", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} There is already the same element in the database as the one to be added. Id of this element: '{group.Id}'.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding element with id '{group?.Id}' to the database. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously deletes <see cref="SightseeingGroup"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="SightseeingGroup"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Groups ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingGroup).Name}' is null.");

            try
            {
                if (_context.Groups.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.Groups.GetType().Name} does not contain any element.");

                if (await _context.Groups.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var groupToBeDeleted = await _context.Groups.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove sightseeing group with id '{groupToBeDeleted.Id}'.");
                _context.Groups.Remove(groupToBeDeleted);
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
                var internalException = new InternalDbServiceException($"Encountered problem when removing element with id '{id}' from database. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="SightseeingGroup"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="SightseeingGroup"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<SightseeingGroup>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Groups ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingGroup).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all sightseeing groups from database.");
                var groups = await _context.Groups.Include(x => x.Tickets).ToListAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {groups.Count} elements.");
                return groups.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing group from database. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs <see cref="SightseeingGroup"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<SightseeingGroup> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Groups ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingGroup).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve sighseeing group with id: '{id}' from database.");
                var group = await _context.Groups.Include(x => x.Tickets).SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return group;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.Groups.Count() == 0 ? $"Element not found because resource {_context.Groups.GetType().Name} is empty. See inner exception for more details."
                    : "Element not found. See inner exception for more details.";
                _logger.LogError(ex, $"{ex.GetType().Name} {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving sighseeing group with id '{id}' from database. See inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="SightseeingGroup"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="SightseeingGroup"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<SightseeingGroup>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Discounts ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingGroup).Name}' is null.");

            try
            {
                IEnumerable<SightseeingGroup> groups = new SightseeingGroup[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.Groups.CountAsync();
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
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {groups.Count()} elements.");
                    return groups;
                }

                _logger.LogDebug($"Starting retrieve data. {nameof(pageNumber)} '{pageNumber.ToString()}', {nameof(pageSize)} '{pageSize.ToString()}'.");
                groups = _context.Groups.Include(x => x.Tickets).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return groups;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing groups from database. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="SightseeingGroup"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="group">The group to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="group"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="group"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<SightseeingGroup> UpdateAsync(SightseeingGroup group)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");

            _ = group ?? throw new ArgumentNullException(nameof(group), $"Argument '{nameof(group)}' cannot be null.");

            if (string.IsNullOrEmpty(group.Id))
                throw new ArgumentException($"Argument '{nameof(group.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Groups ?? throw new InternalDbServiceException($"Table of type '{typeof(SightseeingGroup).Name}' is null.");

            try
            {
                // If _context.Groups does not null, but does not exist (as table in database, not as object using by EF Core)
                // following if statement (exactly Count method) will throw exception about this table ("no such table: 'Groups'." or something like that).
                // So you can catch this exception and re-throw in InternalDbServiceException to next handling in next level layer e.g Controller.

                // Maybe throwing exception in try block seems to be bad practice and a little bit tricky, but in this case is neccessery.
                // Refference to Groups while it does not exist cause throwing exception and without this 2 conditions below you cannot check 
                // is there any element for update in database.
                if (_context.Groups.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{group.Id}' for update. Resource {_context.Groups.GetType().Name} does not contain any element.");

                if (await _context.Groups.ContainsAsync(group) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{group.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update discount with id '{group.Id}'.");
                group.UpdatedAt = DateTime.UtcNow;
                var updatedGroup = _context.Groups.Update(group).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(UpdateAsync)}'.");
                return updatedGroup;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating sighseeing group with id '{group.Id}'. See inner excpetion for more details.", ex);
                throw internalException;
            }
        }


        #region Privates      

        private async Task EnsureDatabaseCreatedAsync()
        {
            if (await _context.Database.EnsureCreatedAsync() == false)
                _logger.LogWarning($"Database with provider '{_context.Database.ProviderName}' does not exist. It Will be created but not using migrations so it cannot be updating using migrations later.");
        }

        #endregion

    }
}