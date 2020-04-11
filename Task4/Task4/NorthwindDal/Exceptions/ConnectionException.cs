using System;

namespace NorthwindDal.Exceptions
{
    public class ConnectionException : Exception
    {
        public override string Message => "Unable to create new connection to DB.";

        public ConnectionException()
        {
        }
    }
}