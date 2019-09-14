using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SDCWebApp.Auth;
using SDCWebApp.Models;
using SDCWebApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Services
{
    [TestFixture]
    public class RefreshTokenManagerTests
    {
        private ILogger<RefreshTokenManager> _logger;
        private RefreshToken _validRefreshToken;
        private IOptions<JwtSettings> _jwtOptions;


        [OneTimeSetUp]
        public void SetUp()
        {
            var options = new JwtSettings { Audience = "google.com", Issuer = "google.com", ExpiryIn = 3600, RefreshTokenExpiryIn = 7200, SecretKey = "secret" };
            _jwtOptions = Mock.Of<IOptions<JwtSettings>>(o => o.Value == options);
            _logger = Mock.Of<ILogger<RefreshTokenManager>>();
            _validRefreshToken = new RefreshToken { Id = "1", Token = "token_here" };
        }


        #region GenerateRefreshToken(IdentityUser user)
        // Generate succeeded -> return RefreshToken with data

        [Test]
        public async Task GenerateRefreshToken__Generate_succeeded__Should_return_refresh_token_with_data()
        {

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    var result = manager.GenerateRefreshToken();

                    result.Should().NotBeNull();
                    result.Token.Should().NotBeEmpty();
                    result.Id.Should().NotBeEmpty();
                }
            }
        }

        #endregion


        #region AddRefreshToken(RefreshToken refreshToken)
        // The 'RefreshTokens' table doesn't exist     -> throw InternalDbmanagerException
        // The 'RefreshTokens' table is null           -> throw InternalDbmanagerException
        // The 'RefreshTokens' resource is empty       -> return an empty IEnumerable<RefreshToken>
        // The parameter 'artilce' is null        -> throw ArgumentNullException
        // There is the same element in database  -> throw InvalidOperationException
        // Add secceeded                          -> return added element
        // Add succeeded                          -> the resource lenght is greater by 1
        // Add succeeded                          -> the resource contains added element

        [Test]
        public async Task AddRefreshToken__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens = null as DbSet<RefreshToken>;
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.AddRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task AddRefreshToken__Resource_does_not_exist__Should_throw_InternalDbmanagerException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop RefreshTokens table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [RefreshTokens]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.AddRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of RefreshToken. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task AddRefreshToken__Argument_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.AddRefreshTokenAsync(null);

                    await action.Should().ThrowExactlyAsync<ArgumentNullException>("Because argument 'refreshToken' is null.");
                }
            }
        }

        [Test]
        public async Task AddRefreshToken__In_resource_exists_the_same_RefreshToken_as_this_one_to_be_added__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.Add(_validRefreshToken);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.AddRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because in resource exists the same RefreshToken as this one to be added.");
                }
            }
        }

        [Test]
        public async Task AddRefreshToken__Add_successful__Resource_length_should_be_greater_by_1()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    int expectedLength = context.RefreshTokens.Count() + 1;
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    await manager.AddRefreshTokenAsync(_validRefreshToken);

                    context.RefreshTokens.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task AddRefreshToken__Add_successful__Resource_contains_added_RefreshToken()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    await manager.AddRefreshTokenAsync(_validRefreshToken);

                    context.RefreshTokens.Contains(_validRefreshToken).Should().BeTrue();
                }
            }
        }

        #endregion


        #region GetSavedRefreshToken(RefreshToken refreshToken)
        // The 'RefreshTokens' table doesn't exist  -> throw InternalDbServiceException
        // The 'RefreshTokens' table is null        -> throw InternalDbServiceException
        // The 'RefreshTokens' resource is empty    -> throw InvalidOperationException
        // The parameter 'id' is null or empty      -> throw ArgumentException
        // One element found                        -> return this element
        // None element found                       -> throw InvalidOperationException

        [Test]
        public async Task GetSavedRefreshToken__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens = null as DbSet<RefreshToken>;
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.GetSavedRefreshTokenAsync(_validRefreshToken.Token);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task GetSavedRefreshToken__Resource_does_not_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop RefreshTokens table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [RefreshTokens]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.GetSavedRefreshTokenAsync(_validRefreshToken.Token);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of RefreshToken. " +
                        "NOTE Excaption actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task GetSavedRefreshToken__Arguemnt_refresh_token_is_null_or_empty__Should_throw_ArgumentNullException([Values(null, "")] string refreshToken)
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.GetSavedRefreshTokenAsync(refreshToken);

                    await action.Should().ThrowExactlyAsync<ArgumentException>("Because refreshToken cannot be null or empty.");
                }
            }
        }

        [Test]
        public async Task GetSavedRefreshToken__Found_zero_matching_refresh_token__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.GetSavedRefreshTokenAsync(_validRefreshToken.Token);

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because RefreshToken not found.");
                }
            }
        }

        [Test]
        public async Task GetSavedRefreshToken__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.RemoveRange(await context.RefreshTokens.ToListAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.GetSavedRefreshTokenAsync(_validRefreshToken.Token);

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot get single instance of RefreshToken.");
                }
            }
        }

        [Test]
        public async Task GetSavedRefreshToken__RefreshToken_found__Should_return_this_RefreshToken()
        {
            RefreshToken expectedRefreshToken;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.Add(_validRefreshToken);
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    expectedRefreshToken = await context.RefreshTokens.FirstOrDefaultAsync();
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    var result = await manager.GetSavedRefreshTokenAsync(expectedRefreshToken.Token);

                    result.Should().BeEquivalentTo(expectedRefreshToken);
                }
            }
        }
        #endregion


        #region DeleteRefreshTokenAsync(RefreshToken refreshToken)
        // The 'RefreshToken' table doesn't exist       -> throw InternalDbServiceException
        // The 'RefreshToken' table is null             -> throw InternalDbServiceException
        // The 'RefreshToken' resource is empty         -> throw InvalidOperationException
        // The parameter 'id' is null or empty          -> throw ArgumentException
        // Element to be deleted not found              -> throw InvalidOperationException
        // Element to be updated has null or empty 'Id' -> throw ArgumentException
        // Delete succeeded                             -> resource doesn't contain deleted element
        // Delete succeeded                             -> resource lenght is less by 1

        [Test]
        public async Task DeleteRefreshTokenAsync__Resource_is_null__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens = null as DbSet<RefreshToken>;
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.DeleteRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource reference is set to null");
                }
            }
        }

        [Test]
        public async Task DeleteRefreshTokenAsync__Resource_does_not_exist__Should_throw_InternalDbServiceException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    // Drop RefreshTokens table.
                    context.Database.ExecuteSqlCommand("DROP TABLE [RefreshTokens]");
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.DeleteRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InternalDbServiceException>("Because resource doesnt exist and cannot get single instance of RefreshToken. " +
                       "NOTE Exception actually is type of 'SqLiteError' only if database provider is SQLite.");
                }
            }
        }

        [Test]
        public async Task DeleteRefreshTokenAsync__Argument_refresh_token_is_null__Should_throw_ArgumentNullException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.DeleteRefreshTokenAsync(null);

                    await action.Should().ThrowExactlyAsync<ArgumentNullException>();
                }
            }
        }

        [Test]
        public async Task DeleteRefreshTokenAsync__Resource_is_empty__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.RemoveRange(await context.RefreshTokens.ToArrayAsync());
                    await context.SaveChangesAsync();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.DeleteRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because resource is empty and cannot delete any element.");
                }
            }
        }

        [Test]
        public async Task DeleteRefreshTokenAsync__RefreshToken_to_be_deleted_not_found__Should_throw_InvalidOperationException()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.Add(new RefreshToken { Id = Guid.NewGuid().ToString(), Token = "sample_new_token" });
                    context.SaveChanges();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    Func<Task> action = async () => await manager.DeleteRefreshTokenAsync(_validRefreshToken);

                    await action.Should().ThrowExactlyAsync<InvalidOperationException>("Because RefreshToken to be deleted not found.");
                }
            }
        }

        [Test]
        public async Task DeleteRefreshTokenAsync__Delete_successful__Resources_length_should_be_less_by_1()
        {
            int expectedLength;

            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.Add(_validRefreshToken);
                    context.SaveChanges();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    expectedLength = context.RefreshTokens.Count() - 1;
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    await manager.DeleteRefreshTokenAsync(_validRefreshToken);

                    context.RefreshTokens.Count().Should().Be(expectedLength);
                }
            }
        }

        [Test]
        public async Task DeleteRefreshTokenAsync__Delete_successful__Resources_should_not_contain_deleted_refresh_token()
        {
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.RefreshTokens.Add(_validRefreshToken);
                    context.SaveChanges();
                }

                using (var context = await factory.CreateContextAsync())
                {
                    var manager = new RefreshTokenManager(context, _logger, _jwtOptions);

                    await manager.DeleteRefreshTokenAsync(_validRefreshToken);

                    context.RefreshTokens.Where(x => x.Token.Equals(_validRefreshToken.Token)).Count().Should().Be(0);
                }
            }
        }

        #endregion

    }
}
