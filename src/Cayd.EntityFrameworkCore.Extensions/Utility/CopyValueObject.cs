using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Utility
{
    internal static partial class Utility
    {
        internal static object? CopyValueObject<TEntity>(this (Expression<Func<TEntity, object>> property, object? value, bool isValueObject) expression)
            where TEntity : class
        {
            if (expression.value == null)
                return null;

            return CopyProperties(expression.value.GetType(), expression.value);
        }

        private static T CopyProperties<T>(Type type, T obj)
        {
            var instance = Activator.CreateInstance(type);

            foreach (var propertyInfo in type.GetProperties())
            {
                var value = propertyInfo.GetValue(obj);
                propertyInfo.SetValue(instance, value);
            }

            return (T)instance!;
        }
    }
}
