using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Utility
{
    internal static partial class Utility
    {
        internal static TEntity GetLoadedEntityByPrimaryKey<TEntity>(this DbContext dbContext, object primaryKey)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var pkEntity = dbContext.Model
                .FindEntityType(entityType)?
                .FindPrimaryKey()?
                .Properties.Select(p => new { p.Name, p.PropertyInfo?.PropertyType })
                .ToList();

            if (pkEntity == null || pkEntity.Count == 0)
                throw new PrimaryKeyNotFoundException(entityType.Name);

            var parameter = Expression.Parameter(entityType, "e");
            Expression<Func<TEntity, bool>> pkFinderExpression;

            var pkType = primaryKey.GetType();
            bool isCompositeKey = pkType.IsCompositeKey();
            if (isCompositeKey)
            {
                var pkParameter = pkType.GetProperties()
                    .Select(p => new { p.Name, Value = p.GetValue(primaryKey) })
                    .ToList();

                if (pkEntity.Count != pkParameter.Count)
                    throw new PrimaryKeyDoesNotMatchException(entityType.Name, pkEntity.Count, pkParameter.Count);

                Expression? checks = null;
                for (int i = 0; i < pkParameter.Count; ++i)
                {
                    var pkProperty = Expression.Property(parameter, pkEntity[i].Name);
                    var compare = Expression.Equal(pkProperty, Expression.Constant(pkParameter[i].Value));

                    if (checks == null)
                    {
                        checks = compare;
                    }
                    else
                    {
                        checks = Expression.And(checks, compare);
                    }
                }

                pkFinderExpression = Expression.Lambda<Func<TEntity, bool>>(checks!, parameter);
            }
            else
            {
                var pkProperty = Expression.Property(parameter, pkEntity[0].Name);
                var compare = Expression.Equal(pkProperty, Expression.Constant(primaryKey));
                pkFinderExpression = Expression.Lambda<Func<TEntity, bool>>(compare, parameter);
            }

            var pkFinderFunc = pkFinderExpression.Compile();
            var entity = dbContext.ChangeTracker.Entries<TEntity>()
                .Where(e => pkFinderFunc.Invoke(e.Entity))
                .FirstOrDefault()?.Entity;

            if (entity == null)
            {
                entity = (TEntity)Activator.CreateInstance(entityType)!;
                if (isCompositeKey)
                {
                    foreach (var pkInfo in pkType.GetProperties())
                    {
                        entityType.GetProperty(pkInfo.Name)!.SetValue(entity, pkInfo.GetValue(primaryKey));
                    }
                }
                else
                {
                    entityType.GetProperty(pkEntity[0].Name)!.SetValue(entity, primaryKey);
                }
            }

            return entity;
        }
    }
}
