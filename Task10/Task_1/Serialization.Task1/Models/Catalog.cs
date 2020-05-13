using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Serialization.Task1.Services.Extensions;

namespace Serialization.Task1.Models
{
    [XmlRoot("catalog")]
    public class Catalog : IXmlSerializable
    {
        public DateTime Date { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Date = DateTime.ParseExact(reader.GetAttribute("date"), Constants.DateTimeFormat,
                CultureInfo.InvariantCulture);
            reader.ReadStartElement();
            reader.Read();
            while (reader.Name == "book")
            {
                var book = new Book();
                book.ReadXml(reader);
                Books.Add(book);
                reader.Read();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(typeof(Catalog).GetPropertyXmlName(nameof(Date)),
                Date.ToString(Constants.DateTimeFormat));
            foreach (var book in Books)
            {
                book.WriteXml(writer);
            }
        }
    }
}