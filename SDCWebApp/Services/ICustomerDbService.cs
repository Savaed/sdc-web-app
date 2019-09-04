using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public interface ICustomerDbService
    {
        /// <summary>
        /// Filters set of data of type <see cref="Customer"/>. Returns filtered data set. Throws an exception if <paramref name="predicate"/> is null, 
        /// or if cannot filter data due to any internal problem.
        /// </summary>
        /// <typeparam name="T">The type of entity to set be filtered.</typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Filterd <see cref="Customer"/> set.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot filter data.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>        
        Task<IEnumerable<Customer>> GetByAsync(Expression<Func<Customer, bool>> predicate);

        /// <summary>
        /// Asynchronously retrievs <see cref="Customer"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrived. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<Customer> GetAsync(string id);

        /// <summary>
        /// Asynchronously retrievs all <see cref="Customer"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="Customer"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<IEnumerable<Customer>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves <see cref="Customer"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="SightseeingTariff"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<IEnumerable<Customer>> GetWithPaginationAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Asynchronously updates <see cref="Customer"/> entity. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="customer">The customer to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="customer"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="customer"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<Customer> UpdateAsync(Customer customer);

        /// <summary>
        /// Asynchronously updates <see cref="Customer"/> entity ignoring read-only properties like as Id, CreatedAt, UpdatedAt, ConcurrencyToken. 
        /// Throws an exception if cannot found entity or any problem with updating occurred.
        /// </summary>
        /// <param name="customer">The customer to be updated. Cannot be null or has Id property set to null or empty string.</param>
        /// <returns>Updated entity.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="customer"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="customer"/> has Id property set to null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity to be updated.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<Customer> RestrictedUpdateAsync(Customer customer);

        /// <summary>
        /// Asynchronously deletes <see cref="Customer"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Customer"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task DeleteAsync(string id);

        /// <summary>
        /// Asynchronously adds <see cref="Customer"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="customer">The customer to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="customer"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Customer"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task<Customer> AddAsync(Customer customer);       

        /// <summary>
        /// Asynchronously adds <see cref="Customer"/> entity to the database. Does not allow to add entity with the same EmailAddress.
        /// Throws an exception if already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="customer">The customer to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="customer"/> is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Customer"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task<Customer> RestrictedAddAsync(Customer customer);
    }
}
