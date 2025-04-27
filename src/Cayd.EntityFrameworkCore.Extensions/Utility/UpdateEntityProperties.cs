using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Utility
{
    internal static partial class Utility
    {
        internal static void UpdateEntityProperties<TEntity>(this DbContext dbContext, TEntity entity, params (Expression<Func<TEntity, object>> property, object? value)[] propertiesAndValues)
            where TEntity : class
        {
            var entityEntry = dbContext.Entry(entity);
            foreach (var (expression, value) in propertiesAndValues)
            {
                var propertyName = GetPropertyNameFromExpression(expression);
                var member = entityEntry.Members
                    .Where(m => m.Metadata.Name == propertyName)
                    .FirstOrDefault();

                if (member == null)
                    throw new PropertyNotFoundException(typeof(TEntity).Name, propertyName);

                member.CurrentValue = value;
                member.IsModified = true;
            }
        }
    }
}
