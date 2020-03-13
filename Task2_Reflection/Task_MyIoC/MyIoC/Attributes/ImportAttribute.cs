using System;

namespace MyIoC.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ImportAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ImportConstructorAttribute : Attribute
    {
    }
}