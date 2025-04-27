namespace Cayd.EntityFrameworkCore.Extensions.Exceptions
{
    internal class PrimaryKeyNotFoundException : Exception
    {
        internal PrimaryKeyNotFoundException() : base() { }
        internal PrimaryKeyNotFoundException(string entityTypeName)
            : base($"{entityTypeName} does not have any primary key.") { }
    }
}
