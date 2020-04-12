using System;

namespace NorthwindDal.Infrastructure.Exceptions
{
    public class ConnectionException : Exception
    {
        public override string Message => "Unable to create new connection to DB.";

        public ConnectionException()
        {
        }
    }
}