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

namespace UnitTests
{
    [TestFixture]
    public class DiscountDbServiceTests
    {
        private ApplicationDbContext _dbContext;
        private Mock<IDbRepository> _repoMock;
        private ILogger<DiscountDbService> _logger;
        private Mock<ICustomValidator<Discount>> _validatorMock;


        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContext = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext))).Object;
            _repoMock = new Mock<IDbRepository>();
            _logger = Mock.Of<ILogger<DiscountDbService>>();
            _validatorMock = new Mock<ICustomValidator<Discount>>();
            _validatorMock.Setup(v => v.Validate(It.IsAny<Discount>())).Returns(new ValidationResult());
        }


        #region GetDiscountAsync(string id)
        // id jest nullem/pusty -> arg exc
        // nie istnieje bilet z takim id -> invalid oper exc
        // zasob jest pusty -> invalid oper exc i repo rzuci wyjatek 
        // zaosb nie istnieje -> null  ref exc
        // znalazlo -> zwraca tylko jeden element

        [Test]
        public async Task GetDiscountAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            _repoMock.Setup(r => r.GetByIdAsync<Discount>(id)).ThrowsAsync(new ArgumentException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'id' is null ore empty string.");
        }

        [Test]
        public async Task GetDiscountAsync__Searching_Discount_not_found__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<Discount>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching element not found.");
        }

        [Test]
        public async Task GetDiscountAsync__The_resource_is_empty__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<Discount>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because the resource is empty.");
        }

        [Test]
        public async Task GetDiscountAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.GetByIdAsync<Discount>(id)).ThrowsAsync(new NullReferenceException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetDiscountAsync__Searching_Discount_found__Should_return_this_Discount()
        {
            Discount expectedDiscount;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedDiscount = await context.Discounts.FirstOrDefaultAsync();
                }
            }
            _repoMock.Setup(r => r.GetByIdAsync<Discount>(expectedDiscount.Id)).ReturnsAsync(expectedDiscount);
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetDiscountAsync(expectedDiscount.Id);

            result.Should().BeEquivalentTo(expectedDiscount);
        }

        #endregion


        #region GetAllDiscountsAsync()
        // zasob nie istnieje -> null ref exc
        // zasob jest pusty -> pusty ienumer i repo rzuci wyjatek 
        // znalazlo -> ienum<Discount> dla wszystkich z zasobu


        [Test]
        public async Task GetAllDiscountsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            _repoMock.Setup(r => r.GetAllAsync<Discount>()).ThrowsAsync(new NullReferenceException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetAllDiscountsAsync();

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetAllDiscountsAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
        {
            _repoMock.Setup(r => r.GetAllAsync<Discount>()).ReturnsAsync(new Discount[] { }.AsEnumerable());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetAllDiscountsAsync();

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetAllDiscountsAsync__All_Discounts_found__Should_return_IEnumerable_for_all_Discounts()
        {
            var expectedDiscounts = new Discount[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedDiscounts = await context.Discounts.ToArrayAsync();
                }
            }
            _repoMock.Setup(r => r.GetAllAsync<Discount>()).ReturnsAsync(expectedDiscounts);
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetAllDiscountsAsync();

            result.Count().Should().Be(expectedDiscounts.Count());
        }

        #endregion


        #region GetDiscountsAsync(IEnumerable<string> ids)
        // zaosb nie istnieje -> null  ref exc
        // zasob jest pusty -> arg out of range
        // podano wiecej ids niz jest elementow w bazie -> arg out of range
        // ids sa nullem albo pusta tablica -> arg exc
        // jakies id nie puasuje -> ienum z mniejsza iloscia biletow
        // zadne id nie pasuje -> pusty ienumer
        // wszystkie id pasuja -> ienumer dla wszysztkich

        [Test]
        public async Task GetDiscountsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string[] ids = { "1", "2" };
            _repoMock.Setup(r => r.GetByIds<Discount>(It.IsNotNull<IEnumerable<string>>())).Throws<NullReferenceException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountsAsync(ids);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetDiscountsAsync__The_resource_which_is_quering_is_empty__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = { "1", "2" };
            _repoMock.Setup(r => r.GetByIds<Discount>(It.IsNotNull<IEnumerable<string>>())).Throws<ArgumentOutOfRangeException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = () => service.GetDiscountsAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task GetDiscountsAsync__Amount_od_ids_is_greater_than_Discounts_number_in_resource__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = new string[] { "1", "2", "3", "4", "5" };
            _repoMock.Setup(r => r.GetByIds<Discount>(ids)).Throws<ArgumentOutOfRangeException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountsAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because amount of ids id greater than overall number of Discounts in quering resource.");
        }

        [Test]
        public async Task GetDiscountsAsync__Ids_are_null_or_empty__Should_throw_ArgumentException([Values(null, new string[] { "", "", "" })] string[] ids)
        {
            _repoMock.Setup(r => r.GetByIds<Discount>(ids)).Throws<ArgumentException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountsAsync(ids);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because arguemnt 'ids' is null or empty.");
        }

        [Test]
        public async Task GetDiscountsAsync__At_least_one_id_dont_match__Should_return_IEnumerable_for_all_other_Discounts()
        {
            var exptectedDiscounts = new Discount[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    exptectedDiscounts = await context.Discounts.ToArrayAsync();
                }
            }
            List<string> ids = new List<string>();
            foreach (Discount Discount in exptectedDiscounts)
            {
                ids.Add(Discount.Id);
            }
            // This one id will be not matching.
            ids[0] = "1";
            _repoMock.Setup(r => r.GetByIds<Discount>(ids)).Returns(exptectedDiscounts.Skip(1));
            int expectedLength = exptectedDiscounts.Count() - 1;
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetDiscountsAsync(ids);

            result.Count().Should().Be(expectedLength);
        }

        [Test]
        public async Task GetDiscountsAsync__Any_id_dont_match__Should_return_empty_IEnumerable()
        {
            string[] ids = new string[] { "-1", "-2" };
            _repoMock.Setup(r => r.GetByIds<Discount>(ids)).Returns(new List<Discount> { }.AsEnumerable());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetDiscountsAsync(ids);

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetDiscountsAsync__All_od_ids_matches__Should_return_IEnumerable_for_all_Discounts()
        {
            var exptectedDiscounts = new Discount[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    exptectedDiscounts = await context.Discounts.ToArrayAsync();
                }
            }
            List<string> ids = new List<string>();
            foreach (Discount Discount in exptectedDiscounts)
            {
                ids.Add(Discount.Id);
            }
            _repoMock.Setup(r => r.GetByIds<Discount>(ids)).Returns(exptectedDiscounts);
            int expectedLength = exptectedDiscounts.Count();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.GetDiscountsAsync(ids);

            result.Count().Should().Be(expectedLength);
        }
        #endregion


        #region GetDiscountsByAsync(Expression<Func<Discount, bool>> predicate)
        // predicate jest nullem -> arg null exc
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> pusty ienumer
        // zaden element nie spelnia predykatu -> pusty ienumer
        // wiecej niz jeden element spelnia predykat -> ienumer z dl. min. 2
        // tylko jeden element spelnia predykat -> ienum z dl. = 1

        [Test]
        public async Task GetDiscountsByAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbOptions = Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext));
            var dbContextMock = new Mock<ApplicationDbContext>(dbOptions);
            dbContextMock.Setup(c => c.Discounts).Returns(null as DbSet<Discount>);
            var service = new DiscountDbService(_logger, dbContextMock.Object, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task GetDiscountsByAsync__The_predicate_is_null__Should_throw_ArgumentNullException()
        {
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.GetDiscountsByAsync(null as Expression<Func<Discount, bool>>);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'predicate' is null.");
        }

        [Test]
        public async Task GetDiscountsByAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.RemoveRange(await context.Discounts.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetDiscountsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetDiscountsByAsync__Any_Discount_satisfy_predicate__Should_return_empty_IEnumerable()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetDiscountsByAsync(x => x.CreatedAt == DateTime.Now.AddYears(-1000));

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetDiscountsByAsync__Only_one_Discount_satisfy_predicate__Should_return_IEnumerable_with_length_equals_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var expectedDiscount = await context.Discounts.FirstAsync();
                    var expectedDiscountCreatedAtDate = expectedDiscount.CreatedAt;
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetDiscountsByAsync(x => x.CreatedAt == expectedDiscountCreatedAtDate);

                    result.Count().Should().Be(1);
                }
            }
        }

        [Test]
        public async Task GetDiscountsByAsync__More_that_one_Discount_satisfy_predicate__Should_return_IEnumerable_for_matches_Discounts()
        {
            Discount[] Discounts = new Discount[]
            {
                new Discount{ Id = "1",  Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily },
                new Discount{ Id = "2",  Description = "SampleDescription", DiscountValueInPercentage = 11, Type = Discount.DiscountType.ForFamily },
                new Discount{ Id = "3",  Description = "SampleDescription", DiscountValueInPercentage = 12, Type = Discount.DiscountType.ForFamily },
                new Discount{ Id = "4",  Description = "SampleDescriptionXYZ", DiscountValueInPercentage = 13, Type = Discount.DiscountType.ForFamily },

            };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.RemoveRange(await context.Discounts.ToArrayAsync());
                    await context.Discounts.AddRangeAsync(Discounts);
                    await context.SaveChangesAsync();

                }
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.GetDiscountsByAsync(x => x.Description.Equals("SampleDescription"));

                    result.Count().Should().Be(3);
                }
            }
        }

        #endregion


        #region AddDiscount(Discount Discount)
        // zasob jest nullem -> null ref exc
        // arg nie przechodzi validacji -> arg exc
        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
        // arg jest nullem -> arg exc
        // validacja biletu sie nie udala (bledne dane) -> arg exc z powodem blednej validacji
        // dodawanie sie udalo -> zwraca dodany bilet
        // dodawanie sie udalo -> zasob jest wiekszy o jeden
        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

        [Test]
        public async Task AddDiscount__Argument_is_null__Should_throw_ArgumentNullException()
        {
            _repoMock.Setup(r => r.Add<Discount>(It.IsAny<Discount>())).Throws<ArgumentNullException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddDiscountAsync(null as Discount);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Discount' is null.");
        }

        //[Test]
        //public async Task AddDiscount__Argument_is_invalid__Should_throw_ArgumentException()
        //{
        //    Discount discount = new Discount { Id = "1", Author = "Kowalski", Title = "Sample", Text = null };
        //    var validatorMock = new Mock<IDiscountValidator>();
        //    validatorMock.Setup(v => v.Validate(discount)).Returns<ValidationResult>(x => x = new ValidationResult());
        //    var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, validatorMock.Object);

        //    Func<Task> result = async () => await service.AddDiscountAsync(discount);

        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Discount' is invalid.").WithMessage("Text");
        //}

        [Test]
        public async Task AddDiscount__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily };
            _repoMock.Setup(r => r.Add<Discount>(It.IsNotNull<Discount>())).Throws<NullReferenceException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }

        [Test]
        public async Task AddDiscount__In_resource_exists_the_same_Discount_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily };
            _repoMock.Setup(r => r.Add<Discount>(It.IsNotNull<Discount>())).Throws<InvalidOperationException>();
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.AddDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same Discount as this one to be added.");
        }

        [Test]
        public async Task AddDiscount__Add_successful__Should_return_added_Discount()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily }; _repoMock.Setup(r => r.Add<Discount>(discount)).Returns(discount);
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            var result = await service.AddDiscountAsync(discount);

            result.Should().BeEquivalentTo(discount);
        }

        [Test]
        public async Task AddDiscount__Add_successful__Resource_length_should_be_greater_by_1()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily };
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Discounts.Count() + 1;
                    _repoMock.Setup(r => r.Add<Discount>(It.IsNotNull<Discount>())).Callback(() => context.Discounts.Add(discount)).Returns(discount);
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.AddDiscountAsync(discount);

                    context.Discounts.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddDiscount__Add_successful__Resource_contains_added_Discount()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    _repoMock.Setup(r => r.Add<Discount>(It.IsNotNull<Discount>())).Callback(() => context.Discounts.Add(discount)).Returns(discount);
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.AddDiscountAsync(discount);

                    context.Discounts.Contains(discount).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateDiscountAsync(Discount Discount)
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
        public async Task UpdateDiscountAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily }; _repoMock.Setup(r => r.UpdateAsync<Discount>(It.IsNotNull<Discount>())).ThrowsAsync(new NullReferenceException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }


        [Test]
        public async Task UpdateDiscountAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
        {
            Discount discount = new Discount { Id = "1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily }; _repoMock.Setup(r => r.UpdateAsync<Discount>(It.IsNotNull<Discount>())).ThrowsAsync(new InvalidOperationException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task UpdateDiscountAsync__Matching_Discount_not_found__Should_throw_InvalidOperationException()
        {
            Discount discount = new Discount { Id = "-1", Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily }; _repoMock.Setup(r => r.UpdateAsync<Discount>(It.IsNotNull<Discount>())).ThrowsAsync(new InvalidOperationException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching Discount not found.");
        }

        [Test]
        public async Task UpdateDiscountAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            _repoMock.Setup(r => r.UpdateAsync<Discount>(null)).ThrowsAsync(new ArgumentNullException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateDiscountAsync(null as Discount);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Discount' cannot be null.");
        }

        [Test]
        public async Task UpdateDiscountAsync__Arguments_Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            Discount discount = new Discount { Id = id, Description = "SampleDescription", DiscountValueInPercentage = 10, Type = Discount.DiscountType.ForFamily }; _repoMock.Setup(r => r.UpdateAsync<Discount>(It.IsNotNull<Discount>())).ThrowsAsync(new ArgumentException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.UpdateDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Discount' has property 'Id' set to null or empty string.");
        }

        //[Test]
        //public async Task UpdateDiscountAsync__Validation_failed__Should_throw_ArgumentException()
        //{
        //    Discount discount = new Discount { Id = "1", DiscountUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000) };
        //    _DiscountValidatorMock.Setup(v => v.Validate(discount));
        //    var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validator);

        //    Func<Task> result = async () => await service.UpdateDiscountAsync(discount);

        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Discount' has property 'Id' set to null or empty string.");
        //}

        [Test]
        public async Task UpdateDiscountAsync__Update_successful__Should_return_updated_Discount()
        {
            Discount discountBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    discountBeforUpdate = await context.Discounts.FirstAsync();
                    Discount discount = discountBeforUpdate;
                    discount.Description = "XYZ";
                    _repoMock.Setup(r => r.UpdateAsync<Discount>(discount)).Callback(() => context.Discounts.Update(discount)).ReturnsAsync(discount);
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateDiscountAsync(discount);

                    result.Should().BeEquivalentTo(discountBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateDiscountAsync__Update_successful__Resource_should_contains_updated_Discount()
        {
            Discount discount;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    discount = await context.Discounts.FirstAsync();
                    discount.Description = "XYZ";
                    _repoMock.Setup(r => r.UpdateAsync<Discount>(discount)).Callback(() => context.Discounts.Update(discount)).ReturnsAsync(discount);
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateDiscountAsync(discount);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.Contains(discount).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateDiscountAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_Discount()
        {
            Discount discountBeforUpdate;
            Discount discount;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    discount = await context.Discounts.FirstAsync();
                    discountBeforUpdate = discount.Clone() as Discount;
                    discount.Description = "XYZ";
                    _repoMock.Setup(r => r.UpdateAsync<Discount>(discount)).Callback(() => context.Discounts.Update(discount)).ReturnsAsync(discount);
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    var result = await service.UpdateDiscountAsync(discount);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.Single(x => x == discount).Should().NotBeSameAs(discountBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateDiscountAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            Discount discountBeforUpdate;
            Discount discount;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    discountBeforUpdate = await context.Discounts.FirstAsync();
                    discount = discountBeforUpdate;
                    discount.Description = "XYZ";
                    _repoMock.Setup(r => r.UpdateAsync<Discount>(discount)).Callback(() => context.Discounts.Update(discount)).ReturnsAsync(discount);
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
                    expectedLength = await context.Discounts.CountAsync();

                    await service.UpdateDiscountAsync(discount);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.Count().Should().Be(expectedLength);
                }
            }
        }
        #endregion


        #region DeleteDiscountAsync(string id)
        // zasob jest nullem -> null ref exc
        // zasob jest pusty -> invalid oper exc
        // nie ma takiego biletu jak podany -> invalid oper exc
        // id jest nullem albo pusty -> arg exc
        // usuwanie sie udalo -> nie w zasobie usunietego biletu
        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

        [Test]
        public async Task DeleteDiscountAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "1";
            _repoMock.Setup(r => r.DeleteAsync<Discount>(id)).ThrowsAsync(new NullReferenceException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
        }


        [Test]
        public async Task DeleteDiscountAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
        {
            string id = "1";
            _repoMock.Setup(r => r.DeleteAsync<Discount>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
        }

        [Test]
        public async Task DeleteDiscountAsync__Searching_Discount_not_found__Should_throw_InvalidOperationException()
        {
            string id = "-1";
            _repoMock.Setup(r => r.DeleteAsync<Discount>(id)).ThrowsAsync(new InvalidOperationException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching Discount not found");
        }

        [Test]
        public async Task DeleteDiscountAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            _repoMock.Setup(r => r.DeleteAsync<Discount>(id)).ThrowsAsync(new ArgumentException());
            var service = new DiscountDbService(_logger, _dbContext, _repoMock.Object, _validatorMock.Object);

            Func<Task> result = async () => await service.DeleteDiscountAsync(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because searching Discount not found");
        }

        [Test]
        public async Task DeleteDiscountAsync__Delete_successful__Resources_length_should_be_less_by_1()
        {
            string id;
            int expectedLength;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Discounts.Count() - 1;
                    Discount DiscountToBeDeleted = context.Discounts.First();
                    id = DiscountToBeDeleted.Id;
                    _repoMock.Setup(r => r.DeleteAsync<Discount>(id)).Callback(() => context.Discounts.Remove(DiscountToBeDeleted));
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);
                    await service.DeleteDiscountAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteDiscountAsync__Delete_successful__Resources_should_not_contain_deleted_Discount()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    Discount DiscountToBeDeleted = context.Discounts.First();
                    id = DiscountToBeDeleted.Id;
                    _repoMock.Setup(r => r.DeleteAsync<Discount>(id)).Callback(() => context.Discounts.Remove(DiscountToBeDeleted));
                    var service = new DiscountDbService(_logger, context, _repoMock.Object, _validatorMock.Object);

                    await service.DeleteDiscountAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Discounts.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
