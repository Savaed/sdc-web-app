using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public class SightseeingTariffDbService : ISightseeingTariffDbService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SightseeingTariffDbService> _logger;
        private readonly IDbRepository _repository;
        private readonly ICustomValidator<SightseeingTariff> _validator;


        public SightseeingTariffDbService(ILogger<SightseeingTariffDbService> logger, ApplicationDbContext dbContext, IDbRepository repository, ICustomValidator<SightseeingTariff> validator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _repository = repository;
            _validator = validator;
        }


        public async Task<SightseeingTariff> AddSightseeingTariffAsync(SightseeingTariff sightseeingTariff)
        {
            SightseeingTariff result;

            var validationResult = _validator.Validate(sightseeingTariff);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                var ex = new ArgumentException(nameof(sightseeingTariff));
                _logger.LogError(ex, $"Argument '{nameof(sightseeingTariff)}' is invalid. {error.ErrorMessage}.");
                throw ex;
            }

            try
            {
                result = _repository.Add<SightseeingTariff>(sightseeingTariff);
                await _dbContext.TrySaveChangesAsync<SightseeingTariff>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingTariff>)} is null.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(sightseeingTariff)}' is null.");
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

        public async Task DeleteSightseeingTariffAsync(string id)
        {
            try
            {
                await _repository.DeleteAsync<SightseeingTariff>(id);
                await _dbContext.TrySaveChangesAsync<SightseeingTariff>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingTariff>)} is null.");
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

        public async Task<IEnumerable<SightseeingTariff>> GetAllSightseeingTariffsAsync()
        {
            IEnumerable<SightseeingTariff> result;
            try
            {
                result = await _repository.GetAllAsync<SightseeingTariff>();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e, $"The resource of type {typeof(DbSet<SightseeingTariff>)} doesnt exist.");
                throw;
            }
            return result;
        }

        public async Task<SightseeingTariff> GetSightseeingTariffAsync(string id)
        {
            SightseeingTariff result;

            try
            {
                result = await _repository.GetByIdAsync<SightseeingTariff>(id);
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingTariff>)} doesnt exist.");
                throw;
            }
            catch (ArgumentException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(id)}' is null or empty string.");
                throw;
            }
            catch (InvalidOperationException e3)
            {
                _logger.LogError(e3, "Searching element not found or resource is empty. See exception for more information.");
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<SightseeingTariff>> GetSightseeingTariffsAsync(IEnumerable<string> ids)
        {
            IEnumerable<SightseeingTariff> result;
            try
            {
                result = _repository.GetByIds<SightseeingTariff>(ids);
                await _dbContext.TrySaveChangesAsync<SightseeingTariff>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingTariff>)} doesnt exist.");
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

        public async Task<IEnumerable<SightseeingTariff>> GetSightseeingTariffsByAsync(Expression<Func<SightseeingTariff, bool>> predicate)
        {
            var result = new SightseeingTariff[] { }.AsEnumerable();
            if (predicate is null)
            {
                var e = new ArgumentNullException(nameof(predicate), $"Argument '{nameof(predicate)}' cannot be null.");
                _logger.LogError(e, $"Argument '{nameof(predicate)}' is null.");
                throw e;
            }

            if (_dbContext.Set<Ticket>() is null)
            {
                var e = new NullReferenceException($"The resource of type {typeof(DbSet<SightseeingTariff>)} doesnt exist.");
                _logger.LogError(e, $"The resource of type {typeof(DbSet<SightseeingTariff>)} doesnt exist.");
                throw e;
            }

            if (_dbContext.Set<SightseeingTariff>().Count() == 0)
                return result;

            result = await _dbContext.Set<SightseeingTariff>().Where(predicate).ToListAsync();
            return result.AsEnumerable();
        }

        public async Task<SightseeingTariff> UpdateSightseeingTariffAsync(SightseeingTariff sightseeingTariff)
        {
            SightseeingTariff result;

            try
            {
                result = await _repository.UpdateAsync<SightseeingTariff>(sightseeingTariff);
                await _dbContext.TrySaveChangesAsync<SightseeingTariff>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingTariff>)} is null.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(sightseeingTariff)}' is null.");
                throw;
            }
            catch (ArgumentException e3)
            {
                _logger.LogError(e3, $"Value of '{nameof(sightseeingTariff.Id)}' is null or empty.");
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
