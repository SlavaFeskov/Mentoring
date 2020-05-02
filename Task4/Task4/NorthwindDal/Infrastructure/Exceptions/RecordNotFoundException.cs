using System;

namespace NorthwindDal.Infrastructure.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public override string Message => "Record not found.";

        public RecordNotFoundException()
        {
        }
    }
}