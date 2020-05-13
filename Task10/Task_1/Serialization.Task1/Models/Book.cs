using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Serialization.Task1.Services.Extensions;

namespace Serialization.Task1.Models
{
    public class Book : IXmlSerializable
    {
        public string Id { get; set; }

        public string Isbn { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public Genre Genre { get; set; }

        public string Publisher { get; set; }

        [XmlElement("publish_date")]
        public DateTime PublishDate { get; set; }

        public string Description { get; set; }

        [XmlElement("registration_date")]
        public DateTime RegistrationDate { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Id = reader.GetAttribute("id");
            reader.Read();

            reader.Read();
            if (reader.Name == "isbn")
            {
                Isbn = reader.ReadElementContentAsString();
                reader.Read();
            }

            reader.Read();
            Author = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadStartElement();
            Title = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadStartElement();
            Genre = reader.ReadEnumValue<Genre>();
            reader.ReadEndElement();
            reader.ReadStartElement();
            Publisher = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadStartElement();
            PublishDate = reader.ReadDateTime(Constants.DateTimeFormat);
            reader.ReadEndElement();
            reader.ReadStartElement();
            Description = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadStartElement();
            RegistrationDate = reader.ReadDateTime(Constants.DateTimeFormat);
            reader.ReadEndElement();

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(nameof(Book).ToLower());
            writer.WriteAttributeString(typeof(Book).GetPropertyXmlName(nameof(Id)), Id);
            if (Isbn != null)
            {
                writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(Isbn)), Isbn);
            }

            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(Author)), Author);
            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(Title)), Title);
            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(Genre)), Genre.GetEnumText());
            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(Publisher)), Publisher);
            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(PublishDate)),
                PublishDate.ToString(Constants.DateTimeFormat));
            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(Description)), Description);
            writer.WriteElement(typeof(Book).GetPropertyXmlName(nameof(RegistrationDate)),
                RegistrationDate.ToString(Constants.DateTimeFormat));
            writer.WriteEndElement();
        }
    }
}