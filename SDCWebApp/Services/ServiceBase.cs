using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;
using System;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public abstract class ServiceBase
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;


        public ServiceBase(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        protected virtual BasicEntity CustomUpdate(BasicEntity originalEntity, BasicEntity entityToBeUpdated)
        {
            _logger.LogDebug($"Starting method '{nameof(CustomUpdate)}'.");

            _context.Attach(originalEntity);
            var properties = originalEntity.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (!property.Name.Equals("CreatedAt"))
                {
                    _logger.LogWarning($"Encouneterd property '{property.Name}' when updating '{typeof(BasicEntity).Name}'. This property is read-only and any attempts to change its value will be ignored.");
                    property.SetValue(originalEntity, property.GetValue(entityToBeUpdated));
                }
            }

            originalEntity.UpdatedAt = DateTime.UtcNow;
            _logger.LogDebug($"Finished method '{nameof(CustomUpdate)}'.");

            return originalEntity;
        }
    }
}