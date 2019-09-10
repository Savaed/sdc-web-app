using SDCWebApp.Models;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IUserActivityLogger
    {
        /// <summary>
        /// Logs user activity.
        /// </summary>
        /// <param name="userName">Username which activity to be logged.</param>
        /// <param name="message">Activity log description.</param>
        /// <param name="activityType">Activity log type.</param>
        Task LoggActivityAsync(string userName, string message, ActivityLog.ActivityType activityType);
    }
}