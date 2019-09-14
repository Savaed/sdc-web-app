using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Models;
using SDCWebApp.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class VisitInfoDbServiceTests
    {
        private static readonly VisitInfo[] _infoForRestrictedUpdateCases = new VisitInfo[]
        {
            new VisitInfo { ConcurrencyToken = Encoding.ASCII.GetBytes("Updated ConcurrencyToken") },    // Attempt to change 'ConcurrencyToken' which is read-only property.
            new VisitInfo { UpdatedAt = DateTime.Now.AddYears(100) }                                     // Attempt to change 'UpdatedAt' which is read-only property.
        };
        private Expression<Func<VisitInfo, bool>> _predicate;
        private Mock<ApplicationDbContext> _dbContextMock;
        private ILogger<VisitInfoDbService> _logger;
        private VisitInfo _validInfo;


        [OneTimeSetUp]
        public void SetUp()
        {
            _validInfo = CreateInfo();
            _predicate = x => x.CreatedAt > DateTime.MinValue;
            _dbContextMock = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext)));
            _logger = Mock.Of<ILogger<VisitInfoDbService>>();
        }

        #region GetByAsync(predicate)
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // predicate jest null -> arg null exc
        // znalazlo -> ienum<VisitInfo> dla wszystkich znalezionych
        // zaden ele nie spelnia war -> pusty ienum

        [Test]
        public async Task GetByAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetByAsync(_predicate);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetByAsync__Argument_predicate_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetByAsync(null);

                    await action.Should().ThrowExactlyAsync<ArgumentNullException>();
                }
            }
        }

        [Test]
        public async Task GetByAsync__Resource_does_not_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfo table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetByAsync(_predicate);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                         "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetByAsync__Resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.GetByAsync(_predicate);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetByAsync__At_least_one_Ticket_tariffs_found__Should_return_IEnumerable_for_this_tarifs()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Info.ToArray().Length;
                    var service = new VisitInfoDbService(context, _logger);

                    // This predicate filters tariffs with CreatedAt after DateTime.MinValue. It will be all of tariffs.
                    var result = await service.GetByAsync(_predicate);

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task GetByAsync__None_Ticket_tariffs_satisfy_predicate__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    // This predicate filters tariffs with CreatedAt equals DateTime.MaxValue. It will none tariffs.
                    var result = await service.GetByAsync(x => x.CreatedAt == DateTime.MaxValue);

                    result.Count().Should().Be(0);
                }
            }
        }

        #endregion


        #region GetAsync(string id)
        // nie istnieje tabela -> internal ex
        // tabela jest nullem -> internal ex
        // id jest nullem/pusty -> arg exc
        // 0 znaleziono -> invalid oper exc
        // zasob jest pusty -> invalid oper exc
        // znalazlo -> zwraca tylko jeden element

        [Test]
        public async Task GetAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new VisitInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetAsync(id);

            await action.Should().ThrowExactlyAsync<ArgumentException>("Because id cannot be null or empty string.");
        }

        [Test]
        public async Task GetAsync__Found_zero_matching_general_sightseeing_info__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because VisitInfo not found.");
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
                    context.Info.RemoveRange(await context.Info.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of VisitInfo.");
                }
            }
        }

        [Test]
        public async Task GetAsync__General_sightseeing_info_found__Should_return_this_general_sightseeing_info()
        {
            VisitInfo expectedVisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedVisitInfo = await context.Info.FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.GetAsync(expectedVisitInfo.Id);
                    result.Should().BeEquivalentTo(expectedVisitInfo);
                }
            }
        }

        #endregion


        #region GetAllAsync()
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // znalazlo -> ienum<VisitInfo> dla wszystkich z zasobu

        [Test]
        public async Task GetAllAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetAllAsync__All_general_sightseeing_infos_found__Should_return_IEnumerable_for_all_ticket_groups()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Info.ToArray().Length;
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        #endregion


        #region Add(VisitInfo info)
        // zasob nie istnieje -> exc z msg
        // zasob jest nullem -> null ref exc
        // problem z zapisaniem zmian -> inter | Nie mam pojecia jak to przetestowac xD
        // arg jest nullem -> arg null exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

        [Test]
        public async Task AddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validInfo);

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
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of 'VisitInfo'. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new VisitInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.AddAsync(null as VisitInfo);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'info' is null.");
        }


        [Test]
        public async Task AddAsync__In_resource_exists_the_same_general_sightseeing_info_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Add(_validInfo);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    // Testing method
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same VisitInfo as this one to be added.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Should_return_added_general_sightseeing_info()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.AddAsync(_validInfo);

                    result.Should().BeEquivalentTo(_validInfo);
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
                    int expectedLength = context.Info.Count() + 1;
                    var service = new VisitInfoDbService(context, _logger);

                    await service.AddAsync(_validInfo);

                    context.Info.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_contains_added_general_sightseeing_info()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    await service.AddAsync(_validInfo);

                    context.Info.Contains(_validInfo).Should().BeTrue();
                }
            }
        }

        #endregion


        #region RestrictedAddAsync(VisitInfo VisitInfo)
        // zasob nie istnieje -> exc z msg
        // zasob jest nullem -> null ref exc
        // problem z zapisaniem zmian -> inter | Nie mam pojecia jak to przetestowac xD
        // arg jest nullem -> arg null exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac (takie samo id, wartosci ) -> invalid oper exc
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet
        // proba doda

        [Test]
        public async Task RestrictedAddAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedAddAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedAddAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new VisitInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.RestrictedAddAsync(null as VisitInfo);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitInfo' is null.");
        }


        [Test]
        public async Task RestrictedAddAsync__In_resource_exists_sightseeing_tariff_with_the_same_id_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Add(_validInfo);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validInfo.Description = "changed description";
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedAddAsync(_validInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same VisitInfo as this one to be added.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Attempt_to_add_entity_with_the_same_description_as_existing_one_but_wit_different_id__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Add(_validInfo);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    _validInfo.Id += "_changed_id";
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedAddAsync(_validInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because this method DOES NOT allow to add a new entity with the same properties value (Description) " +
                        "but different 'Id'. It is intentional behaviour.");
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Should_return_added_sightseeing_tariff()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedAddAsync(_validInfo);

                    result.Should().BeEquivalentTo(_validInfo);
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Info.Count() + 1;
                    var service = new VisitInfoDbService(context, _logger);

                    await service.RestrictedAddAsync(_validInfo);

                    context.Info.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task RestrictedAddAsync__Add_successful__Resource_contains_added_sightseeing_tariff()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    await service.RestrictedAddAsync(_validInfo);

                    context.Info.Contains(_validInfo).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateAsync(VisitInfo info)
        // arg jest nullem -> arg null exc
        // arg ma id ktore jest nullem albo pusty -> arg exc   
        // zasob jest nullem -> inter exc
        // zasob nie istnieje -> inter exc
        // zasob jest pusty -> invalid oper exc
        // nie znaleziono podanego biletu -> invalid oper exc
        // update udal sie -> tyle samo biletow co przed operacja 
        // update sie udal -> w zasobie istnieje zmodyfikowany bilet
        // update sie udal -> w zasobie nie istnieje poprzednia wersja biletu
        // update sie udal -> zwraca zmodyfikowany bilet

        [Test]
        public async Task UpdateAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(null as VisitInfo);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitInfo' cannot be null.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidVisitInfo = new VisitInfo { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(invalidVisitInfo);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'VisitInfo' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfoBeforUpdate = await context.Info.FirstAsync();
                    VisitInfo = VisitInfoBeforUpdate.Clone() as VisitInfo;
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(VisitInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Matching_general_sightseeing_info_not_found__Should_throw_InvalidOperationException()
        {
            VisitInfo VisitInfo = CreateInfo();

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(VisitInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching VisitInfo not found.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Should_return_updated_general_sightseeing_info()
        {
            VisitInfo VisitInfoBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfoBeforUpdate = await context.Info.FirstAsync();
                    VisitInfo VisitInfo = VisitInfoBeforUpdate;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(VisitInfo);

                    result.Should().BeEquivalentTo(VisitInfo);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_contains_updated_general_sightseeing_info()
        {
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(VisitInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Contains(VisitInfo).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_general_sightseeing_info()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfoBeforUpdate = VisitInfo.Clone() as VisitInfo;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(VisitInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Single(x => x == VisitInfo).Should().NotBeSameAs(VisitInfoBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfoBeforUpdate = await context.Info.FirstAsync();
                    VisitInfo = VisitInfoBeforUpdate;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);
                    expectedLength = await context.Info.CountAsync();

                    await service.UpdateAsync(VisitInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MaxValue()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfoBeforUpdate = VisitInfo.Clone() as VisitInfo;
                    VisitInfoBeforUpdate.UpdatedAt = DateTime.MinValue;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(VisitInfo);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfoBeforUpdate = VisitInfo.Clone() as VisitInfo;
                    VisitInfoBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(VisitInfo);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)VisitInfoBeforUpdate.UpdatedAt);
                }
            }
        }

        #endregion


        #region RestrictedUpdateAsync(VisitInfo tariff)
        // wszystko co w normalnym update
        // proba zmian readonly properties -> metoda niczego nie zmieni i zaloguje warny


        [Test]
        public async Task RestrictedUpdateAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedUpdateAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfo table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.RestrictedUpdateAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(null as VisitInfo);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'VisitInfo' cannot be null.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidVisitInfo = new VisitInfo { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(invalidVisitInfo);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'VisitInfo' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfoBeforUpdate = await context.Info.FirstAsync();
                    VisitInfo = VisitInfoBeforUpdate.Clone() as VisitInfo;
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.RestrictedUpdateAsync(VisitInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because esource is empty.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Matching_VisitInfo_not_found__Should_throw_InvalidOperationException()
        {
            VisitInfo VisitInfo = new VisitInfo
            {
                Id = "0",
                Description = "Sample description for only this test.",
                UpdatedAt = DateTime.Now.AddHours(-3)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    // In db does not matching VisitInfo to belowe disount.
                    Func<Task> result = async () => await service.RestrictedUpdateAsync(VisitInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching VisitInfo not found.");
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Should_return_updated_sightseeing_tariff()
        {
            VisitInfo VisitInfoBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfoBeforUpdate = await context.Info.FirstAsync();
                    VisitInfo VisitInfo = VisitInfoBeforUpdate;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(VisitInfo);

                    result.Should().BeEquivalentTo(VisitInfo);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_should_contains_updated_sightseeing_tariff()
        {
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(VisitInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Contains(VisitInfo).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_sightseeing_tariff()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfoBeforUpdate = VisitInfo.Clone() as VisitInfo;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(VisitInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Single(x => x == VisitInfo).Should().NotBeSameAs(VisitInfoBeforUpdate);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfoBeforUpdate = await context.Info.FirstAsync();
                    VisitInfo = VisitInfoBeforUpdate;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);
                    expectedLength = await context.Info.CountAsync();

                    await service.RestrictedUpdateAsync(VisitInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MaxValue()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfoBeforUpdate = VisitInfo.Clone() as VisitInfo;
                    VisitInfoBeforUpdate.UpdatedAt = DateTime.MinValue;
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(VisitInfo);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task RestrictedUpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            VisitInfo VisitInfoBeforUpdate;
            VisitInfo VisitInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo = await context.Info.FirstAsync();
                    VisitInfoBeforUpdate = VisitInfo.Clone() as VisitInfo;
                    VisitInfoBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    VisitInfo.Description = "Changed description.";
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(VisitInfo);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)VisitInfoBeforUpdate.UpdatedAt);
                }
            }
        }

        [TestCaseSource(nameof(_infoForRestrictedUpdateCases))]
        public async Task RestrictedUpdateAsync__Attempt_to_update_readonly_properties__These_changes_will_be_ignored(VisitInfo updatedTariffCase)
        {
            // In fact, any read-only property changes will be ignored and no exception will be thrown, but the method will log any of these changes as warning.

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var tariffBeforeUpdate = await context.Info.FirstAsync();
                    updatedTariffCase.Id = tariffBeforeUpdate.Id;
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.RestrictedUpdateAsync(updatedTariffCase);

                    // Those properties should be unchanged since they are readonly.
                    result.CreatedAt.Should().BeSameDateAs(tariffBeforeUpdate.CreatedAt);
                    result.ConcurrencyToken.Should().BeSameAs(tariffBeforeUpdate.ConcurrencyToken);
                }
            }
        }
        #endregion


        #region DeleteAsync(string id)
        // zasob jest nullem -> internal
        // zasob nie istnieje -> intern
        // id jest nullem albo pusty -> arg exc
        // zasob jest pusty -> invalid oper exc
        // nie ma takiego biletu jak podany -> invalid oper exc
        // usuwanie sie udalo -> nie w zasobie usunietego biletu
        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

        [Test]
        public async Task DeleteAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Resource_doesnt_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new VisitInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.DeleteAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task DeleteAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.DeleteAsync("1");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot delete any element.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__general_sightseeing_info_to_be_deleted_not_found__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    // Only positive Id is valid so there is not Id = '-100'.
                    Func<Task> result = async () => await service.DeleteAsync("-100");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because VisitInfo to be deleted not found.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_length_should_be_less_by_1()
        {
            string id;
            int expectedLength;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Info.Count() - 1;
                    VisitInfo VisitInfoToBeDeleted = context.Info.First();
                    id = VisitInfoToBeDeleted.Id;
                    var service = new VisitInfoDbService(context, _logger);
                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_should_not_contain_deleted_general_sightseeing_info()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    VisitInfo VisitInfoToBeDeleted = context.Info.First();
                    id = VisitInfoToBeDeleted.Id;
                    var service = new VisitInfoDbService(context, _logger);

                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Info.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
                }
            }
        }

        #endregion


        #region GetWithPagination(int pageNumber, int pageSize)
        // zasob jest nullem -> internal
        // zasob nie istnieje -> internal
        // page number/ size jest nullem -> argument null exc
        // page nie moze byc < 1 -> arg out of range
        // size nie moze byc < 0 -> arg out of range
        // przypadek: jest 201 elementow, bierzemy nr 3 i size 100 el, to skip 200 i 1 el sie wyswietli -> zwracamy liste z tym jednym elementem
        // jest 200 ele nr jest 3, size 100 -> pusta lista
        // tabela jest pusta -> jw

        [Test]
        public async Task GetWithPaginationAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Info = null as DbSet<VisitInfo>;
                    var service = new VisitInfoDbService(context, _logger);

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
                    // Drop VisitInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Info]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of VisitInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_number_is_less_than_1__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new VisitInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(0, 10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page number cannot be less than 1.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_size_is_negative__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new VisitInfoDbService(_dbContextMock.Object, _logger);

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
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.Info.AddAsync(new VisitInfo { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

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
                        await context.Info.AddAsync(new VisitInfo { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

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
                    context.Info.RemoveRange(await context.Info.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new VisitInfoDbService(context, _logger);

                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        #endregion


        #region Privates

        private VisitInfo CreateInfo(string id = "1", string description = "test", int maxChildAge = 5, int maxAllowedGroupSize = 30, int maxTicketOrderInterval = 4, int sightseeingDuration = 2)
        {
            return new VisitInfo
            {
                Id = id,
                Description = description,
                MaxAllowedGroupSize = maxAllowedGroupSize,
                MaxChildAge = maxChildAge,
                MaxTicketOrderInterval = maxTicketOrderInterval,
                SightseeingDuration = sightseeingDuration,
                OpeningHours = CreateOpenigHoursInWeek()
            };
        }

        private OpeningHours[] CreateOpenigHoursInWeek()
        {
            return new OpeningHours[]
            {
                new OpeningHours{ ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Monday },
                new OpeningHours{ ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Tuesday },
                new OpeningHours{ ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Wednesday },
                new OpeningHours{ ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Thursday },
                new OpeningHours{ ClosingHour = new TimeSpan(18,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Friday },
                new OpeningHours{ ClosingHour = new TimeSpan(16,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Saturday },
                new OpeningHours{ ClosingHour = new TimeSpan(16,0,0), OpeningHour = new TimeSpan(10,0,0) ,DayOfWeek = DayOfWeek.Sunday }
            };
        }

        #endregion

    }
}
