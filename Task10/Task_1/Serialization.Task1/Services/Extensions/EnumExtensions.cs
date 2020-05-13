using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Serialization.Task1.Services.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnumByXmlEnumAttr<T>(this string value) where T : struct
        {
            foreach (T parsedItem in Enum.GetValues(typeof(T)))
            {
                var enumValue = (parsedItem as Enum);
                var attr = enumValue?.GetType()
                    .GetMember(enumValue.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<XmlEnumAttribute>()?.Name;
                if (value == attr)
                {
                    return parsedItem;
                }
            }

            return default;
        }

        public static string GetEnumText<T>(this T enumValue) =>
            typeof(T).GetMember(enumValue.ToString()).FirstOrDefault()?
                .GetCustomAttribute<XmlEnumAttribute>()?.Name ??
            enumValue.ToString();
    }
}