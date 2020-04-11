using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindDal.Extensions
{
    public static class ReflectionExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object obj) => obj.GetType()
            .GetProperties().ToDictionary(p => p.Name, p => p.GetValue(obj));

            public static IEnumerable<string> GetPropertyNames(this Type type) =>
            type.GetProperties().Select(p => p.Name);
    }
}