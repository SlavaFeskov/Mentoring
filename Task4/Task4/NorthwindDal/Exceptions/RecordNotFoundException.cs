using System;

namespace NorthwindDal.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public override string Message => "Record not found.";

        public RecordNotFoundException()
        {
        }
    }
}