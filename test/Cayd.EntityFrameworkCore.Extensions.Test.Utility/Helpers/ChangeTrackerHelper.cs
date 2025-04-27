using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cayd.EntityFrameworkCore.Extensions.Test.Utility.Helpers
{
    public static class ChangeTrackerHelper
    {
        public static void TrackEntity<TEntity>(this DbSet<TEntity> dbSet, TEntity? entity)
            where TEntity : class
        {
            if (entity == null)
                return;

            var dbContext = dbSet.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
                return;

            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public static void TrackEntity<TEntity>(this DbContext dbContext, TEntity? entity)
            where TEntity : class
        {
            if (entity == null)
                return;

            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public static void UntrackEntity<TEntity>(this DbSet<TEntity> dbSet, TEntity? entity)
            where TEntity : class
        {
            if (entity == null)
                return;

            var dbContext = dbSet.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
                return;

            dbContext.Entry(entity).State = EntityState.Detached;
        }

        public static void UntrackEntity<TEntity>(this DbContext dbContext, TEntity? entity)
            where TEntity : class
        {
            if (entity == null)
                return;

            dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
