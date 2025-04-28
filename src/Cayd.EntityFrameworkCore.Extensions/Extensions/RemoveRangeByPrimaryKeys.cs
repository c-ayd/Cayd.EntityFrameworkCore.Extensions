using Cayd.EntityFrameworkCore.Extensions.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Cayd.EntityFrameworkCore.Extensions
{
    public static partial class EFCoreExtensions
    {
        public static void RemoveRangeByPrimaryKeys<TEntity>(this DbSet<TEntity> dbSet, IEnumerable primaryKeys)
            where TEntity : class
        {
            dbSet.GetDbContext().RemoveRangeByPrimaryKeys<TEntity>(primaryKeys);
        }

        public static void RemoveRangeByPrimaryKeys<TEntity>(this DbContext dbContext, IEnumerable primaryKeys)
            where TEntity : class
        {
            foreach (var pk in primaryKeys)
            {
                dbContext.RemoveByPrimaryKey<TEntity>(pk);
            }
        }
    }
}
