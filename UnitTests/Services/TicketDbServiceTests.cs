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

//namespace UnitTests.Services
//{
//    [TestFixture]
//    public class TicketDbServiceTests
//    {
//        private ApplicationDbContext _dbContext;
//        private Mock<IDbRepository> _repoMock;
//        private ILogger<TicketDbService> _logger;


//        [OneTimeSetUp]
//        public void SetUp()
//        {
//            _dbContext = new Mock<ApplicationDbContext>(Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext))).Object;
//            _repoMock = new Mock<IDbRepository>();
//            _logger = Mock.Of<ILogger<TicketDbService>>();
//        }


//        #region GetTicketAsync(string id)
//        // id jest nullem/pusty -> arg exc
//        // nie istnieje bilet z takim id -> invalid oper exc
//        // zasob jest pusty -> invalid oper exc i repo rzuci wyjatek 
//        // zaosb nie istnieje -> null  ref exc
//        // znalazlo -> zwraca tylko jeden element

//        [Test]
//        public async Task GetTicketAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
//        {
//            _repoMock.Setup(r => r.GetByIdAsync<Ticket>(id)).ThrowsAsync(new ArgumentException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'id' is null ore empty string.");
//        }

//        [Test]
//        public async Task GetTicketAsync__Searching_ticket_not_found__Should_throw_InvalidOperationException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.GetByIdAsync<Ticket>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching element not found.");
//        }

//        [Test]
//        public async Task GetTicketAsync__The_resource_is_empty__Should_throw_InvalidOperationException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.GetByIdAsync<Ticket>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because the resource is empty.");
//        }

//        [Test]
//        public async Task GetTicketAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.GetByIdAsync<Ticket>(id)).ThrowsAsync(new NullReferenceException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetTicketAsync__Searching_ticket_found__Should_return_this_ticket()
//        {
//            Ticket expectedTicket;
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedTicket = await context.Tickets.FirstOrDefaultAsync();
//                }
//            }
//            _repoMock.Setup(r => r.GetByIdAsync<Ticket>(expectedTicket.Id)).ReturnsAsync(expectedTicket);
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.GetTicketAsync(expectedTicket.Id);

//            result.Should().BeEquivalentTo(expectedTicket);
//        }

//        #endregion


//        #region GetAllTicketsAsync()
//        // zasob nie istnieje -> null ref exc
//        // zasob jest pusty -> pusty ienumer i repo rzuci wyjatek 
//        // znalazlo -> ienum<ticket> dla wszystkich z zasobu


//        [Test]
//        public async Task GetAllTicketsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            _repoMock.Setup(r => r.GetAllAsync<Ticket>()).ThrowsAsync(new NullReferenceException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetAllTicketsAsync();

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetAllTicketsAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
//        {
//            _repoMock.Setup(r => r.GetAllAsync<Ticket>()).ReturnsAsync(new Ticket[] { }.AsEnumerable());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.GetAllTicketsAsync();

//            result.Count().Should().Be(0);
//        }

//        [Test]
//        public async Task GetAllTicketsAsync__All_tickets_found__Should_return_IEnumerable_for_all_tickets()
//        {
//            var expectedTickets = new Ticket[] { }.AsEnumerable();
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedTickets = await context.Tickets.ToArrayAsync();
//                }
//            }
//            _repoMock.Setup(r => r.GetAllAsync<Ticket>()).ReturnsAsync(expectedTickets);
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.GetAllTicketsAsync();

//            result.Count().Should().Be(expectedTickets.Count());
//        }

//        #endregion


//        #region GetTicketsAsync(IEnumerable<string> ids)
//        // zaosb nie istnieje -> null  ref exc
//        // zasob jest pusty -> arg out of range
//        // podano wiecej ids niz jest elementow w bazie -> arg out of range
//        // ids sa nullem albo pusta tablica -> arg exc
//        // jakies id nie puasuje -> ienum z mniejsza iloscia biletow
//        // zadne id nie pasuje -> pusty ienumer
//        // wszystkie id pasuja -> ienumer dla wszysztkich

//        [Test]
//        public async Task GetTicketsAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            string[] ids = { "1", "2" };
//            _repoMock.Setup(r => r.GetByIds<Ticket>(It.IsNotNull<IEnumerable<string>>())).Throws<NullReferenceException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketsAsync(ids);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetTicketsAsync__The_resource_which_is_quering_is_empty__Should_throw_ArgumentOutOfRangeException()
//        {
//            string[] ids = { "1", "2" };
//            _repoMock.Setup(r => r.GetByIds<Ticket>(It.IsNotNull<IEnumerable<string>>())).Throws<ArgumentOutOfRangeException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = () => service.GetTicketsAsync(ids);

//            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because resource which is quering is empty.");
//        }

//        [Test]
//        public async Task GetTicketsAsync__Amount_od_ids_is_greater_than_tickets_number_in_resource__Should_throw_ArgumentOutOfRangeException()
//        {
//            string[] ids = new string[] { "1", "2", "3", "4", "5" };
//            _repoMock.Setup(r => r.GetByIds<Ticket>(ids)).Throws<ArgumentOutOfRangeException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketsAsync(ids);

//            await result.Should().ThrowExactlyAsync<ArgumentOutOfRangeException>("Because amount of ids id greater than overall number of tickets in quering resource.");
//        }

//        [Test]
//        public async Task GetTicketsAsync__Ids_are_null_or_empty__Should_throw_ArgumentException([Values(null, new string[] { "", "", "" })] string[] ids)
//        {
//            _repoMock.Setup(r => r.GetByIds<Ticket>(ids)).Throws<ArgumentException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketsAsync(ids);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because arguemnt 'ids' is null or empty.");
//        }

//        [Test]
//        public async Task GetTicketsAsync__At_least_one_id_dont_match__Should_return_IEnumerable_for_all_other_tickets()
//        {
//            var exptectedTickets = new Ticket[] { }.AsEnumerable();
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    exptectedTickets = await context.Tickets.ToArrayAsync();
//                }
//            }
//            List<string> ids = new List<string>();
//            foreach (Ticket ticket in exptectedTickets)
//            {
//                ids.Add(ticket.Id);
//            }
//            // This one id will be not matching.
//            ids[0] = "1";
//            _repoMock.Setup(r => r.GetByIds<Ticket>(ids)).Returns(exptectedTickets.Skip(1));
//            int expectedLength = exptectedTickets.Count() - 1;
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.GetTicketsAsync(ids);

//            result.Count().Should().Be(expectedLength);
//        }

//        [Test]
//        public async Task GetTicketsAsync__Any_id_dont_match__Should_return_empty_IEnumerable()
//        {
//            string[] ids = new string[] { "-1", "-2" };
//            _repoMock.Setup(r => r.GetByIds<Ticket>(ids)).Returns(new List<Ticket> { }.AsEnumerable());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.GetTicketsAsync(ids);

//            result.Count().Should().Be(0);
//        }

//        [Test]
//        public async Task GetTicketsAsync__All_od_ids_matches__Should_return_IEnumerable_for_all_tickets()
//        {
//            var exptectedTickets = new Ticket[] { }.AsEnumerable();
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    exptectedTickets = await context.Tickets.ToArrayAsync();
//                }
//            }
//            List<string> ids = new List<string>();
//            foreach (Ticket ticket in exptectedTickets)
//            {
//                ids.Add(ticket.Id);
//            }
//            _repoMock.Setup(r => r.GetByIds<Ticket>(ids)).Returns(exptectedTickets);
//            int expectedLength = exptectedTickets.Count();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.GetTicketsAsync(ids);

//            result.Count().Should().Be(expectedLength);
//        }
//        #endregion


//        #region GetTicketsAsyncByAsync(Expression<Func<Ticket, bool>> predicate)
//        // predicate jest nullem -> arg null exc
//        // zasob jest nullem -> null ref exc
//        // zasob jest pusty -> pusty ienumer
//        // zaden element nie spelnia predykatu -> pusty ienumer
//        // wiecej niz jeden element spelnia predykat -> ienumer z dl. min. 2
//        // tylko jeden element spelnia predykat -> ienum z dl. = 1

//        [Test]
//        public async Task GetTicketsAsyncByAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            var dbOptions = Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext));
//            var dbContextMock = new Mock<ApplicationDbContext>(dbOptions);
//            dbContextMock.Setup(c => c.Tickets).Returns(null as DbSet<Ticket>);
//            var service = new TicketDbService(_logger, dbContextMock.Object, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task GetTicketsAsyncByAsync__The_predicate_is_null__Should_throw_ArgumentNullException()
//        {
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.GetTicketsByAsync(null as Expression<Func<Ticket, bool>>);

//            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'predicate' is null.");
//        }

//        [Test]
//        public async Task GetTicketsAsyncByAsync__The_resource_is_empty__Should_return_empty_IEnumerable()
//        {
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.RemoveRange(await context.Tickets.ToArrayAsync());
//                    await context.SaveChangesAsync();
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.GetTicketsByAsync(x => x.CreatedAt.Year == DateTime.Now.Year);

//                    result.Count().Should().Be(0);
//                }
//            }
//        }

//        [Test]
//        public async Task GetTicketsAsyncByAsync__Any_ticket_satisfy_predicate__Should_return_empty_IEnumerable()
//        {
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.GetTicketsByAsync(x => x.CreatedAt == DateTime.Now.AddYears(-1000));

//                    result.Count().Should().Be(0);
//                }
//            }
//        }

//        [Test]
//        public async Task GetTicketsAsyncByAsync__Only_one_ticket_satisfy_predicate__Should_return_IEnumerable_with_length_equals_1()
//        {
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    var expectedTicket = await context.Tickets.FirstAsync();
//                    var expectedTicketCreatedAtDate = expectedTicket.CreatedAt;
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.GetTicketsByAsync(x => x.CreatedAt == expectedTicketCreatedAtDate);

//                    result.Count().Should().Be(1);
//                }
//            }
//        }

//        [Test]
//        public async Task GetTicketsAsyncByAsync__More_that_one_ticket_satisfy_predicate__Should_return_IEnumerable_for_matches_tickets()
//        {
//            Ticket[] tickets = new Ticket[]
//            {
//                new Ticket{ TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-1), ValidFor = DateTime.Now.AddDays(4), },
//                new Ticket{ TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-2), ValidFor = DateTime.Now.AddDays(3), },
//                new Ticket{ TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-3), ValidFor = DateTime.Now.AddDays(2), },
//                new Ticket{ TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-4), ValidFor = DateTime.Now.AddDays(1), }
//            };
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.RemoveRange(await context.Tickets.ToArrayAsync());
//                    await context.Tickets.AddRangeAsync(tickets);
//                    await context.SaveChangesAsync();

//                }
//                using (var context = await factory.CreateContextAsync())
//                {
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.GetTicketsByAsync(x => x.PurchaseDate.Day < DateTime.Now.AddDays(-1).Day);

//                    result.Count().Should().Be(3);
//                }
//            }
//        }

//        #endregion


//        #region AddTicket(Ticket ticket)
//        // zasob jest nullem -> null ref exc
//        // istnieje w zasobie taki sam element jak ten, ktory chcemy dodac -> invalid oper exc
//        // arg jest nullem -> arg exc
//        // validacja biletu sie nie udala (bledne dane) -> arg exc z powodem blednej validacji
//        // dodawanie sie udalo -> zwraca dodany bilet
//        // dodawanie sie udalo -> zasob jest wiekszy o jeden
//        // dodawanie sie udalo -> istnieje w zasobie dodany bilet

//        [Test]
//        public async Task AddTicket__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            Ticket ticket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now, ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.Add<Ticket>(It.IsNotNull<Ticket>())).Throws<NullReferenceException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.AddTicketAsync(ticket);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }

//        [Test]
//        public async Task AddTicket__In_resource_exists_the_same_ticket_as_this_one_to_be_added__Should_throw_InvalidOperationException()
//        {
//            Ticket ticket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now, ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.Add<Ticket>(It.IsNotNull<Ticket>())).Throws<InvalidOperationException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.AddTicketAsync(ticket);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same ticket as this one to be added.");
//        }

//        [Test]
//        public async Task AddTicket__Argument_is_null__Should_throw_ArgumentNullException()
//        {
//            _repoMock.Setup(r => r.Add<Ticket>(It.IsAny<Ticket>())).Throws<ArgumentNullException>();
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.AddTicketAsync(null as Ticket);

//            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'ticket' is null");
//        }

//        //[Test]
//        //public async Task AddTicket__Ticket_validation_failed__Should_throw_ArgumentException_with_specified_error_message()
//        ////[Values("", null, "fc67f907-3cec-4dfc-8001-2206402d88f2")] string ticketId,
//        ////[Values("1000-1-1", "2100-1-1", "2019-1-1")] string purchaseDate,
//        ////[Values("1000-1-1", "2100-1-1", "2019-12-12")] string validFor)
//        //{
//        //    Ticket ticket = new Ticket { Id = "1", PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000), TicketUniqueId = "" };

//        //    var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//        //    Func<Task> result = async () => await service.AddTicketAsync(ticket);

//        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'ticket' is invalid.");
//        //}

//        [Test]
//        public async Task AddTicket__Add_successful__Should_return_added_ticket()
//        {
//            Ticket ticket = new Ticket { TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-1), ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.Add<Ticket>(ticket)).Returns(ticket);
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            var result = await service.AddTicketAsync(ticket);

//            result.Should().BeEquivalentTo(ticket);
//        }

//        [Test]
//        public async Task AddTicket__Add_successful__Resource_length_should_be_greater_by_1()
//        {
//            Ticket ticket = new Ticket { TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-1), ValidFor = DateTime.Now.AddDays(21) };
//            int expectedLength;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedLength = context.Tickets.Count() + 1;
//                    _repoMock.Setup(r => r.Add<Ticket>(It.IsNotNull<Ticket>())).Callback(() => context.Tickets.Add(ticket)).Returns(ticket);
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    await service.AddTicketAsync(ticket);

//                    context.Tickets.Count().Should().Be(expectedLength);
//                }
//            }
//        }

//        // TODO Figure out why context in IDbRepository is different from context in TicketDbService
//        [Test]
//        public async Task AddTicket__Add_successful__Resource_contains_added_ticket()
//        {
//            Ticket ticket = new Ticket { TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddDays(-1), ValidFor = DateTime.Now.AddDays(21) };

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    _repoMock.Setup(r => r.Add<Ticket>(It.IsNotNull<Ticket>())).Callback(() => context.Tickets.Add(ticket)).Returns(ticket);
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    await service.AddTicketAsync(ticket);

//                    context.Tickets.Contains(ticket).Should().BeTrue();
//                }
//            }
//        }

//        #endregion


//        #region UpdateTicketAsync(Ticket ticket)
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
//        public async Task UpdateTicketAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            Ticket ticket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now, ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.UpdateAsync<Ticket>(It.IsNotNull<Ticket>())).ThrowsAsync(new NullReferenceException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.UpdateTicketAsync(ticket);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }


//        [Test]
//        public async Task UpdateTicketAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
//        {
//            Ticket ticket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now, ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.UpdateAsync<Ticket>(It.IsNotNull<Ticket>())).ThrowsAsync(new InvalidOperationException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.UpdateTicketAsync(ticket);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
//        }

//        [Test]
//        public async Task UpdateTicketAsync__Matching_ticket_not_found__Should_throw_InvalidOperationException()
//        {
//            Ticket ticket = new Ticket { Id = "-1", TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now, ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.UpdateAsync<Ticket>(It.IsNotNull<Ticket>())).ThrowsAsync(new InvalidOperationException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.UpdateTicketAsync(ticket);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching ticket not found.");
//        }

//        [Test]
//        public async Task UpdateTicketAsync__Argument_is_null__Should_throw_ArgumentNullException()
//        {
//            _repoMock.Setup(r => r.UpdateAsync<Ticket>(null)).ThrowsAsync(new ArgumentNullException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.UpdateTicketAsync(null as Ticket);

//            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'ticket' cannot be null.");
//        }

//        [Test]
//        public async Task UpdateTicketAsync__Arguments_Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
//        {
//            Ticket ticket = new Ticket { Id = id, TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now, ValidFor = DateTime.Now.AddDays(21) };
//            _repoMock.Setup(r => r.UpdateAsync<Ticket>(It.IsNotNull<Ticket>())).ThrowsAsync(new ArgumentException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.UpdateTicketAsync(ticket);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'ticket' has property 'Id' set to null or empty string.");
//        }

//        //[Test]
//        //public async Task UpdateTicketAsync__Validation_failed__Should_throw_ArgumentException()
//        //{
//        //    Ticket ticket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), PurchaseDate = DateTime.Now.AddYears(1000), ValidFor = DateTime.Now.AddYears(-1000) };
//        //    _ticketValidatorMock.Setup(v => v.Validate(ticket));
//        //    var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//        //    Func<Task> result = async () => await service.UpdateTicketAsync(ticket);

//        //    await result.Should().ThrowExactlyAsync<ArgumentException>("Because argument 'ticket' has property 'Id' set to null or empty string.");
//        //}

//        [Test]
//        public async Task UpdateTicketAsync__Update_successful__Should_return_updated_ticket()
//        {
//            Ticket ticketBeforUpdate;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    ticketBeforUpdate = await context.Tickets.FirstAsync();
//                    Ticket ticket = ticketBeforUpdate;
//                    ticket.PurchaseDate.AddDays(-31);
//                    ticket.ValidFor.AddYears(1);
//                    ticket.TicketUniqueId = Guid.NewGuid().ToString();
//                    _repoMock.Setup(r => r.UpdateAsync<Ticket>(ticket)).Callback(() => context.Tickets.Update(ticket)).ReturnsAsync(ticket);
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.UpdateTicketAsync(ticket);

//                    result.Should().BeEquivalentTo(ticketBeforUpdate);
//                }
//            }
//        }

//        [Test]
//        public async Task UpdateTicketAsync__Update_successful__Resource_should_contains_updated_ticket()
//        {
//            Ticket ticket;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    ticket = await context.Tickets.FirstAsync();
//                    ticket.PurchaseDate = ticket.PurchaseDate.AddYears(-1);
//                    ticket.ValidFor = ticket.ValidFor.AddYears(1);
//                    _repoMock.Setup(r => r.UpdateAsync<Ticket>(ticket)).Callback(() => context.Tickets.Update(ticket)).ReturnsAsync(ticket);
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.UpdateTicketAsync(ticket);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.Contains(ticket).Should().BeTrue();
//                }
//            }
//        }

//        [Test]
//        public async Task UpdateTicketAsync__Update_successful__Resource_should_doesnt_contain_previous_version_of_ticket()
//        {
//            Ticket ticketBeforUpdate;
//            Ticket ticket;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    ticket = await context.Tickets.FirstAsync();
//                    ticketBeforUpdate = ticket.Clone() as Ticket;
//                    ticket.PurchaseDate = ticket.PurchaseDate.AddYears(-1);
//                    ticket.ValidFor = ticket.ValidFor.AddYears(1);
//                    _repoMock.Setup(r => r.UpdateAsync<Ticket>(ticket)).Callback(() => context.Tickets.Update(ticket)).ReturnsAsync(ticket);
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    var result = await service.UpdateTicketAsync(ticket);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.Single(x => x == ticket).Should().NotBeSameAs(ticketBeforUpdate);
//                }
//            }
//        }

//        [Test]
//        public async Task UpdateTicketAsync__Update_successful__Resource_length_should_be_unchanged()
//        {
//            Ticket ticketBeforUpdate;
//            Ticket ticket;
//            int expectedLength;

//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    ticketBeforUpdate = await context.Tickets.FirstAsync();
//                    ticket = ticketBeforUpdate;
//                    ticket.PurchaseDate.AddDays(-31);
//                    ticket.ValidFor.AddYears(1);
//                    ticket.TicketUniqueId = Guid.NewGuid().ToString();
//                    _repoMock.Setup(r => r.UpdateAsync<Ticket>(ticket)).Callback(() => context.Tickets.Update(ticket)).ReturnsAsync(ticket);
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);
//                    expectedLength = await context.Tickets.CountAsync();

//                    await service.UpdateTicketAsync(ticket);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.Count().Should().Be(expectedLength);
//                }
//            }
//        }
//        #endregion


//        #region DeleteTicketAsync(string id)
//        // zasob jest nullem -> null ref exc
//        // zasob jest pusty -> invalid oper exc
//        // nie ma takiego biletu jak podany -> invalid oper exc
//        // id jest nullem albo pusty -> arg exc
//        // usuwanie sie udalo -> nie w zasobie usunietego biletu
//        // usuwanie sie udalo -> rozmiar zasobu jest o jeden mniejszy

//        [Test]
//        public async Task DeleteTicketAsync__The_resource_which_is_quering_doesnt_exist__Should_throw_NullReferenceException()
//        {
//            string id = "1";
//            _repoMock.Setup(r => r.DeleteAsync<Ticket>(id)).ThrowsAsync(new NullReferenceException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.DeleteTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because resource which is quering doesnt exist.");
//        }


//        [Test]
//        public async Task DeleteTicketAsync__Quering_resource_is_empty__Should_throw_InvalidOperationException()
//        {
//            string id = "1";
//            _repoMock.Setup(r => r.DeleteAsync<Ticket>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.DeleteTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is quering is empty.");
//        }

//        [Test]
//        public async Task DeleteTicketAsync__Searching_ticket_not_found__Should_throw_InvalidOperationException()
//        {
//            string id = "-1";
//            _repoMock.Setup(r => r.DeleteAsync<Ticket>(id)).ThrowsAsync(new InvalidOperationException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.DeleteTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching ticket not found");
//        }

//        [Test]
//        public async Task DeleteTicketAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
//        {
//            _repoMock.Setup(r => r.DeleteAsync<Ticket>(id)).ThrowsAsync(new ArgumentException());
//            var service = new TicketDbService(_logger, _dbContext, _repoMock.Object);

//            Func<Task> result = async () => await service.DeleteTicketAsync(id);

//            await result.Should().ThrowExactlyAsync<ArgumentException>("Because searching ticket not found");
//        }

//        [Test]
//        public async Task DeleteTicketAsync__Delete_successful__Resources_length_should_be_less_by_1()
//        {
//            string id;
//            int expectedLength;
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    expectedLength = context.Tickets.Count() - 1;
//                    Ticket ticketToBeDeleted = context.Tickets.First();
//                    id = ticketToBeDeleted.Id;
//                    _repoMock.Setup(r => r.DeleteAsync<Ticket>(id)).Callback(() => context.Tickets.Remove(ticketToBeDeleted));
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);
//                    await service.DeleteTicketAsync(id);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.Count().Should().Be(expectedLength);
//                }
//            }
//        }

//        [Test]
//        public async Task DeleteTicketAsync__Delete_successful__Resources_should_not_contain_deleted_ticket()
//        {
//            string id;
//            using (var factory = new DbContextFactory())
//            {
//                using (var context = await factory.CreateContextAsync())
//                {
//                    Ticket ticketToBeDeleted = context.Tickets.First();
//                    id = ticketToBeDeleted.Id;
//                    _repoMock.Setup(r => r.DeleteAsync<Ticket>(id)).Callback(() => context.Tickets.Remove(ticketToBeDeleted));
//                    var service = new TicketDbService(_logger, context, _repoMock.Object);

//                    await service.DeleteTicketAsync(id);
//                }

//                using (var context = await factory.CreateContextAsync())
//                {
//                    context.Tickets.Where(x => x.Id.Equals(id)).Count().Should().Be(0);
//                }
//            }
//        }

//        #endregion

//    }
//}
