using System.Collections.Generic;
using System.Xml;

namespace Serialization.Task1.Services.Extensions
{
    public static class XmlWriterExtensions
    {
        public static void WriteElement(this XmlWriter writer, string elementName, string elementValue,
            Dictionary<string, string> attributes = null)
        {
            writer.WriteStartElement(elementName);
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    writer.WriteAttributeString(attribute.Key, attribute.Value);
                }
            }
            writer.WriteString(elementValue);

            writer.WriteEndElement();
        }
    }
}