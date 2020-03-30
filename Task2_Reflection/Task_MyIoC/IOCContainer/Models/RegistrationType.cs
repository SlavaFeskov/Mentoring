using System;
using System.Collections.Generic;

namespace IOCContainer.Models
{
    public class RegistrationType
    {
        public TypeKind TypeKind { get; set; }

        public IEnumerable<Type> DependentTypes { get; set; }

        public Type Type { get; set; }

        public Type BaseType { get; set; }

        public RegistrationType(Type type, Type baseType)
        {
            Type = type;
            BaseType = baseType;
        }
    }
}