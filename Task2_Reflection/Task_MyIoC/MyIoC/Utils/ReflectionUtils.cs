using System;
using System.Linq;
using System.Reflection;

namespace MyIoC.Utils
{
    public static class ReflectionUtils
    {
        public static bool HasCustomAttribute<T>(this MemberInfo memberInfo)
        {
            return memberInfo.CustomAttributes.Any(ca => ca.AttributeType.Name == typeof(T).Name);
        }
    }
}