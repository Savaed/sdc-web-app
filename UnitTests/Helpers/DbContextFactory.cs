using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
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

        #endregion

    }
}
