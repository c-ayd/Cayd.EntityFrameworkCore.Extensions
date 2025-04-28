using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cayd.EntityFrameworkCore.Extensions.Utility
{
    internal static partial class Utility
    {
        internal static DbContext GetDbContext<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            var dbContext = dbSet.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
                throw new DbContextNotFoundException(typeof(TEntity).Name);

            return dbContext;
        }
    }
}
