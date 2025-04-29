using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        /// <summary>
        /// Removes a single entity via its primary key without fetching it from the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to be removed.</typeparam>
        /// <param name="primaryKey">Primary key of the entity to be removed.</param>
        /// <exception cref="DbContextNotFoundException">If the <see cref="DbContext"/> could not be retrieved via <see cref="DbSet{TEntity}"/>.</exception>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the primary key that is passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entity does not match with the number of the one that is passed.</exception>
        public static void RemoveByPrimaryKey<TEntity>(this DbSet<TEntity> dbSet, object primaryKey)
            where TEntity : class
        {
            dbSet.GetDbContext().RemoveByPrimaryKey<TEntity>(primaryKey);
        }

        /// <summary>
        /// Removes a single entity via its primary key without fetching it from the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to be removed.</typeparam>
        /// <param name="primaryKey">Primary key of the entity to be removed.</param>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the primary key that is passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entity does not match with the number of the one that is passed.</exception>
        public static void RemoveByPrimaryKey<TEntity>(this DbContext dbContext, object primaryKey)
            where TEntity : class
        {
            if (primaryKey == null)
                throw new NoPrimaryKeyIsPassedException();

            var entity = dbContext.GetEntityByPrimaryKey<TEntity>(primaryKey);
            dbContext.Remove(entity);
        }
    }
}
