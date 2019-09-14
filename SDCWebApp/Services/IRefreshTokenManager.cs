using SDCWebApp.Models;
using System;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface IRefreshTokenManager
    {
        /// <summary>
        /// Generates <see cref="RefreshToken"/> with crypthographically strong random sequence of values.
        /// </summary>
        /// <param name="size">A size of generated sequence.</param>
        /// <returns>New generated <see cref="RefreshToken"/>.</returns>
        RefreshToken GenerateRefreshToken(int size = 32);

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
        Task AddRefreshTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Asynchronously retrieves <see cref="RefreshToken"/> token saved in the database.
        /// Throws an exception if cannot found token or any problem with retrieving occurred.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be retrived. Cannot be null.</param>
        /// <returns>The <see cref="RefreshToken"/> refresh token.</returns>
        /// <exception cref="ArgumentException">Argument <paramref name="refreshToken"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Cannot found token.</exception>
        /// <exception cref="InternalDbServiceException">The resource does not exist or has a null value or any
        /// other problems with retrieving data from database occurred.</exception>
        Task<RefreshToken> GetSavedRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Asynchronously deletes <see cref="RefreshToken"/> token from the database. 
        /// Throws an exception if cannot found the token to be deleted or any problem with saving changes occurred.
        /// </summary>
        /// <param name="refreshToken">The refresh token to be deleted. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="refreshToken"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Cannot found specified refresh token.</exception>
        /// <exception cref="InternalDbServiceException">The table with <see cref="RefreshToken"/> entities does not exist or it is null or 
        /// cannot save properly any changes made by add operation.</exception>
        Task DeleteRefreshTokenAsync(RefreshToken refreshToken);
    }
}