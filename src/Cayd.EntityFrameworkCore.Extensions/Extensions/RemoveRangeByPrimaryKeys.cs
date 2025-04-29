using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        /// <summary>
        /// Removes multiple entities via their primary keys without fetching them from the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to be removed.</typeparam>
        /// <param name="primaryKeys">Primary keys of the entities to be removed.</param>
        /// <exception cref="DbContextNotFoundException">If the <see cref="DbContext"/> could not be retrieved via <see cref="DbSet{TEntity}"/>.</exception>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the enumerable or one of the primary keys that are passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entities does not match with the number of the ones that are passed.</exception>
        public static void RemoveRangeByPrimaryKeys<TEntity>(this DbSet<TEntity> dbSet, IEnumerable primaryKeys)
            where TEntity : class
        {
            dbSet.GetDbContext().RemoveRangeByPrimaryKeys<TEntity>(primaryKeys);
        }

        /// <summary>
        /// Removes multiple entities via their primary keys without fetching them from the database.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to be removed.</typeparam>
        /// <param name="primaryKeys">Primary keys of the entities to be removed.</param>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the enumerable or one of the primary keys that are passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entities does not match with the number of the ones that are passed.</exception>
        public static void RemoveRangeByPrimaryKeys<TEntity>(this DbContext dbContext, IEnumerable primaryKeys)
            where TEntity : class
        {
            if (primaryKeys == null)
                throw new NoPrimaryKeyIsPassedException();

            var enumerable = primaryKeys.GetEnumerator();
            while (enumerable.MoveNext())
            {
                if (enumerable.Current == null)
                    throw new NoPrimaryKeyIsPassedException();
            }

            foreach (var pk in primaryKeys)
            {
                dbContext.RemoveByPrimaryKey<TEntity>(pk);
            }
        }
    }
}
