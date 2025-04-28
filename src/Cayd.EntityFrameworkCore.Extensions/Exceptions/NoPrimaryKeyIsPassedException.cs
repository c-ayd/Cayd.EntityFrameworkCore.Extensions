namespace Cayd.EntityFrameworkCore.Extensions.Exceptions
{
    internal class NoPrimaryKeyIsPassedException : Exception
    {
        internal NoPrimaryKeyIsPassedException() : base("No primary key is passed.") { }
    }
}
