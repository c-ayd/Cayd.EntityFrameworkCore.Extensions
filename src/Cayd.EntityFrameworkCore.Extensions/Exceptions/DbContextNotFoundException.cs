namespace Cayd.EntityFrameworkCore.Extensions.Exceptions
{
    internal class DbContextNotFoundException : Exception
    {
        internal DbContextNotFoundException(string entityName) :
            base($"The DbContext in the DbSet could not be found for {entityName}.") { }
    }
}
