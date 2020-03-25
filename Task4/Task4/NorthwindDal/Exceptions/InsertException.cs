using System;

namespace NorthwindDal.Exceptions
{
    public class InsertException : Exception
    {
        public override string Message => "Record wasn't added.";

        public InsertException()
        {
        }
    }
}