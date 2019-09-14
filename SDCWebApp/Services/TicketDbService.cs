using Microsoft.EntityFrameworkCore;
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
    /// Provides methods for GET and ADD operations for <see cref="Ticket"/> entities in the database.
    /// </summary>
    public class TicketDbService : ServiceBase, ITicketDbService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TicketDbService> _logger;


        public TicketDbService(ApplicationDbContext context, ILogger<TicketDbService> logger) : base(context, logger)
        {
            _logger = logger;
            _dbContext = context;
        }


        /// <summary>
        /// Asynchronously adds <see cref="Ticket"/> entity to the database. Throws an exception if 
        /// already there is the same entity in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="ticket">The ticket to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="ticket"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same entity that one to be added in the database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Ticket"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            _logger.LogDebug($"Starting method '{nameof(AddAsync)}'.");

            if (ticket is null)
            {
                throw new ArgumentNullException($"Argument '{nameof(ticket)}' cannot be null.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.Tickets ?? throw new InternalDbServiceException($"Table of type '{typeof(Ticket).Name}' is null.");

            // Set TicketUniqueId for new ticket. It's set only internally.
            ticket.TicketUniqueId = Guid.NewGuid().ToString();

            try
            {
                // Only Id and TicketUniqueId cannot be the same.
                if (_dbContext.Tickets.Any(x => x.TicketUniqueId.Equals(ticket.TicketUniqueId)) || _dbContext.Tickets.Contains(ticket))
                {
                    throw new InvalidOperationException($"There is already the same element in the database as the one to be added. " +
                        $"{nameof(Ticket.TicketUniqueId)} of this element: '{ticket.TicketUniqueId}'.");
                }

                _logger.LogDebug($"Starting add ticket with id '{ticket.Id}'.");
                var addedTicket = _dbContext.Tickets.Add(ticket).Entity;
                await _dbContext.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogDebug($"Finished method '{nameof(AddAsync)}'.");
                return addedTicket;
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
                var internalException = new InternalDbServiceException($"Encountered problem when adding ticket ticketf with id: '{ticket.Id}' to the database. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrievs all <see cref="Ticket"/> entities from the database. 
        /// Throws an exception if any problem with retrieving occurred.
        /// </summary>
        /// <returns>Set of all <see cref="Ticket"/> entities from database.</returns>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.Tickets ?? throw new InternalDbServiceException($"Table of type '{typeof(Ticket).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve all tickets from the database.");
                var tickets = await _dbContext.Tickets.IncludeDetails().ToArrayAsync();
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAllAsync)}'. Returning {tickets.Count()} elements.");
                return tickets.AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving all tickets from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="Ticket"/> entity with given <paramref name="id"/> from the database. 
        /// Throws an exception if cannot found entity or any problem with retrieving occurred.
        /// </summary>
        /// <param name="id">The id of entity to be retrieved. Cannot be nul or empty.</param>
        /// <returns>The entity with given <paramref name="id"/>.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot found entity with given <paramref name="id"/>.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<Ticket> GetAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.Tickets ?? throw new InternalDbServiceException($"Table of type '{typeof(Ticket).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve ticket with id: '{id}' from the database.");
                var ticket = await _dbContext.Tickets.IncludeDetails().SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetAsync)}'.");
                return ticket;
            }
            catch (InvalidOperationException ex)
            {
                string message = _dbContext.Tickets.Count() == 0 ? $"Element not found because resource {_dbContext.Tickets.GetType().Name} does contain any elements. See the inner exception."
                    : "Element not found. See the inner exception.";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving ticket with id '{id}' from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Filters set of data of type <see cref="Ticket"/>. Returns filtered data set. Throws an exception if <paramref name="predicate"/> is null, 
        /// or if cannot filter data due to any internal problem.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Filterd <see cref="Ticket"/> set.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot filter data.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception> 
        public async Task<IEnumerable<Ticket>> GetByAsync(Expression<Func<Ticket, bool>> predicate)
        {
            _logger.LogInformation($"Starting method '{nameof(GetByAsync)}'.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.Tickets ?? throw new InternalDbServiceException($"Table of type '{typeof(Ticket).Name}' is null.");

            try
            {
                var result = GetByPredicate<Ticket>(predicate) as IEnumerable<Ticket>;
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
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving tickets from the database using {nameof(predicate)}. See the inner excpetion for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="Ticket"/> entities with specified page size and page number.
        /// Throws an exception if arguments is out of range or any problem with retrieving occurred.
        /// </summary>
        /// <param name="pageNumber">Page number that will be retrieved. Must be greater than 0.</param>
        /// <param name="pageSize">Page size. Must be a positive number.</param>
        /// <returns>Set of <see cref="Ticket"/> entities.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="pageSize"/> is a negative number or <paramref name="pageNumber"/> is less than 1.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<IEnumerable<Ticket>> GetWithPaginationAsync(int pageNumber = 1, int pageSize = 30)
        {
            _logger.LogInformation($"Starting method '{nameof(GetWithPaginationAsync)}'.");

            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"'{pageNumber}' is not valid value for argument '{nameof(pageNumber)}'. Only number greater or equals to 1 is valid.");
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), $"'{pageSize}' is not valid value for argument '{nameof(pageSize)}'. Only number greater or equals to 0 is valid.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.Tickets ?? throw new InternalDbServiceException($"Table of type '{typeof(Ticket).Name}' is null.");

            try
            {
                IEnumerable<Ticket> tickets = new Ticket[] { }.AsEnumerable();
                int maxNumberOfPageWithData;

                int numberOfResourceElements = await _dbContext.Tickets.CountAsync();
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
                    _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'. Returning {tickets.Count()} elements.");
                    return tickets;
                }

                _logger.LogDebug($"Starting retrieve data. '{nameof(pageNumber)}': {pageNumber.ToString()}, '{nameof(pageSize)}': {pageSize.ToString()}.");
                tickets = _dbContext.Tickets.IncludeDetails().Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetWithPaginationAsync)}'.");
                return tickets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retrieving tickets from database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Asynchronously deletes <see cref="Ticket"/> entity from the database. Throws an exception if cannot found entity 
        /// to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="id">The id of entity to be deleted. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Argument <paramref name="id"/> is null or empty string.</exception>
        /// <exception cref="InvalidOperationException">Cannot foound entity with given <paramref name="id"/> for delete.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Ticket"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"Argument '{nameof(id)}' cannot be null or empty.");
            }

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.Tickets ?? throw new InternalDbServiceException($"Table of type '{typeof(Ticket).Name}' is null.");

            try
            {
                if (_dbContext.Tickets.Count() == 0)
                {
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Resource {_dbContext.Tickets.GetType().Name} does not contain any element.");
                }

                if (await _dbContext.Tickets.AnyAsync(x => x.Id.Equals(id)) == false)
                {
                    throw new InvalidOperationException($"Cannot found element with id '{id}'. Any element does not match to the one to be updated.");
                }

                var ToBeDeleted = await _dbContext.Tickets.SingleAsync(x => x.Id.Equals(id));
                _logger.LogDebug($"Starting remove ticket  with id '{ToBeDeleted.Id}'.");
                _dbContext.Tickets.Remove(ToBeDeleted);
                await _dbContext.TrySaveChangesAsync();
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
                var internalException = new InternalDbServiceException($"Encountered problem when removing ticket with id '{id}' from the database. See the inner exception for more details.", ex);
                throw internalException;
            }
        }


        #region Privates

        protected override async Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            // This implementation uses default equality comparer.
            return await _dbContext.Tickets.ContainsAsync(entity as Ticket);
        }

        #endregion

    }
}
