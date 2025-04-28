using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections;
using Cayd.EntityFrameworkCore.Extensions.Exceptions;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        public static void BulkUpdateByPrimaryKeys<TEntity>(this DbSet<TEntity> dbSet, IEnumerable primaryKeys, params (Expression<Func<TEntity, object>> property, object? value, bool isValueObject)[] propertiesAndValues)
            where TEntity : class
        {
            dbSet.GetDbContext().BulkUpdateByPrimaryKeys(primaryKeys, propertiesAndValues);
        }

        public static void BulkUpdateByPrimaryKeys<TEntity>(this DbContext dbContext, IEnumerable primaryKeys, params (Expression<Func<TEntity, object>> property, object? value, bool isValueObject)[] propertiesAndValues)
            where TEntity : class
        {
            if (primaryKeys == null)
                throw new NoPrimaryKeyIsPassedException();

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
