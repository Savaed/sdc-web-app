using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Data;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    /// <summary>
    /// Provides a way for enabling SqlLite in-memory mode using in unit tests.
    /// </summary>
    public class DbContextFactory : IDisposable
    {
        private DbConnection _connection;


        /// <summary>
        /// Asynchronously crestes and opens a SqlLite connection and initializes new <see cref="ApplicationDbContext"/> instance.
        /// </summary>
        /// <returns><see cref="ApplicationDbContext"/> object.</returns>
        public async Task<ApplicationDbContext> CreateContextAsync()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new ApplicationDbContext(options))
                {
                    await context.Database.EnsureCreatedAsync();

                    ClearSeedData(context);

                    // For enable migration uncomment following line
                    //context.Database.MigrateAsync()
                }
            }
            return new ApplicationDbContext(CreateOptions());
        }

        /// <summary>
        /// Closes any opened database connection if exists.
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }


        #region Privates

        private DbContextOptions<ApplicationDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection).Options;
        }

        private void ClearSeedData(ApplicationDbContext context)
        {
            // BUG: Sql command 'TRUNCATE TABLE [table_name]' does not work as expected. An exception is thrown while performing unit tests:
            // Microsoft.Data.Sqlite.SqliteException : SQLite Error 1: 'near "TRUNCATE": syntax error'.

            // Clear seed data. For unit test purposes it's not needed.
            var propeties = context
                .GetType()
                .GetProperties()
                .Where(x => x.Name != nameof(ApplicationDbContext.ChangeTracker)
                    && x.Name != nameof(ApplicationDbContext.Database)
                    && x.Name != nameof(ApplicationDbContext.Model));

            foreach (var property in propeties)
            {
                context.Database.BeginTransaction();
                const string x = "TRUNCATE TABLE [Groups]";
                context.Database.ExecuteSqlCommand(x);
                context.Database.CommitTransaction();
            }
        }

        #endregion

    }
}
