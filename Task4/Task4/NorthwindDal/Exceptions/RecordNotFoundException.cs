using System;

namespace NorthwindDal.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        private readonly string _message;

        private string GetRecordNotFoundMessage() => _message ?? "Record not found.";

        public override string Message => GetRecordNotFoundMessage();

        public RecordNotFoundException()
        {
        }

        public RecordNotFoundException(string message) : base(message)
        {
            _message = message;
        }
    }
}