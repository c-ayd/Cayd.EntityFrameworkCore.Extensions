using System.Runtime.CompilerServices;

namespace Cayd.EntityFrameworkCore.Extensions.Utility
{
    internal static partial class Utility
    {
        internal static bool IsCompositeKey(this Type type)
        {
            return type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0 &&
                (type.FullName != null ? type.FullName.Contains("AnonymousType") : true);
        }
    }
}
