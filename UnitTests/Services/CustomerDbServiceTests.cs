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
    public class CustomerDbServiceTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private ILogger<CustomerDbService> _logger;
        private readonly Customer _validTariff = new Customer
        {
            Id = "1",
            DateOfBirth = DateTime.Now.AddYears(-20),
            EmailAddres = "sample@mail.com",
            HasFamilyCard = true,
            IsChild = false,
            IsDisabled = false,
            UpdatedAt = DateTime.Now.AddDays(-2)
        };

        [OneTimeSetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext)));
            _logger = Mock.Of<ILogger<CustomerDbService>>();
        }


        #region GetAsync(string id)
        // nie istnieje tabela -> internal ex
        // tabela jest nullem -> internal ex
        // id jest nullem/pusty -> arg exc
        // 0 znaleziono -> invalid oper exc
        // zasob jest pusty -> invalid oper exc
        // znalazlo -> zwraca tylko jeden element
        // znalazlo  -> zwraca i sightseeing tariff zawiera ticket tariffs

        [Test]
        public async Task GetAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers = null as DbSet<Customer>;
                    var service = new CustomerDbService(context, _logger);

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
                    // Drop Customers table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Customers]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Customer. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new CustomerDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetAsync(id);

            await action.Should().ThrowExactlyAsync<ArgumentException>("Because id cannot be null or empty string.");
        }

        [Test]
        public async Task GetAsync__Found_zero_matching_Customer__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because customer not found.");
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
                    context.Customers.RemoveRange(await context.Customers.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAsync("a");

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of Customer.");
                }
            }
        }

        [Test]
        public async Task GetAsync__Customer_found__Should_return_this_customer()
        {
            Customer expectedCustomer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedCustomer = await context.Customers.Include(x => x.Tickets).FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.GetAsync(expectedCustomer.Id);

                    result.Should().BeEquivalentTo(expectedCustomer);
                }
            }
        }

        [Test]
        public async Task GetAsync__Customer_found__Should_return_this_customer_with_not_null_tickets_list()
        {
            Customer expectedCustomer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedCustomer = await context.Customers.Include(x => x.Tickets).FirstOrDefaultAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.GetAsync(expectedCustomer.Id);

                    result.Tickets.Should().NotBeNull();
                }
            }
        }

        #endregion


        #region GetAllAsync()
        // tabela nie istnieje -> internal exc
        // tabela jest nullem - > internal exc
        // zasob jest pusty -> pusty ienumer
        // znalazlo -> ienum<Customer> dla wszystkich z zasobu
        // znalazlo -> wszystkie zwrocome ele zawieraja ticket tariff

        [Test]
        public async Task GetAllAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers = null as DbSet<Customer>;
                    var service = new CustomerDbService(context, _logger);

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
                    // Drop Customers table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Customers]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.GetAllAsync();

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Customer. " +
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
                    context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetAllAsync__All_Customers_found__Should_return_IEnumerable_for_all_sightseeing_groups()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Customers.ToArray().Length;
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    result.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task GetAllAsync__Customers_found__Should_return_customers_with_not_null_tickets_list()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.Customers.ToArray().Length;
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.GetAllAsync();

                    foreach (var customer in result)
                    {
                        customer.Tickets.Should().NotBeNull();
                    }
                }
            }
        }

        #endregion


        #region Add(Customer Customer)
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
                    context.Customers = null as DbSet<Customer>;
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validTariff);

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
                    // Drop Customers table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Customers]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.AddAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Customer. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Argument_is_null__Should_throw_ArgumentNullException()
        {
            var service = new CustomerDbService(_dbContextMock.Object, _logger);

            Func<Task> result = async () => await service.AddAsync(null as Customer);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Customer' is null.");
        }


        [Test]
        public async Task AddAsync__In_resource_exists_the_same_Customer_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers.Add(_validTariff);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    // Testing method
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> result = async () => await service.AddAsync(_validTariff);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same Customer as this one to be added.");
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Should_return_added_customer()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.AddAsync(_validTariff);

                    result.Should().BeEquivalentTo(_validTariff);
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
                    int expectedLength = context.Customers.Count() + 1;
                    var service = new CustomerDbService(context, _logger);

                    await service.AddAsync(_validTariff);

                    context.Customers.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddAsync__Add_successful__Resource_contains_added_customer()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    await service.AddAsync(_validTariff);

                    context.Customers.Contains(_validTariff).Should().BeTrue();
                }
            }
        }

        #endregion


        #region UpdateAsync(Customer tariff)
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
                    context.Customers = null as DbSet<Customer>;
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validTariff);

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
                    // Drop Customers table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Customers]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.UpdateAsync(_validTariff);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Customer. " +
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
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(null as Customer);

                    await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'Customer' cannot be null.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Argument_has_null_or_empty_id__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var invalidCustomer = new Customer { Id = id };
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(invalidCustomer);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'Customer' has null or empty id which is invalid.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            Customer customerBeforUpdate;
            Customer customer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customerBeforUpdate = await context.Customers.FirstAsync();
                    customer = customerBeforUpdate.Clone() as Customer;
                    context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    customer.EmailAddres = "othermail@mail.com";
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> result = async () => await service.UpdateAsync(customer);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Matching_Customer_not_found__Should_throw_InvalidOperationException()
        {
            Customer customer = new Customer
            {
                Id = "0",
                EmailAddres = "othermail@mail.com",
                UpdatedAt = DateTime.Now.AddHours(-3),
                DateOfBirth = DateTime.Now.AddYears(-43)
            };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    // In db does not matching Customer to belowe disount.
                    Func<Task> result = async () => await service.UpdateAsync(customer);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching customer not found.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Should_return_updated_customer()
        {
            Customer customerBeforUpdate;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customerBeforUpdate = await context.Customers.Include(x => x.Tickets).FirstAsync();
                    Customer customer = customerBeforUpdate;
                    customer.EmailAddres = "othermail@mail.com";
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.UpdateAsync(customer);

                    result.Should().BeEquivalentTo(customer);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_contains_updated_customer()
        {
            Customer customer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customer = await context.Customers.Include(x => x.Tickets).FirstAsync();
                    customer.EmailAddres = "othermail@mail.com";
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.UpdateAsync(customer);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers.Contains(customer).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_customer()
        {
            Customer customerBeforUpdate;
            Customer customer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customer = await context.Customers.Include(x => x.Tickets).FirstAsync();
                    customerBeforUpdate = customer.Clone() as Customer;
                    customer.EmailAddres = "othermail@mail.com";
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.UpdateAsync(customer);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers.Single(x => x == customer).Should().NotBeSameAs(customerBeforUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            Customer customerBeforUpdate;
            Customer customer;
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customer = await context.Customers.Include(x => x.Tickets).FirstAsync();
                    customerBeforUpdate = customer.Clone() as Customer;
                    customer.EmailAddres = "othermail@mail.com";
                    var service = new CustomerDbService(context, _logger);
                    expectedLength = await context.Customers.CountAsync();

                    await service.UpdateAsync(customer);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_first_time__Updated_element_should_have_updated_at_not_set_to_MaxValue()
        {
            Customer customerBeforUpdate;
            Customer customer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customer = await context.Customers.FirstAsync();
                    customerBeforUpdate = customer.Clone() as Customer;
                    customerBeforUpdate.UpdatedAt = DateTime.MinValue;
                    customer.EmailAddres = "Changed mail.";
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.UpdateAsync(customer);

                    ((DateTime)result.UpdatedAt).Should().NotBeSameDateAs(DateTime.MinValue);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_not_first_time__Updated_element_should_have_new_updated_at_date_after_previous_one()
        {
            Customer customerBeforUpdate;
            Customer customer;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    customer = await context.Customers.FirstAsync();
                    customerBeforUpdate = customer.Clone() as Customer;
                    customerBeforUpdate.UpdatedAt = DateTime.UtcNow.AddMinutes(-30);
                    customer.EmailAddres = "Changed mail.";
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.UpdateAsync(customer);

                    ((DateTime)result.UpdatedAt).Should().BeAfter((DateTime)customerBeforUpdate.UpdatedAt);
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
                    context.Customers = null as DbSet<Customer>;
                    var service = new CustomerDbService(context, _logger);

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
                    // Drop Customers table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Customers]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.DeleteAsync("1");

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Customer. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var service = new CustomerDbService(_dbContextMock.Object, _logger);

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
                    context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> result = async () => await service.DeleteAsync("1");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot delete any element.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Customer_to_be_deleted_not_found__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    // Only positive Id is valid so there is not Id = '-100'.
                    Func<Task> result = async () => await service.DeleteAsync("-100");

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because Customer to be deleted not found.");
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
                    expectedLength = context.Customers.Count() - 1;
                    Customer customerToBeDeleted = context.Customers.First();
                    id = customerToBeDeleted.Id;
                    var service = new CustomerDbService(context, _logger);
                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resources_should_not_contain_deleted_customer()
        {
            string id;
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    Customer customerToBeDeleted = context.Customers.Include(x => x.Tickets).First();
                    id = customerToBeDeleted.Id;
                    var service = new CustomerDbService(context, _logger);

                    await service.DeleteAsync(id);
                }

                using (var context = await factory.CreateContextAsync())
                {
                    context.Customers.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
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
                    context.Customers = null as DbSet<Customer>;
                    var service = new CustomerDbService(context, _logger);

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
                    // Drop Customers table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [Customers]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    Func<Task> action = async () => await service.GetWithPaginationAsync(1, 1);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of Customer. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_number_is_less_than_1__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new CustomerDbService(_dbContextMock.Object, _logger);

            Func<Task> action = async () => await service.GetWithPaginationAsync(0, 10);

            await action.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because page number cannot be less than 1.");
        }

        [Test]
        public async Task GetWithPaginationAsync__Page_size_is_negative__Should_throw_ArgumentOutOfRangeException()
        {
            var service = new CustomerDbService(_dbContextMock.Object, _logger);

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
                    context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        await context.Customers.AddAsync(new Customer { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

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
                        await context.Customers.AddAsync(new Customer { Id = i.ToString() });
                    }
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

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
                    context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);

                    var result = await service.GetWithPaginationAsync(4, 5);

                    result.Count().Should().Be(0);
                }
            }
        }

        [Test]
        public async Task GetWithPaginationAsync__Found_any_customer__Should_return_these_elements_with_not_null_tickets_list()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var service = new CustomerDbService(context, _logger);
                    int elementsCount = await context.Customers.Include(x => x.Tickets).CountAsync();

                    var result = await service.GetWithPaginationAsync(1, elementsCount);

                    foreach (var customer in result)
                    {
                        customer.Tickets.Should().NotBeNull();
                    }
                }
            }
        }

        #endregion
    }
}
