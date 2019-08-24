using FluentAssertions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Data.Validators;
using SDCWebApp.Models;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class GeneralSightseeingInfoDbServiceTests

    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private ILogger<GeneralSightseeingInfoDbService> _logger;
        private readonly GeneralSightseeingInfo _validInfo = new GeneralSightseeingInfo
        {
            Id = "1",
            ClosingHour = 18,
            OpeningHour = 10,
            MaxChildAge = 5,
            Description = "Sample info",
            MaxAllowedGroupSize = 35,
            UpdatedAt = DateTime.Now.AddDays(-1)
        };

        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext)));
            _logger = Mock.Of<ILogger<GeneralSightseeingInfoDbService>>();
        }


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
                    context.GeneralSightseeingInfo = null as DbSet<GeneralSightseeingInfo>;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

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
                    // Drop GeneralSightseeingInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [GeneralSightseeingInfo]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of GeneralSightseeingInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new GeneralSightseeingInfoDbService(_dbContextMock.Object, _logger);

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
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because GeneralSightseeingInfo not found.");
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
                    context.GeneralSightseeingInfo.RemoveRange(await context.GeneralSightseeingInfo.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of GeneralSightseeingInfo.");
                }
            }
        }

        [Test]
        public async Task GetAsync__General_sightseeing_info_found__Should_return_this_general_sightseeing_info()
        {
            GeneralSightseeingInfo expectedGeneralSightseeingInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedGeneralSightseeingInfo = await context.GeneralSightseeingInfo.FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    var result = await service.GetAsync(expectedGeneralSightseeingInfo.Id);
                    result.Should().BeEquivalentTo(expectedGeneralSightseeingInfo);
                }
            }
        }

        #endregion


        #region GetAllAsync()
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // znalazlo -> ienum<GeneralSightseeingInfo> dla wszystkich z zasobu

        [Test]
        public async Task GetAllAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo = null as DbSet<GeneralSightseeingInfo>;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Resource_doesnt_exit__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop GeneralSightseeingInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [GeneralSightseeingInfo]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of GeneralSightseeingInfo. " +
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
                    context.GeneralSightseeingInfo.RemoveRange(await context.GeneralSightseeingInfo.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    int expectedLength = context.GeneralSightseeingInfo.ToArray().Length;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        #endregion


        #region Add(GeneralSightseeingInfo info)
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
                    context.GeneralSightseeingInfo = null as DbSet<GeneralSightseeingInfo>;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    // Drop GeneralSightseeingInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [GeneralSightseeingInfo]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of 'GeneralSightseeingInfo'. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new GeneralSightseeingInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.AddAsync(null as GeneralSightseeingInfo);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'info' is null.");
        }


        [Test]
        public async Task AddAsync__In_resource_exists_the_same_general_sightseeing_info_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo.Add(_validInfo);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    // Testing method
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same GeneralSightseeingInfo as this one to be added.");
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
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    int expectedLength = context.GeneralSightseeingInfo.Count() + 1;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    await service.AddAsync(_validInfo);

                    context.GeneralSightseeingInfo.Count().Should().Be(expectedLength);
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
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    await service.AddAsync(_validInfo);

                    context.GeneralSightseeingInfo.Contains(_validInfo).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateAsync(GeneralSightseeingInfo info)
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
                    context.GeneralSightseeingInfo = null as DbSet<GeneralSightseeingInfo>;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    // Drop GeneralSightseeingInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [GeneralSightseeingInfo]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validInfo);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of GeneralSightseeingInfo. " +
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
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(null as GeneralSightseeingInfo);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'GeneralSightseeingInfo' cannot be null.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidGeneralSightseeingInfo = new GeneralSightseeingInfo { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(invalidGeneralSightseeingInfo);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'GeneralSightseeingInfo' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            GeneralSightseeingInfo generalSightseeingInfoBeforUpdate;
            GeneralSightseeingInfo generalSightseeingInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    generalSightseeingInfoBeforUpdate = await context.GeneralSightseeingInfo.FirstAsync();
                    generalSightseeingInfo = generalSightseeingInfoBeforUpdate.Clone() as GeneralSightseeingInfo;
                    context.GeneralSightseeingInfo.RemoveRange(await context.GeneralSightseeingInfo.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    generalSightseeingInfo.Description = "Changed description.";
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(generalSightseeingInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Matching_general_sightseeing_info_not_found__Should_throw_InvalidOperationException()
        {
            GeneralSightseeingInfo generalSightseeingInfo = new GeneralSightseeingInfo
            {
                Id = "0",
                Description = "sample",
                MaxAllowedGroupSize = 12,
                MaxChildAge = 3,
                OpeningHour = 8,
                ClosingHour = 16,
                UpdatedAt = DateTime.Now.AddHours(-3)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(generalSightseeingInfo);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching GeneralSightseeingInfo not found.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Should_return_updated_general_sightseeing_info()
        {
            GeneralSightseeingInfo generalSightseeingInfoBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    generalSightseeingInfoBeforUpdate = await context.GeneralSightseeingInfo.FirstAsync();
                    GeneralSightseeingInfo generalSightseeingInfo = generalSightseeingInfoBeforUpdate;
                    generalSightseeingInfo.Description = "Changed description.";
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(generalSightseeingInfo);

                    result.Should().BeEquivalentTo(generalSightseeingInfo);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_contains_updated_general_sightseeing_info()
        {
            GeneralSightseeingInfo generalSightseeingInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    generalSightseeingInfo = await context.GeneralSightseeingInfo.FirstAsync();
                    generalSightseeingInfo.Description = "Changed description.";
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(generalSightseeingInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo.Contains(generalSightseeingInfo).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_general_sightseeing_info()
        {
            GeneralSightseeingInfo generalSightseeingInfoBeforUpdate;
            GeneralSightseeingInfo generalSightseeingInfo;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    generalSightseeingInfo = await context.GeneralSightseeingInfo.FirstAsync();
                    generalSightseeingInfoBeforUpdate = generalSightseeingInfo.Clone() as GeneralSightseeingInfo;
                    generalSightseeingInfo.Description = "Changed description.";
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    var result = await service.UpdateAsync(generalSightseeingInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo.Single(x => x == generalSightseeingInfo).Should().NotBeSameAs(generalSightseeingInfoBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            GeneralSightseeingInfo generalSightseeingInfoBeforUpdate;
            GeneralSightseeingInfo generalSightseeingInfo;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    generalSightseeingInfoBeforUpdate = await context.GeneralSightseeingInfo.FirstAsync();
                    generalSightseeingInfo = generalSightseeingInfoBeforUpdate;
                    generalSightseeingInfo.Description = "Changed description.";
                    var service = new GeneralSightseeingInfoDbService(context, _logger);
                    expectedLength = await context.GeneralSightseeingInfo.CountAsync();

                    await service.UpdateAsync(generalSightseeingInfo);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo.Count().Should().Be(expectedLength);
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
                    context.GeneralSightseeingInfo = null as DbSet<GeneralSightseeingInfo>;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    // Drop GeneralSightseeingInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [GeneralSightseeingInfo]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of GeneralSightseeingInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new GeneralSightseeingInfoDbService(_dbContextMock.Object, _logger);

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
                    context.GeneralSightseeingInfo.RemoveRange(await context.GeneralSightseeingInfo.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    // Only positive Id is valid so there is not Id = '-100'.
                    Func<Task> result = async () => await service.DeleteAsync("-100");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because GeneralSightseeingInfo to be deleted not found.");
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
                    expectedLength = context.GeneralSightseeingInfo.Count() - 1;
                    GeneralSightseeingInfo generalSightseeingInfoToBeDeleted = context.GeneralSightseeingInfo.First();
                    id = generalSightseeingInfoToBeDeleted.Id;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);
                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo.Count().Should().Be(expectedLength);
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
                    GeneralSightseeingInfo generalSightseeingInfoToBeDeleted = context.GeneralSightseeingInfo.First();
                    id = generalSightseeingInfoToBeDeleted.Id;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.GeneralSightseeingInfo.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
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
                    context.GeneralSightseeingInfo = null as DbSet<GeneralSightseeingInfo>;
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    // Drop GeneralSightseeingInfos table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [GeneralSightseeingInfo]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of GeneralSightseeingInfo. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_number_is_less_than_1__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new GeneralSightseeingInfoDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(0, 10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page number cannot be less than 1.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_size_is_negative__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new GeneralSightseeingInfoDbService(_dbContextMock.Object, _logger);

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
                    context.GeneralSightseeingInfo.RemoveRange(await context.GeneralSightseeingInfo.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.GeneralSightseeingInfo.AddAsync(new GeneralSightseeingInfo { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                        await context.GeneralSightseeingInfo.AddAsync(new GeneralSightseeingInfo { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

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
                    context.GeneralSightseeingInfo.RemoveRange(await context.GeneralSightseeingInfo.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new GeneralSightseeingInfoDbService(context, _logger);

                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
