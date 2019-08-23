//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Moq;
//using FluentAssertions;
//using SDCWebApp.Data;
//using Microsoft.EntityFrameworkCore;
//using SDCWebApp.Services;
//using System.Threading.Tasks;
//using SDCWebApp.Models;
//using NLog;
//using UnitTests.Helpers;
//using System.Linq;
//using System.Linq.Expressions;
//using FluentValidation.TestHelper;
//using FluentValidation.Results;
//using Microsoft.Extensions.Logging;
//using SDCWebApp.Data.Validators;

//namespace UnitTests.Services
//{
//    [TestFixture]
//    public class SightseeingTariffDbServiceTests
//    {
//        private ApplicationDbContext _dbContext;
//        private Mock<IDbRepository> _repoMock;
//        private ILogger<SightseeingTariffDbService> _logger;
//        private Mock<ICustomValidator<SightseeingTariff>> _validatorMock;


//        [OneTimeSetUp]
//        public void SetUp()
//        {
//            _dbContext = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext))).Object;
//            _repoMock = new Mock<IDbRepository>();
//            _logger = Mock.Of<ILogger<SightseeingTariffDbService>>();
//            _validatorMock = new Mock<ICustomValidator<SightseeingTariff>>();
//            _validatorMock.Setup(v => v.Validate(It.IsAny<SightseeingTariff>())).Returns(new ValidationResult());
//        }


//        #region GetSightseeingTariffAsync(string id)
//        // id jest nullem/pusty -> arg exc
//        // nie istnieje bilet z takim id -> invalid oper exc
//        // zasob jest pusty -> invalid oper exc i repo rzuci wyjatek 
//        // zaosb nie istnieje -> null  ref exc
//        // znalazlo -> zwraca tylko jeden element

//        [Test]
//        public async Task GetSightseeingTariffAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
//        {
//            _repoMock.Setup(r => r.GetByIdAsync<SightseeingTariff>(id)).ThrowsAsync(new ArgumentException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'id' is null ore empty string.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffAsync__Searching_SightseeingTariff_not_found__Should_throw_InvalidOperationException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.GetByIdAsync<SightseeingTariff>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching element not found.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffAsync__The_resource_is_empty__Should_throw_InvalidOperationException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.GetByIdAsync<SightseeingTariff>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because the resource is empty.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.GetByIdAsync<SightseeingTariff>(id)).ThrowsAsync(new NullReferenceException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffAsync__Searching_SightseeingTariff_found__Should_return_this_SightseeingTariff()
//        {
//            SightseeingTariff expectedSightseeingTariff;
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedSightseeingTariff = await context.SightseeingTariffs.FirstOrDefaultAsync();
//                }
//            }
//            _repoMock.Setup(r => r.GetByIdAsync<SightseeingTariff>(expectedSightseeingTariff.Id)).ReturnsAsync(expectedSightseeingTariff);
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.GetSightseeingTariffAsync(expectedSightseeingTariff.Id);

//            result.Should().BeEquivalentTo(expectedSightseeingTariff);
//        }

//        #endregion


//        #region GetAllSightseeingTariffsAsync()
//        // zasob nie istnieje -> null ref exc
//        // zasob jest pusty -> pusty ienumer i repo rzuci wyjatek 
//        // znalazlo -> ienum<SightseeingTariff> dla wszystkich z zasobu


//        [Test]
//        public async Task GetAllSightseeingTariffsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            _repoMock.Setup(r => r.GetAllAsync<SightseeingTariff>()).ThrowsAsync(new NullReferenceException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetAllSightseeingTariffsAsync();

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetAllSightseeingTariffsAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
//        {
//            _repoMock.Setup(r => r.GetAllAsync<SightseeingTariff>()).ReturnsAsync(new SightseeingTariff[] { }.AsEnumerable());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.GetAllSightseeingTariffsAsync();

//            result.Count().Should().Be(0);
//        }

//        [Test]
//        public async Task GetAllSightseeingTariffsAsync__All_SightseeingTariffs_found__Should_return_IEnumerable_for_all_SightseeingTariffs()
//        {
//            var expectedSightseeingTariffs = new SightseeingTariff[] { }.AsEnumerable();
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedSightseeingTariffs = await context.SightseeingTariffs.ToArrayAsync();
//                }
//            }
//            _repoMock.Setup(r => r.GetAllAsync<SightseeingTariff>()).ReturnsAsync(expectedSightseeingTariffs);
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.GetAllSightseeingTariffsAsync();

//            result.Count().Should().Be(expectedSightseeingTariffs.Count());
//        }

//        #endregion


//        #region GetSightseeingTariffsAsync(IEnumerable<string> ids)
//        // zaosb nie istnieje -> null  ref exc
//        // zasob jest pusty -> arg out of range
//        // podano wiecej ids niz jest elementow w bazie -> arg out of range
//        // ids sa nullem albo pusta tablica -> arg exc
//        // jakies id nie puasuje -> ienum z mniejsza iloscia biletow
//        // zadne id nie pasuje -> pusty ienumer
//        // wszystkie id pasuja -> ienumer dla wszysztkich

//        [Test]
//        public async Task GetSightseeingTariffsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            string[] ids = { "1", "2" };
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(It.IsNotNull<IEnumerable<string>>())).Throws<NullReferenceException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffsAsync(ids);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffsAsync__The_resource_which_is_quering_is_empty__Should_throw_ArgumentOutOfRangeException()
//        {
//            string[] ids = { "1", "2" };
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(It.IsNotNull<IEnumerable<string>>())).Throws<ArgumentOutOfRangeException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = () => service.GetSightseeingTariffsAsync(ids);

//            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because resource which is quering is empty.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffsAsync__Amount_od_ids_is_greater_than_SightseeingTariffs_number_in_resource__Should_throw_ArgumentOutOfRangeException()
//        {
//            string[] ids = new string[] { "1", "2", "3", "4", "5" };
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(ids)).Throws<ArgumentOutOfRangeException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffsAsync(ids);

//            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because amount of ids id greater than overall number of SightseeingTariffs in quering resource.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffsAsync__Ids_are_null_or_empty__Should_throw_ArgumentException([Values(null, new string[] { "", "", "" })] string[] ids)
//        {
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(ids)).Throws<ArgumentException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffsAsync(ids);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because arguemnt 'ids' is null or empty.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffsAsync__At_least_one_id_dont_match__Should_return_IEnumerable_for_all_other_SightseeingTariffs()
//        {
//            var exptectedSightseeingTariffs = new SightseeingTariff[] { }.AsEnumerable();
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    exptectedSightseeingTariffs = await context.SightseeingTariffs.ToArrayAsync();
//                }
//            }
//            List<string> ids = new List<string>();
//            foreach (SightseeingTariff SightseeingTariff in exptectedSightseeingTariffs)
//            {
//                ids.Add(SightseeingTariff.Id);
//            }
//            // This one id will be not matching.
//            ids[0] = "1";
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(ids)).Returns(exptectedSightseeingTariffs.Skip(1));
//            int expectedLength = exptectedSightseeingTariffs.Count() - 1;
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.GetSightseeingTariffsAsync(ids);

//            result.Count().Should().Be(expectedLength);
//        }

//        [Test]
//        public async Task GetSightseeingTariffsAsync__Any_id_dont_match__Should_return_empty_IEnumerable()
//        {
//            string[] ids = new string[] { "-1", "-2" };
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(ids)).Returns(new List<SightseeingTariff> { }.AsEnumerable());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.GetSightseeingTariffsAsync(ids);

//            result.Count().Should().Be(0);
//        }

//        [Test]
//        public async Task GetSightseeingTariffsAsync__All_od_ids_matches__Should_return_IEnumerable_for_all_SightseeingTariffs()
//        {
//            var exptectedSightseeingTariffs = new SightseeingTariff[] { }.AsEnumerable();
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    exptectedSightseeingTariffs = await context.SightseeingTariffs.ToArrayAsync();
//                }
//            }
//            List<string> ids = new List<string>();
//            foreach (SightseeingTariff SightseeingTariff in exptectedSightseeingTariffs)
//            {
//                ids.Add(SightseeingTariff.Id);
//            }
//            _repoMock.Setup(r => r.GetByIds<SightseeingTariff>(ids)).Returns(exptectedSightseeingTariffs);
//            int expectedLength = exptectedSightseeingTariffs.Count();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.GetSightseeingTariffsAsync(ids);

//            result.Count().Should().Be(expectedLength);
//        }
//        #endregion


//        #region GetSightseeingTariffsByAsync(Expression<Func<SightseeingTariff, bool>> predicate)
//        // predicate jest nullem -> arg null exc
//        // zasob jest nullem -> null ref exc
//        // zasob jest pusty -> pusty ienumer
//        // zaden element nie spelnia predykatu -> pusty ienumer
//        // wiecej niz jeden element spelnia predykat -> ienumer z dl. min. 2
//        // tylko jeden element spelnia predykat -> ienum z dl. = 1

//        [Test]
//        public async Task GetSightseeingTariffsByAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            var dbOptions = Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext));
//            var dbContextMock = new Mock<ApplicationDbContext>(dbOptions);
//            dbContextMock.Setup(c => c.SightseeingTariffs).Returns(null as DbSet<SightseeingTariff>);
//            var service = new SightseeingTariffDbService(_logger, dbContextMock.Object, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffsByAsync__The_predicate_is_null__Should_throw_ArgumentNullException()
//        {
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.GetSightseeingTariffsByAsync(null as Expression<Func<SightseeingTariff, bool>>);

//            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'predicate' is null.");
//        }

//        [Test]
//        public async Task GetSightseeingTariffsByAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
//        {
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.RemoveRange(await context.SightseeingTariffs.ToArrayAsync());
//                    await context.SaveChangesAsync();
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.GetSightseeingTariffsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

//                    result.Count().Should().Be(0);
//                }
//            }
//        }

//        [Test]
//        public async Task GetSightseeingTariffsByAsync__Any_SightseeingTariff_satisfy_predicate__Should_return_empty_IEnumerable()
//        {
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.GetSightseeingTariffsByAsync(x => x.CreatedAt == DateTime.Now.AddYears(-1000));

//                    result.Count().Should().Be(0);
//                }
//            }
//        }

//        [Test]
//        public async Task GetSightseeingTariffsByAsync__Only_one_SightseeingTariff_satisfy_predicate__Should_return_IEnumerable_with_length_equals_1()
//        {
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    var expectedSightseeingTariff = await context.SightseeingTariffs.FirstAsync();
//                    var expectedSightseeingTariffCreatedAtDate = expectedSightseeingTariff.CreatedAt;
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.GetSightseeingTariffsByAsync(x => x.CreatedAt == expectedSightseeingTariffCreatedAtDate);

//                    result.Count().Should().Be(1);
//                }
//            }
//        }

//        [Test]
//        public async Task GetSightseeingTariffsByAsync__More_that_one_SightseeingTariff_satisfy_predicate__Should_return_IEnumerable_for_matches_SightseeingTariffs()
//        {
//            SightseeingTariff[] SightseeingTariffs = new SightseeingTariff[]
//            {
//                new SightseeingTariff{ Id = "1", Name = "SampleSightseeingTariffName" },
//                new SightseeingTariff{ Id = "2", Name = "SampleSightseeingTariffName" },
//                new SightseeingTariff{ Id = "3", Name = "SampleSightseeingTariffName" },
//                new SightseeingTariff{ Id = "4", Name = "SampleSightseeingTariffName22222222" }

//            };
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.RemoveRange(await context.SightseeingTariffs.ToArrayAsync());
//                    await context.SightseeingTariffs.AddRangeAsync(SightseeingTariffs);
//                    await context.SaveChangesAsync();

//                }
//                using (var context = await factory.CreateContextAsync())
//                {
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.GetSightseeingTariffsByAsync(x => x.Name.Equals("SampleSightseeingTariffName"));

//                    result.Count().Should().Be(3);
//                }
//            }
//        }

//        #endregion


//        #region AddSightseeingTariff(SightseeingTariff SightseeingTariff)
//        // zasob jest nullem -> null ref exc
//        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
//        // arg jest nullem -> arg exc
//        // validacja biletu sie nie udala (bledne dane) -> arg exc z powodem blednej validacji
//        // dodawanie sie udalo -> zwraca dodany bilet
//        // dodawanie sie udalo -> zasob jest wiekszy o jeden
//        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

//        [Test]
//        public async Task AddSightseeingTariff__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = "1", Name = "SampleSightseeingTariffName" };

//            _repoMock.Setup(r => r.Add<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).Throws<NullReferenceException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.AddSightseeingTariffAsync(sightseeingTariff);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task AddSightseeingTariff__In_resource_exists_the_same_SightseeingTariff_as_this_one_to_be_added__Should_throw_InvalidOperationException()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = "1", Name = "SampleSightseeingTariffName" };
//            _repoMock.Setup(r => r.Add<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).Throws<InvalidOperationException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.AddSightseeingTariffAsync(sightseeingTariff);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same SightseeingTariff as this one to be added.");
//        }

//        [Test]
//        public async Task AddSightseeingTariff__Argument_is_null__Should_throw_ArgumentNullException()
//        {
//            _repoMock.Setup(r => r.Add<SightseeingTariff>(It.IsAny<SightseeingTariff>())).Throws<ArgumentNullException>();
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.AddSightseeingTariffAsync(null as SightseeingTariff);

//            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'SightseeingTariff' is null");
//        }

//        //[Test]
//        //public async Task AddSightseeingTariff__SightseeingTariff_validation_failed__Should_throw_ArgumentException_with_specified_error_message()
//        ////[Values("", null, "fc67f907-3cec-4dfc-8001-2206402d88f2")] string SightseeingTariffId,
//        ////[Values("1000-1-1", "2100-1-1", "2019-1-1")] string purchaseDate,
//        ////[Values("1000-1-1", "2100-1-1", "2019-12-12")] string validFor)
//        //{
//        //    SightseeingTariff SightseeingTariff = new SightseeingTariff { Id = "1", PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000), SightseeingTariffUniqueId = "" };

//        //    var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//        //    Func<Task> result = async () => await service.AddSightseeingTariffAsync(SightseeingTariff);

//        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'SightseeingTariff' is invalid.");
//        //}

//        [Test]
//        public async Task AddSightseeingTariff__Add_successful__Should_return_added_SightseeingTariff()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = "1", Name = "SampleSightseeingTariffName" };
//            _repoMock.Setup(r => r.Add<SightseeingTariff>(sightseeingTariff)).Returns(sightseeingTariff);
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            var result = await service.AddSightseeingTariffAsync(sightseeingTariff);

//            result.Should().BeEquivalentTo(sightseeingTariff);
//        }

//        [Test]
//        public async Task AddSightseeingTariff__Add_successful__Resource_length_should_be_greater_by_1()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = Guid.NewGuid().ToString(), Name = "SampleSightseeingTariffName" };
//            int expectedLength;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedLength = context.SightseeingTariffs.Count() + 1;
//                    _repoMock.Setup(r => r.Add<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).Callback(() => context.SightseeingTariffs.Add(sightseeingTariff)).Returns(sightseeingTariff);
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    await service.AddSightseeingTariffAsync(sightseeingTariff);

//                    context.SightseeingTariffs.Count().Should().Be(expectedLength);
//                }
//            }
//        }

//        [Test]
//        public async Task AddSightseeingTariff__Add_successful__Resource_contains_added_SightseeingTariff()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = Guid.NewGuid().ToString(), Name = "SampleSightseeingTariffName" };

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    _repoMock.Setup(r => r.Add<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).Callback(() => context.SightseeingTariffs.Add(sightseeingTariff)).Returns(sightseeingTariff);
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    await service.AddSightseeingTariffAsync(sightseeingTariff);

//                    context.SightseeingTariffs.Contains(sightseeingTariff).Should().BeTrue();
//                }
//            }
//        }

//        #endregion


//        #region UpdateSightseeingTariffAsync(SightseeingTariff SightseeingTariff)
//        // zasob jest nullem -> null ref exc
//        // zasob jest pusty -> invalid oper exc
//        // nie znaleziono podanego biletu -> invalid oper exc
//        // arg jest nullem -> arg null exc
//        // arg ma id ktore jest nullem albo pusty -> arg exc   
//        // validacja sie nie udala (bledne dane) -> arg exc
//        // update udal sie -> tyle samo biletow co przed operacja 
//        // update sie udal -> w zasobie istnieje zmodyfikowany bilet
//        // update sie udal -> w zasobie nie istnieje poprzednia wersja biletu
//        // update sie udal -> zwraca zmodyfikowany bilet

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = "1", Name = "SampleSightseeingTariffName" };
//            _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new NullReferenceException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.UpdateSightseeingTariffAsync(sightseeingTariff);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }


//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = "1", Name = "SampleSightseeingTariffName" };
//            _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.UpdateSightseeingTariffAsync(sightseeingTariff);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
//        }

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Matching_SightseeingTariff_not_found__Should_throw_InvalidOperationException()
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = "1", Name = "SampleSightseeingTariffName" };
//            _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new InvalidOperationException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.UpdateSightseeingTariffAsync(sightseeingTariff);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching SightseeingTariff not found.");
//        }

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Argument_is_null__Should_throw_ArgumentNullException()
//        {
//            _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(null)).ThrowsAsync(new ArgumentNullException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.UpdateSightseeingTariffAsync(null as SightseeingTariff);

//            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'SightseeingTariff' cannot be null.");
//        }

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Arguments_Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
//        {
//            SightseeingTariff sightseeingTariff = new SightseeingTariff { Id = id, Name = "SampleSightseeingTariffName" };
//            _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(It.IsNotNull<SightseeingTariff>())).ThrowsAsync(new ArgumentException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.UpdateSightseeingTariffAsync(sightseeingTariff);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'SightseeingTariff' has property 'Id' set to null or empty string.");
//        }

//        //[Test]
//        //public async Task UpdateSightseeingTariffAsync__Validation_failed__Should_throw_ArgumentException()
//        //{
//        //    SightseeingTariff SightseeingTariff = new SightseeingTariff { Id = "1", SightseeingTariffUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000) };
//        //    _SightseeingTariffValidatorMock.Setup(v => v.Validate(SightseeingTariff));
//        //    var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//        //    Func<Task> result = async () => await service.UpdateSightseeingTariffAsync(SightseeingTariff);

//        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'SightseeingTariff' has property 'Id' set to null or empty string.");
//        //}

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Update_successful__Should_return_updated_SightseeingTariff()
//        {
//            SightseeingTariff sightseeingTariffBeforUpdate;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    sightseeingTariffBeforUpdate = await context.SightseeingTariffs.FirstAsync();
//                    SightseeingTariff sightseeingTariff = sightseeingTariffBeforUpdate;
//                    sightseeingTariff.Name = "xyz";
//                    _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(sightseeingTariff)).Callback(() => context.SightseeingTariffs.Update(sightseeingTariff)).ReturnsAsync(sightseeingTariff);
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.UpdateSightseeingTariffAsync(sightseeingTariff);

//                    result.Should().BeEquivalentTo(sightseeingTariffBeforUpdate);
//                }
//            }
//        }

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Update_successful__Resource_should_contains_updated_SightseeingTariff()
//        {
//            SightseeingTariff sightseeingTariff;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    sightseeingTariff = await context.SightseeingTariffs.FirstAsync();
//                    sightseeingTariff.Name = "xyz";
//                    _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(sightseeingTariff)).Callback(() => context.SightseeingTariffs.Update(sightseeingTariff)).ReturnsAsync(sightseeingTariff);
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.UpdateSightseeingTariffAsync(sightseeingTariff);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.Contains(sightseeingTariff).Should().BeTrue();
//                }
//            }
//        }

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_SightseeingTariff()
//        {
//            SightseeingTariff sightseeingTariffBeforUpdate;
//            SightseeingTariff sightseeingTariff;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    sightseeingTariff = await context.SightseeingTariffs.FirstAsync();
//                    sightseeingTariffBeforUpdate = sightseeingTariff.Clone() as SightseeingTariff;
//                    sightseeingTariff.Name = "xyz";
//                    _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(sightseeingTariff)).Callback(() => context.SightseeingTariffs.Update(sightseeingTariff)).ReturnsAsync(sightseeingTariff);
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    var result = await service.UpdateSightseeingTariffAsync(sightseeingTariff);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.Single(x => x == sightseeingTariff).Should().NotBeSameAs(sightseeingTariffBeforUpdate);
//                }
//            }
//        }

//        [Test]
//        public async Task UpdateSightseeingTariffAsync__Update_successful__Resource_length_should_be_unchanged()
//        {
//            SightseeingTariff sightseeingTariffBeforUpdate;
//            SightseeingTariff sightseeingTariff;
//            int expectedLength;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    sightseeingTariffBeforUpdate = await context.SightseeingTariffs.FirstAsync();
//                    sightseeingTariff = sightseeingTariffBeforUpdate;
//                    sightseeingTariff.Name = "xyz";
//                    _repoMock.Setup(r => r.UpdateAsync<SightseeingTariff>(sightseeingTariff)).Callback(() => context.SightseeingTariffs.Update(sightseeingTariff)).ReturnsAsync(sightseeingTariff);
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
//                    expectedLength = await context.SightseeingTariffs.CountAsync();

//                    await service.UpdateSightseeingTariffAsync(sightseeingTariff);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.Count().Should().Be(expectedLength);
//                }
//            }
//        }
//        #endregion


//        #region DeleteSightseeingTariffAsync(string id)
//        // zasob jest nullem -> null ref exc
//        // zasob jest pusty -> invalid oper exc
//        // nie ma takiego biletu jak podany -> invalid oper exc
//        // id jest nullem albo pusty -> arg exc
//        // usuwanie sie udalo -> nie w zasobie usunietego biletu
//        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

//        [Test]
//        public async Task DeleteSightseeingTariffAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            string id = "1";
//            _repoMock.Setup(r => r.DeleteAsync<SightseeingTariff>(id)).ThrowsAsync(new NullReferenceException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.DeleteSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }


//        [Test]
//        public async Task DeleteSightseeingTariffAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
//        {
//            string id = "1";
//            _repoMock.Setup(r => r.DeleteAsync<SightseeingTariff>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.DeleteSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
//        }

//        [Test]
//        public async Task DeleteSightseeingTariffAsync__Searching_SightseeingTariff_not_found__Should_throw_InvalidOperationException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.DeleteAsync<SightseeingTariff>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.DeleteSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching SightseeingTariff not found");
//        }

//        [Test]
//        public async Task DeleteSightseeingTariffAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
//        {
//            _repoMock.Setup(r => r.DeleteAsync<SightseeingTariff>(id)).ThrowsAsync(new ArgumentException());
//            var service = new SightseeingTariffDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

//            Func<Task> result = async () => await service.DeleteSightseeingTariffAsync(id);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because searching SightseeingTariff not found");
//        }

//        [Test]
//        public async Task DeleteSightseeingTariffAsync__Delete_successful__Resources_length_should_be_less_by_1()
//        {
//            string id;
//            int expectedLength;
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedLength = context.SightseeingTariffs.Count() - 1;
//                    SightseeingTariff sightseeingTariffToBeDeleted = context.SightseeingTariffs.First();
//                    id = sightseeingTariffToBeDeleted.Id;
//                    _repoMock.Setup(r => r.DeleteAsync<SightseeingTariff>(id)).Callback(() => context.SightseeingTariffs.Remove(sightseeingTariffToBeDeleted));
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
//                    await service.DeleteSightseeingTariffAsync(id);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.Count().Should().Be(expectedLength);
//                }
//            }
//        }

//        [Test]
//        public async Task DeleteSightseeingTariffAsync__Delete_successful__Resources_should_not_contain_deleted_SightseeingTariff()
//        {
//            string id;
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    SightseeingTariff sightseeingTariffToBeDeleted = context.SightseeingTariffs.First();
//                    id = sightseeingTariffToBeDeleted.Id;
//                    _repoMock.Setup(r => r.DeleteAsync<SightseeingTariff>(id)).Callback(() => context.SightseeingTariffs.Remove(sightseeingTariffToBeDeleted));
//                    var service = new SightseeingTariffDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

//                    await service.DeleteSightseeingTariffAsync(id);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.SightseeingTariffs.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
//                }
//            }
//        }

//        #endregion

//    }
//}
