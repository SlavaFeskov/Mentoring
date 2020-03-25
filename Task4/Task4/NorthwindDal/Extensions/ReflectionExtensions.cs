using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NorthwindDal.Extensions
{
    public static class ReflectionExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object obj) => obj.GetType()
            .GetProperties(BindingFlags.Public).ToDictionary(p => p.Name, p => p.GetValue(obj));

        public static IEnumerable<string> GetPropertyNames(this Type type) =>
            type.GetProperties(BindingFlags.Public).Select(p => p.Name);
    }
}