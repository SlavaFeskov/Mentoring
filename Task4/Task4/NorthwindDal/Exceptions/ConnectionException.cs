using System;

namespace NorthwindDal.Exceptions
{
    public class ConnectionException : Exception
    {
        private readonly string _message;

        public override string Message => _message ?? "Unable to create new connection to DB.";

        public ConnectionException()
        {
        }

        public ConnectionException(string message) : base(message)
        {
            _message = message;
        }
    }
}