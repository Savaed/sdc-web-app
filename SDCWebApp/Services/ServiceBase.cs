using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

using SDCWebApp.Data;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Provides basic operations on database services.
    /// </summary>
    public abstract class ServiceBase
    {
        private readonly string[] _readOnlyPropertiesNames = new string[] { "Id", "CreatedAt", "ConcurrencyToken", "UpdatedAt" };
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;


        public ServiceBase(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Updates an entity by ignoring some properties (all <see cref="BasicEntity"/> properties)  
        /// because they are read-only and cannot be changed externally. Returns updated entity with new values.
        /// </summary>
        /// <param name="originalEntity">The original value of updating entity retrieved from the database.</param>
        /// <param name="entityToBeUpdated">The entity to be updated.</param>
        /// <returns>Updated entity.</returns>
        protected virtual BasicEntity BasicRestrictedUpdate(BasicEntity originalEntity, BasicEntity entityToBeUpdated)
        {
            _logger.LogInformation($"Starting method '{nameof(BasicRestrictedUpdate)}'.");

            _context.Attach(originalEntity);
            var properties = originalEntity.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (_readOnlyPropertiesNames.Any(x => x.Equals(property.Name)))
                    _logger.LogWarning($"Encouneterd property '{property.Name}' when updating '{typeof(BasicEntity).Name}'. This property is read-only and any attempts to change its value will be ignored.");
                else
                    property.SetValue(originalEntity, property.GetValue(entityToBeUpdated));
            }

            originalEntity.UpdatedAt = DateTime.UtcNow;
            _logger.LogInformation($"Finished method '{nameof(BasicRestrictedUpdate)}'.");

            return originalEntity;
        }

        /// <summary>
        /// Ensures if database created. If not it will be created. Note that the database will be created but not using migrations so it cannot be updating using migrations later.
        /// </summary>
        protected virtual async Task EnsureDatabaseCreatedAsync()
        {
            if (await _context.Database.EnsureCreatedAsync() == false)
                _logger.LogWarning($"Database with provider '{_context.Database.ProviderName}' does not exist. It will be created but not using migrations so it cannot be updating using migrations later.");
        }

        /// <summary>
        /// Checks if <see cref="BasicEntity"/> entity exists using custom equality comparer.
        /// </summary>
        /// <param name="entity">The <see cref="BasicEntity"/> entity to check if there is the same entity in the database.</param>
        /// <returns></returns>
        protected abstract Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity);
    }
}