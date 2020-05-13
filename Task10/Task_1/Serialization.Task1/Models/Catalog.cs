using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace Serialization.Task1.Models
{
    [XmlRoot("catalog")]
    public class Catalog : List<Book>
    {
        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("date")]
        public string DateString
        {
            get => Date.ToString(Constants.DateTimeFormat);
            set => Date = DateTime.ParseExact(value, Constants.DateTimeFormat, CultureInfo.InvariantCulture);
        }
    }
}