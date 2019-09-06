using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Helpers;

using SDCWebApp.Data;
using SDCWebApp.Models;
using SDCWebApp.Services;

namespace UnitTests.Services
{
    [TestFixture]
    public class ActivityLogDbServiceTests
    {       
        private readonly ActivityLog _validActivityLog = new ActivityLog
        {
            Id = "1",
            User = "user",
            Description = "log message",
            Type = ActivityLog.ActivityType.CreateResource,
            UpdatedAt = DateTime.Now.AddDays(-1)
        };
        private Mock<ApplicationDbContext> _dbContextMock;
        private ILogger<ActivityLogDbService> _logger;


        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext)));
            _logger = Mock.Of<ILogger<ActivityLogDbService>>();
        }


        #region GetAsync(string id)
        // The 'ActivityLogs' table doesn't exist       -> throw InternalDbServiceException
        // The 'ActivityLogs' table is null             -> throw InternalDbServiceException
        // The 'ActivityLogs' resource is empty         -> throw InvalidOperationException
        // The parameter 'id' is null or empty      -> throw ArgumentException
        // One element found                        -> return this element
        // None element found                       -> throw InvalidOperationException

        [Test]
        public async Task GetAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs = null as DbSet<ActivityLog>;
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_doesnt_exit__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop ActivityLogs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [ActivityLogs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of ActivityLog. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new ActivityLogDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetAsync(id);

            await action.Should().ThrowExactlyAsync<ArgumentException>("Because id cannot be null or empty string.");
        }

        [Test]
        public async Task GetAsync__Found_zero_matching_activity_log__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("-1");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because ActivityLog not found.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs.RemoveRange(await context.ActivityLogs.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("1");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of ActivityLog.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Activity_log_found__Should_return_this_ActivityLog()
        {
            ActivityLog expectedActivityLog;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedActivityLog = await context.ActivityLogs.FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    var result = await service.GetAsync(expectedActivityLog.Id);
                    result.Should().BeEquivalentTo(expectedActivityLog);
                }
            }
        }

        #endregion       


        #region Add(ActivityLog ActivityLog)
        // The 'ActivityLogs' table doesn't exist  -> throw InternalDbServiceException
        // The 'ActivityLogs' table is null        -> throw InternalDbServiceException
        // The 'ActivityLogs' resource is empty    -> return an empty IEnumerable<ActivityLog>
        // The parameter 'artilce' is null         -> throw ArgumentNullException
        // There is the same element in database   -> throw InvalidOperationException
        // Add secceeded                           -> return added element
        // Add succeeded                           -> the resource lenght is greater by 1
        // Add succeeded                           -> the resource contains added element

        [Test]
        public async Task AddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs = null as DbSet<ActivityLog>;
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validActivityLog);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task AddAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop ActivityLogs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [ActivityLogs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validActivityLog);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of ActivityLog. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new ActivityLogDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.AddAsync(null as ActivityLog);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'ActivityLog' is null.");
        }

        [Test]
        public async Task AddAsync__In_resource_exists_the_same_activity_log_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs.Add(_validActivityLog);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validActivityLog);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same ActivityLog as this one to be added.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Should_return_added_ActivityLog()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    var result = await service.AddAsync(_validActivityLog);

                    result.Should().BeEquivalentTo(_validActivityLog);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.ActivityLogs.Count() + 1;
                    var service = new ActivityLogDbService(context, _logger);

                    await service.AddAsync(_validActivityLog);

                    context.ActivityLogs.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_contains_added_ActivityLog()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    await service.AddAsync(_validActivityLog);

                    context.ActivityLogs.Contains(_validActivityLog).Should().BeTrue();
                }
            }
        }

        #endregion       


        #region GetWithPagination(int pageNumber, int pageSize)
        // The 'ActivityLogs' table doesn't exist                           -> throw InternalDbServiceException
        // The 'ActivityLogs' table is null                                 -> throw InternalDbServiceException
        // The 'ActivityLogs' resource is empty                             -> return an empty IEnumerable
        // The parameter 'pageNumber' or 'pageSize' is null                 -> throw ArgumentNullException
        // The parameter 'pageNumber' < 1                                   -> throw ArgumentOutOfRange
        // The parameter 'pageSize' < 0                                     -> throw ArgumentOutOfRange
        // Case: there is 201 elements, pageNumber is 3 and pageSize is 100 -> skip 200 elements, and only 1 element will be returned 
        // Case: there is 200 elements, pageNumber is 3 and pageSize is 100 -> skip 200 elements, and return en empty IEnumerable

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs = null as DbSet<ActivityLog>;
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop ActivityLogs table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [ActivityLogs]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of ActivityLog. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_number_is_less_than_1__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new ActivityLogDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(0, 10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page number cannot be less than 1.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_size_is_negative__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new ActivityLogDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(3, -10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page size cannot be negative number.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Number_of_elements_on_single_page_is_less_than_page_size__Should_return_all_these_elements()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs.RemoveRange(await context.ActivityLogs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.ActivityLogs.AddAsync(new ActivityLog { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    // Page will be first with 6 elements and second with 4 elements. Second page will be return.
                    var result = await service.GetWithPaginationAsync(2, 6);

                    // More generic solution is: Count() should be count of elements in resource % pageSize.
                    result.Count().Should().Be(4);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__There_are_not_elements_on_single_page__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.ActivityLogs.AddAsync(new ActivityLog { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    // In this case is only 2 pages with any data.
                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.ActivityLogs.RemoveRange(await context.ActivityLogs.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new ActivityLogDbService(context, _logger);

                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
