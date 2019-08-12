using SDCWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SDCWebApp.Services
{
    public interface IDbRepository
    {
        /// <summary>
        /// Asynchronously retrives matching entity from database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entity to be retrived.</typeparam>
        /// <param name="id">Id of entity.</param>
        /// <returns>Matching <see cref="{TEntity}"/> entity.</returns>
        /// <exception cref="ArgumentException">Argument id is null or empty.</exception>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="InvalidOperationException">Element not found.</exception>
        Task<TEntity> GetByIdAsync<TEntity>(string id) where TEntity : class, IEntity;
        /// <summary>
        /// Asynchronously retrives all entities from database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entities to be retrived.</typeparam>
        /// <returns>An <see cref="IEnumerable{TEntity}"/> of matching entities</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IEntity;
        /// <summary>
        /// Retrives matching entities from database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entities to be retrived.</typeparam>
        /// <param name="ids">Entities ids.</param>
        /// <returns>An <see cref="IEnumerable{TEntity}"/> of matching entities</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentException">An argument <paramref name="ids"/> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The length of <paramref name="ids"/> is grater than overall count of entities in resource or resource is empty.</exception>
        IEnumerable<TEntity> GetByIds<TEntity>(IEnumerable<string> ids) where TEntity : class, IEntity;
        /// <summary>
        /// Adds given entity to database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entity to be added.</typeparam>
        /// <param name="entityToAdd">An entity to be added.</param>
        /// <returns>An entity which was added.</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentNullException">An arguemnt <paramref name="entityToAdd"/> is null.</exception>
        /// <exception cref="InvalidOperationException">An entity to be added already exists in resource</exception>
        TEntity Add<TEntity>(TEntity entityToAdd) where TEntity : class, IEntity;
        /// <summary>
        /// Asynchronously updates given entity with matching entity in database.
        /// </summary>
        /// <typeparam name="TEntity">A type of entity to be updated.</typeparam>
        /// <param name="entityToUpdate">An entity to be updated.</param>
        /// <returns>An entity which was updated.</returns>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentNullException">An argument <paramref name="entityToUpdate"/> is null.</exception>
        /// <exception cref="ArgumentException">A value of Id in given entity is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Resource is empty or matching element not found.</exception>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class, IEntity;
        /// <summary>
        /// Asynchronously deletes entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to be deleted.</typeparam>
        /// <param name="id">Id of entity to be deleted.</param>
        /// <exception cref="NullReferenceException">The resource which is querying is null.</exception>
        /// <exception cref="ArgumentException">An arguement <paramref name="id"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Matching element not found.</exception>
        Task DeleteAsync<TEntity>(string id) where TEntity : class, IEntity;
    }
}
