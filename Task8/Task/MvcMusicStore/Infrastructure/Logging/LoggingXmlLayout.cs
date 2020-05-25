using System.Xml;
using log4net.Core;
using log4net.Layout;

namespace MvcMusicStore.Infrastructure.Logging
{
    public class LoggingXmlLayout : XmlLayoutBase
    {
        protected override void FormatXml(XmlWriter writer, LoggingEvent loggingEvent)
        {
            writer.WriteStartElement("LogEntry");

            writer.WriteStartElement("Level");
            writer.WriteString(loggingEvent.Level.DisplayName);
            writer.WriteEndElement();

            writer.WriteStartElement("Message");
            writer.WriteString(loggingEvent.RenderedMessage);
            writer.WriteEndElement();

            writer.WriteStartElement("TimeStamp");
            writer.WriteString(loggingEvent.TimeStamp.ToString("dd/MM/yyyy HH:mm:ss"));
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}