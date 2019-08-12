using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.Data;
using SDCWebApp.Models;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class DbRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbOptions = Mock.Of<DbContextOptions<ApplicationDbContext>>(o => o.ContextType == typeof(ApplicationDbContext));
        private readonly ILogger<DbRepository> _logger = Mock.Of<ILogger<DbRepository>>();


        #region GetByIdAsync(string id) tests      

        [Test]
        public async Task GetByIdAsync__Searching_element_with_given_id_exists_in_resource__Should_return_only_this_element()
        {
            string id = "1";
            var tickets = GetTickets();
            Ticket expectedElement = tickets.Single(x => x.Id.Equals(id));
            Ticket result = null;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    await context.Tickets.AddRangeAsync(tickets);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    result = await repo.GetByIdAsync<Ticket>(id);
                }
            }

            result.Should().BeEquivalentTo(expectedElement);
        }

        [Test]
        public async Task GetByIdAsync__Searching_element_not_found__Should_throw_InvalidOperationException()
        {
            string id = "1";

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    Func<Task> result = async () => await repo.GetByIdAsync<Ticket>(id);
                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because searching element not found and cannot return it.");
                }
            }

        }

        [Test]
        public async Task GetByIdAsync__The_resource_which_is_querying_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "1";
            var dbContext = Mock.Of<ApplicationDbContext>(c => c.Tickets == null as DbSet<Ticket>);
            var repo = new DbRepository(dbContext, _logger);

            Func<Task> result = async () => await repo.GetByIdAsync<Ticket>(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because the resource which is querying doesn't exist which means is null and cannot return any value.");
        }

        [Test]
        public async Task GetByIdAsync__Id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var tickets = GetTickets();
            var ticketsDbSet = CreateMock.CreateDbSetMock<Ticket>(tickets).Object;
            var dbContextMock = new Mock<ApplicationDbContext>(_dbOptions);
            dbContextMock.Setup(c => c.Tickets).Returns(ticketsDbSet);
            var repo = new DbRepository(dbContextMock.Object, _logger);

            Func<Task> result = async () => await repo.GetByIdAsync<Ticket>(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because parameter: id can not be null or empty string.");
        }

        #endregion


        #region GetAllAsync() tests

        [Test]
        public async Task GetAllAsync__Resource_which_is_querying_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbContext = Mock.Of<ApplicationDbContext>(c => c.Tickets == null as DbSet<Ticket>);
            var repo = new DbRepository(dbContext, _logger);

            Func<Task> result = async () => await repo.GetAllAsync<Ticket>();

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because the resource which is querying doesn't exist which means is null and cannot return any value.");
        }

        [Test]
        public async Task GetAllAsync__Resource_which_is_querying_is_empty__Should_return_empty_IEnumerable()
        {
            var result = new Ticket[] { }.AsEnumerable();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Tickets.RemoveRange(await context.Tickets.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    result = await repo.GetAllAsync<Ticket>();
                }
            }

            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetAllAsync__All_elements_found__Should_return_IEnumerable_with_length_equals_count_of_resource()
        {
            var tickets = GetTickets();
            int expectedLength = tickets.Count();
            var result = new Ticket[] { }.AsEnumerable();

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Tickets.RemoveRange(await context.Tickets.ToArrayAsync());
                    context.Tickets.AddRange(tickets);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    result = await repo.GetAllAsync<Ticket>();
                }
            }

            result.Count().Should().Be(expectedLength);
        }

        #endregion


        #region GetByIds(string[] ids) tests

        [Test]
        public void GetByIds__Resource_which_is_querying_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbContext = Mock.Of<ApplicationDbContext>(c => c.Tickets == null as DbSet<Ticket>);
            var repo = new DbRepository(dbContext, _logger);
            string[] ids = { "1", "2" };

            Action result = () => repo.GetByIds<Ticket>(ids);

            result.Should().ThrowExactly<NullReferenceException>("Because the resource which is querying doesn't exist which means is null and cannot returns any value.");
        }

        [Test]
        public async Task GetByIds__Resource_which_is_querying_is_empty__Should_throw_ArgumentOutOfRangeException()
        {
            string[] ids = { "1", "2" };
            var result = new object() as Action;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Tickets.RemoveRange(await context.Tickets.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    result = () => repo.GetByIds<Ticket>(ids);
                    result.Should().ThrowExactly<ArgumentOutOfRangeException>("Because if resource is empty then max count of ids is 0 but ids can not be empty string array so this exception should be thrown.");
                }
            }
        }

        [Test]
        public async Task GetByIds__All_of_ids_match_to_entities__Should_return_IEnumerable_for_all_ids()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var ids = context.Tickets.Select(x => x.Id);
                    int expectedLength = ids.Count();
                    var repo = new DbRepository(context, _logger);

                    var result = repo.GetByIds<Ticket>(ids.ToArray());

                    result.Count().Should().Be(expectedLength);
                }
            }

        }

        [Test]
        public async Task GetByIds__Amount_of_ids_is_grater_than_overall_count_of_entities__Should_throw_ArgumentOutOfRangeException()
        {

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var ids = context.Tickets.Select(x => x.Id);
                    var repo = new DbRepository(context, _logger);

                    Action result = () => repo.GetByIds<Ticket>(ids.ToList().Append("sdfsdfsfdsf"));

                    result.Should().ThrowExactly<ArgumentOutOfRangeException>("Because amount of ids is grater than overall count of entities so can not fetch data for all ids. " +
                        "Another approach may be return data for all entities in resource but due to security issues it isn't good idea.");
                }
            }

        }

        [Test]
        public async Task GetByIds__Any_of_ids_dont_match_to_entity__Should_return_data_for_all_other_ids()
        {
            var ids = new List<string>();
            int expectedLength;
            var result = new List<Ticket>().AsEnumerable();

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ids = await context.Tickets.Select(x => x.Id).ToListAsync();
                    ids[0] = "somethingDifferent";
                    expectedLength = ids.Count - 1; // One id is different so length of returned value should be less by one from length od all ids in resource
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    result = repo.GetByIds<Ticket>(ids as IEnumerable<string>);
                }
            }
            result.Count().Should().Be(expectedLength);
        }

        [Test]
        public async Task GetByIds__All_of_ids_dont_match_to_entity__Should_return_empty_IEnumerable()
        {
            var ids = new List<string> { "1", "2" };
            var result = new List<Ticket> { }.AsEnumerable();

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    result = repo.GetByIds<Ticket>(ids as IEnumerable<string>);
                }
            }
            result.Count().Should().Be(0);
        }

        [Test]
        public async Task GetByIds__Ids_are_empty_or_null__Should_throw_ArgumentException([Values(new string[] { }, null)] string[] ids)
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);
                    Action result = () => repo.GetByIds<Ticket>(ids);
                    result.Should().ThrowExactly<ArgumentException>("Because argument: ids can not be empty string array or null.");
                }
            }
        }


        #endregion


        #region Add(TEntity entityToAdd) tests

        [Test]
        public async Task Add__Exists_element_with_the_same_id_as_adding_element__Should_throw_InvalidOperationException()
        {
            Ticket ticket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(1) };
            Ticket newTicket = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(12) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Tickets.Add(ticket);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    Action result = () => repo.Add<Ticket>(newTicket);

                    result.Should().ThrowExactly<InvalidOperationException>("Because already exists element with the same id");
                }
            }
        }

        [Test]
        public void Add__Resource_which_is_querying_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbContext = Mock.Of<ApplicationDbContext>(c => c.Tickets == null as DbSet<Ticket>);
            var repo = new DbRepository(dbContext, _logger);
            var ticket = new Ticket { Id = "5", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(4) };

            Action result = () => repo.Add<Ticket>(ticket);

            result.Should().ThrowExactly<NullReferenceException>("Because the resource which is querying doesn't exist which means is null and cannot return any value.");
        }

        [Test]
        public async Task Add__Trying_add_null_element__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    Action result = () => repo.Add<Ticket>(null as Ticket);

                    result.Should().ThrowExactly<ArgumentNullException>("Because an element which is adding to data set can not be null");
                }
            }
        }

        [Test]
        public async Task Add__Adding_element_successful__Data_set_length_should_be_grater_by_1()
        {
            Ticket newTicket = new Ticket { Id = "5", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(12) };
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.Tickets.Count() + 1;
                    var repo = new DbRepository(context, _logger);

                    repo.Add<Ticket>(newTicket);
                    await context.SaveChangesAsync();

                    context.Tickets.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task Add__Adding_element_successful__Data_set_should_contains_element_which_was_added()
        {
            Ticket newTicket = new Ticket { Id = "5", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(12) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    repo.Add<Ticket>(newTicket);
                    await context.SaveChangesAsync();

                    context.Tickets.Contains(newTicket).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task Add__Adding_element_successful__Method_should_return_element_which_was_added()
        {
            Ticket newTicket = new Ticket { TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(12) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    var result = repo.Add<Ticket>(newTicket);

                    result.Should().BeEquivalentTo(newTicket);
                }
            }
        }

        #endregion


        #region UpdateAsync(TEntity entityToUpdate) tests

        [Test]
        public async Task UpdateAsync__Resource_which_is_querying_doesnt_exist__Should_throw_NullReferenceException()
        {
            var dbContext = Mock.Of<ApplicationDbContext>(c => c.Tickets == null as DbSet<Ticket>);
            var repo = new DbRepository(dbContext, _logger);
            var ticketForUpdate = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };

            Func<Task> result = async () => await repo.UpdateAsync<Ticket>(ticketForUpdate);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because the resource which is querying doesn't exist which means is null and cannot return any value.");
        }

        [Test]
        public async Task UpdateAsync__Element_with_matching_id_not_found__Should_throw_InvalidOperationException()
        {
            // Ticket with Id = "1" dont exist
            var ticketForUpdate = new Ticket { Id = "1", TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    Func<Task> result = async () => await repo.UpdateAsync<Ticket>(ticketForUpdate);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching element not found.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Resource_which_is_querying_is_empty__Should_throw_InvalidOperationException()
        {
            var ticketForUpdate = new Ticket { TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ticketForUpdate.Id = await context.Tickets.Select(x => x.Id).FirstOrDefaultAsync();

                    // Create empty resource
                    context.Tickets.RemoveRange(await context.Tickets.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    Func<Task> result = async () => await repo.UpdateAsync<Ticket>(ticketForUpdate);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource which is querying is empty and dont contain any element which could be updated.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__ID_in_element_to_be_added_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var ticketForUpdate = new Ticket { Id = id, TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var repo = new DbRepository(context, _logger);

                    Func<Task> result = async () => await repo.UpdateAsync<Ticket>(ticketForUpdate);

                    await result.Should().ThrowExactlyAsync<ArgumentException>("Because element to be updated dont contain Id. Can not update element without Id.");
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Attempt_to_update_element_with_null_value__Should_throw_ArgumentNullException()
        {
            var tickets = GetTickets();
            var ticketsDbSet = CreateMock.CreateDbSetMock<Ticket>(tickets).Object;
            var dbContextMock = new Mock<ApplicationDbContext>(_dbOptions);
            dbContextMock.Setup(c => c.Tickets).Returns(ticketsDbSet).Verifiable();
            var repo = new DbRepository(dbContextMock.Object, _logger);
            Ticket ticketForUpdate = null;

            Func<Task> result = async () => await repo.UpdateAsync<Ticket>(ticketForUpdate);

            await result.Should().ThrowExactlyAsync<ArgumentNullException>("Because parameter: entityToUpdate can not be null.");
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Should_return_element_which_was_updated()
        {
            var ticketForUpdate = new Ticket { TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ticketForUpdate.Id = await context.Tickets.Select(x => x.Id).FirstAsync();
                    var repo = new DbRepository(context, _logger);

                    var result = await repo.UpdateAsync<Ticket>(ticketForUpdate);

                    result.Should().BeSameAs(ticketForUpdate);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_first_time__Should_updated_UpdatedAt_property_from_null_to_the_time_of_update()
        {
            var ticketForUpdate = new Ticket { TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };
            DateTime? previousUpdatedAt = null;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ticketForUpdate.Id = await context.Tickets.Select(x => x.Id).FirstAsync();
                    previousUpdatedAt = await context.Tickets.Select(x => x.UpdatedAt).FirstAsync();
                    var repo = new DbRepository(context, _logger);

                    var result = await repo.UpdateAsync<Ticket>(ticketForUpdate);

                    result.UpdatedAt.Should().NotBeNull();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful_not_first_time__Should_updated_UpdatedAt_property_from_previous_date_to_the_time_of_current_update()
        {
            var ticketForUpdate = new Ticket { TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4), UpdatedAt = DateTime.UtcNow };
            DateTime? previousUpdatedAt;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ticketForUpdate.Id = await context.Tickets.Select(x => x.Id).FirstAsync();
                    context.Tickets.Update(ticketForUpdate);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    previousUpdatedAt = ticketForUpdate.UpdatedAt;
                    var repo = new DbRepository(context, _logger);
                    var result = await repo.UpdateAsync<Ticket>(ticketForUpdate);

                    result.UpdatedAt.Should().NotBeNull().And.BeAfter((DateTime)previousUpdatedAt);
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_should_contains_element_which_was_updated()
        {
            var ticketForUpdate = new Ticket { TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ticketForUpdate.Id = await context.Tickets.Select(x => x.Id).FirstAsync();
                    var repo = new DbRepository(context, _logger);

                    await repo.UpdateAsync<Ticket>(ticketForUpdate);
                    await context.SaveChangesAsync();

                    context.Tickets.Contains(ticketForUpdate).Should().BeTrue();
                }
            }
        }

        [Test]
        public async Task UpdateAsync__Update_successful__Resource_length_should_be_unchanged()
        {
            var ticketForUpdate = new Ticket { TicketUniqueId = Guid.NewGuid().ToString() + "0xDEADBEEF", ValidFor = DateTime.Now.AddDays(4) };
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    ticketForUpdate.Id = await context.Tickets.Select(x => x.Id).FirstAsync();
                    var repo = new DbRepository(context, _logger);
                    expectedLength = context.Tickets.Count();

                    await repo.UpdateAsync<Ticket>(ticketForUpdate);
                    await context.SaveChangesAsync();

                    context.Tickets.Count().Should().Be(expectedLength);
                }
            }
        }

        #endregion


        #region DeleteAsync(string id) tests

        [Test]
        public async Task DeleteAsync__Resource_which_is_querying_doesnt_exist__Should_throw_NullReferenceException()
        {
            string id = "1";
            var dbContext = Mock.Of<ApplicationDbContext>(c => c.Tickets == null as DbSet<Ticket>);
            var repo = new DbRepository(dbContext, _logger);

            Func<Task> result = async () => await repo.DeleteAsync<Ticket>(id);

            await result.Should().ThrowExactlyAsync<NullReferenceException>("Because the resource which is querying doesn't exist which means is null and cannot return any value.");
        }

        [Test]
        public async Task DeleteAsync__Matching_element_not_found__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    string id = "1";
                    var repo = new DbRepository(context, _logger);

                    Func<Task> result = async () => await repo.DeleteAsync<Ticket>(id);

                    await result.Should().ThrowExactlyAsync<InvalidOperationException>("Because matching element not found and can not delete element.");
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Argument_id_is_null_or_empty__Should_throw_ArgumentException([Values(null, "")] string id)
        {
            var tickets = GetTickets();
            var ticketsDbSet = CreateMock.CreateDbSetMock<Ticket>(tickets).Object;
            var dbContextMock = new Mock<ApplicationDbContext>(_dbOptions);
            dbContextMock.Setup(c => c.Tickets).Returns(ticketsDbSet);
            var repo = new DbRepository(dbContextMock.Object, _logger);

            Func<Task> result = async () => await repo.GetByIdAsync<Ticket>(id);

            await result.Should().ThrowExactlyAsync<ArgumentException>("Because arguemnt id can not be null or empty string.");
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resource_should_not_contains_deleted_element()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    string id = await context.Tickets.Select(x => x.Id).FirstOrDefaultAsync();
                    var element = context.Tickets.Where(x => x.Id.Equals(id));
                    var elementToBeDeleted = element.ToArray()[0];
                    var repo = new DbRepository(context, _logger);

                    await repo.DeleteAsync<Ticket>(id);
                    await context.SaveChangesAsync();
                    var result = context.Tickets.Contains(elementToBeDeleted);

                    result.Should().BeFalse();
                }
            }
        }

        [Test]
        public async Task DeleteAsync__Delete_successful__Resource_length_should_be_less_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    string id = await context.Tickets.Select(x => x.Id).FirstOrDefaultAsync();
                    var elementToBeDeleted = context.Tickets.Where(x => x.Id.Equals(id)).As<Ticket>();
                    int expectedLength = context.Tickets.Count() - 1;
                    var repo = new DbRepository(context, _logger);

                    await repo.DeleteAsync<Ticket>(id);
                    await context.SaveChangesAsync();
                    var result = context.Tickets.Count();

                    result.Should().Be(expectedLength);
                }
            }
        }
        #endregion


        #region Privates

        /// <summary>
        /// Creates <see cref="IEnumerable{Ticket}"/> tickets with Ids = { "1", "2", "3", "4" }
        /// </summary>
        private IEnumerable<Ticket> GetTickets()
        {
            return new List<Ticket>
            {
                new Ticket{ Id = "1", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(4) },
                new Ticket{ Id = "2", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(4) },
                new Ticket{ Id = "3", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(4) },
                new Ticket{ Id = "4", TicketUniqueId = Guid.NewGuid().ToString(), ValidFor = DateTime.Now.AddDays(4) }
            }.AsEnumerable();
        }

        #endregion
    }
}
