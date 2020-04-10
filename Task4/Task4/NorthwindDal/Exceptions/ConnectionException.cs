using System;

namespace NorthwindDal.Exceptions
{
    public class ConnectionException : Exception
    {
        private readonly string _message;

        private string GetUnableToConnectMessage() => _message ?? "Unable to create new connection to DB.";

        public override string Message => GetUnableToConnectMessage();

        public ConnectionException()
        {
        }

        public ConnectionException(string message) : base(message)
        {
            _message = message;
        }
    }
}