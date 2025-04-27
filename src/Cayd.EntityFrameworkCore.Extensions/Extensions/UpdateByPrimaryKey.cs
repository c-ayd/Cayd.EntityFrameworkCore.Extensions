using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        public static void UpdateByPrimaryKey<TEntity>(this DbSet<TEntity> dbSet, object primaryKey, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            var dbContext = dbSet.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
                throw new DbContextNotFoundException(typeof(TEntity).Name);

            dbContext.UpdateByPrimaryKey(primaryKey, propertiesAndValues);
        }

        public static void UpdateByPrimaryKey<TEntity>(this DbContext dbContext, object primaryKey, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            var entity = dbContext.GetLoadedEntityByPrimaryKey<TEntity>(primaryKey);
            dbContext.UpdateEntityProperties(entity, propertiesAndValues);
        }
    }
}
