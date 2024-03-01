namespace Pluralize.Core.Exceptions
{
    public class NullOrEmptyWordException : Exception
    {
        public NullOrEmptyWordException()
        {
        }

        public NullOrEmptyWordException(string? message) : base(message)
        {
        }

        public NullOrEmptyWordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
