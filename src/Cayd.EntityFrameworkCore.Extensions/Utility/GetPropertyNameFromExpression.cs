using Cayd.EntityFrameworkCore.Extensions.Exceptions;
using System.Linq.Expressions;

namespace Cayd.EntityFrameworkCore.Extensions.Utility
{
    internal static partial class Utility
    {
        public static string GetPropertyNameFromExpression<TEntity>(Expression<Func<TEntity, object>> expression)
            where TEntity : class
        {
            if (expression.Body is MemberExpression memberExpression)
                return memberExpression.Member.Name;
            if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                return operand.Member.Name;

            throw new InvalidPropertyExpressionException(expression.ToString());
        }
    }
}
