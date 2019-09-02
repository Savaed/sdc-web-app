using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Threading.Tasks;

namespace SDCWebApp.Helpers.Extensions
{
    /// <summary>
    /// Provides extension method for resolving database concurrency conflicts.
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// Asynchronously trying save any changes made in this <see cref="DbContext"/> context to the database and resolving eventually concurrency conflict. 
        /// 'Database wins' and 'client wins' approach are supported.
        /// Throws a <see cref="NotSupportedException"/> if data cannot be save properly.
        /// </summary>
        /// <param name="maxResolveAttempts">Max attempts for retrying saves data</param>
        /// <param name="clientWins">Conflict resolving strategy. Set to true if want use 'client wins' or false for 'database wins'</param>
        /// <returns>Number of affected rows</returns>
        /// <exception cref="NotSupportedException">Cannot save data properly.</exception>
        public static async Task<int> TrySaveChangesAsync(this DbContext dbContext, int maxResolveAttempts = 3, bool clientWins = true)
        {
            const int DefaultResolveAttempts = 3;
            ILogger logger = LogManager.GetCurrentClassLogger();

            if (maxResolveAttempts < 0 || maxResolveAttempts > 50)
            {
                maxResolveAttempts = DefaultResolveAttempts;
                logger.Warn($"Value of parameter '{ nameof(maxResolveAttempts) }' cannot be negative but '{ maxResolveAttempts }' found. Instead of this value, '{ DefaultResolveAttempts }' will be used.");
            }

            do
            {
                try
                {
                    int result = await dbContext.SaveChangesAsync();
                    return result;
                }
                catch (DbUpdateConcurrencyException e1)
                {
                    foreach (var entry in e1.Entries)
                    {
                        if (clientWins)
                        {
                            logger.Error(e1, $"Concurrency conflict detected while proccessing operation. Remained conflict resolving attempt: '{ maxResolveAttempts }'. Conflict resolving approach: 'client wins'.");

                            var databaseValues = await entry.GetDatabaseValuesAsync();
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else if (clientWins == false)
                        {
                            logger.Error(e1, $"Concurrency conflict detected while proccessing operation. Remained conflict resolving attempt: '{ maxResolveAttempts }'. Conflict resolving approach: 'database wins'.");
                            entry.Reload();
                        }

                    }
                }
                catch (DbUpdateException e2)
                {
                    logger.Error(e2, "Cannot save data properly.");
                    throw;
                }
            }
            while (maxResolveAttempts-- > 0);
            return 0;
        }
    }
}
