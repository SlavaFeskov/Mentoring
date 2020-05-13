using System;
using System.Globalization;
using System.Xml;

namespace Serialization.Task1.Services.Extensions
{
    public static class XmlReaderExtensions
    {
        public static DateTime ReadDateTime(this XmlReader reader, string format)
        {
            return DateTime.ParseExact(reader.ReadContentAsString(), format, CultureInfo.InvariantCulture);
        }

        public static TEnum ReadEnumValue<TEnum>(this XmlReader reader)
            where TEnum : struct
        {
            var line = reader.ReadContentAsString();
            return line.ToEnumByXmlEnumAttr<TEnum>();
        }


    }
}