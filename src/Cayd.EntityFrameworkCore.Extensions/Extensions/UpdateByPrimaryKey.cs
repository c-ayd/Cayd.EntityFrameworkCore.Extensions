using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
#if NET8_0_OR_GREATER
        /// <summary>
        /// Updates a single entity via its primary key without fetching it from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// UpdateByPrimaryKey(id, [
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// ]);
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// UpdateByPrimaryKey(new { id1, id2 }, [
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// ]);
        /// </code>
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to be updated.</typeparam>
        /// <param name="primaryKey">Primary key of the entity to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="DbContextNotFoundException">If the <see cref="DbContext"/> could not be retrieved via <see cref="DbSet{TEntity}"/>.</exception>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the primary key that is passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entity does not match with the number of the one that is passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#else
        /// <summary>
        /// Updates a single entity via its primary key without fetching it from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// UpdateByPrimaryKey(id, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value)[] {
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// });
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// UpdateByPrimaryKey(new { id1, id2 }, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value)[] {
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// });
        /// </code>
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to be updated.</typeparam>
        /// <param name="primaryKey">Primary key of the entity to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="DbContextNotFoundException">If the <see cref="DbContext"/> could not be retrieved via <see cref="DbSet{TEntity}"/>.</exception>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the primary key that is passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entity does not match with the number of the one that is passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#endif
        public static void UpdateByPrimaryKey<TEntity>(this DbSet<TEntity> dbSet, object primaryKey, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            dbSet.GetDbContext().UpdateByPrimaryKey(primaryKey, propertiesAndValues);
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Updates a single entity via its primary key without fetching it from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// UpdateByPrimaryKey(id, [
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// ]);
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// UpdateByPrimaryKey(new { id1, id2 }, [
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// ]);
        /// </code>
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to be updated.</typeparam>
        /// <param name="primaryKey">Primary key of the entity to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the primary key that is passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entity does not match with the number of the one that is passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#else
        /// <summary>
        /// Updates a single entity via its primary key without fetching it from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// UpdateByPrimaryKey(id, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value)[] {
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// });
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// UpdateByPrimaryKey(new { id1, id2 }, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value)[] {
        ///     (x => x.Property1, newValue1),
        ///     (x => x.Property2, newValue2),
        ///     ...
        /// });
        /// </code>
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to be updated.</typeparam>
        /// <param name="primaryKey">Primary key of the entity to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the primary key that is passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entity does not match with the number of the one that is passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#endif
        public static void UpdateByPrimaryKey<TEntity>(this DbContext dbContext, object primaryKey, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            if (primaryKey == null)
                throw new NoPrimaryKeyIsPassedException();

            var entity = dbContext.GetEntityByPrimaryKey<TEntity>(primaryKey);
            dbContext.UpdateEntityProperties(entity, propertiesAndValues);
        }
    }
}
