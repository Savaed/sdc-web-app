using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using SDCWebApp.Data;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Services;
using System.Threading.Tasks;
using SDCWebApp.Models;
using NLog;
using UnitTests.Helpers;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.TestHelper;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data.Validators;

namespace UnitTests.Services
{
    [TestFixture]
    public class SightseeingGroupDbServiceTests
    {
        private ApplicationDbContext _dbContext;
        private Mock<IDbRepository> _repoMock;
        private ILogger<SightseeingGroupDbService> _logger;
        private Mock<ICustomValidator<SightseeingGroup>> _validatorMock;


        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContext = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext))).Object;
            _repoMock = new Mock<IDbRepository>();
            _logger = Mock.Of<ILogger<SightseeingGroupDbService>>();
            _validatorMock = new Mock<ICustomValidator<SightseeingGroup>>();
            _validatorMock.Setup(v => v.Validate(It.IsAny<SightseeingGroup>())).Returns(new ValidationResult());
        }


        #region GetGroupAsync(string id)
        // id jest nullem/pusty -> arg exc
        // nie istnieje bilet z takim id -> invalid oper exc
        // zasob jest pusty -> invalid oper exc i repo rzuci wyjatek 
        // zaosb nie istnieje -> null  ref exc
        // znalazlo -> zwraca tylko jeden element

        [Test]
        public async Task GetGroupAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            _repoMock.Setup(r => r.GetByIdAsync<SightseeingGroup>(id)).ThrowsAsync(new ArgumentException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'id' is null ore empty string.");
        }

        [Test]
        public async Task GetGroupAsync__Searching_SightseeingGroup_not_found__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<SightseeingGroup>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching element not found.");
        }

        [Test]
        public async Task GetGroupAsync__The_resource_is_empty__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<SightseeingGroup>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because the resource is empty.");
        }

        [Test]
        public async Task GetGroupAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<SightseeingGroup>(id)).ThrowsAsync(new NullReferenceException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupAsync(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetGroupAsync__Searching_SightseeingGroup_found__Should_return_this_SightseeingGroup()
        {
            SightseeingGroup expectedSightseeingGroup;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedSightseeingGroup = await context.Groups.FirstOrDefaultAsync();
                }
            }
            _repoMock.Setup(r => r.GetByIdAsync<SightseeingGroup>(expectedSightseeingGroup.Id)).ReturnsAsync(expectedSightseeingGroup);
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetGroupAsync(expectedSightseeingGroup.Id);

            result.Should().BeEquivalentTo(expectedSightseeingGroup);
        }

        #endregion


        #region GetAllGroupsAsync()
        // zasob nie istnieje -> null ref exc
        // zasob jest pusty -> pusty ienumer i repo rzuci wyjatek 
        // znalazlo -> ienum<SightseeingGroup> dla wszystkich z zasobu


        [Test]
        public async Task GetAllGroupsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            _repoMock.Setup(r => r.GetAllAsync<SightseeingGroup>()).ThrowsAsync(new NullReferenceException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetAllGroupsAsync();

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetAllGroupsAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
        {
            _repoMock.Setup(r => r.GetAllAsync<SightseeingGroup>()).ReturnsAsync(new SightseeingGroup[] { }.AsEnumerable());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetAllGroupsAsync();

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetAllGroupsAsync__All_SightseeingGroups_found__Should_return_IEnumerable_for_all_SightseeingGroups()
        {
            var expectedSightseeingGroups = new SightseeingGroup[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedSightseeingGroups = await context.Groups.ToArrayAsync();
                }
            }
            _repoMock.Setup(r => r.GetAllAsync<SightseeingGroup>()).ReturnsAsync(expectedSightseeingGroups);
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetAllGroupsAsync();

            result.Count().Should().Be(expectedSightseeingGroups.Count());
        }

        #endregion


        #region GetGroupsAsync(IEnumerable<string> ids)
        // zaosb nie istnieje -> null  ref exc
        // zasob jest pusty -> arg out of range
        // podano wiecej ids niz jest elementow w bazie -> arg out of range
        // ids sa nullem albo pusta tablica -> arg exc
        // jakies id nie puasuje -> ienum z mniejsza iloscia biletow
        // zadne id nie pasuje -> pusty ienumer
        // wszystkie id pasuja -> ienumer dla wszysztkich

        [Test]
        public async Task GetGroupsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string[] ids = { "1", "2" };
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(It.IsNotNull<IEnumerable<string>>())).Throws<NullReferenceException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupsAsync(ids);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetGroupsAsync__The_resource_which_is_quering_is_empty__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = { "1", "2" };
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(It.IsNotNull<IEnumerable<string>>())).Throws<ArgumentOutOfRangeException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = () => service.GetGroupsAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task GetGroupsAsync__Amount_od_ids_is_greater_than_SightseeingGroups_number_in_resource__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = new string[] { "1", "2", "3", "4", "5" };
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(ids)).Throws<ArgumentOutOfRangeException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupsAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because amount of ids id greater than overall number of SightseeingGroups in quering resource.");
        }

        [Test]
        public async Task GetGroupsAsync__Ids_are_null_or_empty__Should_throw_ArgumentException([Values(null, new string[] { "", "", "" })] string[] ids)
        {
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(ids)).Throws<ArgumentException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupsAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because arguemnt 'ids' is null or empty.");
        }

        [Test]
        public async Task GetGroupsAsync__At_least_one_id_dont_match__Should_return_IEnumerable_for_all_other_SightseeingGroups()
        {
            var exptectedSightseeingGroups = new SightseeingGroup[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    exptectedSightseeingGroups = await context.Groups.ToArrayAsync();
                }
            }
            List<string> ids = new List<string>();
            foreach (SightseeingGroup SightseeingGroup in exptectedSightseeingGroups)
            {
                ids.Add(SightseeingGroup.Id);
            }
            // This one id will be not matching.
            ids[0] = "1";
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(ids)).Returns(exptectedSightseeingGroups.Skip(1));
            int expectedLength = exptectedSightseeingGroups.Count() - 1;
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetGroupsAsync(ids);

            result.Count().Should().Be(expectedLength);
        }

        [Test]
        public async Task GetGroupsAsync__Any_id_dont_match__Should_return_empty_IEnumerable()
        {
            string[] ids = new string[] { "-1", "-2" };
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(ids)).Returns(new List<SightseeingGroup> { }.AsEnumerable());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetGroupsAsync(ids);

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetGroupsAsync__All_od_ids_matches__Should_return_IEnumerable_for_all_SightseeingGroups()
        {
            var exptectedSightseeingGroups = new SightseeingGroup[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    exptectedSightseeingGroups = await context.Groups.ToArrayAsync();
                }
            }
            List<string> ids = new List<string>();
            foreach (SightseeingGroup SightseeingGroup in exptectedSightseeingGroups)
            {
                ids.Add(SightseeingGroup.Id);
            }
            _repoMock.Setup(r => r.GetByIds<SightseeingGroup>(ids)).Returns(exptectedSightseeingGroups);
            int expectedLength = exptectedSightseeingGroups.Count();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetGroupsAsync(ids);

            result.Count().Should().Be(expectedLength);
        }
        #endregion


        #region GetGroupsByAsync(Expression<Func<SightseeingGroup, bool>> predicate)
        // predicate jest nullem -> arg null exc
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> pusty ienumer
        // zaden element nie spelnia predykatu -> pusty ienumer
        // wiecej niz jeden element spelnia predykat -> ienumer z dl. min. 2
        // tylko jeden element spelnia predykat -> ienum z dl. = 1

        [Test]
        public async Task GetGroupsByAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbOptions = Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext));
            var dbContextMock = new Mock<ApplicationDbContext>(dbOptions);
            dbContextMock.Setup(c => c.Groups).Returns(null as DbSet<SightseeingGroup>);
            var service = new SightseeingGroupDbService(_logger, dbContextMock.Object, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetGroupsByAsync__The_predicate_is_null__Should_throw_ArgumentNullException()
        {
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetGroupsByAsync(null as Expression<Func<SightseeingGroup, bool>>);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'predicate' is null.");
        }

        [Test]
        public async Task GetGroupsByAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.RemoveRange(await context.Groups.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetGroupsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetGroupsByAsync__Any_SightseeingGroup_satisfy_predicate__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetGroupsByAsync(x => x.CreatedAt == DateTime.Now.AddYears(-1000));

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetGroupsByAsync__Only_one_SightseeingGroup_satisfy_predicate__Should_return_IEnumerable_with_length_equals_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var expectedSightseeingGroup = await context.Groups.FirstAsync();
                    var expectedSightseeingGroupCreatedAtDate = expectedSightseeingGroup.CreatedAt;
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetGroupsByAsync(x => x.CreatedAt == expectedSightseeingGroupCreatedAtDate);

                    result.Count().Should().Be(1);
                }
            }
        }

        [Test]
        public async Task GetGroupsByAsync__More_that_one_SightseeingGroup_satisfy_predicate__Should_return_IEnumerable_for_matches_SightseeingGroups()
        {
            SightseeingGroup[] SightseeingGroups = new SightseeingGroup[]
            {
                new SightseeingGroup{  SightseeingDate = DateTime.Now.AddDays(2), MaxGroupSize = 30 },
                new SightseeingGroup{  SightseeingDate = DateTime.Now.AddDays(2), MaxGroupSize = 30 },
                new SightseeingGroup{  SightseeingDate = DateTime.Now.AddDays(2), MaxGroupSize = 30 },
                new SightseeingGroup{  SightseeingDate = DateTime.Now.AddDays(2), MaxGroupSize = 32 }

            };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.RemoveRange(await context.Groups.ToArrayAsync());
                    await context.Groups.AddRangeAsync(SightseeingGroups);
                    await context.SaveChangesAsync();

                }
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetGroupsByAsync(x => x.MaxGroupSize == 30);

                    result.Count().Should().Be(3);
                }
            }
        }

        #endregion


        #region AddGroup(SightseeingGroup SightseeingGroup)
        // zasob jest nullem -> null ref exc
        // arg nie przechodzi validacji -> arg exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
        // arg jest nullem -> arg exc
        // validacja biletu sie nie udala (bledne dane) -> arg exc z powodem blednej validacji
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

        [Test]
        public async Task AddGroup__Argument_is_null__Should_throw_ArgumentNullException()
        {
            _repoMock.Setup(r => r.Add<SightseeingGroup>(It.IsAny<SightseeingGroup>())).Throws<ArgumentNullException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddGroupAsync(null as SightseeingGroup);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'SightseeingGroup' is null.");
        }

        //[Test]
        //public async Task AddGroup__Argument_is_invalid__Should_throw_ArgumentException()
        //{
        //    SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "1", Author = "Kowalski", Title = "Sample", Text = null };
        //    var validatorMock = new Mock<ISightseeingGroupValidator>();
        //    validatorMock.Setup(v => v.Validate(sightseeingGroup)).Returns<ValidationResult>(x => x = new ValidationResult());
        //    var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, validatorMock.Object);

        //    Func<Task> result = async () => await service.AddGroupAsync(sightseeingGroup);

        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'SightseeingGroup' is invalid.").WithMessage("Text");
        //}

        [Test]
        public async Task AddGroup__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.Add<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).Throws<NullReferenceException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddGroupAsync(sightseeingGroup);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task AddGroup__In_resource_exists_the_same_SightseeingGroup_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.Add<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).Throws<InvalidOperationException>();
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddGroupAsync(sightseeingGroup);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same SightseeingGroup as this one to be added.");
        }

        [Test]
        public async Task AddGroup__Add_successful__Should_return_added_SightseeingGroup()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "2", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.Add<SightseeingGroup>(sightseeingGroup)).Returns(sightseeingGroup);
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.AddGroupAsync(sightseeingGroup);

            result.Should().BeEquivalentTo(sightseeingGroup);
        }

        [Test]
        public async Task AddGroup__Add_successful__Resource_length_should_be_greater_by_1()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "2", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Groups.Count() + 1;
                    _repoMock.Setup(r => r.Add<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).Callback(() => context.Groups.Add(sightseeingGroup)).Returns(sightseeingGroup);
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.AddGroupAsync(sightseeingGroup);

                    context.Groups.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddGroup__Add_successful__Resource_contains_added_SightseeingGroup()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "2", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    _repoMock.Setup(r => r.Add<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).Callback(() => context.Groups.Add(sightseeingGroup)).Returns(sightseeingGroup);
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.AddGroupAsync(sightseeingGroup);

                    context.Groups.Contains(sightseeingGroup).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateGroupAsync(SightseeingGroup SightseeingGroup)
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> invalid oper exc
        // nie znaleziono podanego biletu -> invalid oper exc
        // arg jest nullem -> arg null exc
        // arg ma id ktore jest nullem albo pusty -> arg exc   
        // validacja sie nie udala (bledne dane) -> arg exc
        // update udal sie -> tyle samo biletow co przed operacja 
        // update sie udal -> w zasobie istnieje zmodyfikowany bilet
        // update sie udal -> w zasobie nie istnieje poprzednia wersja biletu
        // update sie udal -> zwraca zmodyfikowany bilet

        [Test]
        public async Task UpdateGroupAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).ThrowsAsync(new NullReferenceException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateGroupAsync(sightseeingGroup);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }


        [Test]
        public async Task UpdateGroupAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).ThrowsAsync(new InvalidOperationException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateGroupAsync(sightseeingGroup);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task UpdateGroupAsync__Matching_SightseeingGroup_not_found__Should_throw_InvalidOperationException()
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "-1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).ThrowsAsync(new InvalidOperationException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateGroupAsync(sightseeingGroup);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching SightseeingGroup not found.");
        }

        [Test]
        public async Task UpdateGroupAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(null)).ThrowsAsync(new ArgumentNullException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateGroupAsync(null as SightseeingGroup);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'SightseeingGroup' cannot be null.");
        }

        [Test]
        public async Task UpdateGroupAsync__Arguments_Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = id, MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(2) };
            _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(It.IsNotNull<SightseeingGroup>())).ThrowsAsync(new ArgumentException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateGroupAsync(sightseeingGroup);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'SightseeingGroup' has property 'Id' set to null or empty string.");
        }

        //[Test]
        //public async Task UpdateGroupAsync__Validation_failed__Should_throw_ArgumentException()
        //{
        //    SightseeingGroup sightseeingGroup = new SightseeingGroup { Id = "1", SightseeingGroupUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000) };
        //    _SightseeingGroupValidatorMock.Setup(v => v.Validate(sightseeingGroup));
        //    var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validator);

        //    Func<Task> result = async () => await service.UpdateGroupAsync(sightseeingGroup);

        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'SightseeingGroup' has property 'Id' set to null or empty string.");
        //}

        [Test]
        public async Task UpdateGroupAsync__Update_successful__Should_return_updated_SightseeingGroup()
        {
            SightseeingGroup sightseeingGroupBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingGroupBeforUpdate = await context.Groups.FirstAsync();
                    SightseeingGroup sightseeingGroup = sightseeingGroupBeforUpdate;
                    sightseeingGroup.MaxGroupSize = 2;
                    _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(sightseeingGroup)).Callback(() => context.Groups.Update(sightseeingGroup)).ReturnsAsync(sightseeingGroup);
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateGroupAsync(sightseeingGroup);

                    result.Should().BeEquivalentTo(sightseeingGroupBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateGroupAsync__Update_successful__Resource_should_contains_updated_SightseeingGroup()
        {
            SightseeingGroup sightseeingGroup;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingGroup = await context.Groups.FirstAsync();
                    sightseeingGroup.MaxGroupSize = 2;
                    _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(sightseeingGroup)).Callback(() => context.Groups.Update(sightseeingGroup)).ReturnsAsync(sightseeingGroup);
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateGroupAsync(sightseeingGroup);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.Contains(sightseeingGroup).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateGroupAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_SightseeingGroup()
        {
            SightseeingGroup sightseeingGroupBeforUpdate;
            SightseeingGroup sightseeingGroup;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingGroup = await context.Groups.FirstAsync();
                    sightseeingGroupBeforUpdate = sightseeingGroup.Clone() as SightseeingGroup;
                    sightseeingGroup.MaxGroupSize = 2;
                    _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(sightseeingGroup)).Callback(() => context.Groups.Update(sightseeingGroup)).ReturnsAsync(sightseeingGroup);
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateGroupAsync(sightseeingGroup);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.Single(x => x == sightseeingGroup).Should().NotBeSameAs(sightseeingGroupBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateGroupAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            SightseeingGroup sightseeingGroupBeforUpdate;
            SightseeingGroup sightseeingGroup;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    sightseeingGroupBeforUpdate = await context.Groups.FirstAsync();
                    sightseeingGroup = sightseeingGroupBeforUpdate;
                    sightseeingGroup.MaxGroupSize = 2;
                    _repoMock.Setup(r => r.UpdateAsync<SightseeingGroup>(sightseeingGroup)).Callback(() => context.Groups.Update(sightseeingGroup)).ReturnsAsync(sightseeingGroup);
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
                    expectedLength = await context.Groups.CountAsync();

                    await service.UpdateGroupAsync(sightseeingGroup);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.Count().Should().Be(expectedLength);
                }
            }
        }
        #endregion


        #region DeleteGroupAsync(string id)
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> invalid oper exc
        // nie ma takiego biletu jak podany -> invalid oper exc
        // id jest nullem albo pusty -> arg exc
        // usuwanie sie udalo -> nie w zasobie usunietego biletu
        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

        [Test]
        public async Task DeleteGroupAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "1";
            _repoMock.Setup(r => r.DeleteAsync<SightseeingGroup>(id)).ThrowsAsync(new NullReferenceException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteGroupAsync(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }


        [Test]
        public async Task DeleteGroupAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
        {
            string id = "1";
            _repoMock.Setup(r => r.DeleteAsync<SightseeingGroup>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteGroupAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task DeleteGroupAsync__Searching_SightseeingGroup_not_found__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.DeleteAsync<SightseeingGroup>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteGroupAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching SightseeingGroup not found");
        }

        [Test]
        public async Task DeleteGroupAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            _repoMock.Setup(r => r.DeleteAsync<SightseeingGroup>(id)).ThrowsAsync(new ArgumentException());
            var service = new SightseeingGroupDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteGroupAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because searching SightseeingGroup not found");
        }

        [Test]
        public async Task DeleteGroupAsync__Delete_successful__Resources_length_should_be_less_by_1()
        {
            string id;
            int expectedLength;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Groups.Count() - 1;
                    SightseeingGroup sightseeingGroupToBeDeleted = context.Groups.First();
                    id = sightseeingGroupToBeDeleted.Id;
                    _repoMock.Setup(r => r.DeleteAsync<SightseeingGroup>(id)).Callback(() => context.Groups.Remove(sightseeingGroupToBeDeleted));
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
                    await service.DeleteGroupAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteGroupAsync__Delete_successful__Resources_should_not_contain_deleted_SightseeingGroup()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    SightseeingGroup sightseeingGroupToBeDeleted = context.Groups.First();
                    id = sightseeingGroupToBeDeleted.Id;
                    _repoMock.Setup(r => r.DeleteAsync<SightseeingGroup>(id)).Callback(() => context.Groups.Remove(sightseeingGroupToBeDeleted));
                    var service = new SightseeingGroupDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.DeleteGroupAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Groups.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
