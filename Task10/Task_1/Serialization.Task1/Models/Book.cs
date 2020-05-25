using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Serialization.Task1.Models
{
    [XmlType("book")]
    public class Book
    {
        [XmlIgnore]
        public DateTime PublishDate { get; set; }

        [XmlIgnore]
        public DateTime RegistrationDate { get; set; }

        [XmlAttribute("id")] public string Id { get; set; }

        [XmlElement("isbn")] public string Isbn { get; set; }

        [XmlElement("author")] public string Author { get; set; }

        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("genre")] public Genre Genre { get; set; }

        [XmlElement("publisher")] public string Publisher { get; set; }

        [XmlElement("publish_date")]
        public string PublishDateString
        {
            get => PublishDate.ToString(Constants.DateTimeFormat);
            set => PublishDate = DateTime.ParseExact(value, Constants.DateTimeFormat, CultureInfo.InvariantCulture);
        }

        [XmlElement("description")] public string Description { get; set; }

        [XmlElement("registration_date")]
        public string RegistrationDateString
        {
            get => RegistrationDate.ToString(Constants.DateTimeFormat);
            set => RegistrationDate =
                DateTime.ParseExact(value, Constants.DateTimeFormat, CultureInfo.InvariantCulture);
        }
    }
}