using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using SDCWebApp.Data;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using Microsoft.Extensions.Options;
using SDCWebApp.Auth;

namespace SDCWebApp.Services
{
    public class RefreshTokenManager : ServiceBase, IRefreshTokenManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RefreshTokenManager> _logger;
        private readonly IOptions<JwtSettings> _jwtOptions;


        public RefreshTokenManager(ApplicationDbContext dbContext, ILogger<RefreshTokenManager> logger, IOptions<JwtSettings> jwtOptions) : base(dbContext, logger)
        {
            _jwtOptions = jwtOptions;
            _logger = logger;
            _dbContext = dbContext;
        }


        /// <summary>
        /// Asynchronously adds <see cref="RefreshToken"/> token to the database. 
        /// Throws an exception if already there is the same token in database or any problem with saving changes occurred.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be added. Cannot be null.</param>
        /// <returns>The added entity.</returns>
        /// <exception cref="ArgumentNullException">The value of <paramref name="refreshToken"/> to be added is null.</exception>
        /// <exception cref="InvalidOperationException">There is the same token that one to be added in database.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="Article"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            _logger.LogInformation($"Starting method '{nameof(AddRefreshTokenAsync)}'.");

            if (refreshToken is null)
                throw new ArgumentNullException($"Argument '{nameof(refreshToken)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.RefreshTokens ?? throw new InternalDbServiceException($"Table of type '{typeof(RefreshToken).Name}' is null.");

            try
            {
                if (await IsEntityAlreadyExistsAsync(refreshToken))
                    throw new InvalidOperationException($"There is already the same token in the database as the one to be added.");

                _logger.LogDebug($"Starting add refresh token with id '{refreshToken.Id}'.");
                _dbContext.RefreshTokens.Add(refreshToken);
                await _dbContext.TrySaveChangesAsync();
                _logger.LogDebug("Add data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(AddRefreshTokenAsync)}'.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - Changes made by add operations cannot be saved properly. See the inner exception for more details. Operation failed.", ex);
                var internalException = new InternalDbServiceException("Changes made by add operations cannot be saved properly. See the inner exception for more details.", ex);
                throw internalException;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"{ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when adding a refresh token with id: '{refreshToken?.Id}' to the database. See the inner excpetion for more details.", ex);
                throw internalException;
            }

        }

        /// <summary>
        /// Asynchronously deletes <see cref="RefreshToken"/> token from the database. 
        /// Throws an exception if cannot found the token to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be deleted. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="refreshToken"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot found specified refresh token.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="RefreshToken"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        public async Task DeleteRefreshTokenAsync(RefreshToken refreshToken)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteRefreshTokenAsync)}'.");

            if (refreshToken is null)
                throw new ArgumentNullException($"Argument '{nameof(refreshToken)}' cannot be null.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.RefreshTokens ?? throw new InternalDbServiceException($"Table of type '{typeof(RefreshToken).Name}' is null.");

            try
            {
                if (_dbContext.RefreshTokens.Count() == 0)
                    throw new InvalidOperationException($"Cannot found refresh token with token value '{refreshToken.Token}'. Resource {_dbContext.RefreshTokens.GetType().Name} does not contain " +
                        $"any element.");

                if (!await IsEntityAlreadyExistsAsync(refreshToken))
                    throw new InvalidOperationException($"Cannot found refresh token with token value '{refreshToken.Token}'. Any element does not match to the one to be updated.");

                var tokenToBeDeleted = await _dbContext.RefreshTokens.SingleAsync(x => x.Token.Equals(refreshToken.Token));
                _logger.LogDebug($"Starting remove refresh token with id '{tokenToBeDeleted.Id}'.");
                _dbContext.RefreshTokens.Remove(tokenToBeDeleted);
                await _dbContext.TrySaveChangesAsync();
                _logger.LogDebug("Remove data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(DeleteRefreshTokenAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - Cannot found element. See the exception for more details. Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when removing refresh token with token values '{refreshToken.Token}' from the database. " +
                    $"See the inner exception for more details.", ex);
                throw internalException;
            }
        }

        /// <summary>
        /// Generates <see cref="RefreshToken"/> with crypthographically strong random sequence of values.
        /// </summary>
        /// <param name="size">A size of generated sequence.</param>
        /// <returns>New generated <see cref="RefreshToken"/>.</returns>
        public RefreshToken GenerateRefreshToken(int size = 32)
        {
            string token;
            byte[] randomNumber = new byte[size];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            var expiryIn = new DateTimeOffset(DateTime.UtcNow.AddSeconds((double)_jwtOptions.Value.RefreshTokenExpiryIn)).ToUnixTimeSeconds();
            return new RefreshToken { Id = Guid.NewGuid().ToString(), Token = token, ExpiryIn = (int)expiryIn };
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="RefreshToken"/> token saved in the database.
        /// Throws an exception if cannot found token or any problem with retrieving occurred.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be retrived. Cannot be null.</param>
        /// <returns>The refresh token.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="refreshToken"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot found token.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        public async Task<RefreshToken> GetSavedRefreshTokenAsync(string refreshToken)
        {
            _logger.LogInformation($"Starting method '{nameof(GetSavedRefreshTokenAsync)}'.");

            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException($"Argument '{nameof(refreshToken)}' cannot be null or empty.");

            await EnsureDatabaseCreatedAsync();
            _ = _dbContext?.RefreshTokens ?? throw new InternalDbServiceException($"Table of type '{typeof(RefreshToken).Name}' is null.");

            try
            {
                _logger.LogDebug($"Starting retrieve refresh token with token value: '{refreshToken}' from the database.");
                var token = await _dbContext.RefreshTokens.SingleAsync(x => x.Token.Equals(refreshToken));
                _logger.LogDebug("Retrieve data succeeded.");
                _logger.LogInformation($"Finished method '{nameof(GetSavedRefreshTokenAsync)}'.");
                return token;
            }
            catch (InvalidOperationException ex)
            {
                string message = _dbContext.RefreshTokens.Count() == 0 ? $"Token not found because resource {_dbContext.RefreshTokens.GetType().Name} does contain any elements. " +
                    $"See the inner exception for more details." : "Token not found. See the inner exception for more details.";
                _logger.LogError(ex, $"{ex.GetType().Name} - {message} Operation failed.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.GetType().Name} - {ex.Message}");
                var internalException = new InternalDbServiceException($"Encountered problem when retriving refresh token with token value '{refreshToken}' from the database. " +
                    $"See the inner exception for more details.", ex);
                throw internalException;
            }
        }


        #region Privates

        protected override Task<bool> IsEntityAlreadyExistsAsync(BasicEntity entity)
        {
            return _dbContext.RefreshTokens.AnyAsync(x => x.Token.Equals((entity as RefreshToken).Token));
        }

        #endregion

    }
}
