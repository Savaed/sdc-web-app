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
    /// Provides methods for GET, ADD, UPDATE and DELETE operations for <see cref="Customer"/> entities in the database.
    /// </summary>
    public class CustomerDbService : ServiceBase, ICustomerDbService
    {
        private readonly ILogger<CustomerDbService> _logger;
        private readonly ApplicationDbContext _context;


        public CustomerDbService(ApplicationDbContext context, ILogger<CustomerDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="Customer"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="customer">The customer to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="customer"/> is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Customer"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<Customer> AddAsync(Customer customer)
        {
            _logger.LogInformation($"Starting method '{nameof(AddAsync)}'.");
            // Call normal add mode.
            return await AddBaseAsync(customer);
        }

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
        public async Task<Customer> RestrictedAddAsync(Customer customer)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedAddAsync)}'.");
            // Call restricted add mode.
            return await AddBaseAsync(customer, true);
        }

        /// <summary>
        /// Asynchronously deletes <see cref="Customer"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Customer"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Customers ?? throw new InternalDbServiceException($"Table of type '{typeof(Customer).Name}' is null.");

            try
            {
                if (_context.Customers.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_context.Customers.GetType().Name} does not contain any element.");

                if (await _context.Customers.AnyAsync(x => x.Id.Equals(id)) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");

                var customerToBeDeleted = await _context.Customers.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove customer with id '{customerToBeDeleted.Id}'.");
                _context.Customers.Remove(customerToBeDeleted);
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Remove data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(DeleteAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Cannot found element. See exception Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing customer with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="Customer"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="Customer"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Customers ?? throw new InternalDbServiceException($"Table of type '{typeof(Customer).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all customers from database.");
                var customers = await _context.Customers.Include(x => x.Tickets).ToArrayAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {customers.Count()} elements.");
                return customers.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all customers from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

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
        public async Task<Customer> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Customers ?? throw new InternalDbServiceException($"Table of type '{typeof(Customer).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve customer with id: '{id}' from database.");
                var customer = await _context.Customers.Include(x => x.Tickets).SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return customer;
            }
            catch (InvalidOperationException ex)
            {
                string message = _context.Customers.Count() == 0 ? $"Element not found because resource {_context.Customers.GetType().Name} does contain any elements. " +
                    $"See the inner exception for more details." : "Element not found. See the inner exception for more details.";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving customer with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

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
        public async Task<IEnumerable<Customer>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equals to 1 is valid.");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equals to 0 is valid.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Customers ?? throw new InternalDbServiceException($"Table of type '{typeof(Customer).Name}' is null.");

            try
            {
                IEnumerable<Customer> customers = new Customer[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _context.Customers.CountAsync();
                int numberOfElementsOnLastPage = numberOfResourceElements % pageSize;
                int numberOfFullPages = (numberOfResourceElements - numberOfElementsOnLastPage) / pageSize;

                if (numberOfElementsOnLastPage > 0)
                {
                    maxNumberOfPageWithData = ++numberOfFullPages;
                    _logger.LogWarning($"Last page of data contains {numberOfElementsOnLastPage} elements which is less than specified in '{nameof(pageSize)}': {pageSize}.");
                }
                else
                    maxNumberOfPageWithData = numberOfFullPages;

                if (numberOfResourceElements == 0 || pageSize == 0 || pageNumber > maxNumberOfPageWithData)
                {
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {customers.Count()} elements.");
                    return customers;
                }

                _logger.LogDebug($"Starting retrieve customers. {nameof(pageNumber)} '{pageNumber.ToString()}', {nameof(pageSize)} '{pageSize.ToString()}'.");
                customers = _context.Customers.Include(x => x.Tickets).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving customers from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

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
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateAsync)}'.");
            // Call normal update mode.
            return await UpdateBaseAsync(customer);
        }

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
        public async Task<Customer> RestrictedUpdateAsync(Customer customer)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdateAsync)}'.");
            // Call restricted update mode.
            return await UpdateBaseAsync(customer, true);
        }
     

        #region Privates       

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            var allCustomers = await _context.Customers.ToArrayAsync();
            return allCustomers.Any(x => x.Equals(entity as Customer));
        }

        /// <summary>
        /// Asynchronously adds <see cref="Customer"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'EmailAddress' value.
        /// </summary>
        /// <param name="customer"><see cref="Customer"/> to be added.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will check if in database is entity with the same 'EmailAddress' value.</param>
        /// <returns>Added <see cref="Customer"/> entity.</returns>
        private async Task<Customer> AddBaseAsync(Customer customer, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(AddBaseAsync)}'.");

            if (customer is null)
                throw new ArgumentNullException($"Argument '{nameof(customer)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Customers ?? throw new InternalDbServiceException($"Table of type '{typeof(Customer).Name}' is null.");

            try
            {
                if (_context.Customers.Contains(customer))
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{customer.Id}'.");

                Customer addedCustomer = null;

                if (isRestrict)
                {
                    // Resticted add mode that use custom equality comparer. Customers are equal if they have the same EmailAddress.

                    // Check if exist in db customer with the same EmailAddress as adding.
                    if (await IsEntityAlreadyExistsAsync(customer))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. The value of '{nameof(customer.EmailAddress)}' is not unique.");
                }
                else
                {
                    // Normal add mode without any additional restrictions.
                    if (_context.Customers.Contains(customer))
                        throw new InvalidOperationException($"There is already the same element in the database as the one to be added. Id of this element: '{customer.Id}'.");
                }

                _logger.LogDebug($"Starting add customer with id '{customer.Id}'.");
                addedCustomer = _context.Customers.Add(customer).Entity;
                await _context.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddBaseAsync)}'.");
                return addedCustomer;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - Changes made by add operations cannot be saved properly. See the inner exception for more details.. Operation failed.", ex);
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
                var internalException = new InternalDbServiceException($"Encountered problem when adding customer with id: '{customer.Id}' to the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="Customer"/> entity. If <paramref name="isRestrict"/> set to false then no restrictions will be used and update allow entirely entity updating. 
        /// Otherwise the restricted mode will be using. It will ignore updating some read-only properties.
        /// </summary>
        /// <param name="customer"><see cref="Customer"/> to be updated.</param>
        /// <param name="isRestrict">If set to false then no restrictions will be used and update allow entirely entity updating. If set to true then the restricted mode will be used.
        /// It will ignore some read-only properties changes.</param>
        /// <returns>Updated <see cref="Customer"/> entity.</returns>
        private async Task<Customer> UpdateBaseAsync(Customer customer, bool isRestrict = false)
        {
            _logger.LogDebug($"Starting method '{nameof(UpdateBaseAsync)}'.");

            _ = customer ?? throw new ArgumentNullException(nameof(customer), $"Argument '{nameof(customer)}' cannot be null.");

            if (string.IsNullOrEmpty(customer.Id))
                throw new ArgumentException($"Argument '{nameof(customer.Id)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _context?.Customers ?? throw new InternalDbServiceException($"Table of type '{typeof(Customer).Name}' is null.");

            try
            {
                if (_context.Customers.Count() == 0)
                    throw new InvalidOperationException($"Cannot found element with id '{customer.Id}' for update. Resource {_context.Customers.GetType().Name} does not contain any element.");

                if (await _context.Customers.ContainsAsync(customer) == false)
                    throw new InvalidOperationException($"Cannot found element with id '{customer.Id}' for update. Any element does not match to the one to be updated.");

                _logger.LogDebug($"Starting update customer with id '{customer.Id}'.");

                Customer updatedCustomer = null;
                customer.UpdatedAt = DateTime.UtcNow;

                if (isRestrict)
                {
                    // Restricted update mode that ignores all changes in read-only properties like Id, CreatedAt, UpdatedAt, ConcurrencyToken.
                    var originalCustomer = await _context.Customers.SingleAsync(x => x.Id.Equals(customer.Id));
                    updatedCustomer = BasicRestrictedUpdate(originalCustomer, customer) as Customer;
                }
                else
                {
                    // Normal update mode without any additional restrictions.
                    updatedCustomer = _context.Customers.Update(customer).Entity;
                }

                await _context.TrySaveChangesAsync();
                _logger.LogDebug($"Update data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(UpdateBaseAsync)}'.");
                return updatedCustomer;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} Cannot found element for update. See exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when updating customer with id '{customer.Id}'. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        #endregion

    }
}
