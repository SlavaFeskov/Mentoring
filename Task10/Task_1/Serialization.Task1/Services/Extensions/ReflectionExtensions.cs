using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Serialization.Task1.Services.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetPropertyXmlName(this Type type, string propName)
        {
            var prop = type.GetProperties().Single(p => p.Name == propName);
            return prop.GetCustomAttribute<XmlElementAttribute>()?.ElementName ?? prop.Name.ToLower();
        }
    }
}