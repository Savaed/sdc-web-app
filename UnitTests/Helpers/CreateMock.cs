using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Helpers
{
    /// <summary>
    /// This class provides methods for creating mocks for many types of objects.
    /// </summary>
    public static class CreateMock
    {
        /// <summary>
        /// Creates mock of <see cref="DbSet{TEntity}"/> initialized with objects specified in parameter <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="TEntity">DbSet entity type.</typeparam>
        /// <param name="elements">Elements which should be using to create mock.</param>
        /// <returns>Mock of type <see cref="Mock{DbSet{TEntity}}"/>initialized with <paramref name="elements"/>.</returns>
        public static Mock<DbSet<TEntity>> CreateDbSetMock<TEntity>(IEnumerable<TEntity> elements) where TEntity : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<TEntity>>();

            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<TEntity>>().Setup(d => d.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<TEntity>>().Setup(d => d.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<TEntity>>().Setup(d => d.GetEnumerator()).Returns(() => elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
