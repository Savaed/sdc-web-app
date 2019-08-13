using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public class TicketDbService : ITicketDbService
    {
        private readonly ILogger<TicketDbService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbRepository _repository;


        public TicketDbService(ILogger<TicketDbService> logger, ApplicationDbContext dbContext, IDbRepository repository)
        {
            _logger = logger;
            _dbContext = dbContext;
            _repository = repository;
        }


        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {
            Ticket result;

            if (ticket is null)
            {
                var e = new ArgumentNullException(nameof(ticket), $"Argument '{nameof(ticket)}' cannot be null.");
                _logger.LogError(e, $"Argument '{nameof(ticket)}' cannot be null.");
                throw e;
            }

            try
            {
                result = _repository.Add<Ticket>(ticket);
                await _dbContext.TrySaveChangesAsync<Ticket>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Ticket>)} is null.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(ticket)}' is null.");
                throw;
            }
            catch (InvalidOperationException e3)
            {
                _logger.LogError(e3, "An entity to be added already exists in resource.");
                throw;
            }
            catch (NotSupportedException e4)
            {
                _logger.LogError(e4, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            return result;
        }

        public async Task DeleteTicketAsync(string id)
        {
            try
            {
                await _repository.DeleteAsync<Ticket>(id);
                await _dbContext.TrySaveChangesAsync<Ticket>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Ticket>)} is null.");
                throw;
            }
            catch (ArgumentException e2)
            {
                _logger.LogError(e2, $"The argument '{nameof(id)}' is null or empty.");
                throw;
            }
            catch (InvalidOperationException e3)
            {
                _logger.LogError(e3, "The quering resource is empty or matching element not found.");
                throw;
            }
            catch (NotSupportedException e4)
            {
                _logger.LogError(e4, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            IEnumerable<Ticket> result;
            try
            {
                result = await _repository.GetAllAsync<Ticket>();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e, $"The resource of type {typeof(DbSet<Ticket>)} doesnt exist.");
                throw;
            }
            return result;
        }

        public async Task<Ticket> GetTicketAsync(string id)
        {
            Ticket result;
            try
            {
                result = await _repository.GetByIdAsync<Ticket>(id);
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Ticket>)} doesnt exist.");
                throw;
            }
            catch (ArgumentException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(id)}' is null ore empty string.");
                throw;
            }
            catch (InvalidOperationException e3)
            {
                _logger.LogError(e3, "Searching element not found or resource is empty. See exception for more information.");
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(IEnumerable<string> ids)
        {
            IEnumerable<Ticket> result;
            try
            {
                result = _repository.GetByIds<Ticket>(ids);
                await _dbContext.TrySaveChangesAsync<Ticket>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Ticket>)} doesnt exist.");
                throw;
            }
            catch (ArgumentOutOfRangeException e2)
            {
                _logger.LogError(e2, $"The length of '{nameof(ids)}' is grater than overall count of entities in resource or resource is empty.");
                throw;
            }
            catch (ArgumentException e3)
            {
                _logger.LogError(e3, $"Argument '{nameof(ids)}' is null ore empty.");
                throw;
            }
            catch (NotSupportedException e4)
            {
                _logger.LogError(e4, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByAsync(Expression<Func<Ticket, bool>> predicate)
        {
            var result = new Ticket[] { }.AsEnumerable();
            if (predicate is null)
            {
                var e = new ArgumentNullException(nameof(predicate), $"Argument '{nameof(predicate)}' cannot be null.");
                _logger.LogError(e, $"Argument '{nameof(predicate)}' is null.");
                throw e;
            }

            if (_dbContext.Set<Ticket>() is null)
            {
                var e = new NullReferenceException($"The resource of type {typeof(DbSet<Ticket>)} doesnt exist.");
                _logger.LogError(e, $"The resource of type {typeof(DbSet<Ticket>)} doesnt exist.");
                throw e;
            }

            if (_dbContext.Set<Ticket>().Count() == 0)
                return result;

            result = await _dbContext.Set<Ticket>().Where(predicate).ToListAsync();
            return result.AsEnumerable();
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            Ticket result;

            try
            {
                result = await _repository.UpdateAsync<Ticket>(ticket);
                await _dbContext.TrySaveChangesAsync<Ticket>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<Ticket>)} is null.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(ticket)}' is null.");
                throw;
            }
            catch (ArgumentException e3)
            {
                _logger.LogError(e3, $"Value of '{nameof(ticket.Id)}' is null or empty.");
                throw;
            }
            catch (InvalidOperationException e4)
            {
                _logger.LogError(e4, "The quering resource is empty or matching element not found.");
                throw;
            }
            catch (NotSupportedException e5)
            {
                _logger.LogError(e5, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            return result;
        }
    }
}
