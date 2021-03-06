﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="VisitInfo"/> entities in the database.
    /// </summary>
    public class VisitInfoDbService : ServiceBase, IVisitInfoDbService
    {
        private readonly ILogger<VisitInfoDbService> _logger;
        private readonly ApplicationDbContext _context;


        public VisitInfoDbService(ApplicationDbContext context, ILogger<VisitInfoDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Filters set of data of type <see cref="VisitInfo"/>. Returns filtered data set. Throws an exception if <paramref name="predicate"/> is null, 
        /// or if cannot filter data due to any internal problem.
        /// </summary>
        /// <typeparam name="T">The type of entity to set be filtered.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Filterd <see cref="VisitInfo"/> set.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot filter data.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>        
        public async Task<IEnumerable<VisitInfo>> GetByAsync(Expression<Func<VisitInfo, bool>> predicate)
        {
            _logger.LogInformation($"Starting method '{nameof(GetByAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                var result = GetByPredicate<VisitInfo>(predicate) as IEnumerable<VisitInfo>;
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
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving general sightseeing info from the database using {nameof(predicate)}. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously adds <see cref="VisitInfo"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="info">The info to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="info"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="VisitInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<VisitInfo> AddAsync(VisitInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");
            // Call restricted add mode.
            return await AddBaseAsync(info);
        }

        /// <summary>
        /// Asynchronously deletes <see cref="VisitInfo"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="VisitInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                if (_context.Info.Count() == 0)
                {
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.Info.GetType().Name} does not contain any element.");
                }

                if (await _context.Info.AnyAsync(x => x.Id.Equals(id)) == false)
                {
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");
                }

                var infoToBeDeleted = await _context.Info.Include(x => x.OpeningHours).SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove sightseeing info with id '{id}'.");
                _context.Info.Remove(infoToBeDeleted);
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
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing general sightseeing info with id '{id}' from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="VisitInfo"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="VisitInfo"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<VisitInfo>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all sightseeing info from database.");
                var info = await _context.Info.Include(x => x.OpeningHours).ToArrayAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {info.Count()} elements.");
                return info.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all sightseeing info from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="VisitInfo"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<VisitInfo> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve sightseeing info with id: '{id}' from database.");
                var info = await _context.Info.Include(x => x.OpeningHours).SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return info;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.Info.Count() == 0 ? $"Element not found because resource {_context.Info.GetType().Name} does contain any elements. See the inner exception."
                    : "Element not found. See the inner exception.";
                _logger.LogError(ex, $"{ex.GetType().Name} {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving sightseeing info with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="VisitInfo"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="VisitInfo"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<VisitInfo>> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 30)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equal to 1 are valid.");
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equal to 0 are valid.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                IEnumerable<VisitInfo> info = new VisitInfo[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.Info.CountAsync();
                int numberOfElementsOnLastPage = numberOfResourceElements % pageSize;
                int numberOfFullPages = (numberOfResourceElements - numberOfElementsOnLastPage) / pageSize;

                if (numberOfElementsOnLastPage > 0)
                {
                    maxNumberOfPageWithData = ++numberOfFullPages;
                    _logger.LogWarning($"Last page of data contain {numberOfElementsOnLastPage} elements which is less than specified in {nameof(pageSize)}: {pageSize}.");
                }
                else
                {
                    maxNumberOfPageWithData = numberOfFullPages;
                }

                if (numberOfResourceElements == 0 || pageSize == 0 || pageNumber > maxNumberOfPageWithData)
                {
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {info.Count()} elements.");
                    return info;
                }

                _logger.LogDebug($"Starting retrieve data. '{nameof(pageNumber)}': {pageNumber.ToString()}, '{nameof(pageSize)}': {pageSize.ToString()}.");
                info = _context.Info.Include(x => x.OpeningHours).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return info;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving sightseeing info from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="VisitInfo"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="info">The sightseeing info to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="info"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="info"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<VisitInfo> UpdateAsync(VisitInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");
            // Call normal update mode.
            return await UpdateBaseAsync(info);
        }

        /// <summary>
        /// Asynchronously adds <see cref="VisitInfo"/> entity to the database. Do not allow to add entity with the same properties value as existing one.
        /// Throws an exception if already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="info">The info to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="info"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="VisitInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<VisitInfo> RestrictedAddAsync(VisitInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedAddAsync)}'.");
            // Call restricted add mode.
            return await AddBaseAsync(info, true);
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
        public async Task<VisitInfo> RestrictedUpdateAsync(VisitInfo info)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdateAsync)}'.");
            // Call restricted update mode.
            return await UpdateBaseAsync(info, true);

        }


        #region Privates

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allInfo = await _context.Info.ToArrayAsync();
            return allInfo.Any(x => x.Equals(entity as VisitInfo));
        }

        protected override BasicEntity BasicRestrictedUpdate(BasicEntity originalEntity, BasicEntity entityToBeUpdated)
        {
            // Update in restricted mode and delete from the database unused OpeningHours.
            var updatedInfo = base.BasicRestrictedUpdate(originalEntity, entityToBeUpdated) as VisitInfo;
            var originalInfo = originalEntity as VisitInfo;
            var infoToBeUpdated = entityToBeUpdated as VisitInfo;

            // Prevent ArgumentNullException thrown by Except() in DeleteUnusedOpeningHours()
            // when originalInfo.OpeningHours or infoToBeUpdated.OpeningHours will be null.
            if (originalInfo.OpeningHours is null)
            {
                originalInfo.OpeningHours = new OpeningHours[] { };
            }

            if (infoToBeUpdated.OpeningHours is null)
            {
                infoToBeUpdated.OpeningHours = new OpeningHours[] { };
            }

            DeleteUnusedOpeningHours(originalInfo, entityToBeUpdated as VisitInfo);
            return updatedInfo;
        }

        /// <summary>
        /// Asynchronously adds <see cref="VisitInfo"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same Description, OpeningHour, MaxAllowedGroupSize and MaxChildAge values.
        /// </summary>
        /// <param name="info"><see cref="VisitInfo"/> to be added.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same Description, OpeningHour, MaxAllowedGroupSize and MaxChildAge values. </param>
        /// <returns>Added <see cref="VisitInfo"/> entity.</returns>
        private async Task<VisitInfo> AddBaseAsync(VisitInfo info, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(AddBaseAsync)}'.");

            if (info is null)
            {
                throw new ArgumentNullException($"Argument '{nameof(info)}' cannot be null.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                if (isRestrict)
                {
                    // Restricted add mode that use custom equality comparer. Discounts are equal if they have the same Description, DiscountValueInPercentage, GroupSizeForDiscount and Type.

                    // Check if exist in db disount with the same properties as adding.
                    if (await IsEntityAlreadyExistsAsync(info))
                    {
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. " +
                            $"The value of '{nameof(info.Description)}', '{nameof(info.MaxChildAge)}', '{nameof(info.OpeningHours)}'" +
                            $"'{nameof(info.MaxAllowedGroupSize)}' are not unique.");
                    }
                }
                else
                {
                    // Normal add mode without any additional restrictions.
                    if (_context.Info.Contains(info))
                    {
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{info.Id}'.");
                    }
                }

                _logger.LogDebug($"Starting add general sightseeing info with id '{info.Id}'.");
                var addedInfo = _context.Info.Add(info).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddAsync)}'.");
                return addedInfo;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - Changes made by add operations cannot be saved properly. See the inner exception for more details. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception for more details.", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - There is already the same element in the database as the one to be added. Id of this element: '{info.Id}'.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding disount with id '{info.Id}' to database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="VisitInfo"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used and update allow entirely entity updating. 
        /// Otherwise the restricted mode will be using. It will ignore updating some read-only properties.
        /// </summary>
        /// <param name="info"><see cref="VisitInfo"/> to be updated.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will ignore some read-only properties changes.</param>
        /// <returns>Updated <see cref="Customer"/> entity.</returns>
        private async Task<VisitInfo> UpdateBaseAsync(VisitInfo info, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(UpdateBaseAsync)}'.");

            _ = info ?? throw new ArgumentNullException(nameof(info), $"Argument '{nameof(info)}' cannot be null.");

            if (string.IsNullOrEmpty(info.Id))
            {
                throw new ArgumentException($"Argument '{nameof(info.Id)}' cannot be null or empty.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Info ?? throw new InternalDbServiceException($"Table of type '{typeof(VisitInfo).Name}' is null.");

            try
            {
                if (_context.Info.Count() == 0)
                {
                    throw new InvalidOperationException($"Cannot found element with id '{info.Id}' for update. Resource {_context.Info.GetType().Name} does not contain any element.");
                }

                if (await _context.Info.ContainsAsync(info) == false)
                {
                    throw new InvalidOperationException($"Cannot found element with id '{info.Id}' for update. Any element does not match to the one to be updated.");
                }

                _logger.LogDebug($"Starting update customer with id '{info.Id}'.");

                VisitInfo updatedInfo = null;
                info.UpdatedAt = DateTime.UtcNow;

                if (isRestrict)
                {
                    // Restricted update mode that ignores all changes in read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken.
                    var originalInfo = await _context.Info.Include(x => x.OpeningHours).SingleAsync(x => x.Id.Equals(info.Id));
                    updatedInfo = BasicRestrictedUpdate(originalInfo, info) as VisitInfo;
                }
                else
                {
                    // Normal update mode without any additional restrictions.
                    updatedInfo = _context.Info.Update(info).Entity;
                }

                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(UpdateBaseAsync)}'.");
                return updatedInfo;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Cannot found element for update. See exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating general sightseing info with id '{info.Id}'. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        private void DeleteUnusedOpeningHours(VisitInfo originalInfo, VisitInfo newInfo)
        {
            // Delete only those hours which belong to the updating info and don't belong to a new version of this info.
            // The OpeningHoursComparer to check which hours are unused in a new info version, is used here.
            var originalOpeningHours = _context.OpeningDates.Where(x => x.Info.Id.Equals(originalInfo.Id)).AsEnumerable();
            var differentOpeningHours = originalOpeningHours.Except(newInfo.OpeningHours.AsEnumerable(), new OpeningHoursComparer());
            _context.OpeningDates.RemoveRange(differentOpeningHours);
        }


        internal class OpeningHoursComparer : IEqualityComparer<OpeningHours>
        {
            public bool Equals(OpeningHours x, OpeningHours y)
            {
                if (x.GetType() != y.GetType())
                {
                    return false;
                }
                if (x is null && y is null)
                {
                    return false;
                }
                if (x is null || y is null)
                {
                    return false;
                }

                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                return x.OpeningHour == y.OpeningHour && x.ClosingHour == y.ClosingHour && x.DayOfWeek == y.DayOfWeek;
            }

            public int GetHashCode(OpeningHours obj)
            {
                return (obj.OpeningHour.GetHashCode() + obj.ClosingHour.GetHashCode() + (int)obj.DayOfWeek) * 0x10000000;
            }
        }

        #endregion

    }
}
