using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections;
using Cayd.EntityFrameworkCore.Extensions.Exceptions;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
#if NET8_0_OR_GREATER
        /// <summary>
        /// Updates multiple entities with the same values via their primary keys without fetching them from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// BulkUpdateByPrimaryKeys(id, [
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// ]);
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// BulkUpdateByPrimaryKeys(new { id1, id2 }, [
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// ]);
        /// </code>
        /// The third parameter in properties and values controls whether the new value needs to be copied or not. This is necessary for value objects.
        /// If the property is not a value object, set it to FALSE.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to be updated.</typeparam>
        /// <param name="primaryKeys">Primary keys of the entities to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="DbContextNotFoundException">If the <see cref="DbContext"/> could not be retrieved via <see cref="DbSet{TEntity}"/>.</exception>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the enumerable or one of the primary keys that are passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entities does not match with the number of the ones that are passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#else
        /// <summary>
        /// Updates multiple entities with the same values via their primary keys without fetching them from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// BulkUpdateByPrimaryKeys(id, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value, bool isValueObject)[] {
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// });
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// BulkUpdateByPrimaryKeys(new { id1, id2 }, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value, bool isValueObject)[] {
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// });
        /// </code>
        /// The third parameter in properties and values controls whether the new value needs to be copied or not. This is necessary for value objects.
        /// If the property is not a value object, set it to FALSE.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to be updated.</typeparam>
        /// <param name="primaryKeys">Primary keys of the entities to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="DbContextNotFoundException">If the <see cref="DbContext"/> could not be retrieved via <see cref="DbSet{TEntity}"/>.</exception>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the enumerable or one of the primary keys that are passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entities does not match with the number of the ones that are passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#endif
        public static void BulkUpdateByPrimaryKeys<TEntity>(this DbSet<TEntity> dbSet, IEnumerable primaryKeys, params (Expression<Func<TEntity, object>> property, object? value, bool isValueObject)[] propertiesAndValues)
            where TEntity : class
        {
            dbSet.GetDbContext().BulkUpdateByPrimaryKeys(primaryKeys, propertiesAndValues);
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Updates multiple entities with the same values via their primary keys without fetching them from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// BulkUpdateByPrimaryKeys(id, [
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// ]);
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// BulkUpdateByPrimaryKeys(new { id1, id2 }, [
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// ]);
        /// </code>
        /// The third parameter in properties and values controls whether the new value needs to be copied or not. This is necessary for value objects.
        /// If the property is not a value object, set it to FALSE.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to be updated.</typeparam>
        /// <param name="primaryKeys">Primary keys of the entities to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the enumerable or one of the primary keys that are passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entities does not match with the number of the ones that are passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#else
        /// <summary>
        /// Updates multiple entities with the same values via their primary keys without fetching them from the database.
        /// <para>Example usage:</para>
        /// <code>
        /// BulkUpdateByPrimaryKeys(id, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value, bool isValueObject)[] {
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// });
        /// </code>
        /// Or if the primary key is a composite key:
        /// <code>
        /// BulkUpdateByPrimaryKeys(new { id1, id2 }, new (Expression&lt;Func&lt;MyEntity, object&gt;&gt; property, object? value, bool isValueObject)[] {
        ///     (x => x.Property1, newValue1, false),
        ///     (x => x.Property2, newValue2, false),
        ///     (x => x.MyValueObject, newValueObject, true),
        ///     ...
        /// });
        /// </code>
        /// The third parameter in properties and values controls whether the new value needs to be copied or not. This is necessary for value objects.
        /// If the property is not a value object, set it to FALSE.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entities to be updated.</typeparam>
        /// <param name="primaryKeys">Primary keys of the entities to be updated.</param>
        /// <param name="propertiesAndValues">Properties to be updated and their new values.</param>
        /// <exception cref="NoPrimaryKeyIsPassedException">If the enumerable or one of the primary keys that are passed is null.</exception>
        /// <exception cref="PrimaryKeyNotFoundException">If the entity type does not have any primary keys.</exception>
        /// <exception cref="PrimaryKeyDoesNotMatchException">If the primary key is a composite key and the number of keys of the entities does not match with the number of the ones that are passed.</exception>
        /// <exception cref="PropertyNotFoundException">If the given property is not found in the entity type.</exception>
#endif
        public static void BulkUpdateByPrimaryKeys<TEntity>(this DbContext dbContext, IEnumerable primaryKeys, params (Expression<Func<TEntity, object>> property, object? value, bool isValueObject)[] propertiesAndValues)
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

            bool isThereValueObject = propertiesAndValues.Any(t => t.isValueObject);
            if (isThereValueObject)
            {
                foreach (var pk in primaryKeys)
                {
                    var propsAndValues = new (Expression<Func<TEntity, object>> property, object? value)[propertiesAndValues.Length];
                    for (int i = 0; i < propsAndValues.Length; ++i)
                    {
                        if (propertiesAndValues[i].isValueObject)
                        {
                            propsAndValues[i].property = propertiesAndValues[i].property;
                            propsAndValues[i].value = propertiesAndValues[i].CopyValueObject();
                        }
                        else
                        {
                            propsAndValues[i].property = propertiesAndValues[i].property;
                            propsAndValues[i].value = propertiesAndValues[i].value;
                        }
                    }

                    dbContext.UpdateByPrimaryKey(pk, propsAndValues);
                }
            }
            else
            {
                var propsAndValues = new (Expression<Func<TEntity, object>> property, object? value)[propertiesAndValues.Length];
                for (int i = 0; i < propsAndValues.Length; ++i)
                {
                    propsAndValues[i].property = propertiesAndValues[i].property;
                    propsAndValues[i].value = propertiesAndValues[i].value;
                }

                foreach (var pk in primaryKeys)
                {
                    dbContext.UpdateByPrimaryKey(pk, propsAndValues);
                }
            }
        }
    }
}
