using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Data.Validators;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public class SightseeingGroupDbService : ISightseeingGroupDbService
    {
        private readonly ILogger<SightseeingGroupDbService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbRepository _repository;
        private readonly ICustomValidator<SightseeingGroup> _validator;


        public SightseeingGroupDbService(ILogger<SightseeingGroupDbService> logger, ApplicationDbContext dbContext, IDbRepository repository, ICustomValidator<SightseeingGroup> validator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _repository = repository;
            _validator = validator;
        }


        public async Task<SightseeingGroup> AddGroupAsync(SightseeingGroup sightseeingGroup)
        {
            SightseeingGroup result;

            var validationResult = _validator.Validate(sightseeingGroup);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                var ex = new ArgumentException(nameof(sightseeingGroup));
                _logger.LogError(ex, $"Argument '{nameof(sightseeingGroup)}' is invalid. {error.ErrorMessage}.");
                throw ex;
            }

            try
            {
                result = _repository.Add<SightseeingGroup>(sightseeingGroup);
                await _dbContext.TrySaveChangesAsync<SightseeingGroup>();
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, $"The resource of type {typeof(DbSet<SightseeingGroup>)} is null.");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "An entity to be added already exists in resource.");
                throw;
            }
            catch (NotSupportedException ex)
            {
                _logger.LogError(ex, "Exception occured while saving data. See inner exception for details.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(sightseeingGroup)}' is null.");
                throw;
            }
            return result;
        }

        public async Task DeleteGroupAsync(string id)
        {
            try
            {
                await _repository.DeleteAsync<SightseeingGroup>(id);
                await _dbContext.TrySaveChangesAsync<SightseeingGroup>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingGroup>)} is null.");
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

        public async Task<IEnumerable<SightseeingGroup>> GetAllGroupsAsync()
        {
            IEnumerable<SightseeingGroup> result;
            try
            {
                result = await _repository.GetAllAsync<SightseeingGroup>();
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e, $"The resource of type {typeof(DbSet<SightseeingGroup>)} doesnt exist.");
                throw;
            }
            return result;
        }

        public async Task<SightseeingGroup> GetGroupAsync(string id)
        {
            SightseeingGroup result;
            try
            {
                result = await _repository.GetByIdAsync<SightseeingGroup>(id);
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingGroup>)} doesnt exist.");
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

        public async Task<IEnumerable<SightseeingGroup>> GetGroupsAsync(IEnumerable<string> ids)
        {
            IEnumerable<SightseeingGroup> result;
            try
            {
                result = _repository.GetByIds<SightseeingGroup>(ids);
                await _dbContext.TrySaveChangesAsync<SightseeingGroup>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingGroup>)} doesnt exist.");
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

        public async Task<IEnumerable<SightseeingGroup>> GetGroupsByAsync(Expression<Func<SightseeingGroup, bool>> predicate)
        {
            var result = new SightseeingGroup[] { }.AsEnumerable();
            if (predicate is null)
            {
                var e = new ArgumentNullException(nameof(predicate), $"Argument '{nameof(predicate)}' cannot be null.");
                _logger.LogError(e, $"Argument '{nameof(predicate)}' is null.");
                throw e;
            }

            if (_dbContext.Set<SightseeingGroup>() is null)
            {
                var e = new NullReferenceException($"The resource of type {typeof(DbSet<SightseeingGroup>)} doesnt exist.");
                _logger.LogError(e, $"The resource of type {typeof(DbSet<SightseeingGroup>)} doesnt exist.");
                throw e;
            }

            if (_dbContext.Set<Article>().Count() == 0)
                return result;

            result = await _dbContext.Set<SightseeingGroup>().Where(predicate).ToListAsync();
            return result.AsEnumerable();
        }


        public async Task<SightseeingGroup> UpdateGroupAsync(SightseeingGroup sightseeingGroup)
        {
            SightseeingGroup result;

            var validationResult = _validator.Validate(sightseeingGroup);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                var ex = new ArgumentException(nameof(sightseeingGroup));
                _logger.LogError(ex, $"Argument '{nameof(sightseeingGroup)}' is invalid. {error.ErrorMessage}.");
                throw ex;
            }

            try
            {
                result = await _repository.UpdateAsync<SightseeingGroup>(sightseeingGroup);
                await _dbContext.TrySaveChangesAsync<SightseeingGroup>();
            }
            catch (NullReferenceException e1)
            {
                _logger.LogError(e1, $"The resource of type {typeof(DbSet<SightseeingGroup>)} is null.");
                throw;
            }
            catch (ArgumentNullException e2)
            {
                _logger.LogError(e2, $"Argument '{nameof(sightseeingGroup)}' is null.");
                throw;
            }
            catch (ArgumentException e3)
            {
                _logger.LogError(e3, $"Value of '{nameof(sightseeingGroup.Id)}' is null or empty.");
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
