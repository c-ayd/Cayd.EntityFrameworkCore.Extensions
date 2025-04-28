using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        public static void RemoveByPrimaryKey<TEntity>(this DbSet<TEntity> dbSet, object primaryKey)
            where TEntity : class
        {
            dbSet.GetDbContext().RemoveByPrimaryKey<TEntity>(primaryKey);
        }

        public static void RemoveByPrimaryKey<TEntity>(this DbContext dbContext, object primaryKey)
            where TEntity : class
        {
            var entity = dbContext.GetEntityByPrimaryKey<TEntity>(primaryKey);
            dbContext.Remove(entity);
        }
    }
}
