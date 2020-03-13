using System;

namespace ConvertProject
{
    public class StringConverterException : Exception
    {
        private readonly string _message;

        public int Index { get; }

        public string InitialString { get; }

        public override string Message => _message ??
                                          $"Unable to convert {InitialString} to Int32. Character '{InitialString[Index]}' at index [{Index}] is not a numeric character.";

        public StringConverterException(string initialString, int index)
        {
            Index = index;
            InitialString = initialString;
        }

        public StringConverterException(string initialString, int index, string message)
        {
            Index = index;
            _message = message;
            InitialString = initialString;
        }
    }
}