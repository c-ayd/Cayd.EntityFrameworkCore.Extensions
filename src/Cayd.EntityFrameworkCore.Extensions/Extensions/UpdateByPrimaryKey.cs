using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        public static void UpdateByPrimaryKey<TEntity>(this DbSet<TEntity> dbSet, object primaryKey, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            dbSet.GetDbContext().UpdateByPrimaryKey(primaryKey, propertiesAndValues);
        }

        public static void UpdateByPrimaryKey<TEntity>(this DbContext dbContext, object primaryKey, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            var entity = dbContext.GetEntityByPrimaryKey<TEntity>(primaryKey);
            dbContext.UpdateEntityProperties(entity, propertiesAndValues);
        }
    }
}
