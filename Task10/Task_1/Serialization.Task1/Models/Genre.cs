using System.Xml.Serialization;

namespace Serialization.Task1.Models
{
    public enum Genre
    {
        [XmlEnum("Computer")] Computer,
        [XmlEnum("Fantasy")] Fantasy,
        [XmlEnum("Romance")] Romance,
        [XmlEnum("Horror")] Horror,
        [XmlEnum("Science Fiction")] ScienceFiction
    }
}