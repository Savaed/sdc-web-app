using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IGeneralSightseeingInfoDbService
    {
        /// <summary>
        /// Asynchronously retrievs <see cref="GeneralSightseeingInfo"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<GeneralSightseeingInfo> GetAsync(string id);

        /// <summary>
        /// Asynchronously retrievs all <see cref="GeneralSightseeingInfo"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="GeneralSightseeingInfo"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<IEnumerable<GeneralSightseeingInfo>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves <see cref="GeneralSightseeingInfo"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="GeneralSightseeingInfo"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<IEnumerable<GeneralSightseeingInfo>> GetWithPaginationAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously updates <see cref="GeneralSightseeingInfo"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="info">The sightseeing info to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="info"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="info"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<GeneralSightseeingInfo> UpdateAsync(GeneralSightseeingInfo info);

        /// <summary>
        /// Asynchronously deletes <see cref="GeneralSightseeingInfo"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="GeneralSightseeingInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task DeleteAsync(string id);

        /// <summary>
        /// Asynchronously adds <see cref="GeneralSightseeingInfo"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="info">The info to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="info"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="GeneralSightseeingInfo"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task<GeneralSightseeingInfo> AddAsync(GeneralSightseeingInfo info);

        Task<GeneralSightseeingInfo> RestrictedUpdateAsync(GeneralSightseeingInfo info);


        Task<GeneralSightseeingInfo> RestrictedAddAsync(GeneralSightseeingInfo info);


    }
}
