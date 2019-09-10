using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public class UserActivityLogger : IUserActivityLogger
    {
        private readonly IActivityLogDbService _dbService;
        private readonly ILogger<UserActivityLogger> _logger;


        public UserActivityLogger(IActivityLogDbService dbService, ILogger<UserActivityLogger> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }


        /// <summary>
        /// Logs user activity.
        /// </summary>
        /// <param name="userName">Username which activity to be logged.</param>
        /// <param name="message">Activity log description.</param>
        /// <param name="activityType">Activity log type.</param>
        public async Task LoggActivityAsync(string userName, string message, ActivityLog.ActivityType activityType)
        {
            try
            {
                var activityLog = new ActivityLog { Description = message, User = userName, Type = activityType };
                await _dbService.AddAsync(activityLog);
            }
            catch (Exception ex)
            {
                // Just log error. We don't need more specific handling exceptions here.
                _logger.LogError(ex, $"Encountered excpetion when logs user actitvity. {ex.Message}. Log: '{message}' of type: '{activityType.ToString()}' for user: '{userName}'.");
            }

        }
    }
}
