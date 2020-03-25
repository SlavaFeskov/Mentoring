using System;

namespace NorthwindDal.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        private readonly string _message;

        public RecordNotFoundException(string message) : base(message)
        {
            _message = message;
        }

        public RecordNotFoundException()
        {
        }

        public override string Message => _message ?? "Record not found.";
    }
}