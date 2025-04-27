namespace Cayd.EntityFrameworkCore.Extensions.Exceptions
{
    internal class PropertyNotFoundException : Exception
    {
        internal PropertyNotFoundException() : base() { }
        internal PropertyNotFoundException(string entityTypeName, string propertyName) 
            : base($"{propertyName} could not be found in {entityTypeName}.") { }
    }
}
