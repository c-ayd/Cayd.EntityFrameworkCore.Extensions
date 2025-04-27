namespace Cayd.EntityFrameworkCore.Extensions.Exceptions
{
    internal class PrimaryKeyDoesNotMatchException : Exception
    {
        internal PrimaryKeyDoesNotMatchException() : base() { }
        internal PrimaryKeyDoesNotMatchException(string entityTypeName, int numberofPrimaryKeys, int numberofParameters) 
            : base($"{entityTypeName} has {numberofPrimaryKeys} primary key column(s), but {numberofParameters} parameters are provided.") { }
    }
}
