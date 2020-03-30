using System;

namespace IOCContainer.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public Type Type { get; set; }

        public override string Message => $"Type {Type} is not registered.";

        public TypeNotRegisteredException(Type type)
        {
            Type = type;
        }
    }
}