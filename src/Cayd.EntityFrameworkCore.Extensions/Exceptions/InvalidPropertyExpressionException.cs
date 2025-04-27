namespace Cayd.EntityFrameworkCore.Extensions.Exceptions
{
    internal class InvalidPropertyExpressionException : Exception
    {
        internal InvalidPropertyExpressionException() : base() { }
        internal InvalidPropertyExpressionException(string expression) 
            : base($"{expression} does not match the expected expression style: 'x => x.Property'.") { }
    }
}
