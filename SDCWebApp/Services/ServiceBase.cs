using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public abstract class ServiceBase
    {
        private readonly string[] ReadOnlyPropertiesNames = new string[] { "Id", "CreatedAt", "ConcurrencyToken", "UpdatedAt" };
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
        protected virtual BasicEntity RestrictedUpdate(BasicEntity originalEntity, BasicEntity entityToBeUpdated)
        {
            _logger.LogInformation($"Starting method '{nameof(RestrictedUpdate)}'.");

            _context.Attach(originalEntity);
            var properties = originalEntity.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (ReadOnlyPropertiesNames.Any(x => x.Equals(property.Name)))
                    _logger.LogWarning($"Encouneterd property '{property.Name}' when updating '{typeof(BasicEntity).Name}'. This property is read-only and any attempts to change its value will be ignored.");
                else
                    property.SetValue(originalEntity, property.GetValue(entityToBeUpdated));
            }

            originalEntity.UpdatedAt = DateTime.UtcNow;
            _logger.LogInformation($"Finished method '{nameof(RestrictedUpdate)}'.");

            return originalEntity;
        }

        protected abstract Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity);
    }
}